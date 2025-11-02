using AppForSEII2526.API.DTOs.HerramientaDTOs;
using AppForSEII2526.API.DTOs.ReparacionDTOs;
using System.Linq;

namespace AppForSEII2526.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReparacionesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<HerramientasController> _logger;


        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(ReparacionDetalleDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> GetReparacion(int id)
        {
            if (_context.Reparaciones == null)
            {
                _logger.LogError("Error: La tabla Reparaciones no existe");
                return NotFound();
            }

            var rental = await _context.Reparaciones
             .Where(r => r.Id == id)
                 .Include(r => r.ReparacionItem)
                    .ThenInclude(ri => ri.Herramienta)
             .Select(r => new ReparacionDetalleDTO(r.ApplicationUser.Nombre,
                    r.ApplicationUser.Apellidos, r.FechaEntrega, r.FechaRecogida, r.PrecioTotal,
                    r.ReparacionItem
                        .Select(ri => new ReparacionItem(ri.Herramienta, ri.Herramienta.Id,
                                ri.Reparacion, ri.Reparacion.Id, ri.Cantidad,
                                ri.Descripcion, ri.Precio))
                        .ToList<ReparacionItem>()))
             .FirstOrDefaultAsync();

            if (rental == null)
            {
                _logger.LogError($"Error: Reparación con ID {id} no existe");
                return NotFound();
            }

            return Ok(rental);
        }
    }
}
