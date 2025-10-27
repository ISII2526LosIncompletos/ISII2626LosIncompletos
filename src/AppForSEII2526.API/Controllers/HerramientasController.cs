using AppForSEII2526.API.DTOs.HerramientaDTOs;

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
        [ProducesResponseType(typeof(ComprarForCreateDTO), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetCompras(string? nombre, string? material, string? fabricante, float precioCompra, string? descripcion)
        {
            IList<ComprarForCreateDTO> selectHerramientas = await _context.Compras
                .Include(r => r.Fabricante)
                .Include(r => r.CompraItems)
                    .ThenInclude(ri => ri.Compra)
                .Where(r =>( material == null && r.Material.Contains(material)
                && ( precioCompra == null && r.Precio.Equals(precioCompra))))
                .OrderBy(r => r.Nombre)
                .Select(r => new ComprarHerramientasDTO(r.Nombre, r.Material,r.Fabricante, r.Precio, r.cantidad, r.Descripcion))
                .ToListAsync();
            return Ok(selectHerramientas);
        }

        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(HerramientasDTO), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetReparacion(string? nombre, string? material, string? fabricante,
            decimal? precio, DateTime? tiempoReparacion)
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

    }
}