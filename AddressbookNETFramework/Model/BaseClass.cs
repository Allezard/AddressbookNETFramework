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
    public class BaseClass
    {
        protected ApplicationManager app;
        public static Random rnd = new Random();
        public static bool PERFORM_LONG_UI_CHECKS = true;

        /// <summary>
        /// Метод выполняющий инициализацию браузер и логина в систему при каждом обращении к классу.
        /// </summary>
        [SetUp]
        public void SetupApp()
        {
            app = ApplicationManager.GetInstance();
            app.Auth.Login(new AccountData("admin", "secret"));
        }

        /// <summary>
        /// Метод генерирующий случайные символы в диапазоне латинского алфавита.
        /// </summary>
        /// <param name="size">Кол-во сгенерированных символов.</param>
        /// <param name="lowerCase">Если true, то маленькие буквы, если false - большие.</param>
        /// <returns>Возвращает преобразованное значение данного экземпляра в текст.</returns>
        public static string GenerateRandomString(int size, bool lowerCase = true)
        {
            StringBuilder builder = new StringBuilder();

            char l;

            for (int i = 0; i < size; i++)
            {
                l = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * rnd.NextDouble() + 65)));
                builder.Append(l);
            }

            if (lowerCase)
                return builder.ToString().ToLower();

            return builder.ToString();
        }
    }
}