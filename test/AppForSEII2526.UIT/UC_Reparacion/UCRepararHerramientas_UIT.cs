using AppForMovies.UIT.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UIT.UC_Reparacion
{
    public class UCRepararHerramientas_UIT : UC_UIT
    {
        private SelectHerramientasParaReparar_PO selectHerramientasParaReparar_PO;
        private const int herrId1 = 1;
        private const string herrNombre1 = "Destornillador";
        private const string herrMaterial1 = "Acero";
        private const string herrFabricante1 = "Wurt";
        private const string herrPrecio1 = "12.5";
        private const string herrTiempoRep1 = "1";

        public UCRepararHerramientas_UIT(ITestOutputHelper output) : base(output)
        {
            selectHerramientasParaReparar_PO = new SelectHerramientasParaReparar_PO(_driver, _output);
        }

        private void InitialStepsParaRepararHerramientas()
        {
            //Esperamos a que la opción del menú sea visible
            selectHerramientasParaReparar_PO.WaitForBeingVisible(By.Id("CreateReparacion"));
            //Hacemos clic en la opción del menú para hacer una reparación
            _driver.FindElement(By.Id("CreateReparacion")).Click();
        }

        [Fact]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC2_1_2_3_AF0_filtering()
        {
            //Arrange
            InitialStepsParaRepararHerramientas();
            var expectedHerramientas = new List<string[]> { new string[] { herrNombre1, herrMaterial1, herrFabricante1, herrPrecio1, herrTiempoRep1 }, };

            //Act
            selectHerramientasParaReparar_PO.SearchHerramienta("Desto", "");

            //Assert
            Assert.True(selectHerramientasParaReparar_PO.CheckListaHerramientas(expectedHerramientas));

        }

    }
}
