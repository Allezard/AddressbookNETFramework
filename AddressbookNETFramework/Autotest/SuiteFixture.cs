using System;
using System.Text;
using System.Linq;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using AddressbookNETFramework.Helpers;
using AddressbookNETFramework.Model;

namespace AddressbookNETFramework
{
    [SetUpFixture]
    public class SuiteFixture
    {
        //[OneTimeSetUp]
        //public void InitApplicationManager()
        //{
        //    //ApplicationManager.GetInstance().Auth.Login(new AccountData("admin", "secret"));
        //}

        //[OneTimeTearDown]
        //public void StopApplicationManager()
        //{
        //    ApplicationManager.GetInstance().Stop();
        //}

        // Оставил методы для тестов параллельного запуска. Знаю, что не правильно, прошу понять и простить.
    }
}
