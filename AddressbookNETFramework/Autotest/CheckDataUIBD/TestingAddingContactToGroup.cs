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
    public class TestingAddingContactToGroup : BaseClass
    {
        [Test]
        public void AddingContactToGroupTest()
        {
            GroupData group = GroupData.GetAll()[0];
            // Записываем в переменную первую группу из списка.

            List<ContactData> oldList = group.GetContacts();
            Console.Out.WriteLine(oldList.Count);
            // Сохраняем в переменную список контактов добавленных в группы.

            ContactData contact = ContactData.GetAll().Except(oldList).First();
            // Записываем в переменную первый контакт из списка.

            app.Contacts.AddContactToGroupDB(contact, group);
            // Добавляем ранее записанный контакт в переменную "contact" - в группу(group).

            List<ContactData> newList = group.GetContacts();
            Console.Out.WriteLine(newList.Count);
            // Сохраняем в переменную новый список контактов добавленных в группы.

            oldList.Add(contact);
            oldList.Sort();
            newList.Sort();
            Assert.AreEqual(oldList, newList);
            // Добавляем контакт связанный с группой - в старый список, сортируем, сравниваем списки.
        }

        [Test]
        public void RemovingContactFromGroupTest()
        {
            GroupData group = GroupData.GetAll()[0];
            // Записываем в переменную первую группу из списка.

            List<ContactData> oldList = group.GetContacts();
            Console.Out.WriteLine(oldList.Count);
            // Сохраняем в переменную список контактов добавленных в группы.

            ContactData contact = group.GetContacts()[0];
            // Записываем в переменную первый контакт из списка.

            app.Contacts.RemoveContactFromGroupDB(contact, group);
            // Удаляем ранее записанный контакт в переменную "contact" - из группы(group).

            List<ContactData> newList = group.GetContacts();
            Console.Out.WriteLine(newList.Count);
            // Сохраняем в переменную новый список контактов добавленных в группы.

            oldList.Remove(contact);
            oldList.Sort();
            newList.Sort();
            Assert.AreEqual(oldList, newList);
            // Удаляем контакт связанный с группой из старого списка, сортируем, сравниваем списки.
        }
    }
}
