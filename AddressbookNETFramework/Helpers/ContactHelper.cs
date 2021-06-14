using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using AddressbookNETFramework.Model;

namespace AddressbookNETFramework.Helpers
{
    public class ContactHelper : BaseHelper
    {
        private List<ContactData> contactCache = null;

        public ContactHelper(IWebDriver webDriver)
            : base(webDriver)
        {

        }

        public List<ContactData> GetContactList()
        {
            if (contactCache == null)
            {
                contactCache = new List<ContactData>();
                NavigationHelper navigation = new NavigationHelper(webDriver);
                navigation.GoToPage("http://localhost/addressbook/");
                ICollection<IWebElement> elements = webDriver.FindElements(By.Name("entry"));
                foreach (IWebElement element in elements)
                {
                    ContactData contact = new ContactData();
                    var elementTd = element.FindElements(By.CssSelector("td"));
                    string textFirstN = elementTd[2].Text;
                    contact.FirstName = textFirstN;
                    string textLastN = elementTd[1].Text;
                    contact.LastName = textLastN;
                    string textFullName = elementTd[1].Text + elementTd[2].Text;
                    contact.FullName = textFullName;
                    contactCache.Add(contact);
                }
            }
            return new List<ContactData>(contactCache);
        }

        public int GetContactCount()
        {
            return webDriver.FindElements(By.Name("entry")).Count;
        }

        public ContactData GetContactInfoFromEditForm(int index)
        {
            webDriver.FindElement(By.LinkText("home")).Click();
            // Переходим на главную страницу со списком контактов.
            webDriver.FindElements(By.Name("entry"))[index]
                .FindElements(By.TagName("td"))[7]
                .FindElement(By.TagName("a")).Click();
            // Переходим в редактирование выбранного контакта.

            return new ContactData()
            {
                FirstName = webDriver.FindElement(By.Name("firstname")).GetAttribute("value"),
                LastName = webDriver.FindElement(By.Name("lastname")).GetAttribute("value"),
                Address = webDriver.FindElement(By.Name("address")).GetAttribute("value"),
                HomePhone = webDriver.FindElement(By.Name("home")).GetAttribute("value"),
                MobilePhone = webDriver.FindElement(By.Name("mobile")).GetAttribute("value"),
                WorkPhone = webDriver.FindElement(By.Name("work")).GetAttribute("value"),
                HomeAddress = webDriver.FindElement(By.Name("phone2")).GetAttribute("value"),
                Email = webDriver.FindElement(By.Name("email")).GetAttribute("value"),
                Email2 = webDriver.FindElement(By.Name("email2")).GetAttribute("value"),
                Email3 = webDriver.FindElement(By.Name("email3")).GetAttribute("value")
            };
        }

        public ContactData GetContactInfoFromTable(int index)
        {
            webDriver.FindElement(By.LinkText("home")).Click();
            // Переходим на главную страницу со списком контактов.

            IList<IWebElement> cells = webDriver.FindElements(By.Name("entry"))[index]
                .FindElements(By.TagName("td"));

            return new ContactData()
            {
                LastName = cells[1].Text,
                FirstName = cells[2].Text,
                Address = cells[3].Text,
                AllEmails = cells[4].Text,
                AllPhones = cells[5].Text
            };
        }

        public ContactData GetContactDetailsFromEditForm(int index)
        {
            webDriver.FindElement(By.LinkText("home")).Click();
            // Переходим на главную страницу со списком контактов.
            webDriver.FindElements(By.Name("entry"))[index]
                .FindElements(By.TagName("td"))[7]
                .FindElement(By.TagName("a")).Click();
            // Переходим в редактирование выбранного контакта.

            return new ContactData()
            {
                FirstName = webDriver.FindElement(By.Name("firstname")).GetAttribute("value"),
                MiddleName = webDriver.FindElement(By.Name("middlename")).GetAttribute("value"),
                LastName = webDriver.FindElement(By.Name("lastname")).GetAttribute("value"),
                NickName = webDriver.FindElement(By.Name("nickname")).GetAttribute("value"),
                Company = webDriver.FindElement(By.Name("company")).GetAttribute("value"),
                Title = webDriver.FindElement(By.Name("title")).GetAttribute("value"),
                Address = webDriver.FindElement(By.Name("address")).GetAttribute("value"),
                HomePhone = webDriver.FindElement(By.Name("home")).GetAttribute("value"),
                MobilePhone = webDriver.FindElement(By.Name("mobile")).GetAttribute("value"),
                WorkPhone = webDriver.FindElement(By.Name("work")).GetAttribute("value"),
                Fax = webDriver.FindElement(By.Name("fax")).GetAttribute("value"),
                Email = webDriver.FindElement(By.Name("email")).GetAttribute("value"),
                Email2 = webDriver.FindElement(By.Name("email2")).GetAttribute("value"),
                Email3 = webDriver.FindElement(By.Name("email3")).GetAttribute("value"),
                BirthDay = webDriver.FindElement(By.Name("bday")).GetAttribute("value"),
                BirthMonth = webDriver.FindElement(By.Name("bmonth")).GetAttribute("value"),
                YearOfBirth = webDriver.FindElement(By.Name("byear")).GetAttribute("value"),
                AnniversDay = webDriver.FindElement(By.Name("aday")).GetAttribute("value"),
                AnniversMonth = webDriver.FindElement(By.Name("amonth")).GetAttribute("value"),
                YearOfAnnivers = webDriver.FindElement(By.Name("ayear")).GetAttribute("value"),
                Homepage = webDriver.FindElement(By.Name("homepage")).GetAttribute("value"),
                SecondaryAddress = webDriver.FindElement(By.Name("address2")).GetAttribute("value"),
                HomeAddress = webDriver.FindElement(By.Name("phone2")).GetAttribute("value"),
                Notes = webDriver.FindElement(By.Name("notes")).GetAttribute("value")
            };
        }

