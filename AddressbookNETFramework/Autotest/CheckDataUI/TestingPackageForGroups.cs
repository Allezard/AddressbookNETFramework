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
using System.Reflection;

namespace AddressbookNETFramework
{
    public class TestingPackageForGroups : BaseClass
    {
        [Test]
        public void CreateNewGroupTest()
        {
            List<GroupData> oldGroups = app.Groups.GetGroupList();
            Console.Out.WriteLine("Начальное кол-во групп:  " + app.Groups.GetGroupCount() + "\n");
            // Записываем в переменную "oldGroups" список существующих групп.

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

            List<GroupData> newGroups = app.Groups.GetGroupList();
            Console.Out.WriteLine("Конечное кол-во групп:  " + app.Groups.GetGroupCount() + "\n");
            // Записываем новые знаечения групп.

            oldGroups.Add(generateData);
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);
            // Добавляем в список "oldGroups" новую руппу, сортируем списки и сравниваем.
        }

        [Test]
        public void EditFirstGroupTest()
        {
            GroupData generateData = new GroupData
            {
                GroupName = GenerateRandomString(10),
                GroupHeader = GenerateRandomString(30),
                GroupFooter = GenerateRandomString(30)
            };
            app.Groups.PreAddGroup(generateData, 0);
            // Создаем новую группу, если по нулевому индексу она отсутствует.

            List<GroupData> oldGroups = app.Groups.GetGroupList();
            Console.Out.WriteLine("Кол-во групп: " + app.Groups.GetGroupCount() + "\n");
            // Записываем в переменную "oldGroups" список существующих групп.
            GroupData oldData = oldGroups[0];
            Console.Out.WriteLine("ID Группы: " + oldData.Id + "\n" + "Было:\n" + oldData + "\n");
            // Записываем данные группы по нулевому индексу в отдельную переменную для проверки.

            app.Groups.EditFirstGroup(generateData, 0);
            // Редактируем группу по нулевому индексу (очищаем все поля и заполняем их рандомными данными).

            Assert.AreEqual(oldGroups.Count, app.Groups.GetGroupCount());
            Console.Out.WriteLine("ID Группы: " + oldData.Id + "\n" + "Стало:\n" + generateData + "\n");
            // Сравниваем кол-во групп с помощью метода, который получает кол-во групп.

            List<GroupData> newGroups = app.Groups.GetGroupList();
            oldData.GroupName = generateData.GroupName;
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);
            // Меняем старые параметры первой группы на новые, сортируем старый и новый список, сравниваем их.

            foreach (GroupData group in newGroups)
            {
                if (group.GroupName == oldData.GroupName)
                {
                    Assert.AreEqual(generateData.GroupName, group.GroupName);
                }
            }
            // Сравниваем группы из нового списка со старым по имени.
        }

        [Test]
        public void EditParentSecondGroupTest()
        {
            app.Groups.EditParentSecondGroup(1);
            // Редактируем вторую по счету в списке группу.
        }

        [Test]
        public void RemoveFirstGroupTest()
        {
            GroupData generateData = new GroupData
            {
                GroupName = GenerateRandomString(10),
                GroupHeader = GenerateRandomString(30),
                GroupFooter = GenerateRandomString(30)
            };
            app.Groups.PreAddGroup(generateData, 0);
            // Создаем новую группу, если по нулевому индексу она отсутствует.

            List<GroupData> oldGroups = app.Groups.GetGroupList();
            Console.Out.WriteLine("Изначальное кол-во групп: " + app.Groups.GetGroupCount() + "\n");
            // Записываем в переменную "oldGroups" список существующих групп.
            GroupData oldValue = oldGroups[0];
            // Записываем данные группы по нулевому индексу в отдельную переменную для проверки.

            app.Groups.RemoveFirstGroup(0);
            // Удаляем группу по нулевому индексу.

            Assert.AreEqual(oldGroups.Count - 1, app.Groups.GetGroupCount());
            // Сравниваем кол-во групп с помощью метода, который получает кол-во групп.

            List<GroupData> newGroups = app.Groups.GetGroupList();
            Console.Out.WriteLine("Кол-во групп после удаления: " + app.Groups.GetGroupCount() + " (ID удаленной группы: " + oldValue.Id + ")" + "\n" + "\n");
            // Записываем в переменную "newGroups" список существующих групп.

            oldGroups.RemoveAt(0);
            Assert.AreEqual(oldGroups, newGroups);
            Console.Out.WriteLine("Список групп: " + "\n");
            // Удаляем из списка "oldGroups" группу с нулевым индексом и сравниваем оба списка.

            foreach (GroupData group in newGroups)
            {
                Assert.AreNotEqual(group.Id, oldValue.Id);
                Console.Out.WriteLine("ID Группы: " + group.Id);
            }
            // Сравниваем группы из нового списка со старым по id.
        }
    }
}
