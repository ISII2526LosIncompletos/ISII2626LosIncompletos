using AppForSEII2526.API.DTOs.HerramientaDTOs;
using AppForSEII2526.API.DTOs.ReparacionDTOs;

namespace AppForSEII2526.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReparacionesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<HerramientasController> _logger;



        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType(typeof(ReparacionDetalleDTO), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.Conflict)]
        public async Task<ActionResult> CreateReparacion(ReparacionCreacionDTO reparacionCreacion)
        {
            if (reparacionCreacion.FechaRecogida >= DateTime.Today)
                ModelState.AddModelError("FechaEntrega", "Error! La fecha en la que se recogerá la herramienta reparada debe ser posterior a hoy");

            if (reparacionCreacion.FechaEntrega >= reparacionCreacion.FechaRecogida)
                ModelState.AddModelError("FechaEntrega&FechaRecogida", "Error! La fecha de entrega debe ser anterior a la de recogida");

            if (reparacionCreacion.ReparacionItems.Count == 0)
                ModelState.AddModelError("ReparacionItems", "Error! Debe incluir al menos una herramienta para reparar");

            var usuario = _context.ApplicationUsers.FirstOrDefault(au => au.UserName == reparacionCreacion.NombreCliente);
            if (usuario == null)
                ModelState.AddModelError("ReparacionApplicationUser", $"Error! El usuario {reparacionCreacion.NombreCliente} no está registrado");

            if (ModelState.ErrorCount > 0)
                return BadRequest(new ValidationProblemDetails(ModelState));


            var nombreHerramienta = reparacionCreacion.ReparacionItems.Select(ri => ri.Nombre).ToList<string>();

            var herramientas = _context.Herramientas
                .Include(f => f.Fabricante)
                .Include(m => m.ItemsReparacion)
                    .ThenInclude(ri => ri.Reparacion)
                .Where(m => nombreHerramienta.Contains(m.Nombre))
                .ToList();


            Reparacion reparacion = new Reparacion(reparacionCreacion.FechaEntrega, reparacionCreacion.FechaRecogida, 
                reparacionCreacion.PrecioTotal, reparacionCreacion.MetodoPago, 
                new List<ReparacionItem>(), usuario);
            reparacion.PrecioTotal = 0;

            int numDias = (int)(reparacion.FechaRecogida - reparacion.FechaEntrega).TotalDays;

            foreach (var item in reparacionCreacion.ReparacionItems)
            {
                var herramienta = herramientas.FirstOrDefault(m => m.Nombre == item.Nombre);
                if (herramienta == null) //Si la herramienta no existe
                {
                    ModelState.AddModelError("ReparacionItems", $"Error! La herramienta {item.Nombre} no existe");
                }
                else
                {
                    string descripcion = null;
                    if (item.Descripcion.Length > 0) descripcion = item.Descripcion;

                    if (herramienta.TiempoReparacion > numDias)
                    {
                        numDias = herramienta.TiempoReparacion;
                    }
                    reparacion.ReparacionItem.Add(new ReparacionItem
                    {
                        Precio = herramienta.Precio * item.Cantidad,
                        Descripcion = descripcion,
                        Cantidad = item.Cantidad,
                        Herramienta = herramienta,
                        Reparacion = reparacion

                    });
                }
            }
            reparacion.PrecioTotal = reparacion.ReparacionItem.Sum(ri => ri.Precio * numDias);
            //por si hemos modificado el número de días
            reparacion.FechaRecogida = reparacion.FechaEntrega.AddDays(numDias);

            if (ModelState.ErrorCount > 0)
            {
                return BadRequest(new ValidationProblemDetails(ModelState));
            }

            _context.Add(reparacion);

            try
            {
                //guardamos los cambios
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                ModelState.AddModelError("Reparacion", $"Error! Se ha producido un error al guardar su reparación. Por favor, intentelo de nuevo");
                return Conflict("Error" + ex.Message);

            }

            var detalleReparacion = new ReparacionDetalleDTO(usuario.Nombre, usuario.Apellidos,
                reparacion.FechaEntrega, reparacion.FechaRecogida, reparacion.PrecioTotal,
                reparacion.ReparacionItem.Select(ri => new ReparacionItemDTO(
                    ri.Herramienta.Id, ri.Herramienta.Nombre, ri.Precio, ri.Cantidad, ri.Descripcion)).ToList()
            );

            return CreatedAtAction("GetRental", new { id = reparacion.Id }, detalleReparacion);
        }

    }
}