        public ContactData GetContactDetailsFormTable(int index)
        {
            webDriver.FindElement(By.LinkText("home")).Click();
            // Переходим на главную страницу со списком контактов.
            webDriver.FindElements(By.Name("entry"))[index]
                .FindElements(By.TagName("td"))[6]
                .FindElement(By.TagName("a")).Click();
            // Переходим в редактирование выбранного контакта.

            IList<IWebElement> cells = webDriver.FindElements(By.Id("content"));

            string allDetails = cells[0].Text;

            return new ContactData()
            {
                AllDetails = allDetails
            };
        }

        public ContactData AddNewContact()
        {
            ContactData generateContacntData = ContactDataRandom();

            webDriver.FindElement(By.LinkText("add new")).Click();
            // Переходим на страницу для создания контакта.
            webDriver.FindElement(By.Name("email")).SendKeys(generateContacntData.Email);
            webDriver.FindElement(By.Name("email2")).SendKeys(generateContacntData.Email2);
            webDriver.FindElement(By.Name("email3")).SendKeys(generateContacntData.Email3);
            webDriver.FindElement(By.Name("firstname")).SendKeys(generateContacntData.FirstName);
            webDriver.FindElement(By.Name("middlename")).SendKeys(generateContacntData.MiddleName);
            webDriver.FindElement(By.Name("lastname")).SendKeys(generateContacntData.LastName);
            webDriver.FindElement(By.Name("nickname")).SendKeys(generateContacntData.NickName);
            webDriver.FindElement(By.Name("company")).SendKeys(generateContacntData.Company);
            webDriver.FindElement(By.Name("title")).SendKeys(generateContacntData.Title);
            webDriver.FindElement(By.Name("address")).SendKeys(generateContacntData.Address);
            webDriver.FindElement(By.Name("home")).SendKeys(generateContacntData.HomePhone);
            webDriver.FindElement(By.Name("mobile")).SendKeys(generateContacntData.MobilePhone);
            webDriver.FindElement(By.Name("work")).SendKeys(generateContacntData.WorkPhone);
            webDriver.FindElement(By.Name("fax")).SendKeys(generateContacntData.Fax);
            webDriver.FindElement(By.Name("homepage")).SendKeys(generateContacntData.Homepage);
            // Заполняем личные данные.
            webDriver.FindElement(By.Name("bday")).Click();
            new SelectElement(webDriver.FindElement(By.Name("bday"))).SelectByText(generateContacntData.BirthDay);
            webDriver.FindElement(By.Name("bmonth")).Click();
            new SelectElement(webDriver.FindElement(By.Name("bmonth"))).SelectByText(generateContacntData.BirthMonth);
            webDriver.FindElement(By.Name("byear")).SendKeys(generateContacntData.YearOfBirth);
            // Указываем день, месяц, год рождения.
            webDriver.FindElement(By.Name("aday")).Click();
            new SelectElement(webDriver.FindElement(By.Name("aday"))).SelectByText(generateContacntData.AnniversDay);
            webDriver.FindElement(By.Name("amonth")).Click();
            new SelectElement(webDriver.FindElement(By.Name("amonth"))).SelectByText(generateContacntData.AnniversMonth);
            webDriver.FindElement(By.Name("ayear")).SendKeys(generateContacntData.YearOfAnnivers);
            // Указываем день, месяц, год годовщины.
            webDriver.FindElement(By.Name("new_group")).Click();
            new SelectElement(webDriver.FindElement(By.Name("new_group"))).SelectByText("[none]");
            // Выбираем ранее созданную группу.
            webDriver.FindElement(By.Name("address2")).SendKeys(generateContacntData.SecondaryAddress);
            webDriver.FindElement(By.Name("phone2")).SendKeys(generateContacntData.HomeAddress);
            webDriver.FindElement(By.Name("notes")).SendKeys(generateContacntData.Notes);
            webDriver.FindElement(By.Name("submit")).Submit();
            // Добавляем вторичные личные данные.
            webDriver.FindElement(By.LinkText("home")).Click();
            // Возвращаемся на главную страницу (контакты) не дожидаясь редиректа.
            contactCache = null;
            // Очищаем кэш.
            return generateContacntData;
        }

