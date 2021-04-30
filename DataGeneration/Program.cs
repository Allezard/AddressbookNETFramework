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
            string type = args[0]; // Принимает 1-ый аргумент в виде названия типа ("typeGroup" или "typeContact")
            int count = Convert.ToInt32(args[1]); // Принимает 2-ой аргумент, создает нужное кол-во файлов
            StreamWriter writer = new StreamWriter(args[2]); // Принимает 3-ий аргумент в виде названия файла
            string format = args[3]; // Принимает 4-ый аргумент в виде типа файла

            if (type == "typeGroup")
            {
                List<GroupData> groups = new List<GroupData>();
                for (int i = 0; i < count; i++)
                {
                    groups.Add(new GroupData()
                    {
                        GroupName = BaseClass.GenerateRandomString(10),
                        GroupHeader = BaseClass.GenerateRandomString(10),
                        GroupFooter = BaseClass.GenerateRandomString(10)
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
                List<ContactData> contacts = new List<ContactData>();
                for (int i = 0; i < count; i++)
                {
                    contacts.Add(new ContactData()
                    {
                        FirstName = BaseClass.GenerateRandomString(10),
                        MiddleName = BaseClass.GenerateRandomString(10),
                        LastName = BaseClass.GenerateRandomString(10),
                        NickName = BaseClass.GenerateRandomString(10),
                        Company = BaseClass.GenerateRandomString(10),
                        Title = BaseClass.GenerateRandomString(10),
                        Address = BaseClass.GenerateRandomString(10),
                        HomePhone = BaseClass.GenerateRandomString(10),
                        MobilePhone = BaseClass.GenerateRandomString(10),
                        WorkPhone = BaseClass.GenerateRandomString(10),
                        Fax = BaseClass.GenerateRandomString(10),
                        Email = BaseClass.GenerateRandomString(10),
                        Email2 = BaseClass.GenerateRandomString(10),
                        Email3 = BaseClass.GenerateRandomString(10),
                        Homepage = BaseClass.GenerateRandomString(10),
                        SecondaryAddress = BaseClass.GenerateRandomString(10),
                        HomeAddress = BaseClass.GenerateRandomString(10),
                        Notes = BaseClass.GenerateRandomString(10)
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
                    Console.Out.WriteLine("Unrecognized format" + format);
                }
                writer.Close();
            }
            else
            {
                Console.Out.WriteLine("Format not selected or specified incorrectly");
            }
            writer.Close();
        }

        static void WriteGroupsToXMLFile(List<GroupData> groups, StreamWriter writer)
        {
            new XmlSerializer(typeof(List<GroupData>)).Serialize(writer, groups);
        }

        static void WriteGroupsToJsonFile(List<GroupData> groups, StreamWriter writer)
        {
            writer.Write(JsonConvert.SerializeObject(groups, Newtonsoft.Json.Formatting.Indented));
        }

        static void WriteContactsToXMLFile(List<ContactData> contacts, StreamWriter writer)
        {
            new XmlSerializer(typeof(List<ContactData>)).Serialize(writer, contacts);
        }

        static void WriteContactsToJsonFile(List<ContactData> contacts, StreamWriter writer)
        {
            writer.Write(JsonConvert.SerializeObject(contacts, Newtonsoft.Json.Formatting.Indented));
        }
    }
}
