using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;


namespace avitela.Page
{
    public class ProductPage : BasePage
    {
        private IWebElement addToComparisonLink => Driver.FindElement(By.CssSelector(".compare > a"));
        private IWebElement comparisonPageIcon => Driver.FindElement(By.CssSelector(".wishlist #compare-total-header")); //xpath=//p[contains(.,'Palyginimas')] "
        private IWebElement closeProductAddedToComparisonPageSuccessNotification => Driver.FindElement(By.XPath("//button[@type='button' and @data-notify='dismiss']"));
        public ProductPage(IWebDriver webdriver) : base(webdriver) { }

        public void AddProductToComparisonPage()
        {
            addToComparisonLink.Click();
        }

        [Obsolete]
        public void OpenComparisonPage()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            js.ExecuteScript("window.scrollTo(0, 0)");
            GetWait().Until(ExpectedConditions.ElementIsVisible(By.XPath("//button[@type='button' and @data-notify='dismiss']")));
            closeProductAddedToComparisonPageSuccessNotification.Click();

            GetWait().Until(ExpectedConditions.ElementIsVisible(By.CssSelector(".wishlist #compare-total-header")));
            comparisonPageIcon.Click();
        }
    }
}
