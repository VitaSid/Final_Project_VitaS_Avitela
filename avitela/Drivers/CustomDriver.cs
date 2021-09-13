using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System;

namespace avitela.Drivers

{
    public class CustomDriver
    {
        public static IWebDriver GetIncognitoChromeDriver()
        {
            return GetDriver(Browsers.Chrome);
        }

        public static IWebDriver GetFirefoxDriver()
        {
            return GetDriver(Browsers.Firefox);
        }

        public static IWebDriver GetIncognitoChrome()
        {
            return GetDriver(Browsers.IncognitoChrome);
        }

        private static IWebDriver GetDriver(Browsers browserName)
        {
            IWebDriver driver = null;

            switch (browserName)
            {
                case Browsers.Firefox:
                    driver = new FirefoxDriver();
                    break;
                case Browsers.Chrome:
                    driver = new ChromeDriver();
                    break;
                case Browsers.IncognitoChrome:
                    driver = GetChromeWithIncognitoOptions();
                    break;
                case Browsers.ChromeWithOptions:
                    driver = GetChromeWithOptions();
                    break;
            }
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            return driver;
        }

        private static IWebDriver GetChromeWithIncognitoOptions()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("incognito");
            return new ChromeDriver(options);
        }

        private static IWebDriver GetChromeWithOptions()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--disable-notifications");
            return new ChromeDriver(options);
        }
    }
}

