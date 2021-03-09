using System;
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
    public class TestingPackageForGroups : BaseClass
    {
        public static IEnumerable<GroupData> RandomGroupData()
        {
            List<GroupData> generateData = new List<GroupData>();
            for (int i = 0; i < 5; i++)
            {
                generateData.Add(new GroupData()
                {
                    GroupName = GenerateRandomString(10),
                    GroupHeader = GenerateRandomString(30),
                    GroupFooter = GenerateRandomString(30)
                });
            }
            return generateData;
        }

        [Test]
        public void CreateNewGroupTest()
        {
            app.Navigation.GoToBaseUrl();
            app.Auth.Login(new AccountData("admin", "secret"));

            List<GroupData> oldGroups = app.Groups.GetGroupList();
            Console.Out.WriteLine("Начальное кол-во групп:  " + app.Groups.GetGroupCount() + "\n");
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

            List<GroupData> newGroups = app.Groups.GetGroupList();
            Console.Out.WriteLine("Конечное кол-во групп:  " + app.Groups.GetGroupCount() + "\n");
            // Записываем новые знаечения групп.

            oldGroups.Add(generateData);
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);
        }

        [Test]
        public void EditFirstGroupTest()
        {
            app.Navigation.GoToBaseUrl();
            app.Auth.Login(new AccountData("admin", "secret"));

            GroupData generateData = new GroupData
            {
                GroupName = GenerateRandomString(10),
                GroupHeader = GenerateRandomString(30),
                GroupFooter = GenerateRandomString(30)
            };
            app.Groups.PreAddGroup(generateData, 0);

            List<GroupData> oldGroups = app.Groups.GetGroupList();
            Console.Out.WriteLine("Кол-во групп: " + app.Groups.GetGroupCount() + "\n");
            GroupData oldData = oldGroups[0];
            Console.Out.WriteLine("ID Группы: " + oldData.Id + "\n" + "Было:\n" + oldData + "\n");

            app.Groups.EditFirstGroup(generateData, 0);

            Assert.AreEqual(oldGroups.Count, app.Groups.GetGroupCount());
            Console.Out.WriteLine("ID Группы: " + oldData.Id + "\n" + "Стало:\n" + generateData + "\n");

            List<GroupData> newGroups = app.Groups.GetGroupList();
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
        public void EditParentSecondGroupTest()
        {
            app.Navigation.GoToBaseUrl();
            app.Auth.Login(new AccountData("admin", "secret"));
            app.Groups.EditParentSecondGroup(1);
        }

        [Test]
        public void RemoveFirstGroupTest()
        {
            app.Navigation.GoToBaseUrl();
            app.Auth.Login(new AccountData("admin", "secret"));
            GroupData generateData = new GroupData
            {
                GroupName = GenerateRandomString(10),
                GroupHeader = GenerateRandomString(30),
                GroupFooter = GenerateRandomString(30)
            };
            app.Groups.PreAddGroup(generateData, 0);

            List<GroupData> oldGroups = app.Groups.GetGroupList();
            Console.Out.WriteLine("Изначальное кол-во групп: " + app.Groups.GetGroupCount() + "\n");
            GroupData oldValue = oldGroups[0];

            app.Groups.RemoveFirstGroup(0);

            Assert.AreEqual(oldGroups.Count - 1, app.Groups.GetGroupCount());
            List<GroupData> newGroups = app.Groups.GetGroupList();
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
    }
}
