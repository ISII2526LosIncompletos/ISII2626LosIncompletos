using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppForSEII2526.API.Controllers;
using AppForSEII2526.API.DTOs;
using AppForSEII2526.API.DTOs.HerramientaDTOs;
using Microsoft.EntityFrameworkCore;

namespace AppForSEII2526.UT.HerramientasController_test
{
    public class GetHerramientasParaReparar_test : AppForSEII25264SqliteUT
    {
        public GetHerramientasParaReparar_test()
        {

            var fabricante = new List<Fabricante>()
            {
                new Fabricante("Bosch"),
                new Fabricante("Makita"),
                new Fabricante("DeWalt")

            };

            var herramienta = new List<Herramienta>()
            {
                new Herramienta("Acero", 10.3m, "Taladro", 1, fabricante[0], 1),
                new Herramienta("Madera", 20.5m, "Sierra", 2, fabricante[1], 2),
                new Herramienta("Acero", 15.75m, "Lijadora", 3, fabricante[2], 3)

            };

            _context.Fabricantes.AddRange(fabricante);
            _context.Herramientas.AddRange(herramienta);
            _context.SaveChanges();

        }

        //Cada caso contiene filtros
        public static IEnumerable<object[]> CasosDePruebaPara_GetHerramientasParaReparar_test()
        {
            var herramientaDTOs = new List<HerramientasDTO>()
            {
                new HerramientasDTO(1, "Taladro", "Acero", "Bosch", 10.3m, 1),
                new HerramientasDTO(2, "Sierra", "Madera", "Makita",20.5m, 2),
                new HerramientasDTO(3, "Lijadora", "Acero", "DeWalt", 15.75m, 3)
            };

            var herramientaDTOsTC1 = new List<HerramientasDTO>()
            {
                herramientaDTOs[0],
                herramientaDTOs[1],
                herramientaDTOs[2]
            }.OrderBy(h => h.Nombre).ToList();

            var herramientaDTOsTC2 = new List<HerramientasDTO>()
            {
                herramientaDTOs[1]
            }.OrderBy(h => h.Nombre).ToList();

            var herramientaDTOsTC3 = new List<HerramientasDTO>()
            {
                herramientaDTOs[0]
            }.OrderBy(h => h.Nombre).ToList();

            var alltests = new List<object[]>
            {
                new object[] {null, null, herramientaDTOsTC1},
                new object[] {"Sierra", null, herramientaDTOsTC2},
                new object[] {null, 1, herramientaDTOsTC3},
            };
            return alltests;
        }


        //Theory queremos comprobar varios casos (como varios filtros) si solo es uno se usa Fact
        [Theory]
        [MemberData(nameof(CasosDePruebaPara_GetHerramientasParaReparar_test))]
        [Trait("Database", "WithoutFixture")]
        [Trait("LevelTesting", "Unit Testing")]
        public async Task GetHerramientasParaReparar_Ok_test(string? filtroNombre, int? filtroTiempoReparacion, IList<HerramientasDTO> expectedHerramientas)
        {
            //Arrange: creamos el controlador con el contexto de prueba, sin loggearse
            //Se definen las variables a las que necesitamos acceder (HerramientasController)
            var controller = new HerramientasController(_context, null);

            //Act: llamamos al metodo a testear
            //Hacemos la llamada y para que conteste utilizamos await
            var result = await controller.GetReparacion(filtroNombre, filtroTiempoReparacion);

            //Assert: comprobamos que el resultado es el esperado
            //Nos quedarnos con ObjectResult, porque result tiene más cosas/info
            var okResult = Assert.IsType<OkObjectResult>(result);
            //Tambien nos quedamos con Value, porque es donde está la lista a comparar
            var herramientaDTOsActual = Assert.IsType<List<HerramientasDTO>>(okResult.Value);

            //Comparamos lo que hemos obtenido con lo que esperábamos
            Assert.Equal(expectedHerramientas, herramientaDTOsActual);
        }

        /*
        [Fact]
        [Trait("LevelTesting", "Unit Testing")]
        [Trait("Database", "WithoutFixture")]
        public async Task GetHerramientasParaReparacion_Null_test()
        {
            //Arrange
            List<HerramientasDTO> expectedHerramientas = new List<HerramientasDTO>()
            {
                new HerramientasDTO(1, "Taladro", "Acero", new Fabricante("Bosch"), 10.3m, 1),
                new HerramientasDTO(2, "Sierra", "Madera", new Fabricante("Makita"),20.5m, 2),
                new HerramientasDTO(3, "Lijadora", "Acero", new Fabricante("DeWalt"), 15.75m, 3)
            };
            var mock = new Mock<ILogger<HerramientasController>>();
            ILogger<HerramientasController> logger = mock.Object;
            HerramientasController controller = new HerramientasController(_context, logger);

            //Act
            var result = await controller.GetReparacion(null, null);

            //Assert
            //Comprobamos que el tipo de respuesta es OK y obtenemos la lista de herramientas
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var problemDetails = Assert.IsType<ValidationProblemDetails>(badRequestResult.Value);
            var problem = problemDetails.Errors.First().Value[0];

            Assert.Equal("", problem);
        }
        */

    }
}