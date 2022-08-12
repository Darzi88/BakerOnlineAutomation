using BakerOnlineAutomation.Pages;
using OpenQA.Selenium;
using System;

namespace BakerOnlineAutomation
{
    internal class Cart : BasePage
    {
        public Cart(IWebDriver driver) : base(driver) { }

        internal CheckoutPage OrderProducts()
        {
            Driver.FindElements(By.XPath("//*[@class='button fill main']"))[1].Click();
            return new CheckoutPage(Driver);
        }
    }
}