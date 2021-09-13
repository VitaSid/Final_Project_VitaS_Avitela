using avitela.Drivers;
using avitela.Page;
using avitela.Tools;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;

namespace avitela.Test
{
    public class BaseTest
    {
        public static IWebDriver driver;
        public static AvitelaHomePage _avitelaHomePage;
        public static AvitelaSearchResultPage _avitelaSearchResultPage;
        public static ShoppingBagPage _shoppingBagPage;
        public static ProductPage _productPage;
        public static ProductComparisonPage _productComparisonPage;

        [SetUp]
        public static void SetUp()
        {
            driver = CustomDriver.GetIncognitoChrome();
            _avitelaHomePage = new AvitelaHomePage(driver);
            _avitelaSearchResultPage = new AvitelaSearchResultPage(driver);
            _shoppingBagPage = new ShoppingBagPage(driver);
            _productPage = new ProductPage(driver);
            _productComparisonPage = new ProductComparisonPage(driver);
        }

        [TearDown]
        public static void TakeScreeshot()
        {
            if (TestContext.CurrentContext.Result.Outcome != ResultState.Success)
                MyScreenshot.MakeScreeshot(driver);           
        }

        [TearDown]
        public static void TearDown()
        {
            driver.Quit();
        }
    }
}