        public ContactData AddNewContactJson()
        {
            ContactData generateContacntData = ContactDataRandomJson();

            webDriver.FindElement(By.LinkText("add new")).Click();
            // Переходим на страницу для создания контакта.
            webDriver.FindElement(By.Name("email")).SendKeys(generateContacntData.Email);
            webDriver.FindElement(By.Name("email2")).SendKeys(generateContacntData.Email2);
            webDriver.FindElement(By.Name("email3")).SendKeys(generateContacntData.Email3);
            webDriver.FindElement(By.Name("firstname")).SendKeys(generateContacntData.FirstName);
            webDriver.FindElement(By.Name("middlename")).SendKeys(generateContacntData.MiddleName);
            webDriver.FindElement(By.Name("lastname")).SendKeys(generateContacntData.LastName);
            webDriver.FindElement(By.Name("nickname")).SendKeys(generateContacntData.NickName);
            webDriver.FindElement(By.Name("company")).SendKeys(generateContacntData.Company);
            webDriver.FindElement(By.Name("title")).SendKeys(generateContacntData.Title);
            webDriver.FindElement(By.Name("address")).SendKeys(generateContacntData.Address);
            webDriver.FindElement(By.Name("home")).SendKeys(generateContacntData.HomePhone);
            webDriver.FindElement(By.Name("mobile")).SendKeys(generateContacntData.MobilePhone);
            webDriver.FindElement(By.Name("work")).SendKeys(generateContacntData.WorkPhone);
            webDriver.FindElement(By.Name("fax")).SendKeys(generateContacntData.Fax);
            webDriver.FindElement(By.Name("homepage")).SendKeys(generateContacntData.Homepage);
            // Заполняем личные данные.
            webDriver.FindElement(By.Name("bday")).Click();
            new SelectElement(webDriver.FindElement(By.Name("bday"))).SelectByText(generateContacntData.BirthDay);
            webDriver.FindElement(By.Name("bmonth")).Click();
            new SelectElement(webDriver.FindElement(By.Name("bmonth"))).SelectByText(generateContacntData.BirthMonth);
            webDriver.FindElement(By.Name("byear")).SendKeys(generateContacntData.YearOfBirth);
            // Указываем день, месяц, год рождения.
            webDriver.FindElement(By.Name("aday")).Click();
            new SelectElement(webDriver.FindElement(By.Name("aday"))).SelectByText(generateContacntData.AnniversDay);
            webDriver.FindElement(By.Name("amonth")).Click();
            new SelectElement(webDriver.FindElement(By.Name("amonth"))).SelectByText(generateContacntData.AnniversMonth);
            webDriver.FindElement(By.Name("ayear")).SendKeys(generateContacntData.YearOfAnnivers);
            // Указываем день, месяц, год годовщины.
            webDriver.FindElement(By.Name("new_group")).Click();
            new SelectElement(webDriver.FindElement(By.Name("new_group"))).SelectByText("[none]");
            // Выбираем ранее созданную группу.
            webDriver.FindElement(By.Name("address2")).SendKeys(generateContacntData.SecondaryAddress);
            webDriver.FindElement(By.Name("phone2")).SendKeys(generateContacntData.HomeAddress);
            webDriver.FindElement(By.Name("notes")).SendKeys(generateContacntData.Notes);
            webDriver.FindElement(By.Name("submit")).Submit();
            // Добавляем вторичные личные данные.
            webDriver.FindElement(By.LinkText("home")).Click();
            // Возвращаемся на главную страницу (контакты) не дожидаясь редиректа.
            contactCache = null;
            // Очищаем кэш.
            return generateContacntData;
        }

