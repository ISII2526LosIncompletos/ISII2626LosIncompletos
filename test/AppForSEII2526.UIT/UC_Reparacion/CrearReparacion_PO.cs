using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UIT.UC_Reparacion
{
    internal class CrearReparacion_PO : PageObject
    {
        By inputNombre = By.Id("NombreCliente");
        By inputApellido = By.Id("ApellidoCliente");
        By inputFechaEntega = By.Id("FechaEntrega");
        By buttomSubmit = By.Id("Submit");
        By buttomGuardarDialog = By.Id("Button_DialogOK"); //Este botón está en WEB.Shared.Dialog.razor
        

        public CrearReparacion_PO(IWebDriver driver, ITestOutputHelper output) : base(driver, output)
        {
        }

        public void RellenarFormulario(string nombreC, string apellidoC, DateTime fecha)
        {
            WaitForBeingClickable(inputNombre);
            _driver.FindElement(inputNombre).SendKeys(nombreC);

            WaitForBeingClickable(inputApellido);
            _driver.FindElement(inputApellido).SendKeys(apellidoC);

            InputDateInDatePicker(inputFechaEntega, fecha);
        }

        public void SubmitReparacionClick()
        {
            WaitForBeingClickable(buttomSubmit);
            _driver.FindElement(buttomSubmit).Click();
        }

        public void ConfirmarReparacion()
        {
            WaitForBeingClickable(buttomGuardarDialog);
            _driver.FindElement(buttomGuardarDialog).Click();
        }

        public bool CompararMensajeError(string error)
        {
            //Comprobamos si el mensaje de error está en la página (sea donde sea que aparezca)
            return _driver.PageSource.Contains(error);
        }

        public void CambiarCantidadHerramienta(string herram, string cantidad)
        {
            By inputCantidad = By.Id("cantidad_" + herram);
            WaitForBeingClickable(inputCantidad);
            var inputElem = _driver.FindElement(inputCantidad);
            inputElem.Clear(); //Borramos el valor por defecto
            inputElem.SendKeys(cantidad); //Escribimos la cantidad deseada
        }
        
        /*public void AddDescripcion(string herram, string descripcion)
        {
            By inputDescripcion = By.Id("descripcion_" + herram);
            WaitForBeingClickable(inputDescripcion);
            _driver.FindElement(inputDescripcion).SendKeys(descripcion);
        }*/

    }
}
