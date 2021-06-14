using System;
using System.Text;

namespace AddressbookNETFramework.Model
{
    public class AccountData
    {
        private string username;
        private string userpassword;

        public AccountData(string username, string userpassword)
        {
            this.username = username;
            this.userpassword = userpassword;
        }

        public string Username
        {
            get
            {
                return username;
            }
            set
            {
                username = value;
            }
        }
        public string Userpassword
        {
            get
            {
                return userpassword;
            }
            set
            {
                userpassword = value;
            }
        }

    }
}