        public ContactData AddNewContactXML()
        {
            ContactData generateContacntData = ContactDataRandomXML();

            webDriver.FindElement(By.LinkText("add new")).Click();
            // Переходим на страницу для создания контакта.
            webDriver.FindElement(By.Name("email")).SendKeys(generateContacntData.Email);
            webDriver.FindElement(By.Name("email2")).SendKeys(generateContacntData.Email2);
            webDriver.FindElement(By.Name("email3")).SendKeys(generateContacntData.Email3);
            webDriver.FindElement(By.Name("firstname")).SendKeys(generateContacntData.FirstName);
            webDriver.FindElement(By.Name("middlename")).SendKeys(generateContacntData.MiddleName);
            webDriver.FindElement(By.Name("lastname")).SendKeys(generateContacntData.LastName);
            webDriver.FindElement(By.Name("nickname")).SendKeys(generateContacntData.NickName);
            webDriver.FindElement(By.Name("company")).SendKeys(generateContacntData.Company);
            webDriver.FindElement(By.Name("title")).SendKeys(generateContacntData.Title);
            webDriver.FindElement(By.Name("address")).SendKeys(generateContacntData.Address);
            webDriver.FindElement(By.Name("home")).SendKeys(generateContacntData.HomePhone);
            webDriver.FindElement(By.Name("mobile")).SendKeys(generateContacntData.MobilePhone);
            webDriver.FindElement(By.Name("work")).SendKeys(generateContacntData.WorkPhone);
            webDriver.FindElement(By.Name("fax")).SendKeys(generateContacntData.Fax);
            webDriver.FindElement(By.Name("homepage")).SendKeys(generateContacntData.Homepage);
            // Заполняем личные данные.
            webDriver.FindElement(By.Name("bday")).Click();
            new SelectElement(webDriver.FindElement(By.Name("bday"))).SelectByText(generateContacntData.BirthDay);
            webDriver.FindElement(By.Name("bmonth")).Click();
            new SelectElement(webDriver.FindElement(By.Name("bmonth"))).SelectByText(generateContacntData.BirthMonth);
            webDriver.FindElement(By.Name("byear")).SendKeys(generateContacntData.YearOfBirth);
            // Указываем день, месяц, год рождения.
            webDriver.FindElement(By.Name("aday")).Click();
            new SelectElement(webDriver.FindElement(By.Name("aday"))).SelectByText(generateContacntData.AnniversDay);
            webDriver.FindElement(By.Name("amonth")).Click();
            new SelectElement(webDriver.FindElement(By.Name("amonth"))).SelectByText(generateContacntData.AnniversMonth);
            webDriver.FindElement(By.Name("ayear")).SendKeys(generateContacntData.YearOfAnnivers);
            // Указываем день, месяц, год годовщины.
            webDriver.FindElement(By.Name("new_group")).Click();
            new SelectElement(webDriver.FindElement(By.Name("new_group"))).SelectByText("[none]");
            // Выбираем ранее созданную группу.
            webDriver.FindElement(By.Name("address2")).SendKeys(generateContacntData.SecondaryAddress);
            webDriver.FindElement(By.Name("phone2")).SendKeys(generateContacntData.HomeAddress);
            webDriver.FindElement(By.Name("notes")).SendKeys(generateContacntData.Notes);
            webDriver.FindElement(By.Name("submit")).Submit();
            // Добавляем вторичные личные данные.
            webDriver.FindElement(By.LinkText("home")).Click();
            // Возвращаемся на главную страницу (контакты) не дожидаясь редиректа.
            contactCache = null;
            // Очищаем кэш.
            return generateContacntData;
        }

