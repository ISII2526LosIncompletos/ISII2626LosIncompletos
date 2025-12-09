using AppForSEII2526.API.DTOs.OfertaDTOs;
using AppForSEII2526.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

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

            if (ofertaDTO.FechaInicio < DateTime.Today)
            {
                return BadRequest("La fecha de inicio no puede ser anterior a hoy.");
            }

            if (ofertaDTO.FechaFinal <= ofertaDTO.FechaInicio)
            {
                return BadRequest("La fecha final debe ser posterior a la fecha de inicio.");
            }

            if (ofertaDTO.FechaFinal <= ofertaDTO.FechaInicio.AddDays(7))
            {
                return BadRequest("!Error¡ La oferta debe durar al menos una semana");
            }

            if (string.IsNullOrWhiteSpace(ofertaDTO.MetodoPago) ||
                !Enum.TryParse<tiposMetodosPago>(ofertaDTO.MetodoPago, ignoreCase: true, out var metodoPagoParsed))
            {
                return BadRequest("Método de pago inválido.");
            }

            TiposDirigidaOferta? dirigidaAParsed = null;
            if (!string.IsNullOrWhiteSpace(ofertaDTO.DirigidaA))
            {
                if (Enum.TryParse<TiposDirigidaOferta>(ofertaDTO.DirigidaA, ignoreCase: true, out var parsed))
                {
                    dirigidaAParsed = parsed;
                }
                else
                {
                    return BadRequest("El campo 'Dirigida A' contiene un valor inválido.");
                }
            }

            if (ofertaDTO.Items == null || !ofertaDTO.Items.Any())
            {
                return BadRequest("La oferta debe incluir al menos una herramienta.");
            }

            var newOferta = new Oferta
            {
                FechaInicio = ofertaDTO.FechaInicio,
                FechaFinal = ofertaDTO.FechaFinal,
                MetodoPago = metodoPagoParsed,
                DirigidaA = dirigidaAParsed,
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

                var precioFinal = herramienta.Precio * (1 - (itemDTO.Porcentaje / 100m));

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

            var ofertaToReturn = new OfertaDetailDTO
            {
                Id = newOferta.Id,
                FechaInicio = newOferta.FechaInicio,
                FechaFinal = newOferta.FechaFinal,
                FechaOferta = newOferta.FechaOferta,
                MetodoPago = newOferta.MetodoPago.ToString(),
                DirigidaA = newOferta.DirigidaA?.ToString(),
                Items = newOferta.OfertaItems.Select(oi => new OfertaItemDTO
                {
                    HerramientaId = oi.HerramientaId,
                    Porcentaje = oi.Porcentaje,
                    HerramientaNombre = _context.Herramientas.Find(oi.HerramientaId)?.Nombre ?? "Unknown",
                    HerramientaMaterial = _context.Herramientas.Find(oi.HerramientaId)?.Material ?? "Unknown",
                    FabricanteNombre = _context.Fabricantes.Find(_context.Herramientas.Find(oi.HerramientaId)?.FabricanteId)?.Nombre ?? "Unknown",
                    PrecioOriginal = _context.Herramientas.Find(oi.HerramientaId)?.Precio ?? 0,
                    PrecioFinal = oi.PrecioFinal
                }).ToList()
            };

            return CreatedAtAction(nameof(GetDetalleOferta), new { id = newOferta.Id }, ofertaToReturn);
        }
    }
}