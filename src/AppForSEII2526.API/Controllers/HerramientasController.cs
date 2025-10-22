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
            float? precioReparacion, DateTime? tiempoReparacion, string? descripcion)
        {
            IList<HerramientasDTO> selectHerramientas = await _context.Reparaciones
                .Include(r => r.Fabricante)
                .Include(r => r.ReparacionItems)
                    .ThenInclude(ri => ri.Reparacion)
                .Where(r => (nombre == null || r.Nombre.Contains(nombre))
                    && (tiempoReparacion == null || r.TiemoReparacion.Equals(tiempoReparacion)))
                .OrderBy(r => r.Nombre)
                .Select(r => new HerramientasDTO(r.HerramientaID, r.Nombre, r.Material,
                    r.Fabricante, r.PrecioReparacion, r.TiempoReparacion, r.Descripcion))
                .ToListAsync();
            return Ok(selectHerramientas);
        }

    }
}