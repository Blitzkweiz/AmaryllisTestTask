using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookingAutomation.Util
{
    public static class SeleniumExtension
    {
        public static void ClickElementNTimes(IWebElement element, int times)
        {
            if (times == 0)
                return;

            for (int i = 0; i < times; i++)
                element.Click();
        }
    }
}
