using BakerOnlineAutomation.Pages;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;
using System.Windows;

namespace BakerOnlineAutomation
{
    internal class ExternalEmailPage : BasePage
    {
        public ExternalEmailPage(IWebDriver driver) : base(driver) { }

        internal void GoToAndGetEmailAddress(TestUser testUser)
        {
            Driver.Navigate().GoToUrl("https://10minutemail.net/");
            Driver.FindElement(By.Id("copy-button")).Click();
            testUser.EmailAddress = Clipboard.GetText(TextDataFormat.Text);
        }

        internal void GetRidOfAdvertisment()
        {
            var refreshButton = Driver.FindElement(By.XPath("//*[@class='fa fa-refresh fa-fw fa-lg ']"));
            new Actions(Driver).MoveToElement(refreshButton).Click().Perform();
            Thread.Sleep(1000);
            Driver.Navigate().Refresh();
        }

        internal void WaitForResetPasswordLink()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromMinutes(5));
            IWebElement email = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible
                (By.XPath("//tr/td/a[contains(text(), 'Change password - Bakeronline')]")));
            js.ExecuteScript("arguments[0].scrollIntoView(true);", email);
            Thread.Sleep(500);
            new Actions(Driver).MoveToElement(email).Click().Perform();

            WebDriverWait wait2 = new WebDriverWait(Driver, TimeSpan.FromSeconds(5));
            IWebElement link = wait2.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible
                (By.XPath("//a[contains(text(), 'this link')]")));
            js.ExecuteScript("arguments[0].scrollIntoView(true);", link);
            Thread.Sleep(500);
            new Actions(Driver).MoveToElement(link).Click().Perform();
        }
    }
}