using AppForMovies.UIT.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UIT.UC_Reparacion
{
    public class UCRepararHerramientas_UIT : UC_UIT
    {
        private SelectHerramientasParaReparar_PO selectHerramientasParaReparar_PO;

        public UCRepararHerramientas_UIT(ITestOutputHelper output) : base(output)
        {
        }

        private void InitialStepsParaRepararHerramientas()
        {
            //Esperamos a que la opción del menú sea visible
            selectHerramientasParaReparar_PO.WaitForBeingVisible(By.Id("CreateReparacion"));
            //Hacemos clic en la opción del menú para hacer una reparación
            _driver.FindElement(By.Id("CreateReparacion")).Click();
        }

    }
}
