using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookingAutomation.Page
{
    public abstract class BasePage
    {
        protected readonly IWebDriver driver;

        protected readonly int WAIT_TIMEOUT_SECONDS = 10;
        public abstract BasePage OpenPage();

        protected BasePage(IWebDriver driver)
        {
            this.driver = driver;
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(WAIT_TIMEOUT_SECONDS);
        }
    }
}