        public ContactData EditFirstContact(int index)
        {
            ContactData generateContacntData = ContactDataRandom();

            webDriver.FindElement(By.LinkText("home")).Click();
            // Переходим на главную страницу со списком контактов.
            // Делаем проверку на наличии контакта, если его нет, то создаем и повторяем тест.
            webDriver.FindElements(By.Name("entry"))[index]
                    .FindElements(By.TagName("td"))[7]
                    .FindElement(By.TagName("a")).Click();
            // Переходим в редактирование выбранного контакта.
            webDriver.FindElement(By.Name("firstname")).Clear();
            webDriver.FindElement(By.Name("firstname")).SendKeys(generateContacntData.FirstName);
            webDriver.FindElement(By.Name("middlename")).Clear();
            webDriver.FindElement(By.Name("middlename")).SendKeys(generateContacntData.MiddleName);
            webDriver.FindElement(By.Name("lastname")).Clear();
            webDriver.FindElement(By.Name("lastname")).SendKeys(generateContacntData.LastName);
            webDriver.FindElement(By.Name("nickname")).Clear();
            webDriver.FindElement(By.Name("nickname")).SendKeys(generateContacntData.NickName);
            webDriver.FindElement(By.Name("company")).Clear();
            webDriver.FindElement(By.Name("company")).SendKeys(generateContacntData.Company);
            webDriver.FindElement(By.Name("title")).Clear();
            webDriver.FindElement(By.Name("title")).SendKeys(generateContacntData.Title);
            webDriver.FindElement(By.Name("address")).Clear();
            webDriver.FindElement(By.Name("address")).SendKeys(generateContacntData.Address);
            webDriver.FindElement(By.Name("home")).Clear();
            webDriver.FindElement(By.Name("home")).SendKeys(generateContacntData.HomePhone);
            webDriver.FindElement(By.Name("mobile")).Clear();
            webDriver.FindElement(By.Name("mobile")).SendKeys(generateContacntData.MobilePhone);
            webDriver.FindElement(By.Name("work")).Clear();
            webDriver.FindElement(By.Name("work")).SendKeys(generateContacntData.WorkPhone);
            webDriver.FindElement(By.Name("fax")).Clear();
            webDriver.FindElement(By.Name("fax")).SendKeys(generateContacntData.Fax);
            webDriver.FindElement(By.Name("email")).Clear();
            webDriver.FindElement(By.Name("email")).SendKeys(generateContacntData.Email);
            webDriver.FindElement(By.Name("email2")).Clear();
            webDriver.FindElement(By.Name("email2")).SendKeys(generateContacntData.Email2);
            webDriver.FindElement(By.Name("email3")).Clear();
            webDriver.FindElement(By.Name("email3")).SendKeys(generateContacntData.Email3);
            webDriver.FindElement(By.Name("homepage")).Clear();
            webDriver.FindElement(By.Name("homepage")).SendKeys(generateContacntData.Homepage);
            // Редактируем данные.
            webDriver.FindElement(By.Name("bday")).Click();
            new SelectElement(webDriver.FindElement(By.Name("bday"))).SelectByText(generateContacntData.BirthDay);
            webDriver.FindElement(By.Name("bmonth")).Click();
            new SelectElement(webDriver.FindElement(By.Name("bmonth"))).SelectByText(generateContacntData.BirthMonth);
            webDriver.FindElement(By.Name("byear")).Clear();
            webDriver.FindElement(By.Name("byear")).SendKeys(generateContacntData.YearOfBirth);
            // Указываем день, месяц, год рождения.
            webDriver.FindElement(By.Name("aday")).Click();
            new SelectElement(webDriver.FindElement(By.Name("aday"))).SelectByText(generateContacntData.AnniversDay);
            webDriver.FindElement(By.Name("amonth")).Click();
            new SelectElement(webDriver.FindElement(By.Name("amonth"))).SelectByText(generateContacntData.AnniversMonth);
            webDriver.FindElement(By.Name("ayear")).Clear();
            webDriver.FindElement(By.Name("ayear")).SendKeys(generateContacntData.YearOfAnnivers);
            // Указываем день, месяц, год годовщины.
            webDriver.FindElement(By.Name("address2")).Clear();
            webDriver.FindElement(By.Name("address2")).SendKeys(generateContacntData.SecondaryAddress);
            webDriver.FindElement(By.Name("phone2")).Clear();
            webDriver.FindElement(By.Name("phone2")).SendKeys(generateContacntData.HomeAddress);
            webDriver.FindElement(By.Name("notes")).Clear();
            webDriver.FindElement(By.Name("notes")).SendKeys(generateContacntData.Notes);
            webDriver.FindElement(By.Name("update")).Click();
            // Добавляем вторичные личные данные.
            webDriver.FindElement(By.LinkText("home")).Click();
            // Возвращаемся на главную страницу (контакты) не дожидаясь редиректа.
            contactCache = null;
            // Очищаем кэш.
            return generateContacntData;
        }

