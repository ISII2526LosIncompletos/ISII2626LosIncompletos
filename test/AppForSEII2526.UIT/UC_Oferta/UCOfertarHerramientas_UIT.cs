using System;
using System.Collections.Generic;
using System.Threading;
using Xunit;
using Xunit.Abstractions;
using OpenQA.Selenium;
using AppForSEII2526.UIT.Shared;
using AppForSEII2526.UIT.CU_OfertaHerramientas; 
namespace AppForSEII2526.UIT.CU_Ofertas
{
    public class UCOfertarHerramientas_UIT : UC_UIT
    {
        // POs necesarios
        private SelectHerramientasParaOfertar_PO selectHerramientasParaOfertar_PO;
        private DetailOferta_PO detailOferta_PO;
        private CrearOferta_PO crearOferta_PO;

        // Herramienta 1:
        private const string herrId1 = "1";
        private const string herrNombre1 = "Destornillador";
        private const string herrMaterial1 = "Acero";
        private const string herrFabricante1 = "Wurt"; 
        private const string herrPrecio1 = "12,5"; 

        // Herramienta 2: Llave Inglesa 
        private const string herrId2 = "2";
        private const string herrNombre2 = "Llave Inglesa";
        private const string herrMaterial2 = "Acero";
        private const string herrFabricante2 = "Phillips";
        private const string herrPrecio2 = "10,3";

        // Constantes de prueba
        private const string porcentajeCorrecto = "10";
        private const string porcentajeErroneo1 = "-4";
        private const string porcentajeErroneo2 = "105";
        private const string errorGenericoAPI = "Error de validación (400). Revisa que el porcentaje sea mayor a 0 y las fechas correctas.";

