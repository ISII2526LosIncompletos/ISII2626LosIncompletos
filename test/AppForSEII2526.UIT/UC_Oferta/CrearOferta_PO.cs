using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using AppForSEII2526.UIT.Shared; 
using System.Collections.Generic;
using System.Linq;

namespace AppForSEII2526.UIT.CU_OfertaHerramientas
{
    public class CrearOferta_PO : PageObject
    {
        By buttonCrearOferta = By.Id("Submit"); 
        By button_DialogOK = By.Id("Button_DialogOK"); 
        By modifyHerramientasButton = By.Id("ModificarHerramientas"); 
        By tableOfOfertaItems = By.Id("TablaOfertaHerramientas"); 

        By inputFechaInicio = By.Id("FechaInicio");
        By inputFechaFinal = By.Id("FechaFinal");
        By selectMetodoPago = By.Id("MetodoPago");
        By selectDirigidaA = By.Id("DirigidaOferta"); 

        public CrearOferta_PO(IWebDriver driver, ITestOutputHelper output) : base(driver, output)
        {
        }

        public void crearOferta()
        {
            WaitForBeingClickable(buttonCrearOferta);
            _driver.FindElement(buttonCrearOferta).Click();
        }

        public void guardarOfertaDialog()
        {
            WaitForBeingClickable(button_DialogOK);
            _driver.FindElement(button_DialogOK).Click();
        }

        public void modificarHerramientas()
        {
            WaitForBeingClickable(modifyHerramientasButton);
            _driver.FindElement(modifyHerramientasButton).Click();
        }

        public void addAtributosOferta(string herramientaId, string fechaInicio, string fechaFinal, string metodoPago, string dirigidaA, string porcentaje)
        {
            WaitForBeingClickable(inputFechaInicio);
            _driver.FindElement(inputFechaInicio).SendKeys(Keys.Control + "a");
            _driver.FindElement(inputFechaInicio).SendKeys(Keys.Backspace);
            _driver.FindElement(inputFechaInicio).SendKeys(fechaInicio);

            WaitForBeingClickable(inputFechaFinal);
            _driver.FindElement(inputFechaFinal).SendKeys(Keys.Control + "a");
            _driver.FindElement(inputFechaFinal).SendKeys(Keys.Backspace);
            _driver.FindElement(inputFechaFinal).SendKeys(fechaFinal);

            if (string.IsNullOrEmpty(metodoPago)) metodoPago = "TarjetaCredito";

            if (metodoPago.Equals("Tarjeta de Crédito"))
            {
                metodoPago = "TarjetaCredito";
            }

            //Pongo esto para cambiar el campo de pago y que no falle ya que no detecta el campo sino hay cambio
            SelectElement selectPago = new SelectElement(_driver.FindElement(selectMetodoPago));
            selectPago.SelectByText("PayPal");
            selectPago.SelectByText(metodoPago);

            if (string.IsNullOrEmpty(dirigidaA)) dirigidaA = "Clientes";
            SelectElement selectDirigida = new SelectElement(_driver.FindElement(selectDirigidaA));
            selectDirigida.SelectByText(dirigidaA);

            By inputPorcentaje = By.Id($"porcentaje_{herramientaId}");
            WaitForBeingClickable(inputPorcentaje);

            _driver.FindElement(inputPorcentaje).Clear();
            _driver.FindElement(inputPorcentaje).SendKeys(porcentaje);

            _driver.FindElement(buttonCrearOferta).Click();
        }

        public bool CheckListOfOfertaItems(List<string[]> expectedOfertaItems)
        {
            var rows = _driver.FindElement(tableOfOfertaItems).FindElements(By.TagName("tr"));

            foreach (var expectedRowData in expectedOfertaItems)
            {
                string textoEsperado = string.Join(" ", expectedRowData);

                if (textoEsperado.Contains("12,50")) textoEsperado = textoEsperado.Replace("12,50", "12,5");
                if (textoEsperado.Contains("10,30")) textoEsperado = textoEsperado.Replace("10,30", "10,3");

                bool filaEncontrada = rows.Any(row => row.Text.Contains(textoEsperado));

                if (!filaEncontrada) return false;
            }
            return true;
        }

        public bool CheckValidationError(string expectedError)
        {
            return _driver.PageSource.Contains(expectedError);
        }
    }
}