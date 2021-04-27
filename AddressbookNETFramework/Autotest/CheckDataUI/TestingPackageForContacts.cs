﻿using System;
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
using System.Collections.Generic;

namespace AddressbookNETFramework
{
    public class TestingPackageForContacts : BaseClass
    {
        [Test]
        public void AddNewContactTest()
        {
            List<ContactData> oldContacts = app.Contacts.GetContactList();
            Console.Out.WriteLine("Кол-во контактов:  " + app.Contacts.GetContactCount() + "\n");
            // Записываем старые знаечения контактов.

            ContactData generateContacnt = new ContactData
            {
                FirstName = GenerateRandomString(10),
                MiddleName = GenerateRandomString(10),
                LastName = GenerateRandomString(10),
                NickName = GenerateRandomString(10),
                Company = GenerateRandomString(10),
                Title = GenerateRandomString(10),
                Address = GenerateRandomString(10),
                HomePhone = GenerateRandomString(10),
                MobilePhone = GenerateRandomString(10),
                WorkPhone = GenerateRandomString(10),
                Fax = GenerateRandomString(10),
                Email = GenerateRandomString(10),
                Email2 = GenerateRandomString(10),
                Email3 = GenerateRandomString(10),
                Homepage = GenerateRandomString(10),
                SecondaryAddress = GenerateRandomString(10),
                HomeAddress = GenerateRandomString(10),
                Notes = GenerateRandomString(10)
            };
            app.Contacts.AddNewContact(generateContacnt);
            // Создаем новый контакт.
            Console.Out.WriteLine(generateContacnt);

            Assert.AreEqual(oldContacts.Count + 1, app.Contacts.GetContactCount());
            // Сравниваем кол-во контактов перед созданием(без учета созданного) и после создания.

            List<ContactData> newContacts = app.Contacts.GetContactList();
            // Записываем новые знаечения контактов.
            Console.Out.WriteLine("Конечное кол-во контактов:  " + app.Contacts.GetContactCount() + "\n");

            oldContacts.Add(generateContacnt);
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);
            // Добавляем новый контакт в список, сортируем списки и сравниваем их.
        }

        [Test]
        public void EditFirstContactTest()
        {
            ContactData generateContacnt = new ContactData
            {
                FirstName = GenerateRandomString(10),
                MiddleName = GenerateRandomString(10),
                LastName = GenerateRandomString(10),
                NickName = GenerateRandomString(10),
                Company = GenerateRandomString(10),
                Title = GenerateRandomString(10),
                Address = GenerateRandomString(10),
                HomePhone = GenerateRandomString(10),
                MobilePhone = GenerateRandomString(10),
                WorkPhone = GenerateRandomString(10),
                Fax = GenerateRandomString(10),
                Email = GenerateRandomString(10),
                Email2 = GenerateRandomString(10),
                Email3 = GenerateRandomString(10),
                Homepage = GenerateRandomString(10),
                SecondaryAddress = GenerateRandomString(10),
                HomeAddress = GenerateRandomString(10),
                Notes = GenerateRandomString(10)
            };
            app.Contacts.PreAddContact(generateContacnt, 0);
            //Создаем новый контакт, если его нет.

            List<ContactData> oldContacts = app.Contacts.GetContactList();
            // Записываем текущие знаечения контакта/контактов.
            ContactData oldContData = oldContacts[0];
            // Сохраняем первый контакт в отдельную переменную для его проверки.
            Console.Out.WriteLine("Было: " + oldContData + "\n");

            app.Contacts.EditFirstContact(generateContacnt, 0);
            // Редактируем первый контакт.

            Assert.AreEqual(oldContacts.Count, app.Contacts.GetContactCount());
            // Сравниваем кол-во контактов перед редактированием и после редактирования.
            Console.Out.WriteLine("Стало: " + generateContacnt + "\n");

            List<ContactData> newContacts = app.Contacts.GetContactList();
            // Записываем новое знаечения контакта/контактов.

            oldContData.FirstName = generateContacnt.FirstName;
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);
            // Переопределяем переменную FirstName, сортируем списки и сравниваем их.

