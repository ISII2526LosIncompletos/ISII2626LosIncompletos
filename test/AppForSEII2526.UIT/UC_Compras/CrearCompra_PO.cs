using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UIT.UC_Compras
{
    internal class CrearCompra_PO: PageObject
    {
        By inputNombre = By.Id("NombreCliente");
        By inputApellido = By.Id("ApellidoCliente");
        By inputDireccionEnvio= By.Id("DirecciónEnvío");
        By inputMetodoPago = By.Id("MetodoPago");
        By inputTelefono = By.Id("Telefono");
        By inputEmail = By.Id("Email");

        By buttonModificar = By.Id("ModifyHerramientas");
        By tablaItems = By.Id("TableOfItemsCompras");
        By botonComprar = By.Id("Submit");
        By okDialog= By.Id("Button_DialogOK"); //Este botón está en WEB.Shared.Dialog.razor

        public CrearCompra_PO(IWebDriver driver, ITestOutputHelper output): base(driver, output)
        {

        }
        public void RellenarFormularioCompra(string nombre, string apellido, string direccionEnvio, string MetodoPago, string numTelefono, string email)
        {
            WaitForBeingClickable(inputNombre);
            _driver.FindElement(inputNombre).SendKeys(nombre);
            WaitForBeingClickable(inputApellido);
            _driver.FindElement(inputApellido).SendKeys(apellido);
            WaitForBeingClickable(inputDireccionEnvio);
            _driver.FindElement(inputDireccionEnvio).SendKeys(direccionEnvio);
            WaitForBeingClickable(inputMetodoPago);
            _driver.FindElement(inputMetodoPago).SendKeys(MetodoPago);
            WaitForBeingClickable(inputTelefono);
            _driver.FindElement(inputTelefono).SendKeys(numTelefono);
            WaitForBeingClickable(inputEmail);
            _driver.FindElement(inputEmail).SendKeys(email);
        }
        public void rellenarDescripcionHerramienta(string descripcion, string herramienta)
        {
            By inputDescripcion = By.Id("descripcion_" + herramienta);
            WaitForBeingClickable(inputDescripcion);
            _driver.FindElement(inputDescripcion).SendKeys(descripcion);
        }
        public void rellenarCantidad(int cantidad, string nombre)
        {
            By inputCantidad = By.Id("cantidad_" + nombre);
            WaitForBeingClickable(inputCantidad);
            _driver.FindElement(inputCantidad).Clear();
            _driver.FindElement(inputCantidad).SendKeys(cantidad.ToString());
        }
        public void modificarCarrito()
        {
            WaitForBeingClickable(buttonModificar);
            _driver.FindElement(buttonModificar).Click();
        }
        public bool checkListaHerramientasItems(List<string[]> expectedHerramientas)
        {
            return CheckBodyTable(expectedHerramientas, tablaItems);
        }
        public void SubmitCompraClick()
        {
            WaitForBeingClickable(botonComprar);
            _driver.FindElement(botonComprar).Click();
        }

        public void ConfirmarCompra()
        {
            WaitForBeingClickable(okDialog);
            _driver.FindElement(okDialog).Click();
        }

        public bool CompararMensajeError(string error)
        {
            //Comprobamos si el mensaje de error está en la página (sea donde sea que aparezca)
            return _driver.PageSource.Contains(error);
        }
    }
}
