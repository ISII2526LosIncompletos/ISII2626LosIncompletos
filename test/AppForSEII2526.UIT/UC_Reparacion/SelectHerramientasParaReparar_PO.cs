using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AppForSEII2526.UIT.UC_Reparacion
{
    internal class SelectHerramientasParaReparar_PO : PageObject
    {
        By inputNombre = By.Id("inputNombre");
        By inputTiempoRep = By.Id("inputTiempoReparacion");
        By buttonSearchHerramienta = By.Id("searchHerramienta");
        By tablaHerramientasBy = By.Id("TablaHerramientas");
        By buttonRepararHerramientas = By.Id("repararHerramientaButton");

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

        public bool CheckListaHerramientas(List<string[]> expectedHerramientas)
        {

            return CheckBodyTable(expectedHerramientas, tablaHerramientasBy);
        }

        public bool BotonRepararHerramientasOculto()
        {
            try
            {
                //Si el botón está oculto (no visible), devolvemos true
                return !_driver.FindElement(buttonRepararHerramientas).Displayed;
            }
            catch (Exception ex)
            {
                //Si hay alguna excepción, como que el botón no existe, es porque está oculto
                return true;
            }
        }

        public void AddHerramienta(string nombre)
        {
            By buttomAdd = By.Id("herramientaReparar_" + nombre);
            WaitForBeingClickable(buttomAdd);
            _driver.FindElement(buttomAdd).Click();
        }

        public void RemoveHerramienta(string nombre)
        {
            By buttomRemove = By.Id("removeHerramienta_" + nombre);
            WaitForBeingClickable(buttomRemove);
            _driver.FindElement(buttomRemove).Click();
        }

        public void RepararHerrBotonClick()
        {
            WaitForBeingClickable(buttonRepararHerramientas);
            _driver.FindElement(buttonRepararHerramientas).Click();
        }

        public void QuitarFiltro(string filtro)
        {
            By inputFiltro = By.Id("input" + filtro);
            WaitForBeingClickable(inputNombre);
            _driver.FindElement(inputNombre).Clear();
        }
    }
}