            foreach (ContactData contact in newContacts)
            {
                if (contact.FullName == oldContData.FullName)
                {
                    Assert.AreEqual(oldContData.FullName, contact.FullName);
                    Assert.AreEqual(oldContData.AllEmails, contact.AllEmails);
                    Assert.AreEqual(oldContData.AllPhones, contact.AllPhones);
                }
            }
            // Проверяем, что имя, фамилия, емейлы и номера телефонов соответствуют.
        }

        [Test]
        public void DeleteFirstContactTest()
        {
            ContactData generateContacnt = new ContactData
            {
                FirstName = GenerateRandomString(10),
                MiddleName = GenerateRandomString(10),
                LastName = GenerateRandomString(10),
                NickName = GenerateRandomString(10),
                Company = GenerateRandomString(10),
                Title = GenerateRandomString(10),
                Address = GenerateRandomString(10),
                HomePhone = GenerateRandomString(10),
                MobilePhone = GenerateRandomString(10),
                WorkPhone = GenerateRandomString(10),
                Fax = GenerateRandomString(10),
                Email = GenerateRandomString(10),
                Email2 = GenerateRandomString(10),
                Email3 = GenerateRandomString(10),
                Homepage = GenerateRandomString(10),
                SecondaryAddress = GenerateRandomString(10),
                HomeAddress = GenerateRandomString(10),
                Notes = GenerateRandomString(10)
            };
            app.Contacts.PreAddContact(generateContacnt, 0);
            //Создаем новый контакт, если его нет.

            List<ContactData> oldContacts = app.Contacts.GetContactList();
            // Записываем текущие знаечения контакта/контактов.
            ContactData oldContData = oldContacts[0];
            // Сохраняем первый контакт в отдельную переменную для его проверки.

            app.Contacts.DeleteFirstContact(0);
            // Удаляем первый контакт.

            Assert.AreEqual(oldContacts.Count - 1, app.Contacts.GetContactCount());
            // Сравниваем кол-во контактов перед удалением(без учета уданенного) и после удаления.

            List<ContactData> newContacts = app.Contacts.GetContactList();
            // Записываем новое знаечения контакта/контактов.

            oldContacts.RemoveAt(0);
            Assert.AreEqual(oldContacts, newContacts);
            // Удаляем первый контакт из списка и проверяем списки на равенство содержимого.

            foreach (ContactData contact in newContacts)
            {
                Assert.AreNotEqual(contact.FirstName, oldContData.FirstName);
                Console.Out.WriteLine("ID контакта: " + contact.Id);
            }
            // Проверяем, что имена контактов из списка не равны имени удаленного контакта.
        }

        [Test]
        public void CheckContactInfoTest()
        {
            ContactData generateContacnt = new ContactData
            {
                FirstName = GenerateRandomString(10),
                MiddleName = GenerateRandomString(10),
                LastName = GenerateRandomString(10),
                NickName = GenerateRandomString(10),
                Company = GenerateRandomString(10),
                Title = GenerateRandomString(10),
                Address = GenerateRandomString(10),
                HomePhone = GenerateRandomString(10),
                MobilePhone = GenerateRandomString(10),
                WorkPhone = GenerateRandomString(10),
                Fax = GenerateRandomString(10),
                Email = GenerateRandomString(10),
                Email2 = GenerateRandomString(10),
                Email3 = GenerateRandomString(10),
                Homepage = GenerateRandomString(10),
                SecondaryAddress = GenerateRandomString(10),
                HomeAddress = GenerateRandomString(10),
                Notes = GenerateRandomString(10)
            };
            app.Contacts.PreAddContact(generateContacnt, 0);
            //Создаем новый контакт, если его нет.

            ContactData fromForm = app.Contacts.GetContactInfoFromEditForm(0);
            ContactData fromTabble = app.Contacts.GetContactInfoFromTable(0);
            // Записываем данные первого контакта из формы редактирования и с добамшей страницы.
            Console.Out.WriteLine("Table: " + fromTabble + "\n" + "Form: " + fromForm);

            Assert.AreEqual(fromTabble.FirstName, fromForm.FirstName);
            Console.Out.WriteLine("\n" + "Table: \n" + fromTabble.FirstName + "\n\n" + "Form: \n" + fromForm.FirstName);
            Assert.AreEqual(fromTabble.LastName, fromForm.LastName);
            Console.Out.WriteLine("\n" + "Table: \n" + fromTabble.LastName + "\n\n" + "Form: \n" + fromForm.LastName);
            Assert.AreEqual(fromTabble.Address, fromForm.Address);
            Console.Out.WriteLine("\n" + "Table: \n" + fromTabble.Address + "\n\n" + "Form: \n" + fromForm.Address);
            Assert.AreEqual(fromTabble.AllEmails, fromForm.AllEmails);
            Console.Out.WriteLine("\n" + "Table: \n" + fromTabble.AllEmails + "\n\n" + "Form: \n" + fromForm.AllEmails);
            Assert.AreEqual(fromTabble.AllPhones, fromForm.AllPhones);
            Console.Out.WriteLine("\n" + "Table: \n" + fromTabble.AllPhones + "\n\n" + "Form: \n" + fromForm.AllPhones);
            // Сравниваем данные из формы редактирования и с главной страницы.
        }

        [Test]
        public void CheckDetailsInfoTest()
        {
            ContactData generateContacnt = new ContactData
            {
                FirstName = GenerateRandomString(10),
                MiddleName = GenerateRandomString(10),
                LastName = GenerateRandomString(10),
                NickName = GenerateRandomString(10),
                Company = GenerateRandomString(10),
                Title = GenerateRandomString(10),
                Address = GenerateRandomString(10),
                HomePhone = GenerateRandomString(10),
                MobilePhone = GenerateRandomString(10),
                WorkPhone = GenerateRandomString(10),
                Fax = GenerateRandomString(10),
                Email = GenerateRandomString(10),
                Email2 = GenerateRandomString(10),
                Email3 = GenerateRandomString(10),
                Homepage = GenerateRandomString(10),
                SecondaryAddress = GenerateRandomString(10),
                HomeAddress = GenerateRandomString(10),
                Notes = GenerateRandomString(10)
            };
            app.Contacts.PreAddContact(generateContacnt, 0);
            //Создаем новый контакт, если его нет.

            ContactData fromForm = app.Contacts.GetContactDetailsFromEditForm(0);
            // Считываем информацию из формы с деталями первого контакта и записываем их в переменную.
            ContactData fromTabble = app.Contacts.GetContactDetailsFormTable(0);
            // Считываем информацию из полей на главной странице первого контакта и записываем их в переменную.

            Console.Out.WriteLine("\n" + "Table: \n" + fromTabble.AllDetails + "\n\n" + "Form: \n" + fromForm.AllDetails);
            Assert.AreEqual(fromTabble.AllDetails, fromForm.AllDetails);
            // Сравниваем информацию из полей первого контакта и формы детализации.
        }

        [Test]
        public void AddContactInGroupTest()
        {
            ContactData generateContacnt = new ContactData
            {
                FirstName = GenerateRandomString(10),
                MiddleName = GenerateRandomString(10),
                LastName = GenerateRandomString(10),
                NickName = GenerateRandomString(10),
                Company = GenerateRandomString(10),
                Title = GenerateRandomString(10),
                Address = GenerateRandomString(10),
                HomePhone = GenerateRandomString(10),
                MobilePhone = GenerateRandomString(10),
                WorkPhone = GenerateRandomString(10),
                Fax = GenerateRandomString(10),
                Email = GenerateRandomString(10),
                Email2 = GenerateRandomString(10),
                Email3 = GenerateRandomString(10),
                Homepage = GenerateRandomString(10),
                SecondaryAddress = GenerateRandomString(10),
                HomeAddress = GenerateRandomString(10),
                Notes = GenerateRandomString(10)
            };
            app.Contacts.PreAddContact(generateContacnt, 0);
            //Создаем новый контакт, если его нет.

            app.Contacts.AddContactInGroup(0);
            // Добавляем первый контакт в первую группу и затем удаляем его.
        }

        [Test]
        public void ContactSearchTest()
        {
            app.Contacts.GetNumberOfSearchResults();
            Console.Out.WriteLine("Number of results: " + app.Contacts.GetNumberOfSearchResults());
            // Получаем кол-во контактов на текущей странице и выводим их в консоль.
        }
    }
}