using System;
using System.Collections.Generic;
using System.Text;

namespace BookingAutomation.Model
{
    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public User() { }

        public User(string userName, string password)
        {
            Username = userName;
            Password = password;
        }
    }
}
