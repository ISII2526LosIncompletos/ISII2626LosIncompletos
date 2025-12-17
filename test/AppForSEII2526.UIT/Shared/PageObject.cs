using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UIT.Shared {
    public class PageObject {

        protected IWebDriver _driver;
        //this may be used whenever some result should be printed in Explorador de Pruebas
        protected readonly ITestOutputHelper _output;

        private By _modalTitle = By.ClassName("modal-title");
        private By _modalBody = By.ClassName("modal-body");
        private By _okModalDialog = By.Id("Button_DialogOK");


        protected PageObject(IWebDriver driver, ITestOutputHelper output) {
            _driver = driver;
            this._output = output;
        }


        public void InputDateInDatePicker(By datepicker, DateTime date) {
            //first we select the datepicker
            IWebElement webElement = _driver.FindElement(datepicker);

            var action = new Actions(_driver);
            webElement.Clear();
            webElement.Click();
            action.KeyDown(Keys.Left).Perform();
            action.KeyDown(Keys.Left).Perform();
            action.SendKeys(date.ToString("dd")).Perform();

            action.KeyDown(Keys.Left).Perform();
            action.KeyDown(Keys.Left).Perform();
            action.KeyDown(Keys.Right).Perform();
            action.SendKeys(date.ToString("MM")).Perform();

            action.KeyDown(Keys.Right).Perform();
            action.KeyDown(Keys.Right).Perform();
            action.SendKeys(date.ToString("yyyy")).Perform();

        }


        public bool CheckBodyTable(List<string[]> expectedRows, By IdTable) {
            WaitForBeingVisible(IdTable);

            var actualRows = _driver
                .FindElement(IdTable)
                .FindElement(By.TagName("tbody"))
                .FindElements(By.TagName("tr"))
                .ToList();

            if (actualRows.Count != expectedRows.Count)
            {
                _output.WriteLine($"Error: \n Expected number of rows:{expectedRows.Count} \n Actual number of rows:{actualRows.Count}");
                return false;
            }

            for (int i = 0; i < expectedRows.Count; i++)
            {
                var expectedCells = expectedRows[i];
                var cells = actualRows[i].FindElements(By.TagName("td")).ToList();

                if (cells.Count < expectedCells.Length)
                {
                    _output.WriteLine($"Error: fila {i} tiene menos celdas de las esperadas. Esperadas:{expectedCells.Length} Reales:{cells.Count}");
                    return false;
                }

                for (int j = 0; j < expectedCells.Length; j++)
                {
                    string actualCellText = cells[j].Text.Trim();
                    string expectedCellText = expectedCells[j].Trim();

                    // Normalizar separador decimal (si procede)
                    actualCellText = actualCellText.Replace(',', '.');
                    expectedCellText = expectedCellText.Replace(',', '.');

                    if (!actualCellText.StartsWith(expectedCellText))
                    {
                        _output.WriteLine($"Error: \n \t expected cell[{i},{j}]:{expectedCellText} \n \t actual cell[{i},{j}]:{actualCellText}");
                        return false;
                    }
                }
            }

            return true;

        }

        public bool CheckModalBodyText(string expectedBody, By modal) {
            //waiting for the message error to be shown
            WaitForBeingVisible(modal);
            var actualBody = _driver.FindElement(_modalBody).Text;
            return actualBody.Contains(expectedBody);
        }

        public bool CheckModalTitleText(string expectedTitle, By modal) {
            //waiting for the message error to be shown
            WaitForBeingVisible(modal);
            var actualTitle = _driver.FindElement(_modalTitle).Text;
            return actualTitle.Contains(expectedTitle);
        }

        public void PressOkModalDialog() {
            //waiting for the message error to be shown
            WaitForBeingVisible(_okModalDialog);
            _driver.FindElement(_okModalDialog).Click();
        }



        public void WaitForBeingClickable(By IdElement) {
            //used whenever the webelement needs a delay for being clickable
            var wait = new WebDriverWait(_driver, new TimeSpan(0, 0, 30));
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException), typeof(StaleElementReferenceException));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(IdElement));
        }

        public void WaitForBeingVisible(By IdElement) {
            //used whenever the webelement needs a delay for being clickable
            var wait = new WebDriverWait(_driver, new TimeSpan(0, 0, 30));
            wait.Until(ExpectedConditions.ElementIsVisible(IdElement));

        }

        public void WaitForBeingVisibleIgnoringExeptionTypes(By IdElement) {
            //used whenever the webelement needs a delay for being clickable
            var wait = new WebDriverWait(_driver, new TimeSpan(0, 10, 0));


            wait.IgnoreExceptionTypes(typeof(NoSuchElementException),
                typeof(WebDriverTimeoutException),
                typeof(UnhandledAlertException),
                typeof(ElementClickInterceptedException));
            bool notFoundButton = true;
            while (notFoundButton) {
                try {
                    wait.Until(ExpectedConditions.ElementIsVisible(IdElement));
                    notFoundButton = false;
                }
                catch (ElementClickInterceptedException ex) {
                    _output.WriteLine(ex.Message);
                }
            }
        }


        public void WaitForTextToBePresentInElement(By IdElement, string expectedText) {
            //used whenever the webelement needs a delay for being clickable
            var wait = new WebDriverWait(_driver, new TimeSpan(0, 0, 30));
            IWebElement element = _driver.FindElement(IdElement);
            wait.Until(ExpectedConditions.TextToBePresentInElement(element, expectedText));

        }


        //it wait for "seconds" till all the webelements of the page are loaded
        public void ImplicitWait(int seconds) =>
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(seconds);
    }
}

