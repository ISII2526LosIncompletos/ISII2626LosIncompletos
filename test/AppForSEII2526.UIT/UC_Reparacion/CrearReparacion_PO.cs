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
    }
}
