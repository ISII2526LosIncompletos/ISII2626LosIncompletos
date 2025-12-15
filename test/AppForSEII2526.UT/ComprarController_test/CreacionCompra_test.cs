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
    public class CreacionCompra_test: AppForSEII25264SqliteUT
    {
        private const string _NombreUsuario = "Miguel";
        private const string _ApellidoUsuario = "Lopez Parra";
        private const string _CorreoElectronico = "Miguel.Lopez28@alu.uclm";
        private const string _DireccionEnvio = "Avda. España 5";
        private const string _NumTelefono = "123456789";

        private const string _HerrNombre1 = "Tenaza";
        private const string _Herrfabricante1 = "KniPex";
        private const string _HerrNombre2 = "Destornillador";
        private const string _Herrfabricante2 = "Stanley";
        private const string _HerrNombre3 = "Taladro";
        private const string _Herrfabricante3 = "ParkSide";

        public CreacionCompra_test()
        {
            var fabricantes = new List<Fabricante>
            {
                new Fabricante(_Herrfabricante1),
                new Fabricante(_Herrfabricante2),
                new Fabricante(_Herrfabricante3)

            };
            var herramientas = new List<Herramienta>
            {
                new Herramienta ("Acero", 26.41m, _HerrNombre1, 2,fabricantes[0],1),
                new Herramienta ("Acero", 18.75m, _HerrNombre2, 4, fabricantes[1],2),
                new Herramienta ( "Plástico y Metal", 34.90m, _HerrNombre3, 6, fabricantes[2],3)
            };
            ApplicationUser usuario = new ApplicationUser(_NombreUsuario, _ApellidoUsuario, _DireccionEnvio, _CorreoElectronico, _NumTelefono, RolUsuario.Cliente, new DateTime(2025, 11, 3));
            var compra = new Compra(DateTime.Today, 33.33m, tiposMetodosPago.TarjetaCredito, new List<CompraItem>(), usuario);
            compra.CompraItems.Add(new CompraItem(herramientas[0], herramientas[0].Id, compra, compra.Id, 2, "Herramientas Compradas", 33.33m));

            _context.AddRange(fabricantes);
            _context.AddRange(herramientas);
            _context.Add(usuario);
            _context.Add(compra);
            _context.SaveChanges();
        }

        public static IEnumerable<object[]> CasosDePruebaPara_CreacionCompra_test()
        {
            var compraSinHerramientas = new CompraCreacionDTO(_NombreUsuario, _ApellidoUsuario, _DireccionEnvio, tiposMetodosPago.TarjetaCredito,
                _NumTelefono, _CorreoElectronico, DateTime.Today, new List<CompraItemDTO>());
            var compraSinNombre = new CompraCreacionDTO("", _ApellidoUsuario, _DireccionEnvio, tiposMetodosPago.TarjetaCredito,
                _NumTelefono, _CorreoElectronico, DateTime.Today, new List<CompraItemDTO>());
            compraSinNombre.CompraItems.Add(new CompraItemDTO(2, "Destornillador", "Acero", 18.75m, 2, "Descripcion Ejemplo"));

            var compraSinApellido = new CompraCreacionDTO(_NombreUsuario, "", _DireccionEnvio, tiposMetodosPago.TarjetaCredito,
                _NumTelefono, _CorreoElectronico, DateTime.Today, new List<CompraItemDTO>());
            compraSinApellido.CompraItems.Add(new CompraItemDTO(2, "Destornillador", "Acero", 18.75m, 2, "Descripcion Ejemplo"));

            var compraSinDireccion = new CompraCreacionDTO(_NombreUsuario, _ApellidoUsuario, "", tiposMetodosPago.TarjetaCredito,
                _NumTelefono, _CorreoElectronico, DateTime.Today, new List<CompraItemDTO>());
            compraSinDireccion.CompraItems.Add(new CompraItemDTO(2, "Destornillador", "Acero", 18.75m, 2, "Descripcion Ejemplo"));
            
            var compraUsuarioNoRegistrado = new CompraCreacionDTO("Pedro", "Porro", "Calle Falsa 22", tiposMetodosPago.TarjetaCredito,
                "135792468", "Pedro.Porro22@alu.uclm.es", DateTime.Today, new List<CompraItemDTO>());
            compraUsuarioNoRegistrado.CompraItems.Add(new CompraItemDTO(3, "Taladro", "Plastico y Metal", 34.90m, 2, "Descripcion Ejemplo"));

            var compraSinDescripcion = new CompraCreacionDTO(_NombreUsuario, _ApellidoUsuario, _DireccionEnvio, tiposMetodosPago.TarjetaCredito,
                _NumTelefono, _CorreoElectronico, DateTime.Today, new List<CompraItemDTO>());
            compraSinDescripcion.CompraItems.Add(new CompraItemDTO(2, "Destornillador", "Acero", 18.75m, 2, ""));


            var compraCantidad = new CompraCreacionDTO(_NombreUsuario, _ApellidoUsuario, _DireccionEnvio, tiposMetodosPago.TarjetaCredito,
                _NumTelefono, _CorreoElectronico, DateTime.Today, new List<CompraItemDTO>());
            compraCantidad.CompraItems.Add(new CompraItemDTO(2, "Destornillador", "Acero", 18.75m, 0, "Descripcion Ejemplo"));

            var compraErronea = new CompraCreacionDTO(_NombreUsuario, _ApellidoUsuario, _DireccionEnvio, tiposMetodosPago.TarjetaCredito,
                _NumTelefono, _CorreoElectronico, DateTime.Today, new List<CompraItemDTO>());
            compraErronea.CompraItems.Add(new CompraItemDTO(5, "Alicates", "Acero", 9.65m, 1, "Alicates para moldear"));

            var compraPagoEfectivo = new CompraCreacionDTO(_NombreUsuario, _ApellidoUsuario, _DireccionEnvio, tiposMetodosPago.Efectivo,
                _NumTelefono, _CorreoElectronico, DateTime.Today, new List<CompraItemDTO>());
            compraPagoEfectivo.CompraItems.Add(new CompraItemDTO(1,"Tenaza", "Acero", 26.41m, 1, "Descripcion Ejemplo"));


            var allTest = new List<object[]> //Entradas para CreacionCompra y el mensaje de error esperado
            {
                new object[] {compraSinHerramientas,"La compra debe contener al menos un item." },
                new object[] {compraSinNombre, "El nombre no puede estar vacio"},
                new object[] {compraSinApellido, "El apellido no puede estar vacio"},
                new object[] {compraSinDireccion, "La direccion de envio no puede estar vacio" },
                new object[] {compraUsuarioNoRegistrado, "El usuario no existe." },
                new object[] {compraSinDescripcion, "La descripcion no puede estar vacia"},
                new object[] {compraCantidad, "La cantidad debe ser mayor que cero."},
                new object[] {compraErronea, $"La herramienta '{compraErronea.CompraItems[0].Nombre}' no existe." }
            };

            return allTest;
        }

        [Theory]
        [MemberData(nameof(CasosDePruebaPara_CreacionCompra_test))]
        [Trait("Database", "WithoutFisture")]
        [Trait("LevelTesting", "Unit Testing")]
        public async Task CreacionCompra_Error_test(CompraCreacionDTO compraDTO, string errorExpected)
        {
            //Arrange(saca todas las variables que necesitamos)
            var mock = new Mock<ILogger<ComprarController>>();
            ILogger<ComprarController> logger = mock.Object;
            var controller = new ComprarController(_context, logger);

            //Act
            var result = await controller.CreateCompra(compraDTO);

            //Assert: Comprueba que la respuesta es de tipo badRequest, y obtiene el mensaje esperado
            var Badrequestresult = Assert.IsType<BadRequestObjectResult>(result);
            var problemDetails = Assert.IsType<ValidationProblemDetails>(Badrequestresult.Value);
            var errorActual = problemDetails.Errors.Values.First()[0];
            Assert.Equal(errorExpected, errorActual);

        }

        [Fact]
        [Trait("LevelTesting", "Unit Testing")]
        [Trait("Database", "WithoutFixture")]
        public async Task CreacionCompra_OK_test()
        {
            //Arrange
            var mock = new Mock<ILogger<ComprarController>>();
            ILogger<ComprarController> logger = mock.Object;

            var controller = new ComprarController(_context, logger);

            var compraDTO = new CompraCreacionDTO(_NombreUsuario, _ApellidoUsuario, _DireccionEnvio, tiposMetodosPago.TarjetaCredito,
                _NumTelefono, _CorreoElectronico, DateTime.Today, new List<CompraItemDTO>());
            compraDTO.CompraItems.Add(new CompraItemDTO(2, _HerrNombre2, "Acero", 18.75m, 2, "Destornillador de calidad"));

            var expectedCompra = new CompraDetalleDTO(1, _NombreUsuario, _ApellidoUsuario, _DireccionEnvio, compraDTO.PrecioTotal, compraDTO.FechaCompra, new List<CompraItemDTO>());
            expectedCompra.CompraItems.Add(new CompraItemDTO(2, _HerrNombre2, "Acero", 18.75m, 2, "Destornillador de calidad"));
            //Act
            var result = await controller.CreateCompra(compraDTO);


            //Assert: vemos que la respuesta es CreatedAtAction
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            var compraDTOActual = Assert.IsType<CompraDetalleDTO>(createdAtActionResult.Value);
            expectedCompra.CompraId= compraDTOActual.CompraId;
            Assert.Equal(expectedCompra, compraDTOActual);

        }
    }
}
