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
        public async Task<ActionResult> GetDetalleCompras(int id)
        {
            if (_context.Compras == null)
            {
                _logger.LogError("Error: No existe Tabla de Compra");
                return NotFound();
            }
            var compra = await _context.Compras
           .Where(r => r.Id == id)
               .Include(r=>r.ApplicationUser)
               .Include(r => r.CompraItems)
                  .ThenInclude(ri => ri.Herramienta)
           .Select(r => new CompraDetalleDTO(r.Id, r.ApplicationUser.Nombre,
                  r.ApplicationUser.Apellidos, r.ApplicationUser.DireccionEnvio, r.PrecioTotal, r.FechaCompra, r.CompraItems
                      .Select(ri => new CompraItemDTO(ri.Herramienta.Id,
                              ri.Herramienta.Nombre, ri.Herramienta.Material, ri.Precio, ri.Cantidad,
                              ri.Descripcion))
                      .ToList<CompraItemDTO>()))
           .FirstOrDefaultAsync();

            if (compra == null)
            {
                _logger.LogError($"Error: Compra con id {id} no existe");
                return NotFound();
            }


            return Ok(compra);
        }

        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType(typeof(CompraDetalleDTO), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.Conflict)]
        public async Task<ActionResult> CreateCompra(CompraCreacionDTO compraCreacion)
        {
            if (compraCreacion.CompraItems.Count == 0)
                ModelState.AddModelError("CompraItems", "La compra debe contener al menos un item.");           

            if (string.IsNullOrEmpty(compraCreacion.NombreCliente))
            {
                ModelState.AddModelError("Nombre", "El nombre no puede estar vacio");
            }

            if (string.IsNullOrEmpty(compraCreacion.ApellidoCliente))
            {
                ModelState.AddModelError("Apellido", "El apellido no puede estar vacio");
            }

            if (string.IsNullOrEmpty(compraCreacion.DireccionEnvio))
            {
                ModelState.AddModelError("Dirección de envio", "La direccion de envio no puede estar vacio");
            }

            var usuario = _context.ApplicationUsers.FirstOrDefault(au => au.Nombre == compraCreacion.NombreCliente);
            if (usuario == null)
                ModelState.AddModelError("CompraApplicationUser", "El usuario no existe.");

            if (ModelState.ErrorCount > 0)
                return BadRequest(new ValidationProblemDetails(ModelState));

            var nombreHerramienta = compraCreacion.CompraItems.Select(ri => ri.Nombre).ToList<string>();

            var herramientas = _context.Herramientas
                .Include(f => f.Fabricante)
                .Include(m => m.CompraItems)
                    .ThenInclude(ri => ri.Compra)
                .Where(m => nombreHerramienta.Contains(m.Nombre))
                .ToList();

            Compra compra = new Compra
            {
                FechaCompra = compraCreacion.FechaCompra,
                MetodoPago = compraCreacion.MetodoPago,
                ApplicationUser = usuario,
                CompraItems = new List<CompraItem>()

            };
            compra.PrecioTotal = 0;

            foreach (var item in compraCreacion.CompraItems)
            {
                if (item.Cantidad <= 0)
                {
                    ModelState.AddModelError("Cantidad", "La cantidad debe ser mayor que cero.");
                }
                if (string.IsNullOrEmpty(item.Descripcion))
                {
                    ModelState.AddModelError("Descripción", "La descripcion no puede estar vacia");
                }
                if (ModelState.ErrorCount > 0)
                    return BadRequest(new ValidationProblemDetails(ModelState));
                var herramienta = herramientas.FirstOrDefault(m => m.Nombre == item.Nombre);
                if (herramienta == null) //Si la herramienta no existe
                {
                    ModelState.AddModelError("CompraItems", $"La herramienta '{item.Nombre}' no existe.");
                }
                else
                {
                    compra.CompraItems.Add(new CompraItem
                    {
                        HerramientaId=item.HerramientaID,
                        Cantidad = item.Cantidad,
                        Descripcion = item.Descripcion,
                        Precio = herramienta.Precio * item.Cantidad,                       
                        Herramienta = herramienta,
                        Compra = compra

                    });
                }
            }

            compra.PrecioTotal = compra.CompraItems.Sum(ri => ri.Herramienta.Precio);

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
            var detalleCompra = new CompraDetalleDTO(compra.Id, usuario.Nombre, usuario.Apellidos, usuario.DireccionEnvio, compra.PrecioTotal
                , compra.FechaCompra, compra.CompraItems.Select(ri => new CompraItemDTO(
                   ri.Herramienta.Id, ri.Herramienta.Nombre, ri.Herramienta.Material, ri.Herramienta.Precio, ri.Cantidad, ri.Descripcion)).ToList()
           );

            return CreatedAtAction(nameof(GetDetalleCompras), new { id = compra.Id }, detalleCompra);
        }
    }
}