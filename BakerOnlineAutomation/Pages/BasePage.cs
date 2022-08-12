using AventStack.ExtentReports;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace BakerOnlineAutomation.Pages
{
    internal class BasePage
    {
        protected IWebDriver Driver { get; set; }

        public BasePage(IWebDriver driver)
        {
            Driver = driver;
        }

        public bool IsElementVisible(By path)
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(5));
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException), typeof(WebDriverTimeoutException));
            try
            {
                return wait.Until(driver =>
                {
                    try
                    {
                        return driver.FindElement(path).Displayed || driver.FindElements(path)[1].Displayed;
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
}