using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UIT.UC_Compras
{
    internal class DetallesCompra_PO: PageObject
    {
        public DetallesCompra_PO(IWebDriver driver, ITestOutputHelper output) : base(driver, output)
        {
        }
        public bool CheckDetallesCompra(string nombre, string apellidos, string direccion, string precioTotal, DateTime fecha)
        {
            WaitForBeingClickable(By.Id("HerramientasCompradas"));
            bool result = true;
            var nombreYApellidos = nombre + " " + apellidos;
            result = result && _driver.FindElement(By.Id("NombreApellidos")).Text.Contains(nombreYApellidos);
            result = result && _driver.FindElement(By.Id("DireccionEnvio")).Text.Contains(direccion);
            result = result && _driver.FindElement(By.Id("PrecioTotal")).Text.Contains(precioTotal);
            result = result && _driver.FindElement(By.Id("FechaCompra")).Text.Contains(fecha.ToString("dd/MM/yyyy"));

            return result;
        }
        public bool CheckListaHerramientasCompradas(List<string[]> expectedHerramientas)
        {
            return CheckBodyTable(expectedHerramientas, By.Id("HerramientasCompradas"));
        }
    }
}
