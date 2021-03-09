using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using OpenQA.Selenium;
using System.Threading;

namespace AddressbookNETFramework
{
    public class UnitTest
    {
        [Test]
        public void TestMethod1()
        {
            double firstDep = 1000;
            double bonus = 0.9;
            double totalBonus = firstDep - (firstDep * bonus);
            double vipBonus = 0.8;
            double totalVipBonus = firstDep - (firstDep * vipBonus);
            bool vipClien = true;

            if (firstDep >= 1000 && vipClien)
            {
                _ = firstDep - (firstDep * vipBonus);
                Console.Out.Write("Вам начислен бонус в размере 20% от депозита: " + totalVipBonus + " рублей.");
            }
            else if (firstDep >= 1000 || vipClien)
            {
                _ = firstDep - (firstDep * bonus);
                Console.Out.Write("Вам начислен бонус в размере 10% от депозита: " + totalBonus + " рублей.");
            }
            else
            {
                Console.Out.WriteLine("Для начисления бонуса сделайте депозит на сумму 1000 рублей и более, или получите статус VIP.");
            }
        }

        [Test]
        public void TestMethod2()
        {
            string[] s = new string[] { "I", "want", "to", "eat" };

            for (int i = 0; i < s.Length; i++)
            {
                Console.Out.WriteLine(s[i] + "\n");
            }

            foreach (string element in s)
            {
                Console.Out.WriteLine(element + "\n");
            }
        }

        [Test]
        public void TestMethod3()
        {
            IWebDriver webDriver = null;
            int attempt = 0;

            while (webDriver.FindElements(By.Id("test")).Count == 0 && attempt < 60)
            {
                Thread.Sleep(1000);
                attempt++;
            }
        }
    }
}
