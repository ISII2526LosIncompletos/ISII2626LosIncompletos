using AppForSEII2526.API.Controllers;
using AppForSEII2526.API.DTOs;
using AppForSEII2526.API.DTOs.HerramientaDTOs;
using AppForSEII2526.API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UT.ComprarController_test
{
    public class GetHerramientasParaComprar_test: AppForSEII25264SqliteUT
    {
        public GetHerramientasParaComprar_test()
        {
            var fabricantes = new List<Fabricante>()
            {
                new Fabricante("KniPex"),
                new Fabricante("Stanley"),
                new Fabricante("ParkSide")

            };
            var herramientas = new List<Herramienta>
            {
                new Herramienta ("Acero", 26.41m, "Tenaza", 2,fabricantes[0],1),
                new Herramienta ("Acero", 18.75m, "Destornillador",4, fabricantes[1],2),
                new Herramienta ( "Plástico y Metal", 34.90m, "Taladro",6, fabricantes[2],3)
            };

            _context.AddRange(fabricantes);
            _context.AddRange(herramientas);
            _context.SaveChanges();
        }

        public static IEnumerable<object[]> CasosDePruebaPara_GetHerramientasParaComprar_test()
        {
            var herramientasDTO = new List<HerramientasDTO>()
            {
                new HerramientasDTO(1, "Tenaza", "Acero", "KniPex", 26.41m, 2),
                new HerramientasDTO(2,"Destornillador", "Acero", "Stanley", 18.75m, 4),
                new HerramientasDTO(3,"Taladro", "Plástico y Metal", "ParkSide", 34.90m, 6)
            };
            var herramientaDTOsTC1 = new List<HerramientasDTO>()
            {
                herramientasDTO[0],
                herramientasDTO[1],
                herramientasDTO[2]
            }.OrderBy(h => h.Nombre).ToList();
            var herramientaDTOsTC2 = new List<HerramientasDTO>()
            {
                herramientasDTO[0],
                herramientasDTO[1]
            }.OrderBy(h => h.Nombre).ToList();
            var herramientasDTOsTC3= new List<HerramientasDTO>()
            {
                herramientasDTO[2]
            }.OrderBy(h => h.Nombre).ToList();
            var allTests = new List<object[]>
            {
                new object[] { null, null, herramientaDTOsTC1 },
                new object[] { "Acero", null, herramientaDTOsTC2 },
                new object[] { null, 34.90m , herramientasDTOsTC3 }
            };
            return allTests;

        }
        [Theory]//Para meter varios casos de prueba
        [MemberData(nameof(CasosDePruebaPara_GetHerramientasParaComprar_test))]
        [Trait("Database", "WithoutFixture")]
        [Trait("levelTesting", "Unit Testing")]
        public async Task getHerramientasParaComprar_Ok_test(string? filtroMaterial, decimal? filtroPrecio, IList<HerramientasDTO> expectedherramientas)
        {
            //Arrange: con el contexto de prueba, sin loggeo
            //Se definen las variables que se necesitan de HerramientasController
            var controller = new HerramientasController(_context, null);

            //Act: la realización de la prueba a desear
            //hacemos llamada y esperamos respuesta del await
            var result= await controller.GetCompras(filtroMaterial, filtroPrecio);

            //Assert
            //Nos quedamos con objectresult, pues result tiene más cosas
            var okResult = Assert.IsType<OkObjectResult>(result);

            //Nos quedamos con Value, donde está la lista de herramientas
            var herramientaDTOsActual = Assert.IsType<List<HerramientasDTO>>(okResult.Value);
            Assert.Equal(expectedherramientas, herramientaDTOsActual);
        }

    }
}
