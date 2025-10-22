using AppForSEII2526.API.DTOs.CompraDTOs;
using System.Data;
using static AppForSEII2526.API.Models.Compra;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
        [ProducesResponseType(typeof(ComprarForCreateDTO), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetCompras(string? nombre, string? material, string? fabricante, float precioCompra, string? descripcion)
        {
            IList<ComprarForCreateDTO> selectHerramientas = await _context.Compras
                .Include(r => r.Fabricante)
                .Include(r => r.CompraItems)
                    .ThenInclude(ri => ri.Compra)
                .Where(r =>( material == null && r.Material.Contains(material)
                && ( precioCompra == null && r.Precio.Equals(precioCompra))))
                .OrderBy(r => r.Nombre)
                .Select(r => new ComprarHerramientasDTO(r.Nombre, r.Material,r.Fabricante, r.Precio, r.cantidad, r.Descripcion))
                .ToListAsync();
            return Ok(selectHerramientas);
        }
    }
}