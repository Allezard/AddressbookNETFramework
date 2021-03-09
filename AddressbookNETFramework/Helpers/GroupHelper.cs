using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using AddressbookNETFramework.Model;

namespace AddressbookNETFramework.Helpers
{
    public class GroupHelper : BaseHelper
    {
        private List<GroupData> groupCache = null;

        //public bool Id { get; private set; }

        public GroupHelper(IWebDriver webDriver)
            : base(webDriver)
        {
        }

        public List<GroupData> GetGroupList()
        {
            if (groupCache == null)
            {
                groupCache = new List<GroupData>();
                NavigationHelper navigation = new NavigationHelper(webDriver);
                navigation.GoToUrlGroups();
                ICollection<IWebElement> elements = webDriver.FindElements(By.CssSelector("span.group"));
                foreach (IWebElement element in elements)
                {
                    groupCache.Add(new GroupData()
                    {
                        Id = element.FindElement(By.TagName("input")).GetAttribute("value")
                    });
                }

                string allGroupNames = webDriver.FindElement(By.CssSelector("div#content form")).Text;
                string[] parts = allGroupNames.Split("\n");
                int shift = groupCache.Count - parts.Length;
                for (int i = 0; i < groupCache.Count; i++)
                {
                    if (i < shift)
                    {
                        groupCache[i].GroupName = "";
                    }
                    else
                    {
                        groupCache[i].GroupName = parts[i - shift].Trim();
                    }
                }
            }
            return new List<GroupData>(groupCache);
        }

        public int GetGroupCount()
        {
            return webDriver.FindElements(By.CssSelector("span.group")).Count;
        }

        public void CreateNewGroup(GroupData groups)
        {
            webDriver.FindElement(By.ClassName("admin")).Click();
            // Переходим во вкладку "groups".
            webDriver.FindElement(By.Name("new")).Click();
            // Кликаем на кнопку "New group".
            webDriver.FindElement(By.Name("group_name")).SendKeys(groups.GroupName);
            webDriver.FindElement(By.Name("group_header")).SendKeys(groups.GroupHeader);
            webDriver.FindElement(By.Name("group_footer")).SendKeys(groups.GroupFooter);
            // Заполняем поля: "Group name", (Logo), (Comment). 
            webDriver.FindElement(By.Name("submit")).Click();
            // Нажимаем на кнопку "Enter information".
            webDriver.FindElement(By.LinkText("group page")).Click();
            // Возвращаемся на вкладку /addressbook/group по текстовой ссылке "group page".
            groupCache = null;
            // Очищаем кэш.
        }

        public void RemoveFirstGroup(int index)
        {

            webDriver.FindElement(By.ClassName("admin")).Click();
            // Переходим во вкладку "groups".
            webDriver.FindElement(By.XPath("(//input[@name='selected[]'])[" + (index + 1) + "]")).Click();
            webDriver.FindElement(By.Name("delete")).Click();
            // Выбираем и удаляем первую группу
            webDriver.FindElement(By.LinkText("group page")).Click();
            // Возвращаемся на вкладку /addressbook/group по текстовой ссылке "group page".
            groupCache = null;
            // Очищаем кэш.
        }

        public void EditFirstGroup(GroupData groups, int index)
        {
            By locatorFooter = By.Name("group_footer");
            string textFooter = groups.GroupFooter;

            webDriver.FindElement(By.ClassName("admin")).Click();
            // Переходим во вкладку "groups".
            webDriver.FindElement(By.XPath("(//input[@name='selected[]'])[" + (index + 1) + "]")).Click();
            webDriver.FindElement(By.Name("edit")).Click();
            // Выбираем и редактируем вторую группу
            EditGropMethod(By.Name("group_name"), groups.GroupName);
            EditGropMethod(By.Name("group_header"), groups.GroupHeader);
            webDriver.FindElement(locatorFooter).Clear();
            webDriver.FindElement(locatorFooter).SendKeys(textFooter);
            // Очищаем и заполняем поля: "Group name", (Logo), (Comment). 
            webDriver.FindElement(By.Name("update")).Click();
            // Нажимаем на кнопку "Update".
            webDriver.FindElement(By.LinkText("group page")).Click();
            // Возвращаемся на вкладку /addressbook/group по текстовой ссылке "group page".
            groupCache = null;
            // Очищаем кэш.
        }

        public void EditParentSecondGroup(int index)
        {
            webDriver.FindElement(By.ClassName("admin")).Click();
            // Переходим во вкладку "groups".
            webDriver.FindElement(By.XPath("(//input[@name='selected[]'])[" + (index + 1) + "]")).Click();
            webDriver.FindElement(By.Name("edit")).Click();
            webDriver.FindElement(By.Name("group_parent_id")).Click();
            new SelectElement(webDriver.FindElement(By.Name("group_parent_id"))).SelectByText("[none]");
            // Добавляем группу родителя "Parent group". 
            webDriver.FindElement(By.Name("update")).Click();
            // Нажимаем на кнопку "Update".
            webDriver.FindElement(By.LinkText("group page")).Click();
            // Возвращаемся на вкладку /addressbook/group по текстовой ссылке "group page".
            groupCache = null;
            // Очищаем кэш.
        }

        public void PreAddGroup(GroupData groups, int index)
        {
            webDriver.FindElement(By.ClassName("admin")).Click();
            // Переходим во вкладку "groups".
            if (IsElementFound(index))
            {
                return;
            }
            CreateNewGroup(groups);
        }
    }
}
