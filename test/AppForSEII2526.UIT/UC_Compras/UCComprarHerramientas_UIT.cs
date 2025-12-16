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

        public UCComprarHerramientas_UIT(ITestOutputHelper output) : base(output)
        {
            selectHerramientasParaComprar_PO = new SelectHerramientasParaComprar_PO(_driver, _output);
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
        public void UC2_1_2_3_AF0_filtroPrecio()
        {
            //Arrange
            InitialStepsParaComprarHerramientas();
            var expectedHerramientas = new List<string[]> { new string[] { herrMaterial1, herrPrecio1, herrNombre1, herrTiempoRep1, herrFabricante1 },
            };
            

            //Act
            selectHerramientasParaComprar_PO.BuscarHerramientas("12,5", "");

            //Assert
            Assert.True(selectHerramientasParaComprar_PO.CheckListaDeHerramientas(expectedHerramientas));

        }

        [Fact]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC2_1_2_3_AF0_filtroMaterial()
        {
            //Arrange
            InitialStepsParaComprarHerramientas();
            var expectedHerramientas = new List<string[]> { new string[] { herrMaterial1, herrPrecio1, herrNombre1, herrTiempoRep1, herrFabricante1 },
                new string[]{ herrMaterial2, herrPrecio2, herrNombre2, herrTiempoRep2, herrFabricante2 }
            };

            //Act
            selectHerramientasParaComprar_PO.BuscarHerramientas("", "Ace");

            //Assert
            Assert.True(selectHerramientasParaComprar_PO.CheckListaDeHerramientas(expectedHerramientas));

        }

    }
}
