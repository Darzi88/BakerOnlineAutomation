using AventStack.ExtentReports;
using BakerOnlineAutomation.Pages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;

namespace BakerOnlineAutomation
{
    internal class CheckoutPage : BasePage
    {
        public CheckoutPage(IWebDriver driver) : base(driver) { }

        public bool IsSuccessPageVisible
        {
            get
            {
                Reporter.LogTestStepForBugLogger(Status.Info,
                    "Validate that Checkout Success page loaded successfuly");
                WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(5));
                wait.IgnoreExceptionTypes(typeof(NoSuchElementException), typeof(WebDriverTimeoutException));
                try
                {
                    return wait.Until(driver =>
                    {
                        try
                        {
                            return driver.FindElement(CheckoutSuccessPage).Displayed;
                        }
                        catch (NoSuchElementException)
                        {
                            return false;
                        }
                    });
                }
                catch (WebDriverTimeoutException)
                {
                    return false;
                }
            }
        }

        public bool IsVisible => Driver.Title.Contains(PageTitle);
        public string PageTitle => "Order overview - Bakeronline";
        public By CheckoutSuccessPage => By.Id("test-success-header");

        internal void Continue()
        {
            Driver.FindElement(By.XPath("//button[@class='button fill main']")).Click();
        }
        internal void ChoosePickUpAtShop()
        {
            var path = "test-takeout";
            Assert.IsTrue(IsElementVisible(By.Id(path)), "Pick up at shop button is not visible");
            Driver.FindElement(By.Id(path)).Click();
        }

        internal void PickDateAndTime()
        {
            Driver.FindElement(By.Id("test-calendar-date")).Click();
            Thread.Sleep(500);
            IWebElement date = Driver.FindElement(By.XPath("//*[@class='calendar-input-container']//*[@class='available']"));
            new Actions(Driver).MoveToElement(date).Click().Perform();
            Thread.Sleep(500);
            IWebElement hours = Driver.FindElement(By.XPath("//*[@class='calendar-input-container']//*[@class='available']"));
            new Actions(Driver).MoveToElement(hours).Click().Perform();
            Thread.Sleep(500);
            IWebElement minutes = Driver.FindElement(By.XPath("//*[@class='test-minutes']//*[@class='available']"));
            new Actions(Driver).MoveToElement(minutes).Click().Perform();
            Thread.Sleep(500);
        }

        internal void ChoosePaymentAtPickUp()
        {
            var path = "test-payment-method-0";
            Assert.IsTrue(IsElementVisible(By.Id(path)), "Payment at pickup button is not visible");
            Driver.FindElement(By.Id(path)).Click();
            Driver.FindElement(By.XPath("//button[@class='button fill main']")).Click();
        }
    }
    
}