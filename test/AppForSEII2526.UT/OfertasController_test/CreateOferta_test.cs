using AppForSEII2526.API.Controllers;
using AppForSEII2526.API.DTOs.OfertaDTOs;

namespace AppForSEII2526.UT.OfertasController_test
{
    public class CreateOferta_test : AppForSEII25264SqliteUT
    {
        private readonly ApplicationDbContext _context;

        public CreateOferta_test()
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
            _context.SaveChanges();
        }

        [Fact]
        public async Task CreateOferta_Success_test()
        {
            var logger = new Mock<ILogger<OfertasController>>().Object;
            var controller = new OfertasController(_context, logger);

            var inputDto = new OfertaForCreationDTO
            {
                FechaInicio = DateTime.Today.AddDays(1),
                FechaFinal = DateTime.Today.AddDays(10),
                MetodoPago = "TarjetaCredito",
                DirigidaA = "Clientes",
                Items = new List<OfertaItemDTO>
                {
                    new OfertaItemDTO { HerramientaId = 1, Porcentaje = 20 }
                }
            };

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
                    Porcentaje = 20m, // 20m es decimal
                    HerramientaNombre = "Martillo",
                    HerramientaMaterial = "acero",
                    FabricanteNombre = "juan",
                    PrecioOriginal = 1.00m,
                    PrecioFinal = 0.80m
                }
                }
            };

            var result = await controller.CreateOferta(inputDto);

            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            var actualDto = Assert.IsType<OfertaDetailDTO>(createdResult.Value);
            Assert.Equal(expectedDto, actualDto);
        }

        [Theory]
        [MemberData(nameof(TestCasesFor_CreateOferta_BadRequest))]
        public async Task CreateOferta_BadRequest_test(OfertaForCreationDTO dto, string expectedError)
        {
            var logger = new Mock<ILogger<OfertasController>>().Object;
            var controller = new OfertasController(_context, logger);

            var result = await controller.CreateOferta(dto);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(expectedError, badRequestResult.Value);
        }

        public static IEnumerable<object[]> TestCasesFor_CreateOferta_BadRequest()
        {
            yield return new object[]
            {
                new OfertaForCreationDTO
                {
                    FechaInicio = DateTime.Today.AddDays(-1),
                    FechaFinal = DateTime.Today.AddDays(10),
                    MetodoPago = "TarjetaCredito",
                    Items = new List<OfertaItemDTO> { new OfertaItemDTO { HerramientaId = 1, Porcentaje = 10 } }
                },
                "La fecha de inicio no puede ser anterior a hoy."
            };

            yield return new object[]
            {
                new OfertaForCreationDTO
                {
                    FechaInicio = DateTime.Today.AddDays(10),
                    FechaFinal = DateTime.Today.AddDays(5),
                    MetodoPago = "TarjetaCredito",
                    Items = new List<OfertaItemDTO> { new OfertaItemDTO { HerramientaId = 1, Porcentaje = 10 } }
                },
                "La fecha final debe ser posterior a la fecha de inicio."
            };

            yield return new object[]
            {
                new OfertaForCreationDTO
                {
                    FechaInicio = DateTime.Today.AddDays(1),
                    FechaFinal = DateTime.Today.AddDays(10),
                    MetodoPago = "Bizum",
                    Items = new List<OfertaItemDTO> { new OfertaItemDTO { HerramientaId = 1, Porcentaje = 10 } }
                },
                "Método de pago inválido."
            };

            yield return new object[]
            {
                new OfertaForCreationDTO
                {
                    FechaInicio = DateTime.Today.AddDays(1),
                    FechaFinal = DateTime.Today.AddDays(10),
                    MetodoPago = "TarjetaCredito",
                    Items = new List<OfertaItemDTO> { new OfertaItemDTO { HerramientaId = 99, Porcentaje = 10 } } // Id 99 no existe
                },
                "No se encontró la herramienta con ID 99."
            };

            yield return new object[]
            {
                new OfertaForCreationDTO
                {
                    FechaInicio = DateTime.Today.AddDays(5),
                    FechaFinal = DateTime.Today.AddDays(10),
                    MetodoPago = "TarjetaCredito",
                    Items = new List<OfertaItemDTO> { new OfertaItemDTO { HerramientaId = 1, Porcentaje = 10 } } 
                },
                "!Error¡ La oferta debe durar al menos una semana"
            };
        }
    }
}