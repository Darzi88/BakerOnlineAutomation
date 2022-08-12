using AventStack.ExtentReports;
using BakerOnlineAutomation.Pages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;

namespace BakerOnlineAutomation
{
    internal class ForgotPasswordPage : BasePage
    {
        public ForgotPasswordPage(IWebDriver driver) : base(driver) { }

        public bool IsVisible
        {
            get
            {
                try
                {
                    Reporter.LogTestStepForBugLogger(Status.Info,
                        "Validate that Forgot Password page loaded successfuly");
                    var visible = Driver.Title.Contains(PageTitle);
                    return visible;
                }
                catch (NoSuchElementException)
                {
                    return false;
                }
            }
        }
        public string PageTitle => "Forgot password? - Bakeronline";
        public bool EmailSubmitedSuccessfully => Driver.FindElement(By.XPath("//*[@class='success']")).Displayed;

        public IWebElement EmailField => Driver.FindElement(By.Name("email"));
        public IWebElement SendButton => Driver.FindElement(By.XPath("//*[@class='button fill main']"));

        internal void GoTo()
        {
            var url = "https://bakeronline.be/be-en/forgot-password";
            Driver.Navigate().GoToUrl(url);
            Reporter.LogPassingTestStepToBugLogger($"Open url=>{url} for Forgot Password page.");
            Assert.IsTrue(IsVisible,
                $"Forgot password page was not visible.\n" +
                $"Expected=>{PageTitle}\n" +
                $"Actual=>{Driver.Title}");
        }

        internal void SubmitEmailAddress(TestUser testUser)
        {
            EmailField.SendKeys(testUser.EmailAddress);
            SendButton.Submit();
            Assert.IsTrue(EmailSubmitedSuccessfully, "Email was not submited successfully.");
        }
    }
}