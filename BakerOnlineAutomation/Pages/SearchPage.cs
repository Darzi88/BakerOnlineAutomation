using AventStack.ExtentReports;
using BakerOnlineAutomation.Pages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Threading;

namespace BakerOnlineAutomation
{
    internal class SearchPage : BasePage
    {
        public SearchPage(IWebDriver driver) : base(driver) { }

        internal bool Contains(Product productToCheckFor)
        {
            Reporter.LogTestStepForBugLogger(Status.Info,
                       $"Validate that Search page contains product=>{productToCheckFor}");
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

        internal ProductPage ClickOnProduct(Product productToClick)
        {
            switch (productToClick)
            {
                case Product.WholeWheatBread:
                    Driver.FindElement(By.XPath("//a[@href='/be-en/demo-shop/product/214717/test']")).Click();
                    break;
                case Product.ChocolatePie:
                    Driver.FindElement(By.XPath("//a[@href='/be-en/demo-shop/product/214735/chocolate-pie']")).Click();
                    break;
                default:
                    throw new ArgumentOutOfRangeException("No such product exists in this collection.");
            }
            return new ProductPage(Driver);
        }

        internal void AddProductToTheCart(Product product)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            IWebElement title = Driver.FindElement(By.XPath("//*[@class='currenttitle']"));
            js.ExecuteScript("arguments[0].scrollIntoView(true);", title);
            Thread.Sleep(500);
            Driver.FindElements(By.XPath("//*[@class='fill button main']"))[1].Click();
            if(product==Product.WholeWheatBread)
            {
                Thread.Sleep(500);
                var path = "//*[@class='button main fill']";
                Assert.IsTrue(IsElementVisible(By.XPath(path)), "Button is not visible");
                Driver.FindElements(By.XPath(path))[1].Click();
                Thread.Sleep(500);
                Driver.FindElements(By.XPath(path))[1].Click();
            }
            Thread.Sleep(500);
        }
    }
}