using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookingAutomation.Model
{
    public class HomePage : BasePage
    {
        private readonly string HOME_PAGE_URL = "https://www.booking.com";

        [FindsBy(How = How.CssSelector, Using = "button[data-modal-aria-label='Select your currency']")]
        private IWebElement currencyButton;

        [FindsBy(How = How.CssSelector, Using = ".bui-modal__slot a[data-modal-header-async-url-param='changed_currency=1;selected_currency=USD;top_currency=1']")]
        private IWebElement usdCurrency;

        [FindsBy(How = How.CssSelector, Using = "button[data-modal-aria-label='Select your currency'] span span")]
        private IWebElement currentCurrency;

        [FindsBy(How = How.CssSelector, Using = "a[data-decider-header='flights']")]
        private IWebElement flightsButton;
        public HomePage(IWebDriver driver) : base(driver)
        {
            PageFactory.InitElements(driver, this);
        }
        
        public override BasePage OpenPage()
        {
            driver.Navigate().GoToUrl(HOME_PAGE_URL);
            return this;
        }

        public HomePage ChangeCurrencyToUsd()
        {
            currencyButton.Click();
            usdCurrency.Click();
            return this;
        }

        public string GetCurrentCurrency()
        {
            return currentCurrency.Text;
        }

        public FlightsPage GoToFlightsPage()
        {
            flightsButton.Click();
            return new FlightsPage(driver);
        }
    }
}
