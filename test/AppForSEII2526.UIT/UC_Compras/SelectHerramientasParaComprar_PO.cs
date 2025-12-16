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
    }
}
