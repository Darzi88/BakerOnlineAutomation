using BakerOnlineAutomation.Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace BakerOnlineAutomation
{
    [TestClass]
    [TestCategory("BakerOnlineProductsTests")]
    public class PIB2 : BaseTest
    {
        [TestMethod]
        [TestProperty("Author", "MislavZidar")]
        [Description("Validate that a visitor is able search for products successfuly.")]
        public void TCID1()
        {
            HomePage.GoTo();
            var searchPage = Header.SearchProducts("whole wheat bread");
            AssertPageContainsProduct(searchPage, Product.WholeWheatBread);
            var productPage = searchPage.ClickOnProduct(Product.WholeWheatBread);
            AssertProductVisible(productPage, Product.WholeWheatBread);
        }

        [TestMethod]
        [TestProperty("Author", "MislavZidar")]
        [Description("Validate that a logged in user is able search for products successfuly.")]
        public void TCID2()
        {
            LoginPage.GoTo();
            var homePage = LoginPage.InputUserInfoAndLogin(TheTestUser);
            AssertPageVisible(homePage);
            AssertNameMatch(TheTestUser.FirstName);
            var searchPage = Header.SearchProducts("chocolate pie");
            AssertPageContainsProduct(searchPage, Product.ChocolatePie);
            var productPage = searchPage.ClickOnProduct(Product.ChocolatePie);
            AssertProductVisible(productPage, Product.ChocolatePie);
        }

        [TestMethod]
        [TestProperty("Author", "MislavZidar")]
        [Description("Validate that a logged in user is able to order one product, and select Payment at pickup as a Payment method successfuly.")]
        public void TCID3()
        {
            LoginPage.GoTo();
            var homePage = LoginPage.InputUserInfoAndLogin(TheTestUser);
            AssertPageVisible(homePage);
            AssertNameMatch(TheTestUser.FirstName);
            var searchPage = Header.SearchProducts("chocolate pie");
            AssertPageContainsProduct(searchPage, Product.ChocolatePie);
            searchPage.AddProductToTheCart(Product.ChocolatePie);
            AssertProductCount("1", Header.ProductsInCart);
            var cart = Header.ClickOnCart();
            AssertCartContainsProduct(Product.ChocolatePie);
            var checkout = cart.OrderProducts();
            AssertCheckoutPageVisible(checkout);
            checkout.Continue();
            checkout.ChoosePickUpAtShop();
            checkout.PickDateAndTime();
            checkout.Continue();
            checkout.ChoosePaymentAtPickUp();
            AssertCheckoutSuccessPageVisible(checkout);
        }

        [TestMethod]
        [TestProperty("Author", "MislavZidar")]
        [Description("Validate that a logged in user is able to order multiple products, and select Payment at pickup as a Payment method successfuly.")]
        public void TCID4()
        {
            LoginPage.GoTo();
            var homePage = LoginPage.InputUserInfoAndLogin(TheTestUser);
            AssertPageVisible(homePage);
            AssertNameMatch(TheTestUser.FirstName);
            var searchPage = Header.SearchProducts("chocolate pie");
            AssertPageContainsProduct(searchPage, Product.ChocolatePie);
            searchPage.AddProductToTheCart(Product.ChocolatePie);
            Header.ClearSearchField();
            searchPage = Header.SearchProducts("whole wheat bread");
            AssertPageContainsProduct(searchPage, Product.WholeWheatBread);
            searchPage.AddProductToTheCart(Product.WholeWheatBread);
            AssertProductCount("2", Header.ProductsInCart);
            var cart = Header.ClickOnCart();
            AssertCartContainsProduct(Product.ChocolatePie);
            AssertCartContainsProduct(Product.WholeWheatBread);
            var checkout = cart.OrderProducts();
            AssertCheckoutPageVisible(checkout);
            checkout.Continue();
            checkout.ChoosePickUpAtShop();
            checkout.PickDateAndTime();
            checkout.Continue();
            checkout.ChoosePaymentAtPickUp();
            AssertCheckoutSuccessPageVisible(checkout);
        }

        private static void AssertPageContainsProduct(SearchPage searchPage, Product product)
        {
            Assert.IsTrue(searchPage.Contains(product), $"Search page does not contain this product {product}.");
        }
        private static void AssertProductVisible(ProductPage productPage, Product product)
        {
            Assert.IsTrue(productPage.ProductIsVisible(product), $"This product {product} is not visible.");
        }
        private void AssertCartContainsProduct(Product product)
        {
            Assert.IsTrue(Header.CartContains(product), $"This product {product} is not visible in the cart.");
        }
        internal void AssertProductCount(string expectedNumber, string actualNumber)
        {
            Assert.AreEqual(expectedNumber, actualNumber);
        }
        private static void AssertCheckoutPageVisible(CheckoutPage checkout)
        {
            Assert.IsTrue(checkout.IsVisible, "Checkout page was not visible.");
        }
        private static void AssertCheckoutSuccessPageVisible(CheckoutPage checkout)
        {
            Assert.IsTrue(checkout.IsSuccessPageVisible, "Checkout success page was not visible");
        }
    }
}
