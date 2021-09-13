using NUnit.Framework;
using OpenQA.Selenium;


namespace avitela.Page
{
    public class ProductComparisonPage : BasePage
    {
        private IWebElement productInComparisonPage => Driver.FindElement(By.XPath("//a/strong"));
        public ProductComparisonPage(IWebDriver webdriver) : base(webdriver) { }

        public void VerifyProductWasAddedToProductComparisonPage(string resultText)
        {
            string addedProduct = productInComparisonPage.Text;            
            Assert.IsTrue(addedProduct.Contains(resultText), "Product was not added, or wrong product was added");
        }
    }
}