        public ContactData EditFirstContactJson(int index)
        {
            ContactData generateContacntData = ContactDataRandomJson();

            webDriver.FindElement(By.LinkText("home")).Click();
            // Переходим на главную страницу со списком контактов.
            // Делаем проверку на наличии контакта, если его нет, то создаем и повторяем тест.
            webDriver.FindElements(By.Name("entry"))[index]
                    .FindElements(By.TagName("td"))[7]
                    .FindElement(By.TagName("a")).Click();
            // Переходим в редактирование выбранного контакта.
            webDriver.FindElement(By.Name("firstname")).Clear();
            webDriver.FindElement(By.Name("firstname")).SendKeys(generateContacntData.FirstName);
            webDriver.FindElement(By.Name("middlename")).Clear();
            webDriver.FindElement(By.Name("middlename")).SendKeys(generateContacntData.MiddleName);
            webDriver.FindElement(By.Name("lastname")).Clear();
            webDriver.FindElement(By.Name("lastname")).SendKeys(generateContacntData.LastName);
            webDriver.FindElement(By.Name("nickname")).Clear();
            webDriver.FindElement(By.Name("nickname")).SendKeys(generateContacntData.NickName);
            webDriver.FindElement(By.Name("company")).Clear();
            webDriver.FindElement(By.Name("company")).SendKeys(generateContacntData.Company);
            webDriver.FindElement(By.Name("title")).Clear();
            webDriver.FindElement(By.Name("title")).SendKeys(generateContacntData.Title);
            webDriver.FindElement(By.Name("address")).Clear();
            webDriver.FindElement(By.Name("address")).SendKeys(generateContacntData.Address);
            webDriver.FindElement(By.Name("home")).Clear();
            webDriver.FindElement(By.Name("home")).SendKeys(generateContacntData.HomePhone);
            webDriver.FindElement(By.Name("mobile")).Clear();
            webDriver.FindElement(By.Name("mobile")).SendKeys(generateContacntData.MobilePhone);
            webDriver.FindElement(By.Name("work")).Clear();
            webDriver.FindElement(By.Name("work")).SendKeys(generateContacntData.WorkPhone);
            webDriver.FindElement(By.Name("fax")).Clear();
            webDriver.FindElement(By.Name("fax")).SendKeys(generateContacntData.Fax);
            webDriver.FindElement(By.Name("email")).Clear();
            webDriver.FindElement(By.Name("email")).SendKeys(generateContacntData.Email);
            webDriver.FindElement(By.Name("email2")).Clear();
            webDriver.FindElement(By.Name("email2")).SendKeys(generateContacntData.Email2);
            webDriver.FindElement(By.Name("email3")).Clear();
            webDriver.FindElement(By.Name("email3")).SendKeys(generateContacntData.Email3);
            webDriver.FindElement(By.Name("homepage")).Clear();
            webDriver.FindElement(By.Name("homepage")).SendKeys(generateContacntData.Homepage);
            // Редактируем данные.
            webDriver.FindElement(By.Name("bday")).Click();
            new SelectElement(webDriver.FindElement(By.Name("bday"))).SelectByText(generateContacntData.BirthDay);
            webDriver.FindElement(By.Name("bmonth")).Click();
            new SelectElement(webDriver.FindElement(By.Name("bmonth"))).SelectByText(generateContacntData.BirthMonth);
            webDriver.FindElement(By.Name("byear")).Clear();
            webDriver.FindElement(By.Name("byear")).SendKeys(generateContacntData.YearOfBirth);
            // Указываем день, месяц, год рождения.
            webDriver.FindElement(By.Name("aday")).Click();
            new SelectElement(webDriver.FindElement(By.Name("aday"))).SelectByText(generateContacntData.AnniversDay);
            webDriver.FindElement(By.Name("amonth")).Click();
            new SelectElement(webDriver.FindElement(By.Name("amonth"))).SelectByText(generateContacntData.AnniversMonth);
            webDriver.FindElement(By.Name("ayear")).Clear();
            webDriver.FindElement(By.Name("ayear")).SendKeys(generateContacntData.YearOfAnnivers);
            // Указываем день, месяц, год годовщины.
            webDriver.FindElement(By.Name("address2")).Clear();
            webDriver.FindElement(By.Name("address2")).SendKeys(generateContacntData.SecondaryAddress);
            webDriver.FindElement(By.Name("phone2")).Clear();
            webDriver.FindElement(By.Name("phone2")).SendKeys(generateContacntData.HomeAddress);
            webDriver.FindElement(By.Name("notes")).Clear();
            webDriver.FindElement(By.Name("notes")).SendKeys(generateContacntData.Notes);
            webDriver.FindElement(By.Name("update")).Click();
            // Добавляем вторичные личные данные.
            webDriver.FindElement(By.LinkText("home")).Click();
            // Возвращаемся на главную страницу (контакты) не дожидаясь редиректа.
            contactCache = null;
            // Очищаем кэш.
            return generateContacntData;
        }

