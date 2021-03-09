using System;
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
        public static IEnumerable<ContactData> RandomContactData()
        {
            List<ContactData> generateContacnt = new List<ContactData>();
            for (int i = 0; i < 5; i++)
            {
                generateContacnt.Add(new ContactData()
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
                });
            }
            return generateContacnt;
        }

        [Test]
        public void AddNewContactTest()
        {
            app.Navigation.GoToBaseUrl();
            app.Auth.Login(new AccountData("admin", "secret"));

            List<ContactData> oldContacts = app.Contacts.GetContactList();
            Console.Out.WriteLine("Кол-во контактов:  " + app.Contacts.GetContactCount() + "\n");
            // Записываем старые знаечения контактов.
            Console.Out.WriteLine(oldContacts);

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
            Console.Out.WriteLine(generateContacnt);
            // Создаем новый контакт.

            Assert.AreEqual(oldContacts.Count + 1, app.Contacts.GetContactCount());

            List<ContactData> newContacts = app.Contacts.GetContactList();
            Console.Out.WriteLine("Конечное кол-во контактов:  " + app.Contacts.GetContactCount() + "\n");
            // Записываем новые знаечения контактов.

            oldContacts.Add(generateContacnt);
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);
        }

        [Test]
        public void EditFirstContactTest()
        {
            app.Navigation.GoToBaseUrl();
            app.Auth.Login(new AccountData("admin", "secret"));

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
            // Записываем старые знаечения контакта/контактов.
            Console.Out.WriteLine("Кол-во контактов:  " + app.Contacts.GetContactCount() + "\n");
            ContactData oldContData = oldContacts[0];
            // Сохраняем первый контакт в отдельную переменную для его проверки.
            Console.Out.WriteLine("Было: " + oldContData + " (ID: " + oldContData.Id + ")" + "\n");

            app.Contacts.EditFirstContact(generateContacnt, 0);
            // Редактируем первый контакт.

            Assert.AreEqual(oldContacts.Count, app.Contacts.GetContactCount());
            Console.Out.WriteLine("Стало: " + generateContacnt + " (ID: " + oldContData.Id + ")" + "\n");

            List<ContactData> newContacts = app.Contacts.GetContactList();
            Console.Out.WriteLine("Конечное кол-во контактов:  " + app.Contacts.GetContactCount() + "\n");

            oldContacts[0].FirstName = generateContacnt.FirstName;
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);

            foreach (ContactData contact in newContacts)
            {
                if (contact.Id == oldContData.Id)
                {
                    Assert.AreEqual(generateContacnt.FirstName, contact.FirstName);
                }
            }
        }

        [Test]
        public void DeleteFirstContactTest()
        {
            app.Navigation.GoToBaseUrl();
            app.Auth.Login(new AccountData("admin", "secret"));

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

            app.Contacts.DeleteFirstContact(0);
        }

        [Test]
        public void CheckContactInfoTest()
        {
            app.Navigation.GoToBaseUrl();
            app.Auth.Login(new AccountData("admin", "secret"));
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

            ContactData fromForm = app.Contacts.GetContactInfoFromEditForm(0);
            ContactData fromTabble = app.Contacts.GetContactInfoFromTable(0);
            Console.Out.WriteLine("Table: " + fromTabble + "\n" + "Form: " + fromForm);
            Assert.AreEqual(fromTabble.Address, fromForm.Address);
            Console.Out.WriteLine("\n" + "Table: \n" + fromTabble.Address + "\n\n" + "Form: \n" + fromForm.Address);
            Assert.AreEqual(fromTabble.AllEmails, fromForm.AllEmails);
            Console.Out.WriteLine("\n" + "Table: \n" + fromTabble.AllEmails + "\n\n" + "Form: \n" + fromForm.AllEmails);
            Assert.AreEqual(fromTabble.AllPhones, fromForm.AllPhones);
            Console.Out.WriteLine("\n" + "Table: \n" + fromTabble.AllPhones + "\n\n" + "Form: \n" + fromForm.AllPhones);
        }

        [Test]
        public void CheckDetailsInfoTest()
        {
            app.Navigation.GoToBaseUrl();
            app.Auth.Login(new AccountData("admin", "secret"));
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

            ContactData fromForm = app.Contacts.GetContactDetailsFromEditForm(0);
            ContactData fromTabble = app.Contacts.GetContactDetailsFormTable(0);

            Console.Out.WriteLine("\n" + "Table: \n" + fromTabble.AllDetails + "\n\n" + "Form: \n" + fromForm.AllDetails);
            Assert.AreEqual(fromTabble.AllDetails, fromForm.AllDetails);
        }

        [Test]
        public void AddContactInGroupTest()
        {
            app.Navigation.GoToBaseUrl();
            app.Auth.Login(new AccountData("admin", "secret"));
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

            app.Contacts.AddContactInGroup(0);
        }

        [Test]
        public void ContactSearchTest()
        {
            app.Navigation.GoToBaseUrl();
            app.Auth.Login(new AccountData("admin", "secret"));
            app.Contacts.GetNumberOfSearchResults();
            Console.Out.WriteLine("Number of results: " + app.Contacts.GetNumberOfSearchResults());

        }
    }
}
