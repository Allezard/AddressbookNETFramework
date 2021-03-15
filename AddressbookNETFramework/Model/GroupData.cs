using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Linq;
using LinqToDB.Mapping;

namespace AddressbookNETFramework.Model
{
    [Table(Name = "group_list")]
    public class GroupData : IEquatable<GroupData>, IComparable<GroupData>
    {

        public GroupData()
        {

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

        public static List<GroupData> GetAll()
        {
            using (AddressBookDB db = new AddressBookDB())
            {
                return (from g in db.Groups select g).ToList();
            }
        }
    }
}
