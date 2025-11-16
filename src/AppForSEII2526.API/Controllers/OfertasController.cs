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
        public async Task<ActionResult> GetDetalleOferta(int id)
        {
            if (_context.Ofertas == null)
            {
                _logger.LogError("Error: La tabla Ofertas no existe");
                return NotFound();
            }

            var oferta = await _context.Ofertas
                 .Where(o => o.Id == id)
                 .Include(o => o.OfertaItems)
                     .ThenInclude(ri => ri.Herramienta)
                         .ThenInclude(h => h.Fabricante)
                 .Select(o => new OfertaDetailDTO
                 {
                     Id = o.Id,
                     FechaInicio = o.FechaInicio,
                     FechaFinal = o.FechaFinal,
                     FechaOferta = o.FechaOferta,
                     MetodoPago = o.MetodoPago.ToString(),
                     DirigidaA = o.DirigidaA.ToString(),

                     Items = o.OfertaItems.Select(oi => new OfertaItemDTO(
                         oi.HerramientaId,
                         oi.Porcentaje,
                         oi.Herramienta.Nombre,
                         oi.Herramienta.Material,
                         oi.Herramienta.Fabricante.Nombre,
                         oi.Herramienta.Precio,
                         oi.PrecioFinal
                     )).ToList()
                 })
                 .FirstOrDefaultAsync();


            if (oferta == null)
            {
                _logger.LogError($"Error: Oferta con id {id} no existe");
                return NotFound();
            }

            return Ok(oferta);
        }

        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType(typeof(OfertaDetailDTO), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> CreateOferta([FromBody] OfertaForCreationDTO ofertaDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

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

            tiposMetodosPago metodoPagoParsed;
            if (string.IsNullOrWhiteSpace(ofertaDTO.MetodoPago) || !Enum.TryParse<tiposMetodosPago>(ofertaDTO.MetodoPago, ignoreCase: true, out metodoPagoParsed))
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
                DirigidaA = dirigidaA,
                FechaOferta = DateTime.UtcNow,
                OfertaItems = new List<OfertaItem>()
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
                    PrecioFinal = precioFinal,
                    Oferta = newOferta
                };
                newOferta.OfertaItems.Add(newOfertaItem);
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


                Items = ofertaCreada.OfertaItems.Select(oi => new OfertaItemDTO
                {
                    HerramientaId = oi.Herramienta.Id,
                    Porcentaje = oi.Porcentaje,
                    HerramientaNombre = oi.Herramienta.Nombre,
                    HerramientaMaterial = oi.Herramienta.Material,
                    FabricanteNombre = oi.Herramienta.Fabricante.Nombre,
                    PrecioOriginal = oi.Herramienta.Precio,
                    PrecioFinal = oi.PrecioFinal
                }).ToList()

            };

            return CreatedAtAction(nameof(GetDetalleOferta),
                                new { id = newOferta.Id }, ofertaToReturn);
        }
    }
}
