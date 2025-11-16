using AppForSEII2526.API.Controllers;
using AppForSEII2526.API.Models;
using AppForSEII2526.API.DTOs.OfertaDTOs;


namespace AppForSEII2526.UT.OfertasController_test
{
    public class GetDetalleOferta_test : AppForSEII25264SqliteUT
    {
        private readonly ApplicationDbContext _context;

        public GetDetalleOferta_test()
        {
            _context = CreateContext();

            var fabricante = new Fabricante { Nombre = "juan" };
            _context.Fabricantes.Add(fabricante);

            var herramienta = new Herramienta
            {
                Id = 1,
                Nombre = "Martillo",
                Material = "acero",
                Precio = 1.00m,
                TiempoReparacion = 1,
                Fabricante = fabricante
            };
            _context.Herramientas.Add(herramienta);

            var oferta = new Oferta
            {
                Id = 1,
                FechaInicio = DateTime.UtcNow,
                FechaFinal = DateTime.UtcNow.AddDays(5),
                FechaOferta = DateTime.UtcNow,
                MetodoPago = tiposMetodosPago.TarjetaCredito,
                DirigidaA = TiposDirigidaOferta.Clientes,
                OfertaItems = new List<OfertaItem>()
            };

            var ofertaItem = new OfertaItem
            {
                Herramienta = herramienta,
                Oferta = oferta,
                Porcentaje = 10,
                PrecioFinal = 0.90m
            };

            oferta.OfertaItems.Add(ofertaItem);
            _context.Ofertas.Add(oferta);
            _context.SaveChanges();
        }

        [Fact]
        public async Task GetDetalleOferta_Success_test()
        {
            var mockLogger = new Mock<ILogger<OfertasController>>();
            ILogger<OfertasController> logger = mockLogger.Object;

            var controller = new OfertasController(_context, logger);

            var expectedDto = new OfertaDetailDTO
            {
                Id = 1,
                MetodoPago = "TarjetaCredito",
                DirigidaA = "Clientes",
                Items = new List<OfertaItemDTO>
                {
                    new OfertaItemDTO
                    {
                        HerramientaId = 1,
                        Porcentaje = 10m,
                        HerramientaNombre = "Martillo",
                        HerramientaMaterial = "acero",
                        FabricanteNombre = "juan",
                        PrecioOriginal = 1.00m,
                        PrecioFinal = 0.90m
                    }
                }
            };

            var result = await controller.GetDetalleOferta(1);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var actualDto = Assert.IsType<OfertaDetailDTO>(okResult.Value);
            Assert.Equal(expectedDto, actualDto);
        }

        [Fact]
        public async Task GetDetalleOferta_NotFound_test()
        {
            var logger = new Mock<ILogger<OfertasController>>().Object;
            var controller = new OfertasController(_context, logger);

            var result = await controller.GetDetalleOferta(99);

            Assert.IsType<NotFoundResult>(result);
        }
    }
}
