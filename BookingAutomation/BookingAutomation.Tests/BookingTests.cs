using BookingAutomation.Model;
using BookingAutomation.Page;
using BookingAutomation.Service;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

namespace BookingAutomation.Tests
{
    public class BookingTests
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
        public void OpenFlightsPageTest()
        {
            HomePage homePage = new HomePage(driver);
            homePage.OpenPage();
            FlightsPage flightsPage = homePage.GoToFlightsPage();
            Assert.True(flightsPage.IsOpen());
        }

        [Test]
        public void AccessAccountTest()
        {
            User testUser = UserCreator.withCorrectCredentials();
            LoginPage loginPage = new LoginPage(driver);
            loginPage.OpenPage();
            HomePage homePage = loginPage.Login(testUser);
            AccountPage accountPage = homePage.GoToAccountPage();
            Assert.True(accountPage.IsOpen());
        }

        [Test]
        public void FilterTest()
        {
            DateTime checkIn = DateTime.Today.AddDays(7);
            DateTime checkOut = checkIn.AddDays(2);
            HomePage homePage = new HomePage(driver);
            homePage.OpenPage();
            homePage.InputCityName("Minsk");
            homePage.InputDates(checkIn, checkOut);
            homePage.InputGuests(2, 1, 1);
            SearchResultsPage searchResultsPage = homePage.ClickSearchButton();
            Assert.True(searchResultsPage.IsOpen());
        }

        [TearDown]
        public void TearDown()
        {
            DriverSingleton.CloseDriver();
        }
    }
}