using AppForSEII2526.API.DTOs.HerramientasDTOs.ISII2626LosIncompletos.API.DTOs.OfertaDTOs;
using ISII2626LosIncompletos.API.DTOs.OfertaDTOs;
using AppForSEII2526.API.DTOs.HerramientaDTOs;
using System.Data;

namespace AppForSEII2526.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HerramientasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        private readonly ILogger<HerramientasController> _logger;

        public HerramientasController(ApplicationDbContext context, ILogger<HerramientasController> logger)
        {
            _context = context;
            _logger = logger;
        }
                
        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(HerramientasDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> GetOfertas(DateTime? from, DateTime? to, string? nombre, string? material, string? fabricante, double? precio)
        {
            if (from != null && to != null && from > to)
            {
                ModelState.AddModelError("from&to", "La fecha 'from' (desde) no puede ser posterior a la fecha 'to' (hasta)");
                _logger.LogError($"{DateTime.Now} Error: La fecha 'from' ({from}) es posterior a 'to' ({to})");
                return BadRequest(new ValidationProblemDetails(ModelState));
            }

            IList<HerramientasDTO> ofertas = await _context.Ofertas
                .Include(o => o.OfertaItems)
                    .ThenInclude(oi => oi.Herramienta)
                        .ThenInclude(h => h.Fabricante)
                .Where(o => (fabricante == null || o.Fabricante.Contains(fabricante)) &&
                      (precio == null || o.Precio.Equals(precio)))
                .OrderByDescending(o => o.FechaOferta)
                .Select(o => new HerramientasDTO(o.Id, o.FechaInicio, o.FechaFinal, o.FechaOferta, o.MetodoPago, o.DirigidaA))
                .ToListAsync();
            return Ok(ofertas);
        }

        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(HerramientasDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> GetReparacion(string? nombre, string? material, string? fabricante,
          decimal? precio, int? tiempoReparacion)
        {
          IList<HerramientasDTO> selectHerramientas = await _context.Herramientas
            .Include(h => h.Fabricante)
            .Include(h => h.ItemsReparacion)
                .ThenInclude(ri => ri.Reparacion)
            .Where(h => (nombre == null || h.Nombre.Contains(nombre))
                && (tiempoReparacion == null || h.TiempoReparacion.Equals(tiempoReparacion)))
            .OrderBy(h => h.Nombre)
            .Select(h => new HerramientasDTO(h.Id, h.Nombre, h.Material,
                h.Fabricante, h.Precio, h.TiempoReparacion))
            .ToListAsync();
          return Ok(selectHerramientas);
        }


        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(HerramientasDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> GetCompras(string? nombre, string? material, string? fabricante, decimal precioCompra, string? descripcion)
        {
            IList<HerramientasDTO> selectHerramientas = await _context.Herramientas
                .Include(r => r.Fabricante)
                .Include(r => r.CompraItems)
                    .ThenInclude(ri => ri.Compra)
                .Where(r =>( material == null && r.Material.Contains(material)
                   && ( precioCompra == null && r.Precio.Equals(precioCompra))))
                .OrderBy(r => r.Nombre)
                .Select(r => new HerramientasDTO(r.Id, r.Nombre, r.Material, r.Fabricante, r.Precio, r.Cantidad, r.Descripcion))
                .ToListAsync();
             return Ok(selectHerramientas);
         }
    }
}