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

        /// <summary>
        /// Проверяем, что мы залогинены под нужной ролью, если да, то выходим и цикла, если нет, то логинимся в систему.
        /// </summary>
        /// <param name="data">Заглушка</param>
        public void Login(AccountData data)
        {
            if (CheckLoginPresent())
            {
                if (IsLoggedInUser(data))
                {
                    return;
                }
                Logout();

                return;
            }

            webDriver.FindElement(By.Name("user")).SendKeys(data.Username);
            // Ищем поле "User", вводим в него логин.
            webDriver.FindElement(By.Name("pass")).SendKeys(data.Userpassword);
            // Ищем поле "Password", вводим в него пароль.
            webDriver.FindElement(By.XPath("//input[@value='Login']")).Click();
            // Нажимаем на кнопку "Login".
        }

        /// <summary>
        /// Выполняем "Logout" если мы залогинены.
        /// </summary>
        public void Logout()
        {
            if (CheckLoginPresent())
            {
                webDriver.FindElement(By.LinkText("Logout")).Click();
            }
        }
    }
}
