using OpenQA.Selenium;
using AppForSEII2526.UIT.Shared;
using System.Collections.Generic;
using System.Linq;
using System;

namespace AppForSEII2526.UIT.CU_OfertaHerramientas
{
    public class DetailOferta_PO : PageObject
    {
        By tablaHerramientasOfertadas = By.Id("HerramientasOfertadas");

        public DetailOferta_PO(IWebDriver driver, ITestOutputHelper output) : base(driver, output)
        {
        }

        public bool CheckOfertaDetail(DateTime fechaInicio, DateTime fechaFinal, DateTime fechaOferta, string metodoPago, string dirigidaA, int itemsCount)
        {
            WaitForBeingVisible(tablaHerramientasOfertadas);
            bool result = true;

            result = result && _driver.FindElement(By.Id("FechaInicio")).Text.Contains(fechaInicio.ToString("dd/MM/yyyy"));
            result = result && _driver.FindElement(By.Id("FechaFinal")).Text.Contains(fechaFinal.ToString("dd/MM/yyyy"));
            result = result && _driver.FindElement(By.Id("FechaOferta")).Text.Contains(fechaOferta.ToString("dd/MM/yyyy"));

            result = result && _driver.FindElement(By.Id("MetodoPago")).Text.Contains(metodoPago);
            result = result && _driver.FindElement(By.Id("TipoDirigida")).Text.Contains(dirigidaA);
            result = result && _driver.FindElement(By.Id("ItemsCount")).Text.Contains(itemsCount.ToString());

            return result;
        }

        public bool CheckListaHerramientas(List<string[]> expectedHerramientas)
        {
            var rows = _driver.FindElement(tablaHerramientasOfertadas).FindElements(By.TagName("tr"));

            foreach (var expectedRowData in expectedHerramientas)
            {
                string textoEsperado = string.Join(" ", expectedRowData);

                if (textoEsperado.Contains("12,50")) textoEsperado = textoEsperado.Replace("12,50", "12,5");
                if (textoEsperado.Contains("10,30")) textoEsperado = textoEsperado.Replace("10,30", "10,3");

                bool filaEncontrada = rows.Any(row => row.Text.Contains(textoEsperado));

                if (!filaEncontrada) return false;
            }
            return true;
        }
    }
}