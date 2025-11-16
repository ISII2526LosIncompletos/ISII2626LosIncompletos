using AppForSEII2526.API.Controllers;
using AppForSEII2526.API.DTOs;
using AppForSEII2526.API.DTOs.CompraDTOs;
using AppForSEII2526.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UT.ComprarController_test
{
    public class GetCompras_test: AppForSEII25264SqliteUT
    {
        public GetCompras_test()
        {
            var fabricantes = new List<Fabricante>()
            {
                new Fabricante("KniPex"),
                new Fabricante("Stanley"),
                new Fabricante("ParkSide")
            };
            var herramientas = new List<Herramienta>()
            {
                new Herramienta ("Acero", 26.41m, "Tenaza", 2,fabricantes[0],1),
                new Herramienta ("Acero", 18.75m, "Destornillador",4, fabricantes[1],2),
                new Herramienta ( "Plástico y Metal", 34.90m, "Taladro",6, fabricantes[2],3)
            };

            ApplicationUser usuario = new ApplicationUser("Miguel", "Lopez Parra", "C Ejemplo 1", "Miguel.Lopez28@alu.uclm.es", "123456789", RolUsuario.Cliente, new DateTime(2025,11,2));
            
            var compra = new Compra(DateTime.Today, 49.21m, tiposMetodosPago.TarjetaCredito,
                new List<CompraItem>(), usuario);
            compra.CompraItems.Add(new CompraItem(herramientas[1], herramientas[1].Id, compra, compra.Id, 3, "Descripcion ejemplo", 49.21m));

            _context.AddRange(fabricantes);
            _context.AddRange(herramientas);
            _context.Add(usuario);
            _context.Add(compra);
            _context.SaveChanges();
        }

        [Fact]
        [Trait("Database", "WithoutFixture")]
        [Trait("LevelTesting", "Unit Testing")]
        public async Task GetCompras_NotFound_test()
        {
            //Arrange
            //MOCK es para simular la base de datos
            var mock = new Mock<ILogger<ComprarController>>();
            ILogger<ComprarController> logger = mock.Object;
            var controller = new ComprarController(_context, logger);

            //Act
            var result = await controller.GetDetalleCompras(0);

            //Assert
            //Vemos si la respuesta no es de tipo NotFoundResult

            Assert.IsType<NotFoundResult>(result);

        }
        [Fact]
        [Trait("Database", "WithoutFixture")]
        [Trait("LevelTesting", "Unit Testing")]
        public async Task GetCompras_OK_test()
        {
            //Arrange
            var mock = new Mock<ILogger<ComprarController>>();
            ILogger<ComprarController> logger = mock.Object;
            var controller = new ComprarController(_context, logger);

            var expectedCompra = new CompraDetalleDTO("Miguel", "Lopez Parra", "C Ejemplo 1", 49.21m, DateTime.Today, new List<CompraItemDTO>());
            expectedCompra.CompraItems.Add(new CompraItemDTO(2, "Destornillador", "Acero", 49.21m, 3, "Descripcion ejemplo"));
            //Act
            var result = await controller.GetDetalleCompras(1);

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var compraDTOActual= Assert.IsType<CompraDetalleDTO>(okResult.Value);
            var eq = expectedCompra.Equals(compraDTOActual);
            //Comprobamos que la reparación esperada es como la actual
            Assert.Equal(expectedCompra, compraDTOActual);

        }
    }
}
