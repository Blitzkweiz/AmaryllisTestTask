using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookingAutomation.Page
{
    public class AccountPage : BasePage
    {
        private readonly string ACCOUNT_PAGE_URL = "https://account.booking.com/mysettings";

        [FindsBy(How = How.CssSelector, Using = "h1.bui-title__text")]
        private IWebElement accountSettingsText;

        public override BasePage OpenPage()
        {
            driver.Navigate().GoToUrl(ACCOUNT_PAGE_URL);
            return this;
        }

        public AccountPage(IWebDriver driver) : base(driver)
        {
            PageFactory.InitElements(driver, this);
        }

        public bool IsOpen()
        {
            return accountSettingsText.Enabled;
        }
    }
}
