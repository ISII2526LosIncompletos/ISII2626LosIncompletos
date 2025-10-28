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
                        .Select(ri => new ReparacionItemDTO(ri.Herramienta.Id,
                                ri.Herramienta.Nombre, ri.Precio, ri.Cantidad,
                                ri.Descripcion))
                        .ToList<ReparacionItemDTO>()))
             .FirstOrDefaultAsync();

            if (rental == null)
            {
                _logger.LogError($"Error: Reparación con ID {id} no existe");
                return NotFound();
            }

            return Ok(rental);
        }


        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType(typeof(ReparacionDetalleDTO), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.Conflict)]
        public async Task<ActionResult> CreateReparacion(ReparacionCreacionDTO reparacionCreacion)
        {
            if (reparacionCreacion.FechaEntrega <= DateTime.Today)
                ModelState.AddModelError("FechaEntrega", "Error! Su reparación debe empezar antes de hoy");

            if (reparacionCreacion.FechaEntrega >= reparacionCreacion.FechaRecogida)
                ModelState.AddModelError("FechaEntrega&FechaRecogida", "Error! Su reparación debe terminar después de empezar");

            if (reparacionCreacion.ReparacionItems.Count == 0)
                ModelState.AddModelError("ReparacionItems", "Error! Debe incluir al menos una herramienta para reparar");

            var usuario = _context.ApplicationUsers.FirstOrDefault(au => au.UserName == reparacionCreacion.NombreCliente);
            if (usuario == null)
                ModelState.AddModelError("ReparacionApplicationUser", "Error! El nombre de usuario no está registrado");

            if (ModelState.ErrorCount > 0)
                return BadRequest(new ValidationProblemDetails(ModelState));


            var nombreHerramienta = reparacionCreacion.ReparacionItems.Select(ri => ri.Nombre).ToList<string>();

            var herramientas = _context.Herramientas.Include(m => m.ItemsReparacion)
                    .ThenInclude(ri => ri.Reparacion)
                .Where(m => nombreHerramienta.Contains(m.Nombre))
                .Select(m => new {
                    m.Id,
                    m.Nombre,
                    m.TiempoReparacion,
                    PrecioReparacion = m.ItemsReparacion.Max(ri => ri.Precio), //nos quedamos con el precio de reparación más alto, también podría ser la media
                    //Contamos el número de herramientas de reparación que están dentro del período de reparación.
                    NumReparacionItems = m.ItemsReparacion.Count(ri => ri.Reparacion.FechaEntrega <= reparacionCreacion.FechaRecogida
                            && ri.Reparacion.FechaRecogida >= reparacionCreacion.FechaRecogida)
                })
                .ToList();


            Reparacion reparacion = new Reparacion(reparacionCreacion.FechaEntrega, reparacionCreacion.FechaRecogida, reparacionCreacion.PrecioTotal, 
                (AppForSEII2526.API.Models.tiposMetodosPago) reparacionCreacion.MetodoPago, new List<ReparacionItem>(), usuario);


            reparacion.PrecioTotal = 0;
            var numDias = (reparacion.FechaRecogida - reparacion.FechaEntrega).TotalDays;


            foreach (var item in reparacionCreacion.ReparacionItems)
            {
                var herramienta = herramientas.FirstOrDefault(m => m.Nombre == item.Nombre);
                //Si la herramienta no existe o queremos reparar más herramientas de las que hay disponibles
                if ((herramienta == null) || (herramienta.NumReparacionItems >= item.Cantidad))
                {
                    ModelState.AddModelError("ReparacionItems", $"Error! La herramienta '{item.Nombre}' no está disponible para ser alquilada desde {reparacionCreacion.FechaEntrega.ToShortDateString()} hasta {reparacionCreacion.FechaRecogida.ToShortDateString()}");
                }
                else
                {
                    //Relacionamos ReparacionItem con Reparacion, porque aún no existe en la BD ni tiene id válido
                    reparacion.ReparacionItem.Add(new ReparacionItem(new Herramienta(herramienta.Id, herramienta.Nombre,
                            herramienta.TiempoReparacion, herramienta.PrecioReparacion), herramienta.Id, reparacion,
                        reparacion.Id, item.Cantidad, item.Descripcion, herramienta.PrecioReparacion));
                }
            }
            reparacion.PrecioTotal = reparacion.ReparacionItem.Sum(ri => ri.Precio * numDias);


            //if there is any problem because of the available quantity of movies or because the movie does not exist
            if (ModelState.ErrorCount > 0)
            {
                return BadRequest(new ValidationProblemDetails(ModelState));
            }

            _context.Add(reparacion);

            try
            {
                //we store in the database both rental and its rentalitems
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                ModelState.AddModelError("Rental", $"Error! There was an error while saving your rental, plese, try again later");
                return Conflict("Error" + ex.Message);

            }

            //it returns rentalDetail
            var rentalDetail = new ReparacionCreacionDTO(usuario.Nombre, usuario.Apellidos, reparacion.FechaEntrega,
                reparacion.FechaRecogida, reparacion.MetodoPago, usuario.NumTelefono, herramienta.tiempoReparacion, reparacionItems);

            return CreatedAtAction("GetRental", new { id = reparacion.Id }, rentalDetail);
        }

    }
}
