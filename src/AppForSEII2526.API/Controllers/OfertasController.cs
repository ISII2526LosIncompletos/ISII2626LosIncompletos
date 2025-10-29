using AppForSEII2526.API.DTOs.OfertaDTOs;

namespace AppForSEII2526.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OfertasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<OfertasController> _logger;

        public OfertasController(ApplicationDbContext context, ILogger<OfertasController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(OfertaDetailDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> GetRental(int id)
        {
            if (_context.Ofertas == null)
            {
                _logger.LogError("Error: Rentals table does not exist");
                return NotFound();
            }

            var rental = await _context.Ofertas
             .Where(o => o.Id == id)
                 .Include(o => o.OfertaItems)
                    .ThenInclude(ri => ri.Herramienta)
                        .ThenInclude(Herramienta => Herramienta.Id)
             .Select(o => new OfertaDetailDTO
             {
                 Id = o.Id,
                 FechaInicio = o.FechaInicio,
                 FechaFinal = o.FechaFinal,
                 FechaOferta = o.FechaOferta,
                 MetodoPago = o.MetodoPago.ToString(),
                 DirigidaA = o.DirigidaA.ToString(),

                 Items = o.OfertaItems.Select(oi => new OfertaDetailDTO(
                     oi.Herramienta.Nombre,
                     oi.Herramienta.Material,
                     oi.Herramienta.Fabricante,
                     (double)oi.Herramienta.Precio,
                     (double)oi.PrecioFinal,
                     oi.Porcentaje
                 )).ToList()
             })
             .FirstOrDefaultAsync();


            if (rental == null)
            {
                _logger.LogError($"Error: Rental with id {id} does not exist");
                return NotFound();
            }


            return Ok(rental);
        }

        [HttpPost]
        [ProducesResponseType(typeof(OfertaDetailDTO), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> CreateOferta([FromBody] OfertaForCreationDTO ofertaDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (ofertaDTO.FechaInicio < DateTime.Today)
            {
                return BadRequest("La fecha de inicio no puede ser anterior a hoy."); 
            }

            if (ofertaDTO.FechaFinal <= ofertaDTO.FechaInicio)
            {
                return BadRequest("La fecha final debe ser posterior a la fecha de inicio."); 
            }

            if (string.IsNullOrWhiteSpace(ofertaDTO.MetodoPago)||  !Enum.TryParse<tiposMetodosPago>(ofertaDTO.MetodoPago, ignoreCase: true, out var metodoPagoParsed))
            {
                return BadRequest("Método de pago inválido.");
            }

            TiposDirigidaOferta? dirigidaA = null;
            if (!string.IsNullOrWhiteSpace(ofertaDTO.DirigidaA) && Enum.TryParse<TiposDirigidaOferta>(ofertaDTO.DirigidaA, ignoreCase: true, out var dirigidaAParsed))
            {
                dirigidaA = dirigidaAParsed;
            }

            var newOferta = new Oferta
            {
                FechaInicio = ofertaDTO.FechaInicio,
                FechaFinal = ofertaDTO.FechaFinal,
                MetodoPago = metodoPagoParsed,
                DirigidaA = ofertaDTO.DirigidaA,
                Items = new List<OfertaItem>()
            };

            foreach (var itemDTO in ofertaDTO.Items)
            {
                var herramienta = await _context.Herramientas.FindAsync(itemDTO.HerramientaId);
                if (herramienta == null)
                {
                    return BadRequest($"No se encontró la herramienta con ID {itemDTO.HerramientaId}.");
                }

                var precioFinal = herramienta.Precio * (1 - ((decimal)itemDTO.Porcentaje / 100));

                var newOfertaItem = new OfertaItem
                {
                    HerramientaId = itemDTO.HerramientaId,
                    Porcentaje = (int)itemDTO.Porcentaje,
                    Oferta = newOferta
                };
                newOferta.OfertaItems.Add(newOfertaItem); // Cambia 'Items' por 'OfertaItems'
            }

            _context.Ofertas.Add(newOferta);
            await _context.SaveChangesAsync();

            _logger.LogInformation($"Nueva oferta creada con ID: {newOferta.Id}");

            var ofertaCreada = await _context.Ofertas
                .Include(o => o.OfertaItems)
                     .ThenInclude(oi => oi.Herramienta)
                          .ThenInclude(h => h.Fabricante)
                .FirstOrDefaultAsync(o => o.Id == newOferta.Id);

            var ofertaToReturn = new OfertaDetailDTO
            {
                Id = ofertaCreada.Id,
                FechaInicio = ofertaCreada.FechaInicio,
                FechaFinal = ofertaCreada.FechaFinal,
                FechaOferta = ofertaCreada.FechaOferta,
                MetodoPago = ofertaCreada.MetodoPago.ToString(),
                DirigidaA = ofertaCreada.DirigidaA?.ToString(),
                Items = ofertaCreada.OfertaItems.Select(oi => new OfertaDetailDTO(
                    oi.Herramienta.Nombre,
                    oi.Herramienta.Material,
                    oi.Herramienta.Fabricante,
                    (double)oi.Herramienta.Precio,
                    (double)oi.PrecioFinal,
                    oi.Porcentaje
                )).ToList()
            };

            return CreatedAtRoute("GetOfertaById",
                                new { id = newOferta.Id },ofertaToReturn);
        }
    }
}