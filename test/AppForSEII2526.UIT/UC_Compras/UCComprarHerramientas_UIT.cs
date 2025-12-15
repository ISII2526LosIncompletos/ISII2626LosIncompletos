using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppForSEII2526.UIT.Shared;

namespace AppForSEII2526.UIT.UC_Compras
{
    public class UCComprarHerramientas_UIT: UC_UIT
    {
        private SelectHerramientasParaComprar_PO selectHerramientasParaComprar_PO;
        public UCComprarHerramientas_UIT(ITestOutputHelper output) : base(output)
        {

        }
        private void InitialStepsParaComprarHerramientas()
        {

            //Esperamos a que la opción del menú esté visible
            selectHerramientasParaComprar_PO.WaitForBeingVisible(By.Id("CreateCompra"));
            //Damos click en el menú
            _driver.FindElement(By.Id("CreateCompra")).Click();
        }
    }
}
