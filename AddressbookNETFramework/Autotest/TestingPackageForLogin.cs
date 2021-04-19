using System;
using System.Text;
using System.Linq;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using AddressbookNETFramework.Helpers;
using AddressbookNETFramework.Model;

namespace AddressbookNETFramework
{
    public class TestingPackageForLogin : BaseClass
    {
        [Test]
        public void LoginWithValidCredentialsTest()
        {
            app.Auth.Logout();
            // Если мы залогинены, выполняем выход из учетной записи.
            AccountData account = new AccountData("admin", "secret");
            app.Auth.Login(account);
            // Для переменной "account" передаем два параметра: "username" и "userpassword". Далее используем их в качестве данных для входа.

            Assert.IsTrue(app.Auth.IsloggedIn());
            // Если мы успешно залогинены, то тест пройден успешно.
        }

        [Test]
        public void LoginWithInvalidCredentialsTest()
        {
            app.Auth.Logout();
            // Если мы залогинены, выполняем выход из учетной записи.
            AccountData account = new AccountData("admin", "test");
            app.Auth.Login(account);
            // Для переменной "account" передаем два параметра: "username" и "userpassword". Далее используем их в качестве данных для входа.

            Assert.IsFalse(app.Auth.IsloggedIn());
            // Если мы не залогинены, то тест пройден успешно.
        }
    }
}
