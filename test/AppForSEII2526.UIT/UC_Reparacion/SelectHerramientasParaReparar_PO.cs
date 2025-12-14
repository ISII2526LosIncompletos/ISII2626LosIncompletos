using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UIT.UC_Reparacion
{
    internal class SelectHerramientasParaReparar_PO : PageObject
    {
        By inputNombre = By.Id("herrNombre");
        By inputTiempoRep = By.Id("herrTiempoRep");
        By buttonSearchHerramienta = By.Id("SearchHerramienta");
        public SelectHerramientasParaReparar_PO(IWebDriver driver, ITestOutputHelper output) : base(driver, output)
        {
        }
        public void SearchHerramienta(string nombre, string tiempoRep)
        {
            //Espera a que se pueda hacer clic en el elemento web
            WaitForBeingClickable(inputNombre);
            _driver.FindElement(inputNombre).SendKeys(nombre);
            WaitForBeingClickable(inputTiempoRep);
            _driver.FindElement(inputTiempoRep).SendKeys(tiempoRep);
            _driver.FindElement(buttonSearchHerramienta).Click();


        }

        
    }
}
