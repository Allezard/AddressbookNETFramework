﻿using System;
using System.Text;
using System.Linq;
using System.Threading;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
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
    public class TestingPackageForContactsXml : BaseClass
    {
        public static IEnumerable<ContactData> ContactDataFromXmlFile()
        {
            IEnumerable<ContactData> xmlData;
            var a = new XmlSerializer(typeof(List<ContactData>));
            using (StreamReader reader = new StreamReader(@"C:\Users\Professional\source\repos\AddressbookNETFramework\AddressbookNETFramework\TestDataFolder\contacts.xml"))
            {
                xmlData = (List<ContactData>)a.Deserialize(reader);
            }
            return xmlData;
        }

        [Test]
        public void XmlAddNewContactTest()
        {
            List<ContactData> oldContacts = app.Contacts.GetContactList();
            Console.Out.WriteLine("Кол-во контактов:  " + app.Contacts.GetContactCount() + "\n");
            // Записываем старые знаечения контактов.

            ContactData generateContacnt = new ContactData
            {
                FirstName = ContactDataFromXmlFile().First().FirstName,
                MiddleName = ContactDataFromXmlFile().First().MiddleName,
                LastName = ContactDataFromXmlFile().First().LastName,
                NickName = ContactDataFromXmlFile().First().NickName,
                Company = ContactDataFromXmlFile().First().Company,
                Title = ContactDataFromXmlFile().First().Title,
                Address = ContactDataFromXmlFile().First().Address,
                HomePhone = ContactDataFromXmlFile().First().HomePhone,
                MobilePhone = ContactDataFromXmlFile().First().MobilePhone,
                WorkPhone = ContactDataFromXmlFile().First().WorkPhone,
                Fax = ContactDataFromXmlFile().First().Fax,
                Email = ContactDataFromXmlFile().First().Email,
                Email2 = ContactDataFromXmlFile().First().Email2,
                Email3 = ContactDataFromXmlFile().First().Email3,
                Homepage = ContactDataFromXmlFile().First().Homepage,
                SecondaryAddress = ContactDataFromXmlFile().First().SecondaryAddress,
                HomeAddress = ContactDataFromXmlFile().First().HomeAddress,
                Notes = ContactDataFromXmlFile().First().Notes
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
        public void XmlEditFirstContactTest()
        {
            ContactData generateContacnt = new ContactData
            {
                FirstName = ContactDataFromXmlFile().First().FirstName,
                MiddleName = ContactDataFromXmlFile().First().MiddleName,
                LastName = ContactDataFromXmlFile().First().LastName,
                NickName = ContactDataFromXmlFile().First().NickName,
                Company = ContactDataFromXmlFile().First().Company,
                Title = ContactDataFromXmlFile().First().Title,
                Address = ContactDataFromXmlFile().First().Address,
                HomePhone = ContactDataFromXmlFile().First().HomePhone,
                MobilePhone = ContactDataFromXmlFile().First().MobilePhone,
                WorkPhone = ContactDataFromXmlFile().First().WorkPhone,
                Fax = ContactDataFromXmlFile().First().Fax,
                Email = ContactDataFromXmlFile().First().Email,
                Email2 = ContactDataFromXmlFile().First().Email2,
                Email3 = ContactDataFromXmlFile().First().Email3,
                Homepage = ContactDataFromXmlFile().First().Homepage,
                SecondaryAddress = ContactDataFromXmlFile().First().SecondaryAddress,
                HomeAddress = ContactDataFromXmlFile().First().HomeAddress,
                Notes = ContactDataFromXmlFile().First().Notes
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
        public void XmlDeleteFirstContactTest()
        {
            ContactData generateContacnt = new ContactData
            {
                FirstName = ContactDataFromXmlFile().First().FirstName,
                MiddleName = ContactDataFromXmlFile().First().MiddleName,
                LastName = ContactDataFromXmlFile().First().LastName,
                NickName = ContactDataFromXmlFile().First().NickName,
                Company = ContactDataFromXmlFile().First().Company,
                Title = ContactDataFromXmlFile().First().Title,
                Address = ContactDataFromXmlFile().First().Address,
                HomePhone = ContactDataFromXmlFile().First().HomePhone,
                MobilePhone = ContactDataFromXmlFile().First().MobilePhone,
                WorkPhone = ContactDataFromXmlFile().First().WorkPhone,
                Fax = ContactDataFromXmlFile().First().Fax,
                Email = ContactDataFromXmlFile().First().Email,
                Email2 = ContactDataFromXmlFile().First().Email2,
                Email3 = ContactDataFromXmlFile().First().Email3,
                Homepage = ContactDataFromXmlFile().First().Homepage,
                SecondaryAddress = ContactDataFromXmlFile().First().SecondaryAddress,
                HomeAddress = ContactDataFromXmlFile().First().HomeAddress,
                Notes = ContactDataFromXmlFile().First().Notes
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
        public void XmlAddContactInGroupTest()
        {
            ContactData generateContacnt = new ContactData
            {
                FirstName = ContactDataFromXmlFile().First().FirstName,
                MiddleName = ContactDataFromXmlFile().First().MiddleName,
                LastName = ContactDataFromXmlFile().First().LastName,
                NickName = ContactDataFromXmlFile().First().NickName,
                Company = ContactDataFromXmlFile().First().Company,
                Title = ContactDataFromXmlFile().First().Title,
                Address = ContactDataFromXmlFile().First().Address,
                HomePhone = ContactDataFromXmlFile().First().HomePhone,
                MobilePhone = ContactDataFromXmlFile().First().MobilePhone,
                WorkPhone = ContactDataFromXmlFile().First().WorkPhone,
                Fax = ContactDataFromXmlFile().First().Fax,
                Email = ContactDataFromXmlFile().First().Email,
                Email2 = ContactDataFromXmlFile().First().Email2,
                Email3 = ContactDataFromXmlFile().First().Email3,
                Homepage = ContactDataFromXmlFile().First().Homepage,
                SecondaryAddress = ContactDataFromXmlFile().First().SecondaryAddress,
                HomeAddress = ContactDataFromXmlFile().First().HomeAddress,
                Notes = ContactDataFromXmlFile().First().Notes
            };
            app.Contacts.PreAddContact(generateContacnt, 0);
            //Создаем новый контакт, если его нет.

            app.Contacts.AddContactInGroup(0);
            // Добавляем первый контакт в первую группу и затем удаляем его.
        }
    }
}