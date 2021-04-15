using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookingAutomation.Page
{
    public class SearchResultsPage : BasePage
    {
        private readonly string SEARCH_RESULTS_PAGE_URL = "https://www.booking.com/searchresults.html";

        [FindsBy(How = How.XPath, Using = "//h1[contains(text(),'properties')]")]
        private IWebElement searchResultHeader;

        public override BasePage OpenPage()
        {
            driver.Navigate().GoToUrl(SEARCH_RESULTS_PAGE_URL);
            return this;
        }

        public SearchResultsPage(IWebDriver driver) : base(driver)
        {
            PageFactory.InitElements(driver, this);
        }

        public bool IsOpen()
        {
            return searchResultHeader.Enabled;
        }
    }
}
