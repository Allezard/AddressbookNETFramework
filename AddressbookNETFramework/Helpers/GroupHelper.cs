using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using Newtonsoft.Json;
using System.Xml;
using System.Xml.Serialization;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using AddressbookNETFramework.Model;
using System.IO;
using System.Linq;

namespace AddressbookNETFramework.Helpers
{
    public class GroupHelper : BaseHelper
    {
        private List<GroupData> groupCache = null;

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
                navigation.GoToPage("http://localhost/addressbook/group.php");
                ICollection<IWebElement> elements = webDriver.FindElements(By.CssSelector("span.group"));
                foreach (IWebElement element in elements)
                {
                    groupCache.Add(new GroupData()
                    {
                        Id = element.FindElement(By.TagName("input")).GetAttribute("value")
                    });
                }

                string allGroupNames = webDriver.FindElement(By.CssSelector("div#content form")).Text;
                string[] parts = allGroupNames.Split(new string[] { "\n" },
                    StringSplitOptions.RemoveEmptyEntries);
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

        public GroupData CreateNewGroup()
        {
            GroupData generateGroup = GroupDataRandom();

            webDriver.FindElement(By.ClassName("admin")).Click();
            // Переходим во вкладку "groups".да
            webDriver.FindElement(By.Name("new")).Click();
            // Кликаем на кнопку "New group".

            webDriver.FindElement(By.Name("group_name")).SendKeys(generateGroup.GroupName);
            webDriver.FindElement(By.Name("group_header")).SendKeys(generateGroup.GroupHeader);
            webDriver.FindElement(By.Name("group_footer")).SendKeys(generateGroup.GroupFooter);
            // Заполняем поля: "Group name", (Logo), (Comment). 
            webDriver.FindElement(By.Name("submit")).Click();
            // Нажимаем на кнопку "Enter information".
            webDriver.FindElement(By.LinkText("group page")).Click();
            // Возвращаемся на вкладку /addressbook/group по текстовой ссылке "group page".
            groupCache = null;
            // Очищаем кэш.
            return generateGroup;
        }

        public GroupData CreateNewGroupJson()
        {
            GroupData generateGroup = GroupDataRandomJson();

            webDriver.FindElement(By.ClassName("admin")).Click();
            // Переходим во вкладку "groups".
            webDriver.FindElement(By.Name("new")).Click();
            // Кликаем на кнопку "New group".
            webDriver.FindElement(By.Name("group_name")).SendKeys(generateGroup.GroupName);
            webDriver.FindElement(By.Name("group_header")).SendKeys(generateGroup.GroupHeader);
            webDriver.FindElement(By.Name("group_footer")).SendKeys(generateGroup.GroupFooter);
            // Заполняем поля: "Group name", (Logo), (Comment). 
            webDriver.FindElement(By.Name("submit")).Click();
            // Нажимаем на кнопку "Enter information".
            webDriver.FindElement(By.LinkText("group page")).Click();
            // Возвращаемся на вкладку /addressbook/group по текстовой ссылке "group page".
            groupCache = null;
            // Очищаем кэш.
            return generateGroup;
        }

