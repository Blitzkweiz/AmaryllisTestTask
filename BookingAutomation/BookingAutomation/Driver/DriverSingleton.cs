using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

namespace BookingAutomation
{
    public class DriverSingleton
    {
        private static IWebDriver driver;

        public static IWebDriver GetDriver()
        {
            if(driver == null)
            {
                driver = new ChromeDriver();
                driver.Manage().Window.Maximize();
            }

            return driver;
        }

        public static void CloseDriver()
        {
            driver.Quit();
            driver = null;
        }
    }
}
