using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;
using LinqToDB.Mapping;
using AddressbookNETFramework.Helpers;
using System.Threading;

namespace AddressbookNETFramework.Model
{
    /// <summary>
    /// Содержит в себе параметры контакта которые можно заполничть/получить и доп. методы.
    /// </summary>
    [Table(Name = "addressbook")]
    public class ContactData : IEquatable<ContactData>, IComparable<ContactData>
    {
        private string fullName;
        private string allPhones;
        private string allEmails;
        private string allDetails;

        /// <summary>
        /// Пустой метод для реализации полей в контактах.
        /// </summary>
        public ContactData()
        {

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
            return FirstName == other.FirstName;
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
            return (FirstName).GetHashCode();
        }

        public override string ToString()
        {
            return
                "\n" + "First name:  " + FirstName + ", \n" +
                "Middle name:  " + MiddleName + ", \n" +
                "Last name:  " + LastName + ", \n" +
                "NickName:  " + NickName + ", \n" +
                "Company:  " + Company + ", \n" +
                "Title:  " + Title + ", \n" +
                "Address:  " + Address + ", \n" +
                "HomePhone:  " + HomePhone + ", \n" +
                "MobilePhone:  " + MobilePhone + ", \n" +
                "WorkPhone:  " + WorkPhone + ", \n" +
                "Fax:  " + Fax + ", \n" +
                "Email:  " + Email + ", \n" +
                "Email2:  " + Email2 + ", \n" +
                "Email3:  " + Email3 + ", \n" +
                "Homepage:  " + Homepage + ", \n" +
                "SecondaryAddress:  " + SecondaryAddress + ", \n" +
                "HomeAddress:  " + HomeAddress + ", \n" +
                "Notes:  " + Notes + ", \n\n";
        }

        [Column(Name = "firstname")]
        public string FirstName { get; set; }
        [Column(Name = "middlename")]
        public string MiddleName { get; set; }
        [Column(Name = "lastname")]
        public string LastName { get; set; }
        [Column(Name = "nickname")]
        public string NickName { get; set; }
        [Column(Name = "company")]
        public string Company { get; set; }
        [Column(Name = "title")]
        public string Title { get; set; }
        [Column(Name = "address")]
        public string Address { get; set; }
        [Column(Name = "home")]
        public string HomePhone { get; set; }
        [Column(Name = "mobile")]
        public string MobilePhone { get; set; }
        [Column(Name = "work")]
        public string WorkPhone { get; set; }
        [Column(Name = "fax")]
        public string Fax { get; set; }
        [Column(Name = "email")]
        public string Email { get; set; }
        [Column(Name = "email2")]
        public string Email2 { get; set; }
        [Column(Name = "email3")]
        public string Email3 { get; set; }
        [Column(Name = "homepage")]
        public string Homepage { get; set; }
        public string BirthDay { get; set; }
        public string BirthMonth { get; set; }
        public string YearOfBirth { get; set; }
        public string AnniversDay { get; set; }
        public string AnniversMonth { get; set; }
        public string YearOfAnnivers { get; set; }
        [Column(Name = "address2")]
        public string SecondaryAddress { get; set; }
        [Column(Name = "phone2")]
        public string HomeAddress { get; set; }
        [Column(Name = "notes")]
        public string Notes { get; set; }
        [Column(Name = "id"), PrimaryKey, Identity]
        public string Id { get; set; }
        [Column(Name = "deprecated")]
        public string Deprecated { get; set; }

        public string FullName
        {
            get
            {
                if (fullName != null)
                {
                    return fullName;
                }
                else
                {
                    
                    return CleanUp(LastName) + ", " + CleanUp(FirstName).Trim();
                }
            }
            set
            {
                fullName = value;
            }
        }

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
                        $"Birthday {BirthDay}. {BirthMonth} {YearOfBirth} ({CalculateYearOfBirth()})" +
                        "\r\n" +
                        $"Anniversary {AnniversDay}. {char.ToUpper(AnniversMonth[0]) + AnniversMonth.Substring(1)} {YearOfAnnivers} ({CalculateYearOfAnnivers()})" + "\r\n" +
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
            if (String.IsNullOrEmpty(symbol))
            {
                return "";
            }            
            return Regex.Replace(symbol, "[ -()]", "") + "\r\n";
        }

