using AventStack.ExtentReports;
using BakerOnlineAutomation.Pages;
using OpenQA.Selenium;
using System;

namespace BakerOnlineAutomation
{
    internal class ProductPage : BasePage
    {
        public ProductPage(IWebDriver driver) : base(driver) { }

        internal bool ProductIsVisible(Product productToCheck)
        {
            Reporter.LogTestStepForBugLogger(Status.Info,
                       $"Validate that product=>{productToCheck} details are visible");
            switch (productToCheck)
            {
                case Product.WholeWheatBread:
                    return IsElementVisible(By.XPath("//span[contains(text(), 'Whole Wheat Bread')]"));
                case Product.ChocolatePie:
                    return IsElementVisible(By.XPath("//span[contains(text(), 'Chocolate pie')]"));
                default:
                    throw new ArgumentOutOfRangeException("No such product exists in this collection.");
            }
        }
    }
}