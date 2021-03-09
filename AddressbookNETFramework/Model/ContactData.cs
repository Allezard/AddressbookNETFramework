using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Text.RegularExpressions;

namespace AddressbookNETFramework.Model
{
    public class ContactData : IEquatable<ContactData>, IComparable<ContactData>
    {
        private string allPhones;
        private string allEmails;
        private string allDetails;

        public ContactData()
        {
            //FirstName = firstName;
            //LastName = lastName;
        }

        public bool Equals(ContactData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return false;
            }
            if (Object.ReferenceEquals(this, other))
            {
                return true;
            }
            return FirstName == other.FirstName
                && LastName == other.LastName;
        }

        public int CompareTo(ContactData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return 1;
            }
            return FirstName.CompareTo(other.FirstName);
        }

        public override int GetHashCode()
        {
            return (FirstName, LastName).GetHashCode();
        }

        public override string ToString()
        {
            return
                "\nFirst name:  " + FirstName +
                "\nMiddle name:  " + MiddleName +
                "\nLast name:  " + LastName +
                "\nNickName:  " + NickName +
                "\nCompany:  " + Company +
                "\nTitle:  " + Title +
                "\nAddress:  " + Address +
                "\nHomePhone:  " + HomePhone +
                "\nMobilePhone:  " + MobilePhone +
                "\nWorkPhone:  " + WorkPhone +
                "\nFax:  " + Fax +
                "\nEmail:  " + Email +
                "\nEmail2:  " + Email2 +
                "\nEmail3:  " + Email3 +
                "\nHomepage:  " + Homepage +
                "\nSecondaryAddress:  " + SecondaryAddress +
                "\nHomeAddress:  " + HomeAddress +
                "\nNotes:  " + Notes + "\n\n";
        }

        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string NickName { get; set; }
        public string Company { get; set; }
        public string Title { get; set; }
        public string Address { get; set; }
        public string HomePhone { get; set; }
        public string MobilePhone { get; set; }
        public string WorkPhone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string Email2 { get; set; }
        public string Email3 { get; set; }
        public string Homepage { get; set; }
        public string SecondaryAddress { get; set; }
        public string HomeAddress { get; set; }
        public string Notes { get; set; }
        public string Id { get; set; }
        public string AllPhones
        {
            get
            {
                if (allPhones != null)
                {
                    return allPhones;
                }
                else
                {
                    return CleanUp(HomePhone) + CleanUp(MobilePhone) + CleanUp(WorkPhone) + CleanUp(HomeAddress).Trim();
                }
            }
            set
            {
                allPhones = value;
            }
        }

        public string AllEmails
        {
            get
            {
                if (allEmails != null)
                {
                    return allEmails;
                }
                else
                {
                    return CleanUp(Email) + CleanUp(Email2) + CleanUp(Email3).Trim();
                }
            }
            set
            {
                allEmails = value;
            }
        }

        public string AllDetails
        {
            get
            {
                if (allDetails != null)
                {
                    return allDetails;
                }
                else
                {
                    return
                        FirstName + " " + MiddleName + " " + CleanUp(LastName) +
                        NickName + "\r\n" +
                        Title + "\r\n" +
                        Company + "\r\n" +
                        Address + "\r\n" +
                        "\r\n" +
                        "H: " + HomePhone + "\r\n" +
                        "M: " + MobilePhone + "\r\n" +
                        "W: " + WorkPhone + "\r\n" +
                        "F: " + Fax + "\r\n" +
                        "\r\n" +
                        Email + " (www." + Email.Substring(1) + ")" + "\r\n" +
                        Email2 + " (www." + Email2.Substring(1) + ")" + "\r\n" +
                        Email3 + " (www." + Email3.Substring(1) + ")" + "\r\n" +
                        "Homepage:" + "\r\n" +
                        Homepage + "\r\n" +
                        "\r\n" +
                        "Birthday 16. April 1994 (26)" +
                        "\r\n" +
                        "Anniversary 20. March 2020 (0)" + "\r\n" +
                        "\r\n" +
                        SecondaryAddress + "\r\n" +
                        "\r\n" +
                        "P: " + HomeAddress + "\r\n" +
                        "\r\n" +
                        Notes;
                }
            }
            set
            {
                allDetails = value;
            }
        }

        private string CleanUp(string symbol)
        {
            if (symbol == null || symbol == "")
            {
                return "";
            }
            return Regex.Replace(symbol, "[ -()]", "") + "\r\n";
        }
    }
}
