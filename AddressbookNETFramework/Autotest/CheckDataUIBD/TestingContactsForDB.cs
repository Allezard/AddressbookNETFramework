using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;
using System.Text;
using System.Linq;
using System.Threading;
using System.Collections.Generic;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using AddressbookNETFramework.Helpers;
using AddressbookNETFramework.Model;

namespace AddressbookNETFramework
{
    public class TestingContactsForDB : BaseClass
    {
        [Test]
        public void DBAddNewContactTest()
        {
            List<ContactData> oldContacts = app.Contacts.GetContactList();
            Console.Out.WriteLine("Кол-во контактов:  " + app.Contacts.GetContactCount() + "\n");
            // Записываем старые знаечения контактов.
            Console.Out.WriteLine(oldContacts);

            ContactData generateContacnt = app.Contacts.AddNewContact();
            Console.Out.WriteLine(generateContacnt);
            // Создаем новый контакт.

            Assert.AreEqual(oldContacts.Count + 1, app.Contacts.GetContactCount());
            // Сравниваем кол-во контактов перед созданием(без учета созданного) и после создания.

            List<ContactData> newContacts = app.Contacts.GetContactList();
            Console.Out.WriteLine("Конечное кол-во контактов:  " + app.Contacts.GetContactCount() + "\n");
            // Записываем новые знаечения контактов.

            oldContacts.Add(generateContacnt);
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);
            // Добавляем новый контакт в список, сортируем списки и сравниваем их.
        }

        [Test]
        public void DBEditFirstContactTest()
        {
            app.Contacts.PreAddContact(0);
            //Создаем новый контакт, если его нет.

            List<ContactData> oldContacts = ContactData.GetAll();
            // Записываем текущие знаечения контакта/контактов.
            ContactData oldContData = oldContacts[0];
            // Сохраняем первый контакт в отдельную переменную для его проверки.
            Console.Out.WriteLine("Было: " + oldContData + "\n");

            ContactData generateContacnt = app.Contacts.EditFirstContact(0);
            // Редактируем первый контакт.

            Assert.AreEqual(oldContacts.Count, app.Contacts.GetContactCount());
            // Сравниваем кол-во контактов перед редактированием и после редактирования.
            Console.Out.WriteLine("Стало: " + generateContacnt + "\n");

            List<ContactData> newContacts = ContactData.GetAll();
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
        public void DBDeleteFirstContactTest()
        {
            app.Contacts.PreAddContact(0);
            //Создаем новый контакт, если его нет.

            List<ContactData> oldContacts = ContactData.GetAll();
            // Записываем текущие знаечения контакта/контактов.
            ContactData oldContData = oldContacts[0];
            // Сохраняем первый контакт в отдельную переменную для его проверки.

            app.Contacts.DeleteFirstContact(0);
            // Удаляем первый контакт.

            Assert.AreEqual(oldContacts.Count - 1, app.Contacts.GetContactCount());
            // Сравниваем кол-во контактов перед удалением(без учета уданенного) и после удаления.

            List<ContactData> newContacts = ContactData.GetAll();
            // Записываем новое знаечения контакта/контактов.

            oldContacts.RemoveAt(0);
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);
            // Удаляем первый контакт из списка и проверяем списки на равенство содержимого.

            foreach (ContactData contact in newContacts)
            {
                Assert.AreNotEqual(contact.FirstName, oldContData.FirstName);
                Console.Out.WriteLine("ID контакта: " + contact.Id);
            }
            // Проверяем, что имена контактов из списка не равны имени удаленного контакта.
        }
    }
}
