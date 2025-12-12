using AppForSEII2526.API.Controllers;
using AppForSEII2526.API.DTOs.OfertaDTOs;
using AppForSEII2526.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace AppForSEII2526.UT.OfertasController_test
{
    public class GetDetalleOferta_test : AppForSEII25264SqliteUT
    {
        private readonly ApplicationDbContext _context;

        public GetDetalleOferta_test()
        {
            _context = CreateContext();

            var wurt = new Fabricante { Id = 2, Nombre = "Wurt" };

            var destornillador = new Herramienta
            {
                Id = 1,
                Nombre = "Destornillador",
                Material = "Acero",
                Precio = 12.50m,
                TiempoReparacion = 1,
                FabricanteId = 2,
                Fabricante = wurt
            };

            var oferta = new Oferta
            {
                Id = 1,
                FechaInicio = DateTime.Today,
                FechaFinal = DateTime.Today.AddDays(2),
                FechaOferta = DateTime.Today,
                MetodoPago = tiposMetodosPago.TarjetaCredito,
                DirigidaA = TiposDirigidaOferta.Clientes,
                OfertaItems = new List<OfertaItem>()
            };

            var ofertaItem = new OfertaItem
            {
                HerramientaId = 1,
                Herramienta = destornillador,
                Oferta = oferta,
                Porcentaje = 10,
                PrecioFinal = 11.25m
            };

            oferta.OfertaItems.Add(ofertaItem);

            _context.Fabricantes.Add(wurt);
            _context.Herramientas.Add(destornillador);
            _context.Ofertas.Add(oferta);
            _context.SaveChanges();
        }

        [Fact]
        [Trait("LevelTesting", "Unit Testing")]
        [Trait("Database", "WithoutFixture")]
        public async Task GetDetalleOferta_Success_test()
        {
            var logger = new Mock<ILogger<OfertasController>>().Object;
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
                        HerramientaNombre = "Destornillador",
                        HerramientaMaterial = "Acero",
                        FabricanteNombre = "Wurt",
                        PrecioOriginal = 12.50m,
                        PrecioFinal = 11.25m
                    }
                }
            };

            var result = await controller.GetDetalleOferta(1);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var actualDto = Assert.IsType<OfertaDetailDTO>(okResult.Value);

            Assert.Equal(expectedDto.Id, actualDto.Id);
            Assert.Equal(expectedDto.MetodoPago, actualDto.MetodoPago);
            Assert.Equal(expectedDto.Items.Count, actualDto.Items.Count);
            Assert.Equal(expectedDto.Items[0].HerramientaNombre, actualDto.Items[0].HerramientaNombre);
            Assert.Equal(expectedDto.Items[0].PrecioFinal, actualDto.Items[0].PrecioFinal);
            Assert.Equal(expectedDto.Items[0].FabricanteNombre, actualDto.Items[0].FabricanteNombre);
        }

        [Fact]
        [Trait("LevelTesting", "Unit Testing")]
        [Trait("Database", "WithoutFixture")]
        public async Task GetDetalleOferta_NotFound_test()
        {
            var logger = new Mock<ILogger<OfertasController>>().Object;
            var controller = new OfertasController(_context, logger);

            var result = await controller.GetDetalleOferta(99);

            Assert.IsType<NotFoundResult>(result);
        }
    }
}