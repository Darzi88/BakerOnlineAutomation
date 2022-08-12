using AventStack.ExtentReports;
using BakerOnlineAutomation.Pages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;

namespace BakerOnlineAutomation
{
    internal class LoginPage : BasePage
    {
        public LoginPage(IWebDriver driver) : base(driver) { }

        public bool IsVisible
        {
            get
            {
                try
                {
                    Reporter.LogTestStepForBugLogger(Status.Info,
                        "Validate that Login page loaded successfuly");
                    var visible = Driver.Title.Contains(PageTitle);
                    return visible;
                }
                catch (NoSuchElementException)
                {
                    return false;
                }
            }
        }

        public string PageTitle => "Log in - Bakeronline";

        public IWebElement EmailField => Driver.FindElement(By.Name("username"));
        public IWebElement PasswordField => Driver.FindElement(By.Name("password"));
        public IWebElement LoginButton => Driver.FindElement(By.XPath("//*[@class='button fill main']"));

        internal void GoTo()
        {
            var url = "https://bakeronline.be/be-en/demo-shop/login?redirect=/demo-shop";
            Driver.Navigate().GoToUrl(url);
            Reporter.LogPassingTestStepToBugLogger($"Open url=>{url} for Login page.");
            Assert.IsTrue(IsVisible,
                $"Login page was not visible.\n" +
                $"Expected=>{PageTitle}\n" +
                $"Actual=>{Driver.Title}");
        }

        internal HomePage InputUserInfoAndLogin(TestUser testUser)
        {
            EmailField.SendKeys(testUser.EmailAddress);
            PasswordField.SendKeys(testUser.Password);
            LoginButton.Click();
            return new HomePage(Driver);
        }
    }
}