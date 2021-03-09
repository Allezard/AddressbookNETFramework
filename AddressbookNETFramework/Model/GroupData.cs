using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace AddressbookNETFramework.Model
{
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

        public string GroupName { get; set; }
        public string GroupHeader { get; set; }
        public string GroupFooter { get; set; }
        public string Id { get; set; }
    }
}
