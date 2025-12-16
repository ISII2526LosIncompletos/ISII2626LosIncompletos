using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UIT.UC_Reparacion
{
    internal class DetalleReparacion_PO : PageObject
    {
        By tablaHerramietasReparar = By.Id("HerramientasReparadas");

        public DetalleReparacion_PO(IWebDriver driver, ITestOutputHelper output) : base(driver, output)
        {
        }

        public bool CheckDatosPersonales(string nombreC, string apellidoC, DateTime fechaEntrega, string herrTiempoRep, string precioTotal)
        {
            WaitForBeingClickable(tablaHerramietasReparar);
            bool resultado = true;
            resultado &= _driver.FindElement(By.Id("NombreApellidos")).Text.Contains(nombreC+" "+apellidoC);
            resultado &= _driver.FindElement(By.Id("FechaEntrega")).Text.Contains(fechaEntrega.ToString("dd/MM/yyyy"));
            int dias = int.Parse(herrTiempoRep);
            DateTime fechaReparacion = fechaEntrega.AddDays(dias);
            resultado &= _driver.FindElement(By.Id("FechaRecogida")).Text.Contains(fechaReparacion.ToString("dd/MM/yyyy"));
            resultado &= _driver.FindElement(By.Id("PrecioTotal")).Text.Contains(precioTotal+"€");
            return resultado;
        }

        public bool CheckTablaHerramientasReparar(List<string[]> expectedTabla)
        {
            return CheckBodyTable(expectedTabla, tablaHerramietasReparar);
        }
    }
}
