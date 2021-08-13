using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using AddressbookNETFramework.Model;
using AddressbookNETFramework.Helpers;
using System.Threading;

namespace DataGeneration
{
    class Program
    {
        /// <summary>
        /// Генерирует файл в зависимости от выбора формата(json или xml) и записывает в него значения на основе выбранных полей из "GroupData" или "ContactData".
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Console.Write("Укажите тип файла (typeGroup или typeContact): ");
            string type = Console.ReadLine();

            Console.Write("Укажите нужное кол-во копий объекта в файле: ");
            string count = Console.ReadLine();

            Console.Write("Укажите название файла и его формат (name.json or name.xml): ");
            string writerText = Console.ReadLine();
            StreamWriter writer = new StreamWriter(writerText);

            string format = writerText.Substring(writerText.IndexOf(".") + 1);

            if (type == "typeGroup")
            {
                List<GroupData> groups = new List<GroupData>();
                for (int i = 0; i < Convert.ToInt32(count); i++)
                {
                    groups.Add(new GroupData()
                    {
                        GroupName = BaseHelper.GenerateRandomString(10),
                        GroupHeader = BaseHelper.GenerateRandomString(10),
                        GroupFooter = BaseHelper.GenerateRandomString(10)
                    });
                }
                if (format == "json")
                {
                    WriteGroupsToJsonFile(groups, writer);
                }
                else if (format == "xml")
                {
                    WriteGroupsToXMLFile(groups, writer);
                }
                else
                {
                    Console.Out.WriteLine("Unrecognized format" + format);
                }
                writer.Close();
            }
            else if (type == "typeContact")
            {
                ContactData rndDate = new ContactData();
                Random rnd = new Random();
                List<ContactData> contacts = new List<ContactData>();
                for (int i = 0; i < Convert.ToInt32(count); i++)
                {
                    contacts.Add(new ContactData()
                    {
                        BirthDay = rndDate.RandomAllYear(rnd).Day.ToString(),
                        BirthMonth = ((EnumClass.EnumMonths)rndDate.RandomAllYear(rnd).Month).ToString(),
                        YearOfBirth = rndDate.RandomAllYear(rnd).Year.ToString(),
                        AnniversDay = rndDate.RandomAllYear(rnd).Day.ToString(),
                        AnniversMonth = ((EnumClass.EnumMonths)rndDate.RandomAllYear(rnd).Month).ToString(),
                        YearOfAnnivers = rndDate.RandomAllYear(rnd).Year.ToString(),
                        FirstName = BaseHelper.GenerateRandomString(10),
                        MiddleName = BaseHelper.GenerateRandomString(10),
                        LastName = BaseHelper.GenerateRandomString(10),
                        NickName = BaseHelper.GenerateRandomString(10),
                        Company = BaseHelper.GenerateRandomString(10),
                        Title = BaseHelper.GenerateRandomString(10),
                        Address = BaseHelper.GenerateRandomString(10),
                        HomePhone = BaseHelper.GenerateRandomString(10),
                        MobilePhone = BaseHelper.GenerateRandomString(10),
                        WorkPhone = BaseHelper.GenerateRandomString(10),
                        Fax = BaseHelper.GenerateRandomString(10),
                        Email = BaseHelper.GenerateRandomString(10),
                        Email2 = BaseHelper.GenerateRandomString(10),
                        Email3 = BaseHelper.GenerateRandomString(10),
                        Homepage = BaseHelper.GenerateRandomString(10),
                        SecondaryAddress = BaseHelper.GenerateRandomString(10),
                        HomeAddress = BaseHelper.GenerateRandomString(10),
                        Notes = BaseHelper.GenerateRandomString(10)
                    });
                }
                if (format == "json")
                {
                    WriteContactsToJsonFile(contacts, writer);
                }
                else if (format == "xml")
                {
                    WriteContactsToXMLFile(contacts, writer);
                }
                else
                {
                    Console.Out.WriteLine("Unrecognized format: " + format);
                }
                writer.Close();
            }
            else
            {
                Console.Out.WriteLine("Format not selected or specified incorrectly");
            }
            writer.Close();

            Console.WriteLine("\n" + "Файл успешно создан. Нажмите любую кнопку для закрытия консоли.");
            Console.ReadKey();
        }

        static void WriteGroupsToXMLFile(List<GroupData> groups, StreamWriter writer)
        {
            new XmlSerializer(typeof(List<GroupData>)).Serialize(writer, groups);
        }

        static void WriteContactsToXMLFile(List<ContactData> contacts, StreamWriter writer)
        {
            new XmlSerializer(typeof(List<ContactData>)).Serialize(writer, contacts);
        }

        static void WriteGroupsToJsonFile(List<GroupData> groups, StreamWriter writer)
        {
            writer.Write(JsonConvert.SerializeObject(groups, Newtonsoft.Json.Formatting.Indented));
        }

        static void WriteContactsToJsonFile(List<ContactData> contacts, StreamWriter writer)
        {
            writer.Write(JsonConvert.SerializeObject(contacts, Newtonsoft.Json.Formatting.Indented));
        }
    }
}
