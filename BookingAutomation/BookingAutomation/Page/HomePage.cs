using BookingAutomation.Util;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookingAutomation.Page
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

        [FindsBy(How = How.CssSelector, Using = "a[aria-label='Open the profile menu']")]
        private IWebElement profileButton;

        [FindsBy(How = How.XPath, Using = "//span[contains(text(),'Manage account')]")]
        private IWebElement manageAccountButton;

        [FindsBy(How = How.Id, Using = "ss")]
        private IWebElement cityInput;

        [FindsBy(How = How.ClassName, Using = "xp__dates")]
        private IWebElement datesInput;

        [FindsBy(How = How.ClassName, Using = "xp__guests")]
        private IWebElement guestsInput;

        [FindsBy(How = How.CssSelector, Using = "[data-adults-count]")]
        private IWebElement adultsCount;
        
        [FindsBy(How = How.XPath, Using = "//div[contains(@class,'sb-group__field-adults')]//span[text()='+']")]
        private IWebElement increaseAdultsCountButton;

        [FindsBy(How = How.XPath, Using = "//div[contains(@class,'sb-group__field-adults')]//span[text()='-']")]
        private IWebElement decreaseAdultsCountButton;

        [FindsBy(How = How.CssSelector, Using = "[data-children-count]")]
        private IWebElement childrenCount;

        [FindsBy(How = How.XPath, Using = "//div[contains(@class,'sb-group-children')]//span[text()='+']")]
        private IWebElement increaseChildrenCountButton;

        [FindsBy(How = How.XPath, Using = "//div[contains(@class,'sb-group-children')]//span[text()='-']")]
        private IWebElement decreaseChildrenCountButton;

        [FindsBy(How = How.CssSelector, Using = "[data-room-count]")]
        private IWebElement roomsCount;

        [FindsBy(How = How.XPath, Using = "//div[contains(@class,'sb-group__field-rooms')]//span[text()='+']")]
        private IWebElement increaseRoomsCountButton;

        [FindsBy(How = How.XPath, Using = "//div[contains(@class,'sb-group__field-rooms')]//span[text()='-']")]
        private IWebElement decreaseRoomsCountButton;

        [FindsBy(How = How.ClassName, Using = "sb-searchbox__button")]
        private IWebElement searchButton;

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

        public AccountPage GoToAccountPage()
        {
            profileButton.Click();
            manageAccountButton.Click();
            return new AccountPage(driver);
        }

        public void InputCityName(string cityName)
        {
            cityInput.Clear();
            cityInput.SendKeys(cityName);
        }

        public void InputDates(DateTime checkIn, DateTime checkOut)
        {
            datesInput.Click();
            driver.FindElement(By.CssSelector("td[data-date='" + checkIn.ToString("yyyy-MM-dd") + "']")).Click();
            driver.FindElement(By.CssSelector("td[data-date='" + checkOut.ToString("yyyy-MM-dd") + "']")).Click();
        }

        public void InputGuests(int adults, int children, int rooms)
        {
            guestsInput.Click();
            ChangeAdultsCount(adults);
            ChangeChildrenCount(children);
            ChangeRoomsCount(rooms);
        }

        public void ChangeAdultsCount(int adults)
        {
            int current = int.Parse(adultsCount.Text.Substring(0, 1));
            if (current == adults)
                return;

            if (current < adults)
                SeleniumExtension.ClickElementNTimes(increaseAdultsCountButton, adults - current);
            else
                SeleniumExtension.ClickElementNTimes(decreaseAdultsCountButton, current - adults);

        }

        public void ChangeChildrenCount(int children)
        {
            int current = int.Parse(childrenCount.Text.Substring(0, 1));
            if (current == children)
                return;

            if (current < children)
                SeleniumExtension.ClickElementNTimes(increaseChildrenCountButton, children - current);
            else
                SeleniumExtension.ClickElementNTimes(decreaseChildrenCountButton, current - children);
        }

        public void ChangeRoomsCount(int rooms)
        {
            int current = int.Parse(roomsCount.Text.Substring(0, 1));
            if (current == rooms)
                return;

            if (current < rooms)
                SeleniumExtension.ClickElementNTimes(increaseRoomsCountButton, rooms - current);
            else
                SeleniumExtension.ClickElementNTimes(decreaseRoomsCountButton, current - rooms);
        }

        public SearchResultsPage ClickSearchButton()
        {
            searchButton.Click();
            return new SearchResultsPage(driver);
        }
    }
}
