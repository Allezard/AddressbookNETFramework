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
    public class TestingGroupsForDB : GroupBaseClass
    {
        [Test]
        public void DBCreateNewGroupTest()
        {
            //app.Navigation.GoToBaseUrl();
            //app.Auth.Login(new AccountData("admin", "secret"));

            List<GroupData> oldGroups = GroupData.GetAll();
            Console.Out.WriteLine("Начальное кол-во групп:  " + oldGroups.Count + "\n");
            // Записываем старые знаечения групп.

            GroupData generateData = new GroupData
            {
                GroupName = GenerateRandomString(10),
                GroupHeader = GenerateRandomString(30),
                GroupFooter = GenerateRandomString(30)
            };
            app.Groups.CreateNewGroup(generateData);
            Console.Out.WriteLine(generateData);
            // Создаем новую группу.

            Assert.AreEqual(oldGroups.Count + 1, app.Groups.GetGroupCount());

            List<GroupData> newGroups = GroupData.GetAll();
            Console.Out.WriteLine("Конечное кол-во групп:  " + newGroups.Count + "\n");
            // Записываем новые знаечения групп.

            oldGroups.Add(generateData);
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);
        }

        [Test]
        public void DBEditFirstGroupTest()
        {
            //app.Navigation.GoToBaseUrl();
            //app.Auth.Login(new AccountData("admin", "secret"));

            GroupData generateData = new GroupData
            {
                GroupName = GenerateRandomString(10),
                GroupHeader = GenerateRandomString(30),
                GroupFooter = GenerateRandomString(30)
            };
            app.Groups.PreAddGroup(generateData, 0);

            List<GroupData> oldGroups = GroupData.GetAll();
            Console.Out.WriteLine("Кол-во групп: " + app.Groups.GetGroupCount() + "\n");
            GroupData oldData = oldGroups[0];
            Console.Out.WriteLine("ID Группы: " + oldData.Id + "\n" + "Было:\n" + oldData + "\n");

            app.Groups.EditFirstGroupBD(generateData, oldData);

            Assert.AreEqual(oldGroups.Count, app.Groups.GetGroupCount());
            Console.Out.WriteLine("ID Группы: " + oldData.Id + "\n" + "Стало:\n" + generateData + "\n");

            List<GroupData> newGroups = GroupData.GetAll();
            oldGroups[0].GroupName = generateData.GroupName;
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);

            foreach (GroupData group in newGroups)
            {
                if (group.Id == oldData.Id)
                {
                    Assert.AreEqual(generateData.GroupName, group.GroupName);
                }
            }
        }

        [Test]
        public void DBRemoveFirstGroupTest()
        {
            //app.Navigation.GoToBaseUrl();
            //app.Auth.Login(new AccountData("admin", "secret"));
            GroupData generateData = new GroupData
            {
                GroupName = GenerateRandomString(10),
                GroupHeader = GenerateRandomString(30),
                GroupFooter = GenerateRandomString(30)
            };
            app.Groups.PreAddGroup(generateData, 0);

            List<GroupData> oldGroups = GroupData.GetAll();
            Console.Out.WriteLine("Изначальное кол-во групп: " + app.Groups.GetGroupCount() + "\n");
            GroupData oldValue = oldGroups[0];

            app.Groups.RemoveFirstGroupBD(oldValue);

            Assert.AreEqual(oldGroups.Count - 1, app.Groups.GetGroupCount());
            List<GroupData> newGroups = GroupData.GetAll();
            Console.Out.WriteLine("Кол-во групп после удаления: " + app.Groups.GetGroupCount() + " (ID удаленной группы: " + oldValue.Id + ")" + "\n" + "\n");

            oldGroups.RemoveAt(0);
            Assert.AreEqual(oldGroups, newGroups);
            Console.Out.WriteLine("Список групп: " + "\n");

            foreach (GroupData group in newGroups)
            {
                Assert.AreNotEqual(group.Id, oldValue.Id);
                Console.Out.WriteLine("ID Группы: " + group.Id);
            }
        }

        [Test]
        public void DBConnectivityTest()
        {
            //DateTime start = DateTime.Now;
            //_ = app.Groups.GetGroupList();
            //DateTime end = DateTime.Now;
            //Console.Out.WriteLine("Время считывания данных UI: " + end.Subtract(start));

            //DateTime startDb = DateTime.Now;
            //_ = GroupData.GetAll();
            //DateTime endDb = DateTime.Now;
            //Console.Out.WriteLine("Время считывания данных BD: " + endDb.Subtract(startDb));

            //foreach (ContactData contact in GroupData.GetAll()[0].GetContacts())
            //{
            //    Console.Out.WriteLine("Контакт который входит в группу с нулевым индексом \n" + contact);
            //}

            foreach (ContactData contact in ContactData.GetAll())
            {
                Console.Out.WriteLine(contact.Deprecated);
            }

        }
    }
}
