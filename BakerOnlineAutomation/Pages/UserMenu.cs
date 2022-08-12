using AventStack.ExtentReports;
using BakerOnlineAutomation.Pages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace BakerOnlineAutomation
{
    internal class UserMenu : BasePage
    {
        public UserMenu(IWebDriver driver) : base(driver) { }

        public bool IsVisible
        {
            get
            {
                Reporter.LogTestStepForBugLogger(Status.Info,
                        "Validate that User Menu loaded successfuly");
                WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(5));
                wait.IgnoreExceptionTypes(typeof(NoSuchElementException), typeof(WebDriverTimeoutException));
                try
                {
                    return wait.Until(driver =>
                    {
                        try
                        {
                            return driver.FindElement(UserMenuHeader).Displayed;
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

        public bool LoggedOut
        {
            get
            {
                Reporter.LogTestStepForBugLogger(Status.Info,
                        "Validate that user looged out successfuly");
                WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(5));
                wait.IgnoreExceptionTypes(typeof(NoSuchElementException), typeof(WebDriverTimeoutException));
                try
                {
                    return wait.Until(driver =>
                    {
                        try
                        {
                            return driver.FindElement(LoginButton).Displayed;
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

        public By UserMenuHeader => By.XPath("//*[@class='dropdown-container']//header/h1");
        public By LoginButton => By.XPath("//*[@class='visible']/a[@class='menu-button']");

        internal void Open()
        {
            Driver.FindElement(By.XPath("//*[@class='visible']/button[@class='menu-button user arrow']")).Click();
            Assert.IsTrue(IsVisible, "User menu was not visible.");
        }

        internal void LogOut()
        {
            Driver.FindElement(By.XPath("//a[contains(text(), 'Log out')]")).Click();
        }
    }
}