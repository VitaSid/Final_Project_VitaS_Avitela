using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;

namespace avitela.Page
{
    public class AvitelaSearchResultPage : BasePage
    {
        private const string sortByHighestPriceText = "Kaina (Aukšta > Žema)";
        IReadOnlyCollection<IWebElement> FilteredProducts => Driver.FindElements(By.XPath("//span[@class='price-new']"));
        private IWebElement minPriceRange => Driver.FindElement(By.XPath("//input[@name='bfp_price_min']"));
        private IWebElement maxPriceRange => Driver.FindElement(By.XPath("//input[@name='bfp_price_max']"));
        private IWebElement toBagButton => Driver.FindElement(By.CssSelector(".btn-add-to-cart"));
        private IWebElement buyButton => Driver.FindElement(By.CssSelector(".btn.btn-cart"));
        private SelectElement SortByDropdown => new SelectElement(Driver.FindElement(By.CssSelector(".align-items-center.d-flex.product-filter > .align-items-center.d-flex.sort > .select > select"))); 
        private IWebElement openProductPage => Driver.FindElement(By.CssSelector(".product-image"));
        public AvitelaSearchResultPage(IWebDriver webdriver) : base(webdriver) { }

        public void ClickOnProductToOpenProductPage()
        {
            openProductPage.Click();
        }

        [Obsolete]
        public void SortByHighestPrice()
        {
            GetWait().Until(ExpectedConditions.ElementIsVisible(By.CssSelector(".align-items-center.d-flex.product-filter > .align-items-center.d-flex.sort > .select > select")));
            SortByDropdown.SelectByText(sortByHighestPriceText);
        }

        public void InsertMinPrice(string minPrice)
        {
            minPriceRange.SendKeys(minPrice);
        }

        [Obsolete]
        public void InsertMaxPrice(string maxPrice)
        {
            GetWait().Until(ExpectedConditions.ElementIsVisible(By.XPath("//input[@name='bfp_price_max']")));
            maxPriceRange.Clear();
            maxPriceRange.SendKeys(maxPrice);
        }

        public void SubmitPriceRanges()
        {            
            Actions action = new Actions(Driver);
            action.SendKeys(Keys.Enter);
            action.Build().Perform();
        }

        [Obsolete]
        public void VerifyFilteringResults()
        {                 
            Thread.Sleep(1000);
            int _getMaxPriceRange = int.Parse(maxPriceRange.GetAttribute("value"));
            Thread.Sleep(1000);
            int _getMinPriceRange = int.Parse(minPriceRange.GetAttribute("value"));

            GetWait().Until(ExpectedConditions.ElementIsVisible(By.XPath("//div[@class='price-new']")));
            IWebElement firstResultElement = FilteredProducts.First();
            IWebElement LastResultElement = FilteredProducts.Last();

            string firstResultElementPrice = firstResultElement.Text.Replace("€", string.Empty).Replace(",", ".");
            string lastResultElementPrice = LastResultElement.Text.Replace("€", string.Empty).Replace(",", ".");

            decimal highestPriceDecimal = decimal.Parse(firstResultElementPrice, NumberStyles.Currency);
            decimal lowestPriceDecimal = decimal.Parse(lastResultElementPrice, NumberStyles.Currency);
            
            Assert.GreaterOrEqual(lowestPriceDecimal, _getMinPriceRange, "Some goods in search results are cheaper then searched"); 
            Assert.LessOrEqual(highestPriceDecimal, _getMaxPriceRange, "Some goods in search results are more expensive then searched"); 
        }

        public void AddProductToShoppingBag()
        {
            toBagButton.Click();
            buyButton.Click();
        }

        public void VerifySortingIsCorrect()
        {
            IList<float> priceList = new List<float>();

            foreach (IWebElement productPrice in FilteredProducts)
            {
                string price = productPrice.Text.Replace("€", string.Empty).Replace(",", ".");
                float _price = float.Parse(price);
                priceList.Add(_price);
            }

            for (int i = 0; i < priceList.Count() - 1; i++)
            {
                bool result;
                float currentPrice = priceList[i];
                float nextPrice = priceList[i + 1];
                if (currentPrice >= nextPrice)
                {
                    result = true;
                }
                else
                {
                    result = false;
                }
                bool expectedResult = true;
                Assert.AreEqual(expectedResult, result);
            }
        }
    }
}
