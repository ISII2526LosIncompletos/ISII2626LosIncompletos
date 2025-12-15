using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UIT.UC_Compras
{
    public class SelectHerramientasParaComprar_PO: PageObject
    {
        By inputPrecio = By.Id("precioHerramienta");
        By inputMaterial= By.Id("materialHerramienta");
        By buttonSearchHerramienta = By.Id("buscarHerramientas");
        public SelectHerramientasParaComprar_PO(IWebDriver driver, ITestOutputHelper output) : base(driver, output)
        { 
        }
        public void BuscarHerramientas(string precio, string material)
        {
            //Espera a que el elemento sea clickable
            WaitForBeingClickable(inputPrecio);
            _driver.FindElement(inputPrecio).SendKeys(precio);
            _driver.FindElement(buttonSearchHerramienta).Click();


        }
    }
}
