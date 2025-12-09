using AppForSEII2526.API.Controllers;
using AppForSEII2526.API.DTOs.OfertaDTOs;
using AppForSEII2526.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace AppForSEII2526.UT.OfertasController_test
{
    public class CreateOferta_test : AppForSEII25264SqliteUT
    {
        private readonly ApplicationDbContext _context;

        public CreateOferta_test()
        {
            _context = CreateContext();

            var fabricantes = new List<Fabricante>
            {
                new Fabricante { Id = 1, Nombre = "Phillips" },
                new Fabricante { Id = 2, Nombre = "Wurt" }
            };

            var herramientas = new List<Herramienta>
            {
                new Herramienta { Id = 1, Nombre = "Destornillador", Material = "Acero", Precio = 12.50m, TiempoReparacion = 1, FabricanteId = 2, Fabricante = fabricantes[1] },
                new Herramienta { Id = 2, Nombre = "Llave Inglesa", Material = "Acero", Precio = 10.30m, TiempoReparacion = 2, FabricanteId = 1, Fabricante = fabricantes[0] }
            };

            _context.Fabricantes.AddRange(fabricantes);
            _context.Herramientas.AddRange(herramientas);
            _context.SaveChanges();
        }

        public static IEnumerable<object[]> TestCasesFor_CreateOferta()
        {
            var itemsValidos = new List<OfertaItemInput> { new OfertaItemInput { HerramientaId = 1, Porcentaje = 20 } };

            var ofertaInicioInvalido = new OfertaForCreationDTO
            {
                FechaInicio = DateTime.Today.AddDays(-1),
                FechaFinal = DateTime.Today.AddDays(10),
                MetodoPago = "TarjetaCredito",
                DirigidaA = "Clientes",
                Items = itemsValidos
            };

            var ofertaFinInvalido = new OfertaForCreationDTO
            {
                FechaInicio = DateTime.Today.AddDays(10),
                FechaFinal = DateTime.Today.AddDays(5),
                MetodoPago = "TarjetaCredito",
                DirigidaA = "Clientes",
                Items = itemsValidos
            };

            var ofertaPagoInvalido = new OfertaForCreationDTO
            {
                FechaInicio = DateTime.Today.AddDays(1),
                FechaFinal = DateTime.Today.AddDays(10),
                MetodoPago = "Bizum",
                DirigidaA = "Clientes",
                Items = itemsValidos
            };

            var ofertaHerramientaNoExiste = new OfertaForCreationDTO
            {
                FechaInicio = DateTime.Today.AddDays(1),
                FechaFinal = DateTime.Today.AddDays(10),
                MetodoPago = "TarjetaCredito",
                DirigidaA = "Clientes",
                Items = new List<OfertaItemInput> { new OfertaItemInput { HerramientaId = 99, Porcentaje = 20 } }
            };

            var ofertaDuracionCorta = new OfertaForCreationDTO
            {
                FechaInicio = DateTime.Today.AddDays(5),
                FechaFinal = DateTime.Today.AddDays(6),
                MetodoPago = "TarjetaCredito",
                DirigidaA = "Clientes",
                Items = itemsValidos
            };

            return new List<object[]>
            {
                new object[] { ofertaInicioInvalido, "La fecha de inicio no puede ser anterior a hoy." },
                new object[] { ofertaFinInvalido, "La fecha final debe ser posterior a la fecha de inicio." },
                new object[] { ofertaPagoInvalido, "Método de pago inválido." },
                new object[] { ofertaHerramientaNoExiste, "No se encontró la herramienta con ID 99." },
                new object[] { ofertaDuracionCorta, "!Error¡ La oferta debe durar al menos una semana" }
            };
        }

        [Theory]
        [Trait("LevelTesting", "Unit Testing")]
        [Trait("Database", "WithoutFixture")]
        [MemberData(nameof(TestCasesFor_CreateOferta))]
        public async Task CreateOferta_BadRequest_test(OfertaForCreationDTO dto, string expectedError)
        {
            var logger = new Mock<ILogger<OfertasController>>().Object;
            var controller = new OfertasController(_context, logger);

            var result = await controller.CreateOferta(dto);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(expectedError, badRequestResult.Value);
        }

        [Fact]
        [Trait("LevelTesting", "Unit Testing")]
        [Trait("Database", "WithoutFixture")]
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
                Items = new List<OfertaItemInput>
                {
                    new OfertaItemInput { HerramientaId = 2, Porcentaje = 50 }
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
                        HerramientaId = 2,
                        Porcentaje = 50,
                        HerramientaNombre = "Llave Inglesa",
                        HerramientaMaterial = "Acero",
                        FabricanteNombre = "Phillips",
                        PrecioOriginal = 10.30m,
                        PrecioFinal = 5.15m
                    }
                }
            };

            var result = await controller.CreateOferta(inputDto);

            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            var actualDto = Assert.IsType<OfertaDetailDTO>(createdResult.Value);

            Assert.Equal(expectedDto.MetodoPago, actualDto.MetodoPago);
            Assert.Equal(expectedDto.Items.First().HerramientaNombre, actualDto.Items.First().HerramientaNombre);
            Assert.Equal(expectedDto.Items.First().PrecioFinal, actualDto.Items.First().PrecioFinal);
            Assert.Equal(expectedDto.Items.First().FabricanteNombre, actualDto.Items.First().FabricanteNombre);
        }
    }
} 