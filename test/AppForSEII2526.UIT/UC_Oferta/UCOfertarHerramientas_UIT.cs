using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using OpenQA.Selenium;
using Xunit.Abstractions;
using AppForSEII2526.UIT.Shared;
using AppForSEII2526.UIT.CU_Ofertas;

namespace AppForSEII2526.UIT.CU_Ofertas
{
    public class UCOfertarHerramientas_UIT : UC_UIT
    {
        private SelectHerramientasParaOfertar_PO selectHerramientasParaOfertar_PO;

        private const string herrId1 = "1";
        private const string herrNombre1 = "Destornillador";
        private const string herrMaterial1 = "Acero";
        private const string herrFabricante1 = "Wurt";
        private const string herrPrecio1 = "12,50";

        private const string herrId2 = "2";
        private const string herrNombre2 = "Llave Inglesa";
        private const string herrMaterial2 = "Acero";
        private const string herrFabricante2 = "Phillips";
        private const string herrPrecio2 = "10,30";

        public UCOfertarHerramientas_UIT(ITestOutputHelper output) : base(output)
        {
            selectHerramientasParaOfertar_PO = new SelectHerramientasParaOfertar_PO(_driver, _output);
        }

        private void InitialStepsParaOfertarHerramientas()
        {
            Initial_step_opening_the_web_page();

            By menuLink = By.Id("SelectHerramientasParaOfertar");

            selectHerramientasParaOfertar_PO.WaitForBeingVisible(menuLink);
            Thread.Sleep(500);
            _driver.FindElement(menuLink).Click();
        }

        [Theory]
        [InlineData(herrNombre1, herrMaterial1, herrFabricante1, herrPrecio1, "Wurt", "")]
        [InlineData(herrNombre1, herrMaterial1, herrFabricante1, herrPrecio1, "", "12,50")]
        [InlineData(herrNombre2, herrMaterial2, herrFabricante2, herrPrecio2, "Phillips", "10,30")]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC3_FiltrarHerramientas_PorFabricanteYPrecio(string herrNombre, string herrMaterial, string herrFabricante,
            string herrPrecio, string filtroFabricante, string filtroPrecio)
        {
            InitialStepsParaOfertarHerramientas();

            var expectedHerramientas = new List<string[]>
            {
                new string[] { herrNombre, herrFabricante, herrMaterial, herrPrecio }
            };

            selectHerramientasParaOfertar_PO.SearchHerramientas(filtroFabricante, filtroPrecio);
            Thread.Sleep(500);

            Assert.True(selectHerramientasParaOfertar_PO.CheckListOfHerramientas(expectedHerramientas));
        }

        [Fact]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC3_CarritoOfertas_BotonOcultoSiVacio()
        {
            InitialStepsParaOfertarHerramientas();
            selectHerramientasParaOfertar_PO.SearchHerramientas("", "");
            Thread.Sleep(500);

            Assert.True(selectHerramientasParaOfertar_PO.OfertaNotAvailable());
        }

        [Fact]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC3_CarritoOfertas_AñadirYBorrar()
        {
            InitialStepsParaOfertarHerramientas();
            selectHerramientasParaOfertar_PO.SearchHerramientas("Wurt", "");
            Thread.Sleep(500);

            selectHerramientasParaOfertar_PO.AddHerramientaToOfertaCart(herrId1);
            Thread.Sleep(500);

            selectHerramientasParaOfertar_PO.RemoveHerramientaFromOfertaCart(herrId1);
            Thread.Sleep(500);

            Assert.True(selectHerramientasParaOfertar_PO.OfertaNotAvailable());
        }
    }
}