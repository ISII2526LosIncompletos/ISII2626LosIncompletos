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