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
            List<GroupData> oldGroups = GroupData.GetAll();
            Console.Out.WriteLine("Начальное кол-во групп:  " + oldGroups.Count + "\n");
            // Записываем в переменную "oldGroups" список существующих групп из БД.

            GroupData generateData = new GroupData
            {
                GroupName = GenerateRandomString(10),
                GroupHeader = GenerateRandomString(30),
                GroupFooter = GenerateRandomString(30)
            };
            app.Groups.CreateNewGroup(generateData);
            Console.Out.WriteLine(generateData);
            // Создаем новую группу и заполняем ее рандомными данными.

            Assert.AreEqual(oldGroups.Count + 1, app.Groups.GetGroupCount());
            // Сравниваем кол-во групп с помощью метода, который получает кол-во групп.

            List<GroupData> newGroups = GroupData.GetAll();
            Console.Out.WriteLine("Конечное кол-во групп:  " + newGroups.Count + "\n");
            // Записываем новые знаечения групп.

            oldGroups.Add(generateData);
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);
            // Добавляем в старый список группу которую создали, сортируем старый и новый список, сравниваем их.
        }

        [Test]
        public void DBEditFirstGroupTest()
        {
            GroupData generateData = new GroupData
            {
                GroupName = GenerateRandomString(10),
                GroupHeader = GenerateRandomString(30),
                GroupFooter = GenerateRandomString(30)
            };
            app.Groups.PreAddGroup(generateData, 0);
            // Создаем новую группу, если по нулевому индексу она отсутствует.

            List<GroupData> oldGroups = GroupData.GetAll();
            Console.Out.WriteLine("Кол-во групп: " + app.Groups.GetGroupCount() + "\n");
            // Записываем в переменную "oldGroups" список существующих групп из БД.
            GroupData oldData = oldGroups[0];
            Console.Out.WriteLine("ID Группы: " + oldData.Id + "\n" + "Было:\n" + oldData + "\n");
            // Записываем данные группы по нулевому индексу в отдельную переменную для проверки.

            app.Groups.EditFirstGroupBD(generateData, oldData);
            // Редактируем группу которая была получена в "oldData" из "oldGroups" по нулевому индексу. После очистки полей заполняем рандомными данными.

            Assert.AreEqual(oldGroups.Count, app.Groups.GetGroupCount());
            Console.Out.WriteLine("ID Группы: " + oldData.Id + "\n" + "Стало:\n" + generateData + "\n");
            // Сравниваем кол-во групп с помощью метода, который получает кол-во групп.

            List<GroupData> newGroups = GroupData.GetAll();
            // Записываем в переменную "newGroups" список существующих групп из БД.
            oldGroups[0].GroupName = generateData.GroupName;
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);
            // Меняем старые параметры первой группы на новые, сортируем старый и новый список, сравниваем их.

            foreach (GroupData group in newGroups)
            {
                if (group.Id == oldData.Id)
                {
                    Assert.AreEqual(generateData.GroupName, group.GroupName);
                }
            }
            // Сравниваем группы из нового списка со старым по имени.
        }

        [Test]
        public void DBRemoveFirstGroupTest()
        {
            GroupData generateData = new GroupData
            {
                GroupName = GenerateRandomString(10),
                GroupHeader = GenerateRandomString(30),
                GroupFooter = GenerateRandomString(30)
            };
            app.Groups.PreAddGroup(generateData, 0);
            // Создаем новую группу, если по нулевому индексу она отсутствует.

            List<GroupData> oldGroups = GroupData.GetAll();
            Console.Out.WriteLine("Изначальное кол-во групп: " + app.Groups.GetGroupCount() + "\n");
            // Записываем в переменную "oldGroups" список существующих групп из БД.
            GroupData oldValue = oldGroups[0];
            // Записываем данные группы по нулевому индексу в отдельную переменную для проверки.

            app.Groups.RemoveFirstGroupBD(oldValue);
            // Удаляем группу которая была получена в "oldValue" из "oldGroups" по нулевому индексу.

            Assert.AreEqual(oldGroups.Count - 1, app.Groups.GetGroupCount());
            // Сравниваем кол-во групп с помощью метода, который получает кол-во групп.

            List<GroupData> newGroups = GroupData.GetAll();
            Console.Out.WriteLine("Кол-во групп после удаления: " + app.Groups.GetGroupCount() + " (ID удаленной группы: " + oldValue.Id + ")" + "\n" + "\n");
            // Записываем в переменную "newGroups" список существующих групп из БД.

            oldGroups.RemoveAt(0);
            Assert.AreEqual(oldGroups, newGroups);
            Console.Out.WriteLine("Список групп: " + "\n");
            // Удаляем из старого списка группу с нулевым индексом и сравниваем списки.

            foreach (GroupData group in newGroups)
            {
                Assert.AreNotEqual(group.Id, oldValue.Id);
                Console.Out.WriteLine("ID Группы: " + group.Id);
            }
            // Сравниваем группы из нового списка со старым по id.
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
