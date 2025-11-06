using AppForSEII2526.API.DTOs.CompraDTOs;
using AppForSEII2526.API.DTOs.HerramientaDTOs;
using AppForSEII2526.API.DTOs.ReparacionDTOs;

namespace AppForSEII2526.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComprarController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ComprarController> _logger;
        public ComprarController(ApplicationDbContext context, ILogger<ComprarController> logger)
        {
            _context = context;
            _logger = logger;
        }
        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(CompraDetalleDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> GetCompras(int id)
        {
            if (_context.Compras == null)
            {
                _logger.LogError("Error: Tabla Compras no existe");
                return NotFound();
            }
            var compra = await _context.Compras
           .Where(r => r.Id == id)
               .Include(r => r.CompraItems)
                  .ThenInclude(ri => ri.Herramienta)
           .Select(r => new CompraDetalleDTO(r.ApplicationUser.Nombre,
                  r.ApplicationUser.Apellidos, r.ApplicationUser.DireccionEnvio, r.PrecioTotal,r.FechaCompra, r.CompraItems
                      .Select(ri => new CompraItem(ri.Herramienta, ri.Herramienta.Id,
                              ri.Compra, ri.Compra.Id, ri.Cantidad,
                              ri.Descripcion, ri.Precio))
                      .ToList<CompraItem>()))
           .FirstOrDefaultAsync();

            if (compra == null)
            {
                _logger.LogError($"Error: Compra con id {id} no existe");
                return NotFound();
            }


            return Ok(compra);
        }
    }
}
