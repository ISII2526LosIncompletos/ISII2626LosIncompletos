using AppForSEII2526.API.DTOs.HerramientaDTOs;
using AppForSEII2526.API.DTOs.ReparacionDTOs;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppForSEII2526.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReparacionesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ReparacionesController> _logger;

        public ReparacionesController(ApplicationDbContext context, ILogger<ReparacionesController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(ReparacionDetalleDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> GetDetalleReparar(int id)
        {
            if (_context.Reparaciones == null)
            {
                _logger.LogError("Error! La tabla de Reparaciones no existe");
                return NotFound();
            }

            //fuerza evaluación en memoria antes de la proyección a DTOs (evita error de SQL APPLY en SQLite)
            var reparacion = _context.Reparaciones //se ha quitado el await
                .Where(r => r.Id == id)
                .Include(r => r.ItemsReparacion)
                    .ThenInclude(ri => ri.Herramienta)
                .Include(r => r.ApplicationUser) //cargamos el usuario, evitamos null
                .AsEnumerable() //forzar LINQ to Objects aquí
                .Select(r => new ReparacionDetalleDTO(
                    r.Id,
                    r.ApplicationUser.Nombre,
                    r.ApplicationUser.Apellidos,
                    r.FechaEntrega,  
                    r.FechaRecogida, 
                    r.ApplicationUser.NumTelefono, 
                    r.ItemsReparacion
                        .Select(ri => new ReparacionItemDTO(
                            ri.Herramienta.Id, 
                            ri.Herramienta.Nombre,
                            ri.Precio, 
                            (r.FechaRecogida - r.FechaEntrega).Days, 
                            ri.Cantidad, 
                            ri.Descripcion
                        )).ToList<ReparacionItemDTO>()))
                .FirstOrDefault(); //la consulta se ejecuta en memoria, ya no es async

            if (reparacion == null)
            {
                _logger.LogError($"Error! No existe ninguna reparación con id {id}");
                return NotFound();
            }
            return Ok(reparacion);
        }


        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType(typeof(ReparacionDetalleDTO), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.Conflict)]
        public async Task<ActionResult> CrearReparacion(ReparacionCreacionDTO creacionReparacion)
        {
            if (creacionReparacion.FechaEntrega < DateTime.Today)
            {
                ModelState.AddModelError("FechaEntrega", "Error! La fecha en la que se entrega la herramienta debe ser, como mínimo, hoy");
            }

            if (creacionReparacion.ItemsReparacion.Count == 0)
            {
                ModelState.AddModelError("ItemsReparacion", "Error! Debe incluir al menos una herramienta para reparar");
            }

            var usuario = _context.ApplicationUsers.FirstOrDefault(au => au.Nombre == creacionReparacion.NombreCliente
                        && au.Apellidos == creacionReparacion.ApellidoCliente);
            if (usuario == null)
                ModelState.AddModelError("ReparacionApplicationUser", $"Error! El usuario {creacionReparacion.NombreCliente} {creacionReparacion.ApellidoCliente} no está registrado");

            if (ModelState.ErrorCount > 0)
                return BadRequest(new ValidationProblemDetails(ModelState));

            var nombreHerramientas = creacionReparacion.ItemsReparacion.Select(ri => ri.Nombre).ToList();

            var herramientas = _context.Herramientas
                .Include(f => f.Fabricante)
                .Where(h => nombreHerramientas.Contains(h.Nombre))
                .ToList();

            Reparacion reparacion = new Reparacion //Para crear la reparación con los datos que tenemos por ahora
            {
                FechaEntrega = creacionReparacion.FechaEntrega,
                MetodoPago = creacionReparacion.MetodoPago,
                ItemsReparacion = new List<ReparacionItem>(),
                ApplicationUser = usuario
            };

            reparacion.PrecioTotal = 0m;
            int numDias = 0;

            foreach (var item in creacionReparacion.ItemsReparacion)
            {
                var herramienta = herramientas.FirstOrDefault(h => h.Nombre == item.Nombre);
                if (herramienta == null)
                {
                    ModelState.AddModelError("Herramienta", $"Error! La herramienta {item.Nombre} no existe");
                }
                else
                {
                    string descripcion = null;
                    if (item.Descripcion.Length > 0)
                    {
                        descripcion = item.Descripcion;
                    }

                    if (herramienta.TiempoReparacion > numDias)
                    {
                        numDias = herramienta.TiempoReparacion;
                    }
                    reparacion.ItemsReparacion.Add(new ReparacionItem
                    {
                        Precio = herramienta.Precio * item.Cantidad,
                        Descripcion = descripcion,
                        Cantidad = item.Cantidad,
                        Herramienta = herramienta,
                        Reparacion = reparacion

                    });
                }
            }

            reparacion.PrecioTotal = reparacion.ItemsReparacion.Sum(ri => ri.Precio);
            reparacion.FechaRecogida = reparacion.FechaEntrega.AddDays(numDias);

            if (ModelState.ErrorCount > 0)
            {
                return BadRequest(new ValidationProblemDetails(ModelState));
            }

            _context.Add(reparacion);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message}");
                ModelState.AddModelError("Reparacion", "Error! Se ha producido un error al guardar su reparación. Por favor, intentelo de nuevo");
                return Conflict("Error" + ex.Message);
            }

            var detalleReparacion = new ReparacionDetalleDTO(reparacion.Id, usuario.Nombre, usuario.Apellidos,
                reparacion.FechaEntrega, reparacion.FechaRecogida, usuario.NumTelefono, reparacion.ItemsReparacion
                    .Select(ri => new ReparacionItemDTO(
                        ri.Herramienta.Id, ri.Herramienta.Nombre, ri.Precio, numDias, ri.Cantidad, ri.Descripcion)
                    ).ToList()
                );

            return CreatedAtAction("CrearReparacion", new { id = reparacion.Id }, detalleReparacion);
        }
    }
}