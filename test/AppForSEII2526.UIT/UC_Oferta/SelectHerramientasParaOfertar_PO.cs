using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Xunit.Abstractions;
using AppForSEII2526.UIT.Shared;

namespace AppForSEII2526.UIT.CU_Ofertas
{
    public class SelectHerramientasParaOfertar_PO : PageObject
    {

        private By inputFabricante = By.Id("inputGenre");

        private By inputPrecio = By.Id("inputTitle");

        private By buttonSearchHerramientas = By.Id("buscarHerramientas");

        private By tableOfHerramientasBy = By.Id("Tabla de herramientas");

        private By errorShownBy = By.Id("ErrorsShown");

        private By buttonCrearOfertaCarrito = By.Id("purchaseMovieButton");

        private By botonCrearOfertaCarrito = By.Id("purchaseMovieButton");

        public SelectHerramientasParaOfertar_PO(IWebDriver driver, ITestOutputHelper output) : base(driver, output)
        {
        }

        public void SearchHerramientas(string fabricante, string precio)
        {
            WaitForBeingClickable(inputPrecio);

            var precioBox = _driver.FindElement(inputPrecio);
            precioBox.Clear();
            precioBox.SendKeys(precio);

            var fabricanteBox = _driver.FindElement(inputFabricante);
            fabricanteBox.Clear();
            if (fabricante != "All" && !string.IsNullOrEmpty(fabricante))
            {
                fabricanteBox.SendKeys(fabricante);
            }

            _driver.FindElement(buttonSearchHerramientas).Click();
        }

        public void CrearOfertaCarrito()
        {
            WaitForBeingClickable(buttonCrearOfertaCarrito);
            _driver.FindElement(buttonCrearOfertaCarrito).Click();
        }

        public bool CheckListOfHerramientas(List<string[]> expectedHerramientas)
        {
            var rows = _driver.FindElement(tableOfHerramientasBy).FindElements(By.TagName("tr"));

            foreach (var expectedRowData in expectedHerramientas)
            {
                string textoEsperado = string.Join(" ", expectedRowData);

                {
                    textoEsperado = textoEsperado.Replace("12,50", "12,5");
                }

                if (textoEsperado.Contains("10,30"))
                {
                    textoEsperado = textoEsperado.Replace("10,30", "10,3");
                }

                bool filaEncontrada = rows.Any(row => row.Text.Contains(textoEsperado));

                if (!filaEncontrada)
                {
                    return false;
                }
            }
            return true;
        }

        public bool CheckMessageError(string errorMessage)
        {
            WaitForBeingVisible(errorShownBy);
            IWebElement actualErrorShown = _driver.FindElement(errorShownBy);
            _output.WriteLine($"actual Message shown:{actualErrorShown.Text}");
            return actualErrorShown.Text.Contains(errorMessage);
        }

        public void AddHerramientaToOfertaCart(string herramientaId)
        {
            By addBtn = By.Id("movieToRent_" + herramientaId);
            WaitForBeingClickable(addBtn);
            _driver.FindElement(addBtn).Click();
        }

        public void RemoveHerramientaFromOfertaCart(string herramientaId)
        {
            By removeBtn = By.Id("removeMovie_" + herramientaId);
            WaitForBeingClickable(removeBtn);
            _driver.FindElement(removeBtn).Click();
        }

        public void crearOfertaCarrito()
        {
            WaitForBeingClickable(botonCrearOfertaCarrito);
            _driver.FindElement(botonCrearOfertaCarrito).Click();
        }

        public bool OfertaNotAvailable()
        {
            try
            {
                var element = _driver.FindElement(buttonCrearOfertaCarrito);
                return !element.Displayed;
            }
            catch (NoSuchElementException)
            {
                return true;
            }
        }
    }
}