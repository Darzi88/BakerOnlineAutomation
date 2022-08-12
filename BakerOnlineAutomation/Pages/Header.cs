using AventStack.ExtentReports;
using BakerOnlineAutomation.Pages;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;

namespace BakerOnlineAutomation
{
    internal class Header : BasePage
    {
        public Header(IWebDriver driver) : base(driver)
        {
            
        }

        public string ProductsInCart => Driver.FindElements(By.XPath("//*[@class='cart-item']/div/span"))[1].Text;

        internal SearchPage SearchProducts(string productToSearchFor)
        {
            Driver.FindElement(By.Name("query")).SendKeys(productToSearchFor);
            new Actions(Driver).SendKeys(Keys.Enter).Perform();
            return new SearchPage(Driver);
        }

        internal Cart ClickOnCart()
        {
            IWebElement cart = Driver.FindElement(By.XPath("//*[@class='cart-item']"));
            new Actions(Driver).MoveToElement(cart).Click().Perform();
            return new Cart(Driver);
        }

        internal bool CartContains(Product productToCheckFor)
        {
            Reporter.LogTestStepForBugLogger(Status.Info,
                       $"Validate that Cart contains product=>{productToCheckFor}");
            switch (productToCheckFor)
            {
                case Product.WholeWheatBread:
                    return IsElementVisible(By.XPath("//a[contains(text(), 'Whole Wheat Bread')]"));
                case Product.ChocolatePie:
                    return IsElementVisible(By.XPath("//a[contains(text(), 'Chocolate pie')]"));
                default:
                    throw new ArgumentOutOfRangeException("No such product exists in this collection.");
            }
        }

        internal void ClearSearchField()
        {
            Driver.FindElement(By.Name("query")).Click();
            new Actions(Driver).KeyDown(Keys.LeftControl).SendKeys("a").KeyUp(Keys.LeftControl).
                SendKeys(Keys.Delete).Perform();
        }
    }
}