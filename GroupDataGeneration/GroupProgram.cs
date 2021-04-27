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

namespace GroupDataGeneration
{
    class GroupProgram
    {
        static void Main(string[] args)
        {
            int count = Convert.ToInt32(args[0]);
            StreamWriter writer = new StreamWriter(args[1]);
            string format = args[2];

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

        static void WriteGroupsToXMLFile(List<GroupData> groups, StreamWriter writer)
        {
            new XmlSerializer(typeof(List<GroupData>)).Serialize(writer, groups);
        }

        static void WriteGroupsToJsonFile(List<GroupData> groups, StreamWriter writer)
        {
            writer.Write(JsonConvert.SerializeObject(groups, Newtonsoft.Json.Formatting.Indented));
        }
    }
}
