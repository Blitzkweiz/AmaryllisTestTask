using BookingAutomation.Model;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookingAutomation.Page
{
    public class LoginPage : BasePage
    {
        private readonly string LOGIN_PAGE_URL = "https://account.booking.com/sign-in";

        [FindsBy(How = How.Id, Using = "username")]
        private IWebElement usernameInput;

        [FindsBy(How = How.Id, Using = "password")]
        private IWebElement passwordInput;

        public override BasePage OpenPage()
        {
            driver.Navigate().GoToUrl(LOGIN_PAGE_URL);
            return this;
        }

        public LoginPage(IWebDriver driver) : base(driver)
        {
            PageFactory.InitElements(driver, this);
        }

        public void InputUsername(string username)
        {
            usernameInput.SendKeys(username);
            usernameInput.SendKeys(Keys.Enter);
        }

        public void InputPassword(string password)
        {
            passwordInput.SendKeys(password);
            passwordInput.SendKeys(Keys.Enter);
        }

        public HomePage Login(User user)
        {
            InputUsername(user.Username);
            InputPassword(user.Password);
            return new HomePage(driver);
        }
    }
}
