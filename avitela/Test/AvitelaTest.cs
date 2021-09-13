using NUnit.Framework;
using System;


namespace avitela.Test
{
    class AvitelaTest : BaseTest
    {
        [Test]
        [Obsolete]
        public static void TestPriceFilter()
        {
            _avitelaHomePage.NavigateToPage();
            _avitelaHomePage.CloseCookies();
            _avitelaHomePage.SearchByText("jura");
            _avitelaSearchResultPage.SortByHighestPrice();
            _avitelaSearchResultPage.InsertMinPrice("500");
            _avitelaSearchResultPage.InsertMaxPrice("2000");
            _avitelaSearchResultPage.SubmitPriceRanges();
            _avitelaSearchResultPage.VerifyFilteringResults();
        }

        [Test]
        public static void TestTotalSumCorrectness()
        {
            _avitelaHomePage.NavigateToPage();
            _avitelaHomePage.CloseCookies();
            _avitelaHomePage.SearchByText("epson");
            _avitelaSearchResultPage.AddProductToShoppingBag();
            _shoppingBagPage.InscreaseQuantity("3");
            _shoppingBagPage.VerifyTotalProductSum();
        }

        [Test]
        [Obsolete]
        public static void TestRemovingProductFromBag()
        {
            _avitelaHomePage.NavigateToPage();
            _avitelaHomePage.CloseCookies();
            _avitelaHomePage.SearchByText("BLUETOOTH JABRA");
            _avitelaSearchResultPage.AddProductToShoppingBag();
            _shoppingBagPage.RemoveProductFromBag();
            _shoppingBagPage.VerifyIfBagIsEmpty("Jūsų prekių krepšelis tuščias.");
        }

        [Test]
        [Obsolete]
        public static void TestSortingByHighestPriceCorrectness()
        {
            _avitelaHomePage.NavigateToPage();
            _avitelaHomePage.CloseCookies();
            _avitelaHomePage.SearchByText("BLUETOOTH JABRA");
            _avitelaSearchResultPage.SortByHighestPrice();
            _avitelaSearchResultPage.VerifySortingIsCorrect();
        }

        [Test]
        [Obsolete]
        public static void TestProductWasAddedToProductComparisonPage()
        {
            _avitelaHomePage.NavigateToPage();
            _avitelaHomePage.CloseCookies();
            _avitelaHomePage.SearchByText("xbox");
            _avitelaSearchResultPage.ClickOnProductToOpenProductPage();
            _productPage.AddProductToComparisonPage();
            _productPage.OpenComparisonPage();
            _productComparisonPage.VerifyProductWasAddedToProductComparisonPage("Žaidimų konsolė Xbox");
        }
    }
}
