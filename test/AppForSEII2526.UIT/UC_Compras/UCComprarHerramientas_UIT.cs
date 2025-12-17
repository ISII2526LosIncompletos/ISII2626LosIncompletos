using AppForSEII2526.UIT.Shared;
using AppForSEII2526.UIT.UC_Reparacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UIT.UC_Compras
{
    public class UCComprarHerramientas_UIT: UC_UIT
    {

        private SelectHerramientasParaComprar_PO selectHerramientasParaComprar_PO;
        private CrearCompra_PO crearHerramientasParaComprar_PO;
        private DetallesCompra_PO detalleHerramientasParaComprar_PO;
        private const string herrNombre1 = "Destornillador";
        private const string herrMaterial1 = "Acero";
        private const string herrFabricante1 = "Wurt";
        private const string herrPrecio1 = "12.5";
        private const string herrTiempoRep1 = "1";
        private const string herrNombre2 = "Llave Inglesa";
        private const string herrMaterial2 = "Acero";
        private const string herrFabricante2 = "Phillips";
        private const string herrPrecio2 = "10.3";
        private const string herrTiempoRep2 = "2";

        private const string nombreC = "Miguel";
        private const string apellidoC = "Ruiz";
        private const string direccionEnvio = "C Calle 11";
        private const string metodoPago = "Tarjeta de crédito";
        private const string numTelefono = "123456789";
        private const string email = "miguel.ruiz@example.com";

        public UCComprarHerramientas_UIT(ITestOutputHelper output) : base(output)
        {
            selectHerramientasParaComprar_PO = new SelectHerramientasParaComprar_PO(_driver, _output);
            crearHerramientasParaComprar_PO = new CrearCompra_PO(_driver, _output);
            detalleHerramientasParaComprar_PO = new DetallesCompra_PO(_driver, _output);
        }
        private void InitialStepsParaComprarHerramientas()
        {
            Initial_step_opening_the_web_page();
            //Esperamos a que la opción del menú esté visible
            selectHerramientasParaComprar_PO.WaitForBeingVisibleIgnoringExeptionTypes(By.Id("CreateCompra"));
            selectHerramientasParaComprar_PO.WaitForBeingVisible(By.Id("CreateCompra"));
            Thread.Sleep(500);

            //Damos click en el menú
            _driver.FindElement(By.Id("CreateCompra")).Click();
        }
        [Fact]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC1_5_AF1_filtroPrecio()
        {
            //Arrange
            InitialStepsParaComprarHerramientas();//Muy importante
            var expectedHerramientas = new List<string[]> { new string[] { herrMaterial1, herrPrecio1, herrNombre1, herrTiempoRep1, herrFabricante1 },
            };
            

            //Act
            selectHerramientasParaComprar_PO.BuscarHerramientas("12,5", "");

            //Assert
            Assert.True(selectHerramientasParaComprar_PO.CheckListaDeHerramientas(expectedHerramientas));

        }

        [Fact]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC1_6_AF1_filtroMaterial()
        {
            //Arrange
            InitialStepsParaComprarHerramientas();//Muy importante
            var expectedHerramientas = new List<string[]> { new string[] { herrMaterial1, herrPrecio1, herrNombre1, herrTiempoRep1, herrFabricante1 },
                new string[]{ herrMaterial2, herrPrecio2, herrNombre2, herrTiempoRep2, herrFabricante2 }
            };

            //Act
            selectHerramientasParaComprar_PO.BuscarHerramientas("", "Ace");

            //Assert
            Assert.True(selectHerramientasParaComprar_PO.CheckListaDeHerramientas(expectedHerramientas));

        }

        [Fact]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC1_8_AF3_carritoVacio()
        {
            //Arrange
            InitialStepsParaComprarHerramientas();
            selectHerramientasParaComprar_PO.BuscarHerramientas("", "");
            Thread.Sleep(500);
            //Act
            //No se añade ninguna herramienta

            //Assert
            Assert.True(selectHerramientasParaComprar_PO.BotonComprarHerramientasOculto());
        }

        [Fact]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC1_7_AF2_modificarHerramientas()
        {
            //Arrange
            InitialStepsParaComprarHerramientas();
            selectHerramientasParaComprar_PO.BuscarHerramientas("", "");
            Thread.Sleep(500);

            //Act
            
            //Añadimos una herramienta al carrito de compras
            selectHerramientasParaComprar_PO.AddHerramienta(herrNombre1);
            selectHerramientasParaComprar_PO.AddHerramienta(herrNombre2);
            Thread.Sleep(500);
            selectHerramientasParaComprar_PO.PulsarComprarHerramienta();
                        Thread.Sleep(500);
            //Ahora estamos en la página de crear compra, donde podemos modificar el carrito
            crearHerramientasParaComprar_PO.modificarCarrito();
            //Ahora quitamos esa herramienta
            selectHerramientasParaComprar_PO.RemoveHerramienta(herrNombre1);
            Thread.Sleep(500);
            selectHerramientasParaComprar_PO.PulsarComprarHerramienta();
            Thread.Sleep(500);

            var expectedHerramientas = new List<string[]> { new string[] { herrNombre2, herrMaterial2 },
            };
            Thread.Sleep(500);




            //Assert
            Assert.True(crearHerramientasParaComprar_PO.checkListaHerramientasItems(expectedHerramientas));
        }
        //Post
        [Theory]
        [InlineData("", "Ruiz", "C Calle 11", "Tarjeta de crédito", "123456789", "miguel.ruiz@example.com", "NombreCliente")]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC1_9_AF4_datosObligatoriosnorellenos(string nombreC, string apellidoC, string DireccionEnvio, string metodoPago, string telefono, string email, string expectedError )
        {
            //Arrange
            InitialStepsParaComprarHerramientas();
            selectHerramientasParaComprar_PO.BuscarHerramientas("", "");
            Thread.Sleep(500);
            //Añadimos una herramienta al carrito de compras
            selectHerramientasParaComprar_PO.AddHerramienta(herrNombre1);
            Thread.Sleep(500);
            selectHerramientasParaComprar_PO.PulsarComprarHerramienta();
            Thread.Sleep(500);

            //Act
            crearHerramientasParaComprar_PO.RellenarFormularioCompra(nombreC, apellidoC, DireccionEnvio, metodoPago, telefono, email);
            Thread.Sleep(500);
            crearHerramientasParaComprar_PO.SubmitCompraClick();
            Thread.Sleep(500);

            //Assert
            Assert.True(crearHerramientasParaComprar_PO.CompararMensajeError(expectedError));

        }

        [Fact]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC1_10_AF5_CantidadErronea()
        {
            //Arrange
            InitialStepsParaComprarHerramientas();
            selectHerramientasParaComprar_PO.BuscarHerramientas("", "");
            Thread.Sleep(500);

            //Act
            //Añadimos una herramienta al carrito de compras
            selectHerramientasParaComprar_PO.AddHerramienta(herrNombre1);
            Thread.Sleep(500);
            selectHerramientasParaComprar_PO.PulsarComprarHerramienta();
            Thread.Sleep(500);
            //Dentro de crear compra, rellenamos los datos del cliente
            crearHerramientasParaComprar_PO.RellenarFormularioCompra("Miguel", "Ruiz", "C Calle 11", "Tarjeta de crédito", "123456789", "miguel.ruiz@example.com");
            Thread.Sleep(500);
            //Rellenamos la descripción de la herramienta
            crearHerramientasParaComprar_PO.rellenarDescripcionHerramienta("Para arreglar cosas", herrNombre1);
            Thread.Sleep(500);
            //Rellenamos una cantidad errónea
            crearHerramientasParaComprar_PO.rellenarCantidad(0, herrNombre1);
            Thread.Sleep(500);
            crearHerramientasParaComprar_PO.SubmitCompraClick();
            Thread.Sleep(500);
            crearHerramientasParaComprar_PO.ConfirmarCompra();
                        Thread.Sleep(500);

            //Assert
            Assert.True(crearHerramientasParaComprar_PO.CompararMensajeError("Cantidad"));

        }

        


    }
}
