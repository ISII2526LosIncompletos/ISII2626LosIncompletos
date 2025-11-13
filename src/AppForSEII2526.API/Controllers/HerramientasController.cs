using AppForSEII2526.API.DTOs.HerramientaDTOs;
using AppForSEII2526.API.Models;
using System.Data;
using System.Linq;


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
        [ProducesResponseType(typeof(IList<HerramientasDTO>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetReparacion(string? nombre, int? tiempoReparacion)
        {
            IList<HerramientasDTO> selectHerramientas = await _context.Herramientas
              .Include(h => h.Fabricante)
              .Where(h => (h.Nombre.Contains(nombre) || nombre == null)
                  && (h.TiempoReparacion == tiempoReparacion || tiempoReparacion == null))
              .OrderBy(h => h.Nombre)
              .Select(h => new HerramientasDTO(h.Id, h.Nombre, h.Material,
                    h.Fabricante, h.Precio, h.TiempoReparacion))
              .ToListAsync();
            return Ok(selectHerramientas);
        }
        


        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(IList<HerramientasDTO>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetCompras(string? material, decimal? precio)
        {
            var selectHerramientas = await _context.Herramientas
                .Include(r => r.Fabricante)
                .Where(r => ((r.Material.Contains(material) || material == null)
                    && (r.Precio.Equals(precio)) || precio == null))
                .OrderBy(r => r.Nombre)
                .Select(r => new HerramientasDTO(r.Id, r.Nombre, r.Material, r.Fabricante, r.Precio, r.TiempoReparacion))
                .ToListAsync();
            return Ok(selectHerramientas);
        }

        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(IList<HerramientasDTO>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetOfertas(string? fabricanteNombre, decimal? precio)
        {
            var herramientas = await _context.Herramientas
                .Include(herramienta => herramienta.Fabricante)
                .Where(h => (h.Fabricante.Nombre.Contains(fabricanteNombre) || fabricanteNombre == null)
                         && (h.Precio == precio || precio == null))
                .Select(h => new HerramientasDTO(h.Id, h.Nombre, h.Material, h.Fabricante, h.Precio, h.TiempoReparacion))
                .ToListAsync();
            return Ok(herramientas);
        }

    }
}