using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using OpenQA.Selenium;
using AddressbookNETFramework.Model;

namespace AddressbookNETFramework.Helpers
{
    public class BaseHelper
    {
        protected IWebDriver webDriver;
        protected static string urlLogin = "http://localhost/addressbook/";
        protected static string urlHomePage = "http://localhost/addressbook/";
        protected static string urlGruopList = "http://localhost/addressbook/group.php";

        public BaseHelper(IWebDriver webDriver)
        {
            this.webDriver = webDriver;
        }

        /// <summary>
        /// Метод объединяющий в себе очистку поля и его заполнения если оно не пустое.
        /// </summary>
        /// <param name="locator">Принимает локатор.</param>
        /// <param name="text">Текст для заполнения поля.</param>
        public void EditGropMethod(By locator, string text)
        {
            if (text != null)
            {
                webDriver.FindElement(locator).Clear();
                webDriver.FindElement(locator).SendKeys(text);
            }
        }

        /// <summary>
        /// Проверка наличия "Logout" на странице.
        /// </summary>
        /// <returns>true - если залогинены, false - не залогинены.</returns>
        public bool CheckLoginPresent()
        {
            try
            {
                webDriver.FindElement(By.LinkText("Logout"));
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        /// <summary>
        /// Записывает в переменную "text" логин по которому мы вошли.
        /// </summary>
        /// <returns>Очищенный от "()" логин.</returns>
        public string GetLoggedUserName()
        {
            //string text = webDriver.FindElement(By.Name("Logout")).FindElement(By.TagName("b")).Text;
            string text = webDriver.FindElement(By.TagName("b")).Text;
            return text.Substring(1, text.Length - 2);
        }

        /// <summary>
        /// Проверяем, что мы залогинены в систему и под корректным юзером.
        /// </summary>
        /// <param name="data">Заглушка, для возврата логина.</param>
        /// <returns>true - если залогинены и роль текст логина на главной равен логину входа, false - если одно из условий не выполнено.</returns>
        public bool IsLoggedInUser(AccountData data)
        {
            return CheckLoginPresent()
                && GetLoggedUserName() == data.Username;
        }

        /// <summary>
        /// Проверка на наличие объекта (контакта или группы).
        /// </summary>
        /// <param name="index">Переменная для выбора нужного объекта.</param>
        /// <returns>true - если объект по индексу найден, false - если не найден.</returns>
        public bool CheckElementPresent(int index)
        {
            try
            {
                webDriver.FindElement(By.XPath("(//input[@name='selected[]'])[" + (index + 1) + "]"));
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }
    }
}