        public GroupData CreateNewGroupXML()
        {
            GroupData generateGroup = GroupDataRandomXML();

            webDriver.FindElement(By.ClassName("admin")).Click();
            // Переходим во вкладку "groups".
            webDriver.FindElement(By.Name("new")).Click();
            // Кликаем на кнопку "New group".
            webDriver.FindElement(By.Name("group_name")).SendKeys(generateGroup.GroupName);
            webDriver.FindElement(By.Name("group_header")).SendKeys(generateGroup.GroupHeader);
            webDriver.FindElement(By.Name("group_footer")).SendKeys(generateGroup.GroupFooter);
            // Заполняем поля: "Group name", (Logo), (Comment). 
            webDriver.FindElement(By.Name("submit")).Click();
            // Нажимаем на кнопку "Enter information".
            webDriver.FindElement(By.LinkText("group page")).Click();
            // Возвращаемся на вкладку /addressbook/group по текстовой ссылке "group page".
            groupCache = null;
            // Очищаем кэш.
            return generateGroup;
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

        public GroupData EditFirstGroup(int index)
        {
            GroupData generateGroup = GroupDataRandom();

            By locatorFooter = By.Name("group_footer");
            string textFooter = generateGroup.GroupFooter;

            webDriver.FindElement(By.ClassName("admin")).Click();
            // Переходим во вкладку "groups".
            webDriver.FindElement(By.XPath("(//input[@name='selected[]'])[" + (index + 1) + "]")).Click();
            webDriver.FindElement(By.Name("edit")).Click();
            // Выбираем и редактируем вторую группу
            EditGropMethod(By.Name("group_name"), generateGroup.GroupName);
            EditGropMethod(By.Name("group_header"), generateGroup.GroupHeader);
            webDriver.FindElement(locatorFooter).Clear();
            webDriver.FindElement(locatorFooter).SendKeys(textFooter);
            // Очищаем и заполняем поля: "Group name", (Logo), (Comment). 
            webDriver.FindElement(By.Name("update")).Click();
            // Нажимаем на кнопку "Update".
            webDriver.FindElement(By.LinkText("group page")).Click();
            // Возвращаемся на вкладку /addressbook/group по текстовой ссылке "group page".
            groupCache = null;
            // Очищаем кэш.
            return generateGroup;
        }

        public GroupData EditFirstGroupJson(int index)
        {
            GroupData generateGroup = GroupDataRandomJson();

            By locatorFooter = By.Name("group_footer");
            string textFooter = generateGroup.GroupFooter;

            webDriver.FindElement(By.ClassName("admin")).Click();
            // Переходим во вкладку "groups".
            webDriver.FindElement(By.XPath("(//input[@name='selected[]'])[" + (index + 1) + "]")).Click();
            webDriver.FindElement(By.Name("edit")).Click();
            // Выбираем и редактируем вторую группу
            EditGropMethod(By.Name("group_name"), generateGroup.GroupName);
            EditGropMethod(By.Name("group_header"), generateGroup.GroupHeader);
            webDriver.FindElement(locatorFooter).Clear();
            webDriver.FindElement(locatorFooter).SendKeys(textFooter);
            // Очищаем и заполняем поля: "Group name", (Logo), (Comment). 
            webDriver.FindElement(By.Name("update")).Click();
            // Нажимаем на кнопку "Update".
            webDriver.FindElement(By.LinkText("group page")).Click();
            // Возвращаемся на вкладку /addressbook/group по текстовой ссылке "group page".
            groupCache = null;
            // Очищаем кэш.
            return generateGroup;
        }

        public GroupData EditFirstGroupXML(int index)
        {
            GroupData generateGroup = GroupDataRandomXML();

            By locatorFooter = By.Name("group_footer");
            string textFooter = generateGroup.GroupFooter;

            webDriver.FindElement(By.ClassName("admin")).Click();
            // Переходим во вкладку "groups".
            webDriver.FindElement(By.XPath("(//input[@name='selected[]'])[" + (index + 1) + "]")).Click();
            webDriver.FindElement(By.Name("edit")).Click();
            // Выбираем и редактируем вторую группу
            EditGropMethod(By.Name("group_name"), generateGroup.GroupName);
            EditGropMethod(By.Name("group_header"), generateGroup.GroupHeader);
            webDriver.FindElement(locatorFooter).Clear();
            webDriver.FindElement(locatorFooter).SendKeys(textFooter);
            // Очищаем и заполняем поля: "Group name", (Logo), (Comment). 
            webDriver.FindElement(By.Name("update")).Click();
            // Нажимаем на кнопку "Update".
            webDriver.FindElement(By.LinkText("group page")).Click();
            // Возвращаемся на вкладку /addressbook/group по текстовой ссылке "group page".
            groupCache = null;
            // Очищаем кэш.
            return generateGroup;
        }

        public void RemoveFirstGroupBD(GroupData group)
        {
            webDriver.FindElement(By.ClassName("admin")).Click();
            // Переходим во вкладку "groups".
            SelectGroupBD(group.Id);
            // Выбираем группу из списка по id.
            webDriver.FindElement(By.Name("delete")).Click();
            // Выбираем и удаляем первую группу
            webDriver.FindElement(By.LinkText("group page")).Click();
            // Возвращаемся на вкладку /addressbook/group по текстовой ссылке "group page".
            groupCache = null;
            // Очищаем кэш.
        }

        public GroupData EditFirstGroupBD(GroupData group)
        {
            GroupData generateGroup = GroupDataRandom();

            By locatorFooter = By.Name("group_footer");
            string textFooter = generateGroup.GroupFooter;

            webDriver.FindElement(By.ClassName("admin")).Click();
            // Переходим во вкладку "groups".
            SelectGroupBD(group.Id);
            // Выбираем группу из списка по id.
            webDriver.FindElement(By.Name("edit")).Click();
            // Выбираем и редактируем вторую группу
            EditGropMethod(By.Name("group_name"), generateGroup.GroupName);
            EditGropMethod(By.Name("group_header"), generateGroup.GroupHeader);
            webDriver.FindElement(locatorFooter).Clear();
            webDriver.FindElement(locatorFooter).SendKeys(textFooter);
            // Очищаем и заполняем поля: "Group name", (Logo), (Comment). 
            webDriver.FindElement(By.Name("update")).Click();
            // Нажимаем на кнопку "Update".
            webDriver.FindElement(By.LinkText("group page")).Click();
            // Возвращаемся на вкладку /addressbook/group по текстовой ссылке "group page".
            groupCache = null;
            // Очищаем кэш.
            return generateGroup;
        }

        public void SelectGroupBD(String id)
        {
            webDriver.FindElement(By.XPath("(//input[@name='selected[]' and @value='" + id + "'])")).Click();
            // Выбираем группу из списка по id.
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

        public GroupData PreAddGroup(int index)
        {
            webDriver.FindElement(By.ClassName("admin")).Click();
            // Переходим во вкладку "groups".
            if (CheckElementPresent(index)) // Ищем первыую группу по индексу.
            {
                return null; // Если группа найдена, то завершаем проверку.
            }
            return CreateNewGroup(); // Создаем новую группу, если элемент в условии "if" не найден.
        }

        public GroupData PreAddGroupJson(int index)
        {
            webDriver.FindElement(By.ClassName("admin")).Click();
            // Переходим во вкладку "groups".
            if (CheckElementPresent(index)) // Ищем первыую группу по индексу.
            {
                return null; // Если группа найдена, то завершаем проверку.
            }
            return CreateNewGroupJson(); // Создаем новую группу, если элемент в условии "if" не найден.
        }

        public GroupData PreAddGroupXML(int index)
        {
            webDriver.FindElement(By.ClassName("admin")).Click();
            // Переходим во вкладку "groups".
            if (CheckElementPresent(index)) // Ищем первыую группу по индексу.
            {
                return null; // Если группа найдена, то завершаем проверку.
            }
            return CreateNewGroupXML(); // Создаем новую группу, если элемент в условии "if" не найден.
        }
    }
}
