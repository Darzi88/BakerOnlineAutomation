using AventStack.ExtentReports;
using BakerOnlineAutomation.Pages;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace BakerOnlineAutomation
{
    internal class ResetPasswordPage : BasePage
    {
        public bool IsVisible
        {
            get
            {
                Reporter.LogTestStepForBugLogger(Status.Info,
                        "Validate that Reset password page loaded successfuly");
                WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(5));
                wait.IgnoreExceptionTypes(typeof(NoSuchElementException), typeof(WebDriverTimeoutException));
                try
                {
                    return wait.Until(driver =>
                    {
                        try
                        {
                            return driver.FindElement(ResetPasswordPageHeader).Displayed;
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

        public ResetPasswordPage(IWebDriver driver) : base(driver) { }

        public By ResetPasswordPageHeader => By.XPath("//h1[contains(text(), 'New password')]");

        public IWebElement PasswordField => Driver.FindElement(By.Name("password"));
        public IWebElement RepeatPasswordField => Driver.FindElement(By.Name("retype-password"));
        public IWebElement SaveButton => Driver.FindElement(By.XPath("//*[@class='button fill main']"));

        internal void SubimtNewPassword(string newPassword)
        {
            PasswordField.SendKeys(newPassword);
            RepeatPasswordField.SendKeys(newPassword);
            SaveButton.Submit();
        }
    }
}