        public UCOfertarHerramientas_UIT(ITestOutputHelper output) : base(output)
        {
            selectHerramientasParaOfertar_PO = new SelectHerramientasParaOfertar_PO(_driver, _output);
            detailOferta_PO = new DetailOferta_PO(_driver, _output);
            crearOferta_PO = new CrearOferta_PO(_driver, _output);
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
        [InlineData(herrId1, herrNombre1)]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC3_1_BF_CrearOferta(string herramientaId, string herramientaNombre)
        {
            InitialStepsParaOfertarHerramientas();
            Thread.Sleep(500);

            selectHerramientasParaOfertar_PO.AddHerramientaToOfertaCart(herramientaId); 
            Thread.Sleep(500);

            selectHerramientasParaOfertar_PO.crearOfertaCarrito();
            Thread.Sleep(1000);

            string fechaInicio = DateTime.Today.AddDays(3).ToString("dd/MM/yyyy");
            string fechaFin = DateTime.Today.AddDays(11).ToString("dd/MM/yyyy");

            crearOferta_PO.addAtributosOferta(herramientaId, fechaInicio, fechaFin, "PayPal", "Socios", porcentajeCorrecto);
            Thread.Sleep(1000);

            crearOferta_PO.guardarOfertaDialog();
            Thread.Sleep(1000);

            Assert.True(detailOferta_PO.CheckOfertaDetail(DateTime.Today.AddDays(3), DateTime.Today.AddDays(11), DateTime.Today, "PayPal", "Socios", 1));
        }

        [Theory]
        [InlineData(herrId1, herrNombre1, herrMaterial1, herrPrecio1, herrFabricante1, "Wurt", "")]
        [InlineData(herrId1, herrNombre1, herrMaterial1, herrPrecio1, herrFabricante1, "", "12,50")]
        [InlineData(herrId2, herrNombre2, herrMaterial2, herrPrecio2, herrFabricante2, "Phillips", "10,30")]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC3_2_3_4_AF0_filtroPorFabricantePrecio(string hId, string hNombre, string hMat, string hPrecio, string hFab,
            string filtroFab, string filtroPrecio)
        {
            InitialStepsParaOfertarHerramientas();

            var expectedHerramientas = new List<string[]>
            {
                new string[] { hNombre, hFab, hMat, hPrecio } 
            };

            selectHerramientasParaOfertar_PO.SearchHerramientas(filtroFab, filtroPrecio);
            Thread.Sleep(500);

            Assert.True(selectHerramientasParaOfertar_PO.CheckListOfHerramientas(expectedHerramientas));
        }

        [Theory]
        [InlineData(herrId1, herrNombre1, "26/12/2025", "14/12/2025", errorGenericoAPI)]
        [InlineData(herrId1, herrNombre1, "09/12/2021", "14/12/2025", errorGenericoAPI)]
        [InlineData(herrId1, herrNombre1, "14/12/2025", "17/12/2025", errorGenericoAPI)]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC3_5_6_7_AF1_FechasErroneas(string herramientaId, string herramientaNombre, string fechaInicio, string fechaFinal, string expectedError)
        {
            InitialStepsParaOfertarHerramientas();
            Thread.Sleep(1000); 

            selectHerramientasParaOfertar_PO.AddHerramientaToOfertaCart(herramientaId);

            Thread.Sleep(500);
            selectHerramientasParaOfertar_PO.crearOfertaCarrito();
            Thread.Sleep(1000);

            crearOferta_PO.addAtributosOferta(herramientaId, fechaInicio, fechaFinal, "Efectivo", "Socios", porcentajeCorrecto);
            Thread.Sleep(1000);

            crearOferta_PO.guardarOfertaDialog();
            Thread.Sleep(2000); 

            Assert.True(crearOferta_PO.CheckValidationError(expectedError),
                $"La web no mostró el mensaje esperado. \nEsperado: '{expectedError}'");
        }

        [Fact]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC3_AF2_ModificarCarrito()
        {
            InitialStepsParaOfertarHerramientas();

            selectHerramientasParaOfertar_PO.AddHerramientaToOfertaCart(herrId1);
            selectHerramientasParaOfertar_PO.AddHerramientaToOfertaCart(herrId2);
            Thread.Sleep(500);

            selectHerramientasParaOfertar_PO.crearOfertaCarrito();
            Thread.Sleep(500);

            crearOferta_PO.modificarHerramientas();
            Thread.Sleep(500);

            selectHerramientasParaOfertar_PO.RemoveHerramientaFromOfertaCart(herrId1);
            Thread.Sleep(500);

            selectHerramientasParaOfertar_PO.crearOfertaCarrito();
            Thread.Sleep(500);

            var expectedOfertaItems = new List<string[]>
            {
                new string[] { herrNombre2, herrMaterial2, herrPrecio2 }
            };

            Assert.True(crearOferta_PO.CheckListOfOfertaItems(expectedOfertaItems));
        }

        [Theory]
        [InlineData(herrId1, herrNombre1, porcentajeErroneo1, errorGenericoAPI)]
        [InlineData(herrId1, herrNombre1, porcentajeErroneo2, errorGenericoAPI)]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC3_8_9_AF3_PorcentajeErroneo(string herramientaId, string herramientaNombre, string porcentaje, string expectedError)
        {
            InitialStepsParaOfertarHerramientas();
            Thread.Sleep(500);

            selectHerramientasParaOfertar_PO.AddHerramientaToOfertaCart(herramientaId); 
            Thread.Sleep(500);

            selectHerramientasParaOfertar_PO.crearOfertaCarrito();
            Thread.Sleep(1000);

            crearOferta_PO.addAtributosOferta(herramientaId, DateTime.Today.AddDays(5).ToString("dd/MM/yyyy"),
                DateTime.Today.AddDays(15).ToString("dd/MM/yyyy"), "Tarjeta de Crédito", "Clientes", porcentaje);
            Thread.Sleep(1000);

            crearOferta_PO.guardarOfertaDialog();
            Thread.Sleep(2000); 

            Assert.True(crearOferta_PO.CheckValidationError(expectedError),
                $"Se esperaba el mensaje: '{expectedError}'");
        }

        [Fact]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC3_10_AF4_CarritoVacio()
        {
            InitialStepsParaOfertarHerramientas();
            selectHerramientasParaOfertar_PO.SearchHerramientas("", ""); 
            Thread.Sleep(500);

            Assert.True(selectHerramientasParaOfertar_PO.OfertaNotAvailable());
        }

        [Theory]
        [InlineData(herrId1, herrNombre1, "20/12/2025", "", "10", "The FechaFinal field must be a date.")]
        [InlineData(herrId1, herrNombre1, "", "30/12/2025", "10", "The FechaInicio field must be a date.")]
        [InlineData(herrId1, herrNombre1, "20/12/2025", "30/12/2025", "", "The Porcentaje field must be a number.")]

        [Trait("LevelTesting", "Funcional Testing")]
        public void UC3_11_12_AF5_CamposObligatorios(string herramientaId, string herramientaNombre, string fechaInicio, string fechaFinal, string porcentaje, string expectedError)
        {
            InitialStepsParaOfertarHerramientas();
            Thread.Sleep(500);

            selectHerramientasParaOfertar_PO.AddHerramientaToOfertaCart(herramientaId);
            Thread.Sleep(500);
            selectHerramientasParaOfertar_PO.crearOfertaCarrito();
            Thread.Sleep(1000);

            crearOferta_PO.addAtributosOferta(herramientaId, fechaInicio, fechaFinal, "Efectivo", "Socios", porcentaje);
            Thread.Sleep(1000);

            bool errorEncontrado = crearOferta_PO.CheckValidationError(expectedError);

            if (!errorEncontrado)
            {
                _output.WriteLine("CONTENIDO DE PÁGINA: " + _driver.PageSource);
            }

            Assert.True(errorEncontrado,
                $"Fallo: Se esperaba el texto '{expectedError}' pero no apareció.");
        }
    }
}