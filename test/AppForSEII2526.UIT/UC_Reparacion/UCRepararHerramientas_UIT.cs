using Microsoft.VisualStudio.TestPlatform.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppForSEII2526.UIT.Shared;
using System.Runtime.CompilerServices;

namespace AppForSEII2526.UIT.UC_Reparacion
{
    public class UCRepararHerramientas_UIT : UC_UIT
    {
        private SelectHerramientasParaReparar_PO selectHerramientasParaReparar_PO;
        private CrearReparacion_PO crearReparacion_PO;
        private DetalleReparacion_PO detalleReparacion_PO;

        private const string herrNombre1 = "Destornillador";
        private const string herrMaterial1 = "Acero";
        private const string herrFabricante1 = "Wurt";
        private const string herrPrecio1 = "12,5";
        private const string herrTiempoRep1 = "1";
        private const string herrCantidad1 = "1";
        private const string herrDescripcion1 = "Se ha partido";

        private const string herrNombre2 = "Llave Inglesa";
        private const string herrMaterial2 = "Acero";
        private const string herrFabricante2 = "Phillips";
        private const string herrPrecio2 = "10,3";
        private const string herrTiempoRep2 = "2";

        private const string nombreC = "Lucia";
        private const string apellidoC = "Martinez";

        private const string herrCantidadMal = "0";
        private const string nombreMal = "John";
        private const string apellidoMal = "Doe";

        public UCRepararHerramientas_UIT(ITestOutputHelper output) : base(output)
        {
            selectHerramientasParaReparar_PO = new SelectHerramientasParaReparar_PO(_driver, _output);
            crearReparacion_PO = new CrearReparacion_PO(_driver, _output);
            detalleReparacion_PO = new DetalleReparacion_PO(_driver, _output);
        }

        private void InitialStepsParaRepararHerramientas()
        {
            //Buscamos la página de inicio de nuestra página (esta función está en UC_UIT)
            Initial_step_opening_the_web_page();
            //Esperamos a que la opción del menú sea visible y clicable
            selectHerramientasParaReparar_PO.WaitForBeingVisible(By.Id("CreateReparacion"));
            //Esperamos un poco para asegurarnos de que la página ha cargado completamente
            Thread.Sleep(500);
            //Hacemos clic en la opción del menú para hacer una reparación
            _driver.FindElement(By.Id("CreateReparacion")).Click();
        }

        [Theory]
        [InlineData(herrNombre1, herrMaterial1, herrFabricante1, herrPrecio1, herrTiempoRep1, "Desto", "")]
        [InlineData(herrNombre2, herrMaterial2, herrFabricante2, herrPrecio2, herrTiempoRep2, "", "2")]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC2_1_2_3_AF0_filteringNombreYTiempo(string herrNombre, string herrMaterial, string herrFabricante, 
            string herrPrecio, string herrTiempoRep, string filtroNombre, string filtroTiempoRep)
        {
            //Arrange
            InitialStepsParaRepararHerramientas(); //MUY IMPORTANTE
            var expectedHerramientas = new List<string[]> { new string[] { herrNombre, herrMaterial, herrFabricante, herrPrecio, herrTiempoRep }, };

            //Act
            selectHerramientasParaReparar_PO.SearchHerramienta(filtroNombre, filtroTiempoRep);
            Thread.Sleep(500); //Esperamos a que aparexca la tabla

            //Assert
            Assert.True(selectHerramientasParaReparar_PO.CheckListaHerramientas(expectedHerramientas));

        }

        [Fact]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC2_4_AF3_carritoVacio()
        {
            //Arrange
            InitialStepsParaRepararHerramientas();
            selectHerramientasParaReparar_PO.SearchHerramienta("", "");
            Thread.Sleep(500);

            //Act: No añadimos ninguna herramienta al carrito de reparación

            //Assert
            Assert.True(selectHerramientasParaReparar_PO.BotonRepararHerramientasOculto());
        }

