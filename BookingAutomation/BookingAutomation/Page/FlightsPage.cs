using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookingAutomation.Page
{
    public class FlightsPage : BasePage
    {
        private readonly string FLIGHTS_PAGE_URL = "https://booking.kayak.com/";

        [FindsBy(How = How.XPath, Using = "//a[span[2]/text()='Flights']")]
        private IWebElement flightsButton;

        public FlightsPage(IWebDriver driver) : base(driver)
        {
            PageFactory.InitElements(driver, this);
        }

        public override BasePage OpenPage()
        {
            driver.Navigate().GoToUrl(FLIGHTS_PAGE_URL);
            return this;
        }

        public bool IsOpen()
        {
            return flightsButton.GetAttribute("class") == "selected";
        }
    }
}