        public ContactData EditFirstContactXML(int index)
        {
            ContactData generateContacntData = ContactDataRandomXML();

            webDriver.FindElement(By.LinkText("home")).Click();
            // Переходим на главную страницу со списком контактов.
            // Делаем проверку на наличии контакта, если его нет, то создаем и повторяем тест.
            webDriver.FindElements(By.Name("entry"))[index]
                    .FindElements(By.TagName("td"))[7]
                    .FindElement(By.TagName("a")).Click();
            // Переходим в редактирование выбранного контакта.
            webDriver.FindElement(By.Name("firstname")).Clear();
            webDriver.FindElement(By.Name("firstname")).SendKeys(generateContacntData.FirstName);
            webDriver.FindElement(By.Name("middlename")).Clear();
            webDriver.FindElement(By.Name("middlename")).SendKeys(generateContacntData.MiddleName);
            webDriver.FindElement(By.Name("lastname")).Clear();
            webDriver.FindElement(By.Name("lastname")).SendKeys(generateContacntData.LastName);
            webDriver.FindElement(By.Name("nickname")).Clear();
            webDriver.FindElement(By.Name("nickname")).SendKeys(generateContacntData.NickName);
            webDriver.FindElement(By.Name("company")).Clear();
            webDriver.FindElement(By.Name("company")).SendKeys(generateContacntData.Company);
            webDriver.FindElement(By.Name("title")).Clear();
            webDriver.FindElement(By.Name("title")).SendKeys(generateContacntData.Title);
            webDriver.FindElement(By.Name("address")).Clear();
            webDriver.FindElement(By.Name("address")).SendKeys(generateContacntData.Address);
            webDriver.FindElement(By.Name("home")).Clear();
            webDriver.FindElement(By.Name("home")).SendKeys(generateContacntData.HomePhone);
            webDriver.FindElement(By.Name("mobile")).Clear();
            webDriver.FindElement(By.Name("mobile")).SendKeys(generateContacntData.MobilePhone);
            webDriver.FindElement(By.Name("work")).Clear();
            webDriver.FindElement(By.Name("work")).SendKeys(generateContacntData.WorkPhone);
            webDriver.FindElement(By.Name("fax")).Clear();
            webDriver.FindElement(By.Name("fax")).SendKeys(generateContacntData.Fax);
            webDriver.FindElement(By.Name("email")).Clear();
            webDriver.FindElement(By.Name("email")).SendKeys(generateContacntData.Email);
            webDriver.FindElement(By.Name("email2")).Clear();
            webDriver.FindElement(By.Name("email2")).SendKeys(generateContacntData.Email2);
            webDriver.FindElement(By.Name("email3")).Clear();
            webDriver.FindElement(By.Name("email3")).SendKeys(generateContacntData.Email3);
            webDriver.FindElement(By.Name("homepage")).Clear();
            webDriver.FindElement(By.Name("homepage")).SendKeys(generateContacntData.Homepage);
            // Редактируем данные.
            webDriver.FindElement(By.Name("bday")).Click();
            new SelectElement(webDriver.FindElement(By.Name("bday"))).SelectByText(generateContacntData.BirthDay);
            webDriver.FindElement(By.Name("bmonth")).Click();
            new SelectElement(webDriver.FindElement(By.Name("bmonth"))).SelectByText(generateContacntData.BirthMonth);
            webDriver.FindElement(By.Name("byear")).Clear();
            webDriver.FindElement(By.Name("byear")).SendKeys(generateContacntData.YearOfBirth);
            // Указываем день, месяц, год рождения.
            webDriver.FindElement(By.Name("aday")).Click();
            new SelectElement(webDriver.FindElement(By.Name("aday"))).SelectByText(generateContacntData.AnniversDay);
            webDriver.FindElement(By.Name("amonth")).Click();
            new SelectElement(webDriver.FindElement(By.Name("amonth"))).SelectByText(generateContacntData.AnniversMonth);
            webDriver.FindElement(By.Name("ayear")).Clear();
            webDriver.FindElement(By.Name("ayear")).SendKeys(generateContacntData.YearOfAnnivers);
            // Указываем день, месяц, год годовщины.
            webDriver.FindElement(By.Name("address2")).Clear();
            webDriver.FindElement(By.Name("address2")).SendKeys(generateContacntData.SecondaryAddress);
            webDriver.FindElement(By.Name("phone2")).Clear();
            webDriver.FindElement(By.Name("phone2")).SendKeys(generateContacntData.HomeAddress);
            webDriver.FindElement(By.Name("notes")).Clear();
            webDriver.FindElement(By.Name("notes")).SendKeys(generateContacntData.Notes);
            webDriver.FindElement(By.Name("update")).Click();
            // Добавляем вторичные личные данные.
            webDriver.FindElement(By.LinkText("home")).Click();
            // Возвращаемся на главную страницу (контакты) не дожидаясь редиректа.
            contactCache = null;
            // Очищаем кэш.
            return generateContacntData;
        }

        public void DeleteFirstContact(int index)
        {
            // Делаем проверку на наличии контакта, если его нет, то создаем и повторяем тест.
            webDriver.FindElement(By.LinkText("home")).Click();
            // Переходим на главную страницу со списком контактов.
            webDriver.FindElement(By.XPath("(//input[@name='selected[]'])[" + (index + 1) + "]")).Click();
            // Ищем первый контакт и проставляем в нем чек-бокс.
            webDriver.FindElement(By.XPath("(//input[@value='Delete'])")).Click();
            // Ищем кнопку "Delete", нажимаем на нее.
            webDriver.SwitchTo().Alert().Accept();
            // Подтверждаем удаление в всплывающем окне.
            webDriver.FindElement(By.LinkText("home")).Click();
            // Возвращаемся на главную страницу (контакты) не дожидаясь редиректа.
            contactCache = null;
            // Очищаем кэш.
        }

        public void AddContactInGroup(int index)
        {
            webDriver.FindElement(By.LinkText("home")).Click();
            // Переходим на главную страницу со списком контактов.
            webDriver.FindElement(By.XPath("(//input[@name='selected[]'])[" + (index + 1) + "]")).Click();
            // Выбираем первый контакт.
            webDriver.FindElement(By.CssSelector("div.right input")).Click();
            // Добавляем контакт в случайную группу.
            webDriver.FindElement(By.CssSelector("div.msgbox a")).Click();
            // Переходим в раздел "contacts" (выставлен фильтр группы, которую мы присвоили).
            webDriver.FindElement(By.XPath("(//input[@name='selected[]'])[" + (index + 1) + "]")).Click();
            // Выбираем первый контакт.
            webDriver.FindElement(By.Name("remove")).Click();
            // Выбираем первый контакт.
            webDriver.FindElement(By.CssSelector("div.msgbox a")).Click();
            // Переходим в раздел "contacts" (выставлен фильтр группы, которую мы удалили).
            webDriver.FindElement(By.CssSelector("form#right")).Click();
            // Кликаем по селектору со списком групп.
            webDriver.FindElement(By.XPath("/html/body/div/div[4]/form[1]/select/option[2]")).Click();
            // Возвращаем видимость всех контактов. 
        }

