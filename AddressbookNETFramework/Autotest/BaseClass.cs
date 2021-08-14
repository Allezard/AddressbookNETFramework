using System;
using System.Linq;
using System.Text;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using AddressbookNETFramework.Helpers;
using System.Collections.Generic;

namespace AddressbookNETFramework.Model
{
    /// <summary>
    /// Содержит в себе рандомайзер для тестов, отвечает за открытие, закрытие браузер, вход. Инициализирует "ApplicationManager", который является ключевым звеном всех тестов.
    /// </summary>
    public class BaseClass
    {
        protected ApplicationManager app;
        public static bool PERFORM_LONG_UI_CHECKS = true;

        /// <summary>
        /// Метод выполняющий инициализацию браузер и логина в систему при каждом обращении к классу.
        /// </summary>
        [SetUp]
        public void SetupApp()
        {
            app = ApplicationManager.GetInstance();
            //TODO Закодировать логин с паролем.
            app.Auth.Login(new AccountData("admin", "secret"));
        }
    }
}