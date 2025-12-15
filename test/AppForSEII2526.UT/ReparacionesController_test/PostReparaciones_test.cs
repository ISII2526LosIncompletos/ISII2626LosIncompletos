using AppForSEII2526.API.Controllers;
using AppForSEII2526.API.DTOs.ReparacionDTOs;
using AppForSEII2526.UT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UT.ReparacionesController_test
{
    public class PostReparaciones_test : AppForSEII25264SqliteUT
    {
        private const string _nombreUsuario = "Lucia";
        private const string _apellidosUsuario = "Cebrian Perez";
        private const string _correoElectronico = "lucia.cebrian2@alu.uclm.es";
        private const string _direccionEnvio = "Avenida Inventada 4";
        private const string _numTelefono = "012345678";
        
        private const string _herr1nombre = "Destornillador";
        private const string _herr1fabricante = "Bosch";
        private const string _herr2nombre = "Martillo";
        private const string _herr2fabricante = "Wurt";

        public PostReparaciones_test()
        {
            var fabricantes = new List<Fabricante>() {
                new Fabricante(_herr1fabricante),
                new Fabricante(_herr2fabricante),
            };

            var herramientas = new List<Herramienta>(){
                new Herramienta("Acero", 6.50m, _herr1nombre, 2, fabricantes[0], fabricantes[0].Id),
                new Herramienta("Madera", 10.05m, _herr2nombre, 4, fabricantes[1], fabricantes[1].Id),
            };

            ApplicationUser user = new ApplicationUser(_nombreUsuario, _apellidosUsuario, _direccionEnvio,
                _correoElectronico, _numTelefono, RolUsuario.Cliente, new DateTime(2025, 10, 30));

            var reparacion = new Reparacion(DateTime.Today, DateTime.Today.AddDays(1), 6.50m,
                tiposMetodosPago.TarjetaCredito, new List<ReparacionItem>(), user);
            reparacion.ItemsReparacion.Add(new ReparacionItem(herramientas[0], herramientas[0].Id,
                reparacion, reparacion.Id, 2, "Herramientas rotas", 7.25m));

            _context.ApplicationUsers.Add(user);
            _context.AddRange(fabricantes);
            _context.AddRange(herramientas);
            _context.Add(reparacion);
            _context.SaveChanges();
        }


        public static IEnumerable<object[]> CasosDePruebaPara_CrearReparacion()
        {
            var reparacionNoItem = new ReparacionCreacionDTO(_nombreUsuario, _apellidosUsuario,
                _numTelefono, DateTime.Today.AddDays(1), tiposMetodosPago.TarjetaCredito, new List<ReparacionItemDTO>());

            var reparacionItems = new List<ReparacionItemDTO>() { new ReparacionItemDTO(1, _herr1nombre, 6.50m, 1, 2, "Muy útil") };

            var reparacionEntregaAntesHoy = new ReparacionCreacionDTO(_nombreUsuario, _apellidosUsuario,
                _numTelefono, new DateTime(2025, 10, 01), tiposMetodosPago.TarjetaCredito, reparacionItems);

            var reparacionEntregaSemana = new ReparacionCreacionDTO(_nombreUsuario, _apellidosUsuario,
                _numTelefono, DateTime.Today.AddDays(10), tiposMetodosPago.TarjetaCredito, reparacionItems);

            var reparacionApplicationUser = new ReparacionCreacionDTO("Pepito", "Diaz",
                "987654321", DateTime.Today, tiposMetodosPago.TarjetaCredito, reparacionItems);

            var reparacionHerramNoExiste = new ReparacionCreacionDTO(_nombreUsuario, _apellidosUsuario,
                _numTelefono, DateTime.Today.AddDays(2), tiposMetodosPago.PayPal,
                new List<ReparacionItemDTO>() { new ReparacionItemDTO(11, "Sierra", 12.25m, 3, 1) });


            var allTests = new List<object[]> //entradas para CearReparacion y error esperado
            {
                new object[] { reparacionNoItem, "Error! Debe incluir al menos una herramienta para reparar", },
                new object[] { reparacionEntregaAntesHoy, "Error! La fecha en la que se entrega la herramienta debe ser, como mínimo, hoy", },
                new object[] { reparacionEntregaSemana, "Error! Debes entregar tus herramientas antes de que pase una semana", },
                new object[] { reparacionApplicationUser, "Error! El usuario Pepito Diaz no está registrado", },
                new object[] { reparacionHerramNoExiste, "Error! La herramienta Sierra no existe", },
            };

            return allTests;
        }


        [Theory]
        [Trait("LevelTesting", "Unit Testing")]
        [Trait("Database", "WithoutFixture")]
        [MemberData(nameof(CasosDePruebaPara_CrearReparacion))]
        public async Task CrearReparacion_Error_test(ReparacionCreacionDTO rentalDTO, string expectedError)
        {
            // Arrange
            var mock = new Mock<ILogger<ReparacionesController>>();
            ILogger<ReparacionesController> logger = mock.Object;

            var controller = new ReparacionesController(_context, logger);

            // Act
            var result = await controller.CrearReparacion(rentalDTO);

            //Assert: comprobamos que la respuesta es BadRequest y obtenemos el error devuelto
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var problemDetails = Assert.IsType<ValidationProblemDetails>(badRequestResult.Value);

            var errorActual = problemDetails.Errors.First().Value[0];

            //we check that the expected error message and actual are the same
            Assert.StartsWith(expectedError, errorActual);

        }

        
        [Fact]
        [Trait("LevelTesting", "Unit Testing")]
        [Trait("Database", "WithoutFixture")]
        public async Task CrearReparacion_Exito_test()
        {
            // Arrange
            var mock = new Mock<ILogger<ReparacionesController>>();
            ILogger<ReparacionesController> logger = mock.Object;

            var controller = new ReparacionesController(_context, logger);

            DateTime entrega = DateTime.Today.AddDays(5);
            DateTime recogida = DateTime.Today.AddDays(7);

            var reparacionDTO = new ReparacionCreacionDTO(_nombreUsuario, _apellidosUsuario,
                _numTelefono, entrega, tiposMetodosPago.TarjetaCredito, new List<ReparacionItemDTO>() 
                {new ReparacionItemDTO(1, _herr1nombre, 6.50m, 2, 2, "Tienen arañazos")}
            );

            var expectedReparacionDetalleDTO = new ReparacionDetalleDTO(
                2, _nombreUsuario, _apellidosUsuario, entrega, recogida,
                _numTelefono, new List<ReparacionItemDTO>()
                {new ReparacionItemDTO(1, _herr1nombre, 13.00m, 2, 2, "Tienen arañazos")}
            );

            // Act
            var result = await controller.CrearReparacion(reparacionDTO);

            //Assert: comprobamos que la respuesta es CreatedAtAction
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            var actualReparacionDetalleDTO = Assert.IsType<ReparacionDetalleDTO>(createdResult.Value);

            Assert.Equal(expectedReparacionDetalleDTO, actualReparacionDetalleDTO);
        }

    }
}