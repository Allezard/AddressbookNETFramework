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

            webDriver.Navigate().GoToUrl(BaseHelper.urlLogin);
            webDriver.Manage().Cookies.DeleteAllCookies();
            webDriver.Manage().Window.Maximize();
        }

        /// <summary>
        /// Переходим во вкладку с группами.
        /// </summary>
        public void GoToUrlGroups()
        {
            webDriver.Navigate().GoToUrl(BaseHelper.urlGruopList);
        }

        /// <summary>
        /// Переходим во вкладку с контактами(главная страница).
        /// </summary>
        public void GoToUrContacts()
        {
            webDriver.Navigate().GoToUrl(BaseHelper.urlHomePage);
        }
    }
}
