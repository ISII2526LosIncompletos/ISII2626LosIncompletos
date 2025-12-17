using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Support.UI;

namespace AppForSEII2526.UIT.UC_Compras
{
    public class SelectHerramientasParaComprar_PO: PageObject
    {
        By inputPrecio = By.Id("inputPrecio");
        By inputMaterial= By.Id("inputMaterial");
        By tableHerramientasBy = By.Id("TablaHerramientas");
        By buttonSearchHerramienta = By.Id("BuscarHerramientas");
        By comprarHerramientaButton = By.Id("comprarHerramientaButton");
        public SelectHerramientasParaComprar_PO(IWebDriver driver, ITestOutputHelper output) : base(driver, output)
        { 
        }
        public void BuscarHerramientas(string precio, string material)
        {
            //Espera a que el elemento sea clickable
            WaitForBeingClickable(inputPrecio);
            _driver.FindElement(inputPrecio).SendKeys(precio);

            WaitForBeingClickable(inputMaterial);
            _driver.FindElement(inputMaterial).SendKeys(material);

            _driver.FindElement(buttonSearchHerramienta).Click();


        }

        public bool CheckListaDeHerramientas(List<string[]> expectedHerramientas)
        {

            return CheckBodyTable(expectedHerramientas, tableHerramientasBy);
        }

        public bool BotonComprarHerramientasOculto()
        {
            try
            {
                //Si el botón está oculto (no visible), devolvemos true
                return !_driver.FindElement(comprarHerramientaButton).Displayed;
            }
            catch(Exception ex)
            {
                //Si hay una excepción es porque el botón está oculto
                return true;
            }
        }
        public void AddHerramienta(string nombre)
        {
            // Esperar a que la tabla esté visible y cargada
            WaitForBeingVisible(tableHerramientasBy);

            // Localizar la fila cuya columna "Nombre" (tercera columna) coincide con el nombre pasado
            string xpath = $"//table[@id='TablaHerramientas']//tr[td[3][normalize-space()='{nombre}']]//button[contains(@id,'herramientaParaComprar_')]";
            var buttonAdd = By.XPath(xpath);

            // Esperar y hacer click en el botón encontrado
            WaitForBeingClickable(buttonAdd);
            try
            {
                _driver.FindElement(buttonAdd).Click();
            }
            catch (NoSuchElementException)
            {
                throw new InvalidOperationException($"No se encontró el botón de añadir para la herramienta con nombre '{nombre}'. Comprueba que la tabla contiene ese nombre.");
            }
        }

        public void RemoveHerramienta(string nombre)
        {
            By buttonRemove = By.Id("eliminarherramientas_" + nombre);
            WaitForBeingClickable(buttonRemove);
            _driver.FindElement(buttonRemove).Click();
        }
        public void PulsarComprarHerramienta()
        {
            WaitForBeingClickable(comprarHerramientaButton);
            _driver.FindElement(comprarHerramientaButton).Click();
        }
    }
}
