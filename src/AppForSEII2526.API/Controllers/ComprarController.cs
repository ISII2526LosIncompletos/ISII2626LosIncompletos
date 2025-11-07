using AppForSEII2526.API.DTOs.CompraDTOs;
using AppForSEII2526.API.DTOs.HerramientaDTOs;
using AppForSEII2526.API.DTOs.ReparacionDTOs;
using AppForSEII2526.API.Models;

namespace AppForSEII2526.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComprarController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<HerramientasController> _logger;

        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType(typeof(CompraDetalleDTO), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.Conflict)]
        public async Task<ActionResult> CreateCompra(CompraCreacionDTO compraCreacion)
        {
            if (compraCreacion.CompraItems.Count == 0)
                ModelState.AddModelError("CompraItems", "Error! Debe incluir al menos una herramienta para comprar");

            var usuario = _context.ApplicationUsers.FirstOrDefault(au => au.UserName == compraCreacion.NombreCliente);
            if (usuario == null)
                ModelState.AddModelError("CompraApplicationUser", $"Error! El usuario {compraCreacion.NombreCliente} no estÃ¡ registrado");

            if (ModelState.ErrorCount > 0)
                return BadRequest(new ValidationProblemDetails(ModelState));

            var nombreHerramienta = compraCreacion.CompraItems.Select(ri => ri.Nombre).ToList<string>();

            var herramientas = _context.Herramientas
                .Include(f => f.Fabricante)
                .Include(m => m.CompraItems)
                    .ThenInclude(ri => ri.Compra)
                .Where(m => nombreHerramienta.Contains(m.Nombre))
                .ToList();

            Compra compra = new Compra(compraCreacion.FechaCompra, compraCreacion.PrecioTotal, compraCreacion.MetodoPago, new List<CompraItem>(), usuario);
            compra.PrecioTotal = 0;

            foreach (var item in compraCreacion.CompraItems)
            {
                var herramienta = herramientas.FirstOrDefault(m => m.Nombre == item.Nombre);
                if (herramienta == null) //Si la herramienta no existe
                {
                    ModelState.AddModelError("CompraItems", $"Error! La herramienta {item.Nombre} no existe");
                }
                else
                {
                    string descripcion = null;
                    if (item.Descripcion.Length > 0) descripcion = item.Descripcion;


                    compra.CompraItems.Add(new CompraItem
                    {
                        Precio = herramienta.Precio * item.Cantidad,
                        Descripcion = descripcion,
                        Cantidad = item.Cantidad,
                        Herramienta = herramienta,
                        Compra = compra

                    });
                }
            }

            compra.PrecioTotal = compra.CompraItems.Sum(ri => ri.Precio);

            if (ModelState.ErrorCount > 0)
            {
                return BadRequest(new ValidationProblemDetails(ModelState));
            }

            _context.Add(compra);
            try
            {
                //guardamos los cambios
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                ModelState.AddModelError("Reparacion", $"Error! Se ha producido un error al guardar su compra. Por favor, intentelo de nuevo");
                return Conflict("Error" + ex.Message);

            }
            var detalleCompra = new CompraDetalleDTO(usuario.Nombre, usuario.Apellidos, usuario.DireccionEnvio, compra.PrecioTotal
                , compra.FechaCompra, compra.CompraItems.Select(ri => new CompraItemDTO(
                   ri.Herramienta.Id, ri.Herramienta.Nombre, ri.Herramienta.Material, ri.Precio, ri.Cantidad, ri.Descripcion)).ToList()
           );

            return CreatedAtAction("GetCompra", new { id = compra.Id }, detalleCompra);
        }
    }
}
