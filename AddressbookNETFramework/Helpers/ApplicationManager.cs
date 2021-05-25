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

        /// <summary>
        /// Инициализирует помощников(групп, контактов, навигаци, логин, вебдрайвер) для всех тестов.
        /// </summary>
        private ApplicationManager()
        {
            ChromeOptions chromeIncognito = new ChromeOptions();
            chromeIncognito.AddArgument("--incognito");
            // Запускаем браузер в режиме incognito.

            webDriver = new ChromeDriver(chromeIncognito);
            LoginHelper = new LoginHelper(webDriver);
            NavigationHelper = new NavigationHelper(webDriver);
            GroupHelper = new GroupHelper(webDriver);
            ContactHelper = new ContactHelper(webDriver);
        }

        /// <summary>
        /// Инициализатор браузера.
        /// </summary>
        /// <returns>Возвращает или задает значение для данного потока.</returns>
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

        /// <summary>
        /// После выполнения заданного кода завершаем работу "ApplicationManager" с помошью методай "Quit" для вебдрайвера.
        /// </summary>
        ~ApplicationManager()
        {
            try
            {
                webDriver.Quit();
            }
            catch (Exception)
            {
                //Ignore errors if unable to close the browser
            }
        }

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
