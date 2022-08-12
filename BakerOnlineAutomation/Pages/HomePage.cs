using AventStack.ExtentReports;
using BakerOnlineAutomation.Pages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace BakerOnlineAutomation
{
    internal class HomePage : BasePage
    {
        public HomePage(IWebDriver driver) : base(driver) { }

        public bool IsVisible {
            get
            {
                Reporter.LogTestStepForBugLogger(Status.Info,
                    "Validate that Home page loaded successfuly");
                WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(5));
                wait.IgnoreExceptionTypes(typeof(NoSuchElementException), typeof(WebDriverTimeoutException));
                try {
                    return wait.Until(driver =>
                    {
                        try
                        {
                            return driver.FindElement(HomePageBanner).Displayed;
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

        public By HomePageBanner => By.XPath("//*[@class='shop-banner  ']");

        internal void GoTo()
        {
            var url = "https://bakeronline.be/be-en/demo-shop";
            Driver.Navigate().GoToUrl(url);
            Reporter.LogPassingTestStepToBugLogger($"Open url=>{url} for Home Page.");
            Assert.IsTrue(IsVisible, "Home page was not visible.");
        }
    }
}