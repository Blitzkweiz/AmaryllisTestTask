using BookingAutomation.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookingAutomation.Service
{
    public class UserCreator
    {
        private static readonly string TESTDATA_CORRECT_USER_NAME = "test.booking.user@gmail.com";
        private static readonly string TESTDATA_CORRECT_PASSWORD = "TestBookingPassword1";

        public static User withCorrectCredentials()
        {
            return new User(TESTDATA_CORRECT_USER_NAME, 
                TESTDATA_CORRECT_PASSWORD);
        }
    }
}
