using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UIT.UC_Reparacion
{
    internal class SelectHerramientasParaReparar_PO : PageObject
    {
        By inputNombre = By.Id("inputNombre");
        By inputTiempoRep = By.Id("inputTiempoReparacion");
        By buttonSearchHerramienta = By.Id("SearchHerramienta");
        By tablaHerramientasBy = By.Id("TablaHerramientas");

        public SelectHerramientasParaReparar_PO(IWebDriver driver, ITestOutputHelper output) : base(driver, output)
        {
        }

        public void SearchHerramienta(string nombre, string tiempoRep)
        {
            //Espera a que se pueda hacer clic en el elemento web
            WaitForBeingClickable(inputNombre);
            _driver.FindElement(inputNombre).SendKeys(nombre);
            if (tiempoRep != null)
                _driver.FindElement(inputTiempoRep).SendKeys(tiempoRep);
            _driver.FindElement(buttonSearchHerramienta).Click();
        }

        public bool CheckListaHerramientas(List<string[]> expectedHerramientas)
        {

            return CheckBodyTable(expectedHerramientas, tablaHerramientasBy);
        }
        
    }
}
