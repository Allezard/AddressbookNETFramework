using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using OpenQA.Selenium;
using AddressbookNETFramework.Model;

namespace AddressbookNETFramework.Helpers
{
    public class LoginHelper : BaseHelper
    {
        public LoginHelper(IWebDriver webDriver)
            : base(webDriver)
        {
        }

        public void Login(AccountData data)
        {
            if (IsloggedIn())
            {
                //if (IsLoggedInUser(data))
                //{
                //    return;
                //}        
                //Logout();

                return;
            }

            webDriver.FindElement(By.Name("user")).SendKeys(data.Username);
            // Ищем поле "User", вводим в него логин.
            webDriver.FindElement(By.Name("pass")).SendKeys(data.Userpassword);
            // Ищем поле "Password", вводим в него пароль.
            webDriver.FindElement(By.XPath("//input[@value='Login']")).Click();
            // Нажимаем на кнопку "Login".
        }

        public void Logout()
        {
            if (IsloggedIn())
            {
                webDriver.FindElement(By.LinkText("Logout")).Click();
            }
        }
    }
}
