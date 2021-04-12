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

            List<ContactData> oldList = group.GetContacts();
            Console.Out.WriteLine(oldList.Count);

            ContactData contact = ContactData.GetAll().Except(oldList).First();

            app.Contacts.AddContactToGroupDB(contact, group);

            List<ContactData> newList = group.GetContacts();
            Console.Out.WriteLine(newList.Count);
            oldList.Add(contact);
            oldList.Sort();
            newList.Sort();
            Assert.AreEqual(oldList, newList);
        }

        [Test]
        public void RemovingContactFromGroupTest()
        {
            GroupData group = GroupData.GetAll()[0];

            List<ContactData> oldList = group.GetContacts();
            Console.Out.WriteLine(oldList.Count);

            ContactData contact = group.GetContacts()[0];

            app.Contacts.RemoveContactFromGroupDB(contact, group);

            List<ContactData> newList = group.GetContacts();
            Console.Out.WriteLine(newList.Count);

            oldList.Remove(contact);
            oldList.Sort();
            newList.Sort();
            Assert.AreEqual(oldList, newList);
        }
    }
}
