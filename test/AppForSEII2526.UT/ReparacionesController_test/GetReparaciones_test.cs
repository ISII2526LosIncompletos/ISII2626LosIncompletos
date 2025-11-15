using AppForSEII2526.API.Controllers;
using AppForSEII2526.API.DTOs;
using AppForSEII2526.API.DTOs.ReparacionDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UT.ReparacionesController_test
{
    public class GetReparaciones_test : AppForSEII25264SqliteUT
    {
        public GetReparaciones_test()
        {
            var fabricantes = new List<Fabricante>()
            {
                new Fabricante("Bosch"),
                new Fabricante("Makita"),
                new Fabricante("DeWalt")

            };

            var herramientas = new List<Herramienta>()
            {
                new Herramienta("Acero", 10.3m, "Taladro", 1, fabricantes[0], 1),
                new Herramienta("Madera", 20.5m, "Sierra", 2, fabricantes[1], 2),
                new Herramienta("Acero", 15.75m, "Lijadora", 3, fabricantes[2], 3)

            };

            ApplicationUser user = new ApplicationUser("Lucia", "Cebrian Perez", "Calle Ejemplo 1", "lucia.cebrian2@alu.uclm.es", "123456789", RolUsuario.Cliente, new DateTime(2025, 11, 01));

            var reparacion = new Reparacion(DateTime.Today, DateTime.Today.AddDays(1), 7.25m, tiposMetodosPago.Efectivo, new List<ReparacionItem>(), user);

            reparacion.ItemsReparacion.Add(new ReparacionItem(herramientas[0], 
                herramientas[0].Id, reparacion, reparacion.Id,
                2, "Descripción ejemplo", 7.25m));

            _context.ApplicationUsers.Add(user);
            _context.AddRange(fabricantes);
            _context.AddRange(herramientas);
            _context.Add(reparacion);
            _context.SaveChanges();
        }
        
        [Fact]
        [Trait("Database", "WithoutFixture")]
        [Trait("LevelTesting", "Unit Testing")]
        public async Task GetReparacion_NotFound_test()
        {
            //MOCK --> para simular la base de datos o, al menos, una parte de ella
            // Arrange
            var mock = new Mock<ILogger<ReparacionesController>>();
            ILogger<ReparacionesController> logger = mock.Object;
            var controller = new ReparacionesController(_context, logger);

            // Act
            var result = await controller.GetDetalleReparar(0);

            //Assert: comprobamos que la respuesta es de tipo NotFoundResult
            Assert.IsType<NotFoundResult>(result);

        }

        [Fact]
        [Trait("LevelTesting", "Unit Testing")]
        [Trait("Database", "WithoutFixture")]
        public async Task GetReparacion_OK_test()
        {
            // Arrange
            var mock = new Mock<ILogger<ReparacionesController>>();
            ILogger<ReparacionesController> logger = mock.Object;
            var controller = new ReparacionesController(_context, logger);

            var expectedReparacion = new ReparacionDetalleDTO("Lucia", "Cebrian Perez",
                DateTime.Today, DateTime.Today.AddDays(1), "123456789", new List<ReparacionItemDTO>());
            
            expectedReparacion.ItemsReparacion.Add(new ReparacionItemDTO(1, "Taladro", 
                7.25m, 1, 2, "Descripción ejemplo"));

            // Act 
            var result = await controller.GetDetalleReparar(1);

            //Assert
            //Comprobamos que la respuesta es OK
            var okResult = Assert.IsType<OkObjectResult>(result);
            var reparacionDTOActual = Assert.IsType<ReparacionDetalleDTO>(okResult.Value);
            var eq = expectedReparacion.Equals(reparacionDTOActual);
            //Comprobamos que la reparación esperada es como la actual
            Assert.Equal(expectedReparacion, reparacionDTOActual);

        }
    }
}