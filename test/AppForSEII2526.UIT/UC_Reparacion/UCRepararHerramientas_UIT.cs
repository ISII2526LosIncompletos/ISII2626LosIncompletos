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

        private const string herrNombre1 = "Destornillador";
        private const string herrMaterial1 = "Acero";
        private const string herrFabricante1 = "Wurt";
        private const string herrPrecio1 = "12,5";
        private const string herrTiempoRep1 = "1";

        private const string herrNombre2 = "Llave Inglesa";
        private const string herrMaterial2 = "Acero";
        private const string herrFabricante2 = "Phillips";
        private const string herrPrecio2 = "10,3";
        private const string herrTiempoRep2 = "2";

        public UCRepararHerramientas_UIT(ITestOutputHelper output) : base(output)
        {
            selectHerramientasParaReparar_PO = new SelectHerramientasParaReparar_PO(_driver, _output);
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
            InitialStepsParaRepararHerramientas();
            var expectedHerramientas = new List<string[]> { new string[] { herrNombre, herrMaterial, herrFabricante, herrPrecio, herrTiempoRep }, };

            //Act
            selectHerramientasParaReparar_PO.SearchHerramienta(filtroNombre, filtroTiempoRep);
            Thread.Sleep(500); //Esperamos a que aparexca la tabla

            //Assert
            Assert.True(selectHerramientasParaReparar_PO.CheckListaHerramientas(expectedHerramientas));

        }

    }
}