        public void AddContactToGroupDB(ContactData contact, GroupData group)
        {
            webDriver.FindElement(By.LinkText("home")).Click();
            // Переходим на главную страницу со списком контактов.
            ClearGroupFilter();
            SelectContact(contact.Id);
            SelectGroupToAdd(group.GroupName);
            CommitAddingContactToGroup();
            //new WebDriverWait(webDriver, TimeSpan.FromSeconds(10))
            //    .Until(d => d.FindElement(By.CssSelector("div.msgbox")));
            webDriver.FindElement(By.CssSelector("div.msgbox")).Click();
        }

        public void RemoveContactFromGroupDB(ContactData contact, GroupData group)
        {
            webDriver.FindElement(By.LinkText("home")).Click();
            // Переходим на главную страницу со списком контактов.
            FindGroupFilter(group.GroupName);
            SelectContact(contact.Id);
            RemoveContactToGroup();
            webDriver.FindElement(By.CssSelector("div.msgbox")).Click();
        }

        public void ClearGroupFilter()
        {
            new SelectElement(webDriver.FindElement(By.Name("group"))).SelectByText("[all]");
            // В селекторе групп выбираем [all].
        }

        public void FindGroupFilter(string groupName)
        {
            new SelectElement(webDriver.FindElement(By.Name("group"))).SelectByText(groupName);
        }

        public void SelectContact(string contactId)
        {
            webDriver.FindElement(By.Id(contactId)).Click();
        }

        public void SelectGroupToAdd(string groupName)
        {
            new SelectElement(webDriver.FindElement(By.Name("to_group"))).SelectByText(groupName);
        }

        public void CommitAddingContactToGroup()
        {
            webDriver.FindElement(By.Name("add")).Click();
        }

        public void RemoveContactToGroup()
        {
            webDriver.FindElement(By.Name("remove")).Click();
        }

        public int GetNumberOfSearchResults()
        {
            webDriver.FindElement(By.LinkText("home")).Click();
            // Переходим на главную страницу со списком контактов.
            string numberOfResult = webDriver.FindElement(By.TagName("label")).Text;
            // Записываем в переменную значения из поля Number of results: 6(число контактов).
            Match number = new Regex(@"\d+").Match(numberOfResult);
            return Int32.Parse(number.Value);
            // С помощью регулярного выражения получаем число без текста.
        }

        public ContactData PreAddContact(int index)
        {
            webDriver.FindElement(By.LinkText("home")).Click();
            // Переходим на главную страницу со списком контактов.
            if (CheckElementPresent(index)) // Ищем первый контакт по индексу.
            {
                return null; // Если контакт найден, то завершаем проверку.
            }
            return AddNewContact(); // Создаем новый контакт, если элемент в условии "if" не найден.
        }

        public ContactData PreAddContactJson(int index)
        {
            webDriver.FindElement(By.LinkText("home")).Click();
            // Переходим на главную страницу со списком контактов.
            if (CheckElementPresent(index)) // Ищем первый контакт по индексу.
            {
                return null; // Если контакт найден, то завершаем проверку.
            }
            return AddNewContactJson(); // Создаем новый контакт, если элемент в условии "if" не найден.
        }

        public ContactData PreAddContactXML(int index)
        {
            webDriver.FindElement(By.LinkText("home")).Click();
            // Переходим на главную страницу со списком контактов.
            if (CheckElementPresent(index)) // Ищем первый контакт по индексу.
            {
                return null; // Если контакт найден, то завершаем проверку.
            }
            return AddNewContactXML(); // Создаем новый контакт, если элемент в условии "if" не найден.
        }

        public Dictionary<string, string> DictionaryTest()
        {
            Dictionary<string, string> generateContacntDataDict = ContactDataList();

            webDriver.FindElement(By.LinkText("add new")).Click();
            // Переходим на страницу для создания контакта.
            foreach (var item in generateContacntDataDict)
            {
                if (item.Key == "bday" || item.Key == "bmonth" || 
                    item.Key == "aday" || item.Key == "amonth")
                {
                    webDriver.FindElement(By.Name(item.Key)).Click();
                    new SelectElement(webDriver.FindElement(By.Name(item.Key))).SelectByText(item.Value);
                }
                webDriver.FindElement(By.Name(item.Key)).SendKeys(item.Value);
            }
            webDriver.FindElement(By.Name("submit")).Submit();
            // Добавляем вторичные личные данные.
            webDriver.FindElement(By.LinkText("home")).Click();
            // Возвращаемся на главную страницу (контакты) не дожидаясь редиректа.
            contactCache = null;
            // Очищаем кэш.
            return generateContacntDataDict;
        }
    }
}