        public static List<ContactData> GetAll()
        {
            using (AddressBookDB db = new AddressBookDB())
            {
                return (from c in db.Contacts.Where(x => x.Deprecated == "0000-00-00 00:00:00") select c).ToList();
            }
        }

        public DateTime RandomBirthYear()
        {
            Random rnd = new Random();
            var rangeDay = rnd.Next(1, 31);
            var rangeMonth = rnd.Next(0, 12);
            var rangeYear = rnd.Next(0, 102);

            DateTime dateTime = new DateTime(1920, 1, 1);
            DateTime randomBirthDMY = dateTime.AddDays(rangeDay).AddMonths(rangeMonth).AddYears(rangeYear);

            return randomBirthDMY;
        }

        public string CalculateYearOfBirth()
        {
            DateTime nowYear = DateTime.Today; // Передаем в переменную типа "DateTime" текущее значение dd, MM, yyyy.
            int differenceYear = nowYear.Year; // Вытаскиваем год из текущего значения.
            int data = differenceYear - Convert.ToInt32(YearOfBirth); // Получаем возраст путем вычитания года(в детализации) из текущего года.
            DateTime dateTime = new DateTime(0001, 1, 1); //Передаем в переменную типа "DateTime" исходное значение dd, MM, yyyy.
            int yd = Convert.ToInt32(YearOfBirth); // Конвертируем год рождения из профиля в int.
            EnumClass.EnumMonths enumM = (EnumClass.EnumMonths)Enum.Parse(typeof(EnumClass.EnumMonths), BirthMonth, true);
            // Создаем переменную типа "Enum", передаем конвертированную строку с учетом регистра.
            int bm = (int)enumM; // Конвертируем месяц рождения(enam) из профиля в int.
            int bd = Convert.ToInt32(BirthDay); // Конвертируем день рождения из профиля в int.
            // 1. Передаем ранее созданные переменные с числом, месяцем и годом.
            // 2. Вычитаем из них исходные значения чтобы получить данные из профиля в виде "DateTime".
            DateTime ymd = dateTime.AddYears(yd-1).AddMonths(bm-1).AddDays(bd-1);

            // 3. Получаем точно кол-во лет путем проверок данных в профиле и текущего числа/месяца года.
            if (ymd.Month >= nowYear.Month && ymd.Day >= nowYear.Day ||
                ymd.Month >= nowYear.Month && ymd.Day > nowYear.Day ||
                ymd.Month > nowYear.Month && ymd.Day < nowYear.Day)
            {
                // Если одно из 3х условий - true, то вычитаем один год из "data", где у нас уже посчитан "грязный" возраст.
                data--;
            }
            return data.ToString();
        }

        public DateTime RandomAnniversYear()
        {
            Random rnd = new Random();
            var rangeDay = rnd.Next(1, 31);
            var rangeMonth = rnd.Next(0, 12);
            var rangeYear = rnd.Next(0, 102);

            DateTime dateTime = new DateTime(1920, 1, 1);
            DateTime randomAnniversDMY = dateTime.AddDays(rangeDay).AddMonths(rangeMonth).AddYears(rangeYear);

            return randomAnniversDMY;
        }

        public string CalculateYearOfAnnivers()
        {
            DateTime nowYear = DateTime.Today;
            int differenceYear = nowYear.Year;
            int data = differenceYear - Convert.ToInt32(YearOfAnnivers);
            DateTime dateTime = new DateTime(0001, 1, 1);

            int yd = Convert.ToInt32(YearOfAnnivers);
            EnumClass.EnumMonths enumM = (EnumClass.EnumMonths)Enum.Parse(typeof(EnumClass.EnumMonths), AnniversMonth, true);
            int bm = (int)enumM;
            int bd = Convert.ToInt32(AnniversDay);

            DateTime ymd = dateTime.AddYears(yd - 1).AddMonths(bm - 1).AddDays(bd - 1);

            if (ymd.Month > nowYear.Month)
            {
                data--;
            }
            return data.ToString();
        }
    }
}