        [Fact]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC2_5_AF2_modificarBorrando()
        {
            //Arrange
            InitialStepsParaRepararHerramientas();
            selectHerramientasParaReparar_PO.SearchHerramienta("", "");
            Thread.Sleep(500);

            //Act
            //Añadimos una herramienta al carrito de reparación
            selectHerramientasParaReparar_PO.AddHerramienta(herrNombre1);
            Thread.Sleep(500);
            //Ahora quitamos esa herramienta
            selectHerramientasParaReparar_PO.RemoveHerramienta(herrNombre1);
            Thread.Sleep(500);

            //Assert
            Assert.True(selectHerramientasParaReparar_PO.BotonRepararHerramientasOculto());
        }

        [Fact]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC2_5_AF1_fechaAnteriorHoy()
        {
            //Arrange
            InitialStepsParaRepararHerramientas();
            selectHerramientasParaReparar_PO.SearchHerramienta("", ""); //Seleccionamos
            Thread.Sleep(500);
            selectHerramientasParaReparar_PO.AddHerramienta(herrNombre1); //Añadimos
            Thread.Sleep(500);
            selectHerramientasParaReparar_PO.RepararHerrBotonClick(); //Pasamos al post
            Thread.Sleep(500);

            //Act
            DateTime fechaAntes = DateTime.Today.AddDays(-1); //Fecha anterior a hoy
            crearReparacion_PO.RellenarFormulario(nombreC, apellidoC, fechaAntes);
            Thread.Sleep(500);
            crearReparacion_PO.SubmitReparacionClick();
            Thread.Sleep(500);
            crearReparacion_PO.ConfirmarReparacion();
            Thread.Sleep(500);

            //Assert
            Assert.True(crearReparacion_PO.CompararMensajeError("(*) Error! La fecha en la que se entrega la herramienta debe ser, como mínimo, hoy"));
        }

        [Fact]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC2_5_AF1_fechaMasSemana()
        {
            //Arrange
            InitialStepsParaRepararHerramientas();
            selectHerramientasParaReparar_PO.SearchHerramienta("", ""); //Seleccionamos
            Thread.Sleep(500);
            selectHerramientasParaReparar_PO.AddHerramienta(herrNombre1); //Añadimos
            Thread.Sleep(500);
            selectHerramientasParaReparar_PO.RepararHerrBotonClick(); //Pasamos al post
            Thread.Sleep(500);

            //Act
            DateTime fechaSemana = DateTime.Today.AddDays(8); //Fecha dentro de más de una semana
            crearReparacion_PO.RellenarFormulario(nombreC, apellidoC, fechaSemana);
            Thread.Sleep(500);
            crearReparacion_PO.SubmitReparacionClick();
            Thread.Sleep(500);
            crearReparacion_PO.ConfirmarReparacion();
            Thread.Sleep(500);

            //Assert
            Assert.True(crearReparacion_PO.CompararMensajeError("(*) Error! Debes entregar tus herramientas antes de que pase una semana"));
        }

        [Theory]
        [InlineData("", apellidoC, "NombreCliente")]
        [InlineData(nombreC, "", "ApellidoCliente")]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC2_6_AF4_datosObligatorios(string nombre, string apellido, string expectedError)
        {
            //Arrange
            InitialStepsParaRepararHerramientas();
            selectHerramientasParaReparar_PO.SearchHerramienta("", ""); //Seleccionamos
            Thread.Sleep(500);
            selectHerramientasParaReparar_PO.AddHerramienta(herrNombre1); //Añadimos
            Thread.Sleep(500);
            selectHerramientasParaReparar_PO.RepararHerrBotonClick(); //Pasamos al post
            Thread.Sleep(500);

            //Act
            crearReparacion_PO.RellenarFormulario(nombre, apellido, DateTime.Today);
            Thread.Sleep(500);
            crearReparacion_PO.SubmitReparacionClick();
            Thread.Sleep(500);
            //No hace falta confirmar la reparación, el mensaje aparece antes
            //crearReparacion_PO.ConfirmarReparacion();
            //Thread.Sleep(500);

            //Assert
            Assert.True(crearReparacion_PO.CompararMensajeError(expectedError));
        }

