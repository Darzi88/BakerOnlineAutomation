using BakerOnlineAutomation.Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System.Linq;

namespace BakerOnlineAutomation
{
    [TestClass]
    [TestCategory("BakerOnlineUserTests")]
    public class PBI1 : BaseTest
    {
        [TestMethod]
        [TestProperty("Author", "MislavZidar")]
        [Description("Validate that the user is able to fill out the registration form and register successfuly.")]
        public void TCID1()
        {
            RegistrationPage.GoTo();
            Driver.SwitchTo().NewWindow(WindowType.Tab);
            EmailPage.GoToAndGetEmailAddress(TheTestUser);
            var windowHandlesList = Driver.WindowHandles.ToList();
            Driver.SwitchTo().Window(windowHandlesList[0]);
            var homePage = RegistrationPage.FillOutFormAndSubmit(TheTestUser);
            AssertPageVisible(homePage);
            AssertNameMatch(TheTestUser.FirstName);
        }

        [TestMethod]
        [TestProperty("Author", "MislavZidar")]
        [Description("Validate that the user is able to login and logout successfuly using valid data.")]
        public void TCID2()
        {
            LoginPage.GoTo();
            var homePage = LoginPage.InputUserInfoAndLogin(TheTestUser);
            AssertPageVisible(homePage);
            AssertNameMatch(TheTestUser.FirstName);
            UserMenu.Open();
            UserMenu.LogOut();
            AssertLoggedOut(UserMenu);
        }

        [TestMethod]
        [TestProperty("Author", "MislavZidar")]
        [Description("Validate that the user is able to reset the password after registration, and login using the new passowrd.")]
        public void TCID3()
        {
            RegistrationPage.GoTo();
            Driver.SwitchTo().NewWindow(WindowType.Tab);
            EmailPage.GoToAndGetEmailAddress(TheTestUser);
            var windowHandlesList = Driver.WindowHandles.ToList();
            Driver.SwitchTo().Window(windowHandlesList[0]);
            var homePage = RegistrationPage.FillOutFormAndSubmit(TheTestUser);
            AssertPageVisible(homePage);
            AssertNameMatch(TheTestUser.FirstName);
            UserMenu.Open();
            UserMenu.LogOut();
            ForgotPasswordPage.GoTo();
            ForgotPasswordPage.SubmitEmailAddress(TheTestUser);
            Driver.SwitchTo().Window(windowHandlesList[1]);
            EmailPage.GetRidOfAdvertisment();
            EmailPage.WaitForResetPasswordLink();
            windowHandlesList = Driver.WindowHandles.ToList();
            Driver.SwitchTo().Window(windowHandlesList[2]);
            AssertPageVisible(ResetPasswordPage);
            var newPassword = "newpass1234";
            ResetPasswordPage.SubimtNewPassword(newPassword);
            TheTestUser.Password = newPassword;
            UserMenu.Open();
            UserMenu.LogOut();
            LoginPage.GoTo();
            homePage = LoginPage.InputUserInfoAndLogin(TheTestUser);
            AssertPageVisible(homePage);
            AssertNameMatch(TheTestUser.FirstName);
        }

        private void AssertLoggedOut(UserMenu userMenu)
        {
            Assert.IsTrue(userMenu.LoggedOut, "User was not logged out.");
        }
        private void AssertPageVisible(ResetPasswordPage resetPasswordPage)
        {
            Assert.IsTrue(resetPasswordPage.IsVisible, "Reset password page was not visible.");
        }
    }
}
