using AppForSEII2526.API.Controllers;
using AppForSEII2526.API.Models;
using AppForSEII2526.API.DTOs.HerramientaDTOs;

namespace AppForSEII2526.UT.HerramientasController_test
{
    public class GetOfertas_test : AppForSEII25264SqliteUT
    {
        private readonly ApplicationDbContext _context;
        private static HerramientasDTO martilloDTO;

        public GetOfertas_test()
        {
            _context = CreateContext();

            var fabricanteJuan = new Fabricante { Nombre = "juan" };
            var fabricanteOtro = new Fabricante { Nombre = "Otro" };
            _context.Fabricantes.AddRange(fabricanteJuan, fabricanteOtro);

            var martillo = new Herramienta
            {
                Id = 1,
                Nombre = "Martillo",
                Material = "acero",
                Precio = 1.00m,
                TiempoReparacion = 1,
                Fabricante = fabricanteJuan
            };
            var destornillador = new Herramienta
            {
                Id = 2,
                Nombre = "Destornillador",
                Material = "acero",
                Precio = 2.00m,
                TiempoReparacion = 1,
                Fabricante = fabricanteOtro
            };
            _context.Herramientas.AddRange(martillo, destornillador);

            var oferta = new Oferta
            {
                Id = 1,
                FechaInicio = DateTime.UtcNow,
                FechaFinal = DateTime.UtcNow.AddDays(5),
                FechaOferta = DateTime.UtcNow,
                MetodoPago = tiposMetodosPago.TarjetaCredito
            };
            _context.Ofertas.Add(oferta);

            var ofertaItem = new OfertaItem
            {
                HerramientaId = 1,
                OfertaId = 1,
                Porcentaje = 10,
                PrecioFinal = 0.90m
            };
            _context.OfertaItems.Add(ofertaItem);

            _context.SaveChanges();

            martilloDTO = new HerramientasDTO(
                martillo.Id, martillo.Nombre, martillo.Material,
                fabricanteJuan.Nombre, martillo.Precio, martillo.TiempoReparacion
            );
        }

        [Theory]
        [MemberData(nameof(TestCasesFor_GetOfertas))]
        public async Task GetOfertas_ConFiltros_test(
            string? fabricanteNombre,
            decimal? precio,
            List<HerramientasDTO> expectedTools)
        {
            var logger = new Mock<ILogger<HerramientasController>>().Object;
            var controller = new HerramientasController(_context, logger);

            var result = await controller.GetOfertas(fabricanteNombre, precio);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var actualTools = Assert.IsType<List<HerramientasDTO>>(okResult.Value);

            Assert.Equal(expectedTools, actualTools);
        }

        public static IEnumerable<object[]> TestCasesFor_GetOfertas()
        {

            var martilloDtoEsperado = new HerramientasDTO(
                id: 1,
                nombre: "Martillo",
                material: "acero",
                fabricante: "juan",
                precio: 1.00m,
                tiempoReparacion: 1
            );

            yield return new object[]
            {
        null,
        null,
        new List<HerramientasDTO> { martilloDtoEsperado }
            };

            yield return new object[]
            {
        "juan",
        null,
        new List<HerramientasDTO> { martilloDtoEsperado }
            };

            yield return new object[]
            {
        "Otro",
        null,
        new List<HerramientasDTO>()
            };

            yield return new object[]
            {
        null,
        1.00m,
        new List<HerramientasDTO> { martilloDtoEsperado }
            };
        }
    }
}
