using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using OpenQA.Selenium;
using AddressbookNETFramework.Model;

namespace AddressbookNETFramework.Helpers
{
    public class NavigationHelper : BaseHelper
    {
        public NavigationHelper(IWebDriver webDriver)
            : base(webDriver)
        {
        }

        /// <summary>
        /// Если мы не залогинены, то открываем браузер, чистим куки, открываем браузер на весь экран.
        /// </summary>
        public void GoToBaseUrl()
        {
            if (CheckLoginPresent())
            {
                return;
            }

            GoToPage("http://localhost/addressbook/");
            webDriver.Manage().Cookies.DeleteAllCookies();
            webDriver.Manage().Window.Maximize();
        }

        /// <summary>
        /// Метод для перехода на выбранную страницу.
        /// </summary>
        /// <param name="path">Принимает ссылку</param>
        public void GoToPage(string path)
        {
            webDriver.Navigate().GoToUrl(path);
        }
    }
}
