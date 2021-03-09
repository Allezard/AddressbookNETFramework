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

        public void GoToBaseUrl()
        {
            if (IsloggedIn())
            {
                return;
            }

            webDriver.Manage().Cookies.DeleteAllCookies();
            webDriver.Manage().Window.Maximize();
            webDriver.Navigate().GoToUrl(BaseHelper.urlLogin);
        }

        public void GoToUrlGroups()
        {
            webDriver.Navigate().GoToUrl(BaseHelper.urlGruopList);
        }

        public void GoToUrContacts()
        {
            webDriver.Navigate().GoToUrl(BaseHelper.urlHomePage);
        }
    }
}
