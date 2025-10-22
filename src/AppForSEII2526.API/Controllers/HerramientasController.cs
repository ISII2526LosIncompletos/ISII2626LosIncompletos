using AppForSEII2526.API.DTOs.HerramientasDTOs.ISII2626LosIncompletos.API.DTOs.OfertaDTOs;
using ISII2626LosIncompletos.API.DTOs.OfertaDTOs;

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

                .Where(o => (fabricante == null || o.Fabricante.Contains(fabricante) &&
                      (precio == null || o.Precio.Equals(precio)))
                    
                )

                .OrderByDescending(o => o.FechaOferta)

                .Select(o => new HerramientasDTO(o.Id, o.FechaInicio, o.FechaFinal, o.FechaOferta, o.MetodoPago, o.DirigidaA))
                .ToListAsync();

            return Ok(ofertas);
        }
    }
}