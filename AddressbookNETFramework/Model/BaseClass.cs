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

        [SetUp]
        public void SetupApp()
        {
            app = ApplicationManager.GetInstance();
        }

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