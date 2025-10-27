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
        [ProducesResponseType(typeof(decimal), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> ComputeDivision(decimal op1, decimal op2)
        {
            if (op2 == 0)
            {
                _logger.LogError($"{DateTime.Now} Exception: op2=0, division by 0");
                return BadRequest("op2 must be different from 0");
            }
            decimal result = decimal.Round(op1 / op2, 2);
            return Ok(result);
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