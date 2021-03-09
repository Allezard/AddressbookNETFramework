using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Threading;
using NUnit.Framework;
using AddressbookNETFramework.Model;
using AddressbookNETFramework.Helpers;

namespace AddressbookNETFramework.Helpers
{
    public class ApplicationManager
    {
        protected IWebDriver webDriver;
        protected LoginHelper LoginHelper;
        protected NavigationHelper NavigationHelper;
        protected GroupHelper GroupHelper;
        protected ContactHelper ContactHelper;
        private static readonly ThreadLocal<ApplicationManager> app = new ThreadLocal<ApplicationManager>();

        private ApplicationManager()
        {
            ChromeOptions chromeIncognito = new ChromeOptions();
            chromeIncognito.AddArgument("--incognito");

            webDriver = new ChromeDriver(chromeIncognito);
            LoginHelper = new LoginHelper(webDriver);
            NavigationHelper = new NavigationHelper(webDriver);
            GroupHelper = new GroupHelper(webDriver);
            ContactHelper = new ContactHelper(webDriver);
        }

        public static ApplicationManager GetInstance()
        {
            if (!app.IsValueCreated)
            {
                ApplicationManager newInstance = new ApplicationManager();
                newInstance.Navigation.GoToBaseUrl();
                app.Value = newInstance;
            }
            return app.Value;
        }

        public void Stop()
        {
            try
            {
                webDriver.Quit();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
        }

        //~ApplicationManager()
        //{
        //    try
        //    {
        //        webDriver.Quit();
        //    }
        //    catch (Exception)
        //    {
        //        Ignore errors if unable to close the browser
        //    }
        //}

        public LoginHelper Auth
        {
            get { return LoginHelper; }
        }

        public NavigationHelper Navigation
        {
            get { return NavigationHelper; }
        }

        public GroupHelper Groups
        {
            get { return GroupHelper; }
        }

        public ContactHelper Contacts
        {
            get { return ContactHelper; }
        }
    }
}
