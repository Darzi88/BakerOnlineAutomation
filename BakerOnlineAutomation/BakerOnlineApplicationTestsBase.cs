using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;

namespace BakerOnlineAutomation
{
    [TestCategory("BakerOnlineApplication"), TestClass]
    public class BakerOnlineApplicationTestsBase
    {

        [TestMethod]
        public void RegistrationTest("test_email", "test1234", "John", "Doe", "+32466901248")
        {
            Driver = GetChromeDriver();
            var registrationPage = new RegistrationPage(Driver);
            registrationPage.GoTo();
            var newUser = new NewUser("test_email", "test1234", "John", "Doe", "+32466901248");
            Assert.IsTrue(registrationPage.IsVisible, "Registration page was not visible.");
            var homePage = registrationPage.FillOutFormAndSubmit(newUser);
            Assert.IsTrue(homePage.IsVisible, "Home page was not visible.");
            Assert.AreEqual(newUser.FirstName.ToUpper(), Driver.FindElements(By.XPath("//*[@class='menu-button user arrow']"))[1].Text);
            Driver.Close();
            Driver.Quit();
        }
    }
}