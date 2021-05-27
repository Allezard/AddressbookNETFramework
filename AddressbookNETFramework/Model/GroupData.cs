using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Linq;
using LinqToDB.Mapping;
using Newtonsoft.Json;
using System.IO;
using System.Xml.Serialization;
using AddressbookNETFramework.Helpers;

namespace AddressbookNETFramework.Model
{
    /// <summary>
    /// Содержит в себе параметры группы которые можно заполничть/получить и доп. методы.
    /// </summary>
    [Table(Name = "group_list")]
    public class GroupData : IEquatable<GroupData>, IComparable<GroupData>
    {
        public static IEnumerable<GroupData> GroupDataFromJsonFile()
        {
            var path = File.ReadAllText(@"C:\Users\Professional\source\repos\AddressbookNETFramework\AddressbookNETFramework\TestDataFolder\groups.json");
            var fileJson = JsonConvert.DeserializeObject<List<GroupData>>(path);
            return fileJson;
        }

        public static IEnumerable<GroupData> GroupDataFromXmlFile()
        {
            IEnumerable<GroupData> xmlData;
            var a = new XmlSerializer(typeof(List<GroupData>));
            using (StreamReader reader = new StreamReader(@"C:\Users\Professional\source\repos\AddressbookNETFramework\AddressbookNETFramework\TestDataFolder\groups.xml"))
            {
                xmlData = (List<GroupData>)a.Deserialize(reader);
            }
            return xmlData;
        }

        /// <summary>
        /// Пустой метод для реализации полей в группах.
        /// </summary>
        public GroupData()
        {

        }

        public GroupData GroupDataRandom()
        {
            GroupData generateGroup = new GroupData
            {
                GroupName = BaseHelper.GenerateRandomString(10),
                GroupHeader = BaseHelper.GenerateRandomString(10),
                GroupFooter = BaseHelper.GenerateRandomString(10)
            };
            return generateGroup;
        }

        public GroupData GroupDataRandomJson()
        {
            GroupData generateGroup = new GroupData
            {
                GroupName = GroupDataFromJsonFile().First().GroupName,
                GroupHeader = GroupDataFromJsonFile().First().GroupHeader,
                GroupFooter = GroupDataFromJsonFile().First().GroupFooter
            };
            return generateGroup;
        }

        public GroupData GroupDataRandomXML()
        {
            GroupData generateGroup = new GroupData
            {
                GroupName = GroupDataFromXmlFile().First().GroupName,
                GroupHeader = GroupDataFromXmlFile().First().GroupHeader,
                GroupFooter = GroupDataFromXmlFile().First().GroupFooter
            };
            return generateGroup;
        }

        public bool Equals(GroupData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return false;
            }
            if (Object.ReferenceEquals(this, other))
            {
                return true;
            }
            return GroupName == other.GroupName;
        }

        public int CompareTo(GroupData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return 1;
            }
            return GroupName.CompareTo(other.GroupName);
        }

        public override int GetHashCode()
        {
            return GroupName.GetHashCode();
        }

        public override string ToString()
        {
            return "Group name:  " + GroupName + "\nGroup header:  " + GroupHeader + "\nGroup footer:  " + GroupFooter;
        }

        [Column(Name = "group_name")]
        public string GroupName { get; set; }
        [Column(Name = "group_header")]
        public string GroupHeader { get; set; }
        [Column(Name = "group_footer")]
        public string GroupFooter { get; set; }
        [Column(Name = "group_id"), PrimaryKey, Identity]
        public string Id { get; set; }
        [Column(Name = "deprecated")]
        public string Deprecated { get; set; }


        public static List<GroupData> GetAll()
        {
            using (AddressBookDB db = new AddressBookDB())
            {
                return (from g in db.Groups select g).ToList();
            }
        }

        public List<ContactData> GetContacts()
        {
            using (AddressBookDB db = new AddressBookDB())
            {
                return (from c in db.Contacts
                        from gcr in db.GCR.Where(p => p.GroupID == Id && p.ContactID == c.Id && c.Deprecated == "0000-00-00 00:00:00")
                        select c).Distinct().ToList();
            }
        }
    }
}
