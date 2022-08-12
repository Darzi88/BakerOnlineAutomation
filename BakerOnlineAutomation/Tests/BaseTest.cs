using AutomationResources;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NLog;
using OpenQA.Selenium;
using System;

namespace BakerOnlineAutomation.Tests
{
    public class BaseTest
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        internal IWebDriver Driver { get; set; }
        public TestContext TestContext { get; set; }
        private ScreenshotTaker ScreenshotTaker { get; set; }
        internal Header Header { get; private set; }
        internal HomePage HomePage { get; private set; }
        internal RegistrationPage RegistrationPage { get; private set; }
        internal ExternalEmailPage EmailPage { get; private set; }
        internal LoginPage LoginPage { get; private set; }
        internal UserMenu UserMenu { get; private set; }
        internal ForgotPasswordPage ForgotPasswordPage { get; private set; }
        internal ResetPasswordPage ResetPasswordPage { get; private set; }
        internal TestUser TheTestUser { get; private set; }

        [TestInitialize]
        public void SetupForEveryTestMethod()
        {
            Logger.Debug("*************************************** TEST STARTED");
            Reporter.AddTestCaseMetadataToHtmlReport(TestContext);
            var factory = new WebDriverFactory();
            Driver = factory.Create(BrowserType.Chrome);
            Driver.Manage().Window.Maximize();
            ScreenshotTaker = new ScreenshotTaker(Driver, TestContext);
            Header = new Header(Driver);
            HomePage = new HomePage(Driver);
            RegistrationPage = new RegistrationPage(Driver);
            EmailPage = new ExternalEmailPage(Driver);
            LoginPage = new LoginPage(Driver);
            UserMenu = new UserMenu(Driver);
            ForgotPasswordPage = new ForgotPasswordPage(Driver);
            ResetPasswordPage = new ResetPasswordPage(Driver);
            TheTestUser = new TestUser
            {
                FirstName = "John",
                LastName = "Doe",
                EmailAddress = "testemail1234@jeoce.com",
                Password = "test1234",
                PhoneNumber = "+32466901248"
            };
        }

        [TestCleanup]
        public void CleanupAfterEveryTestMethod()
        {
            Logger.Debug(GetType().FullName + " started a method teardown");
            try
            {
                TakeScreenshotForTestFailure();
            }
            catch (Exception e)
            {
                Logger.Error(e.Source);
                Logger.Error(e.StackTrace);
                Logger.Error(e.InnerException);
                Logger.Error(e.Message);
            }
            finally
            {
                StopBrowser();
                Logger.Debug(TestContext.TestName);
                Logger.Debug($"*************************************** TEST STOPPED\n");
            }
        }

        private void TakeScreenshotForTestFailure()
        {
            if(ScreenshotTaker != null)
            {
                ScreenshotTaker.CreateScreenshotIfTestFailed();
                Reporter.ReportTestOutcome(ScreenshotTaker.ScreenshotFilePath);
            }
            else
            {
                Reporter.ReportTestOutcome("");
            }
        }
        private void StopBrowser()
        {
            if(Driver == null)
            {
                return;
            }
            Driver.Quit();
            Driver = null;
            Logger.Trace("Browser stopped successfuly");
        }

        internal static void AssertPageVisible(HomePage homePage)
        {
            Assert.IsTrue(homePage.IsVisible, "Home page was not visible.");
        }
        internal void AssertNameMatch(string firstName)
        {
            Assert.AreEqual(firstName.ToUpper(),
                Driver.FindElement(By.XPath("//*[@class='visible']/button[@class='menu-button user arrow']")).Text);
        }
    }
}