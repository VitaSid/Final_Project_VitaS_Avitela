using OpenQA.Selenium;


namespace avitela.Page
{
    public class AvitelaHomePage : BasePage
    {
        private const string PageAddress = "https://avitela.lt/";
        private IWebElement cookieButton => Driver.FindElement(By.CssSelector(".operations"));
        private IWebElement searchField => Driver.FindElement(By.XPath("//input[@name='search2']"));
        private IWebElement viewAllProductsButton => Driver.FindElement(By.CssSelector(".msf-button-more"));
        public AvitelaHomePage(IWebDriver webdriver) : base(webdriver)   { }

        public void NavigateToPage()
        {
            if (Driver.Url != PageAddress)
                Driver.Url = PageAddress;
        }

        public void CloseCookies()
        {
            cookieButton.Click();
        }

        public void SearchByText(string text)
        {
            searchField.Clear();
            searchField.SendKeys(text);
            viewAllProductsButton.Click();
        }
    }
}