        [Fact]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC2_6_AF4_usuarioNoRegistrado()
        {
            //Arrange
            InitialStepsParaRepararHerramientas();
            selectHerramientasParaReparar_PO.SearchHerramienta("", ""); //Seleccionamos
            Thread.Sleep(500);
            selectHerramientasParaReparar_PO.AddHerramienta(herrNombre1); //Añadimos
            Thread.Sleep(500);
            selectHerramientasParaReparar_PO.RepararHerrBotonClick(); //Pasamos al post
            Thread.Sleep(500);

            //Act
            crearReparacion_PO.RellenarFormulario(nombreMal, apellidoMal, DateTime.Today);
            Thread.Sleep(500);
            crearReparacion_PO.SubmitReparacionClick();
            Thread.Sleep(500);
            crearReparacion_PO.ConfirmarReparacion();
            Thread.Sleep(500);

            //Assert
            Assert.True(crearReparacion_PO.CompararMensajeError($"(*) Error! El usuario {nombreMal} {apellidoMal} no está registrado"));
        }

        [Fact]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC2_6_AF5_cantidadCero()
        {
            //Arrange
            InitialStepsParaRepararHerramientas();
            selectHerramientasParaReparar_PO.SearchHerramienta("", ""); //Seleccionamos
            Thread.Sleep(500);
            selectHerramientasParaReparar_PO.AddHerramienta(herrNombre1); //Añadimos
            Thread.Sleep(500);
            selectHerramientasParaReparar_PO.RepararHerrBotonClick(); //Pasamos al post
            Thread.Sleep(500);

            //Act
            crearReparacion_PO.RellenarFormulario(nombreC, apellidoC, DateTime.Today);
            Thread.Sleep(500);
            crearReparacion_PO.CambiarCantidadHerramienta(herrNombre1, herrCantidadMal);
            Thread.Sleep(500);
            crearReparacion_PO.SubmitReparacionClick();
            Thread.Sleep(500);
            crearReparacion_PO.ConfirmarReparacion();
            Thread.Sleep(500);

            //Assert
            Assert.True(crearReparacion_PO.CompararMensajeError("(*) Error! Para reparar herramientas la cantidad de ellas debe ser superior a 0"));
        }

        [Fact]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC2_FB_procesoCompleto()
        {
            //Pasos en la pantalla de select
            InitialStepsParaRepararHerramientas();
            selectHerramientasParaReparar_PO.SearchHerramienta("", ""); //Seleccionamos
            Thread.Sleep(500);
            selectHerramientasParaReparar_PO.AddHerramienta(herrNombre1); //Añadimos
            Thread.Sleep(500);
            selectHerramientasParaReparar_PO.RepararHerrBotonClick(); //Pasamos al post
            Thread.Sleep(500);

            //Pasos en la pantalla de post
            crearReparacion_PO.RellenarFormulario(nombreC, apellidoC, DateTime.Today);
            Thread.Sleep(500);
            //Añadimos una descripción, no lo hemos hecho hasta ahora
            crearReparacion_PO.AddDescripcion(herrNombre1, herrDescripcion1);
            Thread.Sleep(500);
            crearReparacion_PO.SubmitReparacionClick();
            Thread.Sleep(500);
            crearReparacion_PO.ConfirmarReparacion();
            Thread.Sleep(500);

            //Pasos en la pantalla de detail
            detalleReparacion_PO.CheckDatosReparacion(nombreC, apellidoC, herrNombre1,
                herrMaterial1, herrFabricante1, herrPrecio1, herrTiempoRep1, herrCantidad1, herrDescripcion1);            
        }

    }
}
