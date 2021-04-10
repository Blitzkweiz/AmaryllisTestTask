using BookingAutomation.Model;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace BookingAutomation.Tests
{
    public class Tests
    {
        protected IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = DriverSingleton.GetDriver();
        }

        [Test]
        public void ChangeCurrencyTest()
        {
            HomePage homePage = new HomePage(driver);
            homePage.OpenPage();
            homePage.ChangeCurrencyToUsd();
            Assert.True(homePage.GetCurrentCurrency() == "USD");
        }

        [Test]
        public void IsOpenableFlights()
        {
            HomePage homePage = new HomePage(driver);
            homePage.OpenPage();
            FlightsPage flightsPage = homePage.GoToFlightsPage();
            Assert.True(flightsPage.IsOpen());
        }

        [TearDown]
        public void TearDown()
        {
            DriverSingleton.CloseDriver();
        }
    }
}