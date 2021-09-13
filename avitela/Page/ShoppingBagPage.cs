using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;


namespace avitela.Page
{
    public class ShoppingBagPage : BasePage
    {
        private IWebElement quantityField => Driver.FindElement(By.CssSelector(".col-md-1.d-none.d-md-block > input"));
        private IWebElement refreshQuantityIcon => Driver.FindElement(By.CssSelector("[class='col-md-1 d-none d-md-block'] [alt]"));
        private IWebElement productUnitPrice => Driver.FindElement(By.CssSelector(".col-md-2.d-none.d-md-block.text-nowrap"));
        private IWebElement totalProductSum => Driver.FindElement(By.CssSelector(".col-md-1.d-none.d-md-block.text-nowrap"));
        private IWebElement removeProductIcon => Driver.FindElement(By.CssSelector(".col-md-1.d-md-block.d-none > a > img"));
        private IWebElement emptyBagMessage => Driver.FindElement(By.CssSelector("div#content > p"));
        public ShoppingBagPage(IWebDriver webdriver) : base(webdriver) { }

        public void InscreaseQuantity(string quantity)
        {
            quantityField.Clear();
            quantityField.SendKeys(quantity);
            refreshQuantityIcon.Click();
        }

        public void VerifyTotalProductSum()
        {
            string getQuantity = quantityField.GetAttribute("value");
            int _getQuantity = int.Parse(getQuantity);

            string unitPrice = productUnitPrice.Text.Replace("€", string.Empty).Replace(",", ".");
            decimal _unitPrice = decimal.Parse(unitPrice);

            string totalSum = totalProductSum.Text.Replace("€", string.Empty).Replace(",", ".");
            decimal _totalSum = decimal.Parse(totalSum);

            Assert.AreEqual(_unitPrice * _getQuantity, _totalSum, "Sum counted incorrectly");
        }

        public void RemoveProductFromBag()
        {
            removeProductIcon.Click();
        }

        [Obsolete]
        public void VerifyIfBagIsEmpty(string message)
        {
            GetWait().Until(ExpectedConditions.ElementIsVisible(By.XPath("//p[contains(text(),'tuščias')]")));
            Assert.IsTrue(emptyBagMessage.Text.Equals(message), $"Message is incorrect, expexted {message}, but was {emptyBagMessage.Text}");
        }
    }
}
