using AventStack.ExtentReports;
using BakerOnlineAutomation.Pages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace BakerOnlineAutomation
{
    internal class RegistrationPage : BasePage
    {
        public RegistrationPage(IWebDriver driver) : base(driver) { }

        public bool IsVisible
        {
            get
            {
                try
                {
                    Reporter.LogTestStepForBugLogger(Status.Info,
                        "Validate that Registration page loaded successfuly");
                    var visible = Driver.Title.Contains(PageTitle);
                    return visible;
                }
                catch (NoSuchElementException)
                {
                    return false;
                }
            }
        }
        public string PageTitle => "Register - Bakeronline";

        public IWebElement EmailField => Driver.FindElement(By.Name("email"));
        public IWebElement PasswordField => Driver.FindElement(By.Name("password"));
        public IWebElement RepeatPasswordField => Driver.FindElement(By.Name("password-repeat"));
        public IWebElement FirstNameField => Driver.FindElement(By.Name("firstname"));
        public IWebElement LastNameField => Driver.FindElement(By.Name("lastname"));
        public IWebElement PhoneNumberField => Driver.FindElement(By.Name("telephone"));
        public IWebElement RegisterButton => Driver.FindElement(By.XPath("//*[@class='button fill main']"));
        public IWebElement TermsButton => Driver.FindElement(By.XPath("//*[@class='main fill button small']"));

        internal void GoTo()
        {
            var url = "https://bakeronline.be/be-en/demo-shop/register";
            Driver.Navigate().GoToUrl(url);
            Reporter.LogPassingTestStepToBugLogger($"Open url=>{url} for Registration page.");
            Assert.IsTrue(IsVisible,
                $"Registration page was not visible.\n" +
                $"Expected=>{PageTitle}\n" +
                $"Actual=>{Driver.Title}");
        }

        internal HomePage FillOutFormAndSubmit(TestUser testUser)
        {
            EmailField.SendKeys(testUser.EmailAddress);
            PasswordField.SendKeys(testUser.Password);
            RepeatPasswordField.SendKeys(testUser.Password);
            FirstNameField.SendKeys(testUser.FirstName);
            LastNameField.SendKeys(testUser.LastName);
            PhoneNumberField.SendKeys(testUser.PhoneNumber);
            RegisterButton.Click();
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(5));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//*[@class='main fill button small']")));
            Assert.AreEqual("I read and accept these terms", TermsButton.Text);
            TermsButton.Click();
            Assert.AreEqual("I took notice of the privacy policy", TermsButton.Text);
            TermsButton.Click();
            return new HomePage(Driver);
        }
    }
}