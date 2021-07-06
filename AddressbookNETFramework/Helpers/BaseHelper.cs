using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using OpenQA.Selenium;
using AddressbookNETFramework.Model;
using System.Threading;
using System.IO;
using Newtonsoft.Json;
using System.Xml.Serialization;
using System.Linq;

namespace AddressbookNETFramework.Helpers
{
    public class BaseHelper
    {
        protected IWebDriver webDriver;
        public static Random rnd = new Random();
        public static string derictoryPath = @"C:\Users\User\Source\Repos\AddressbookNETFramework\AddressbookNETFramework\TestDataFolder\";

        public BaseHelper(IWebDriver webDriver)
        {
            this.webDriver = webDriver;
        }

        public static IEnumerable<ContactData> ContactDataFromJsonFile(string fileName)
        {
            var path = File.ReadAllText(derictoryPath + fileName);
            var fileJson = JsonConvert.DeserializeObject<List<ContactData>>(path);
            return fileJson;
        }

        public static IEnumerable<ContactData> ContactDataFromXmlFile(string fileName)
        {
            IEnumerable<ContactData> xmlData;
            var a = new XmlSerializer(typeof(List<ContactData>));
            using (StreamReader reader = new StreamReader(derictoryPath + fileName))
            {
                xmlData = (List<ContactData>)a.Deserialize(reader);
            }
            return xmlData;
        }

        public static IEnumerable<GroupData> GroupDataFromJsonFile(string fileName)
        {
            var path = File.ReadAllText(derictoryPath + fileName);
            var fileJson = JsonConvert.DeserializeObject<List<GroupData>>(path);
            return fileJson;
        }

        public static IEnumerable<GroupData> GroupDataFromXmlFile(string fileName)
        {
            IEnumerable<GroupData> xmlData;
            var a = new XmlSerializer(typeof(List<GroupData>));
            using (StreamReader reader = new StreamReader(derictoryPath + fileName))
            {
                xmlData = (List<GroupData>)a.Deserialize(reader);
            }
            return xmlData;
        }

        /// <summary>
        /// Метод объединяющий в себе очистку поля и его заполнения если оно не пустое.
        /// </summary>
        /// <param name="locator">Принимает локатор.</param>
        /// <param name="text">Текст для заполнения поля.</param>
        public void EditGropMethod(By locator, string text)
        {
            if (text != null)
            {
                webDriver.FindElement(locator).Clear();
                webDriver.FindElement(locator).SendKeys(text);
            }
        }

        /// <summary>
        /// Проверка наличия "Logout" на странице.
        /// </summary>
        /// <returns>true - если залогинены, false - не залогинены.</returns>
        public bool CheckLoginPresent()
        {
            try
            {
                webDriver.FindElement(By.LinkText("Logout"));
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        /// <summary>
        /// Записывает в переменную "text" логин по которому мы вошли.
        /// </summary>
        /// <returns>Очищенный от "()" логин.</returns>
        public string GetLoggedUserName()
        {
            //string text = webDriver.FindElement(By.Name("Logout")).FindElement(By.TagName("b")).Text;
            string text = webDriver.FindElement(By.TagName("b")).Text;
            return text.Substring(1, text.Length - 2);
        }

        /// <summary>
        /// Проверяем, что мы залогинены в систему и под корректным юзером.
        /// </summary>
        /// <param name="data">Заглушка, для возврата логина.</param>
        /// <returns>true - если залогинены и роль текст логина на главной равен логину входа, false - если одно из условий не выполнено.</returns>
        public bool IsLoggedInUser(AccountData data)
        {
            return CheckLoginPresent()
                && GetLoggedUserName() == data.Username;
        }

        /// <summary>
        /// Проверка на наличие объекта (контакта или группы).
        /// </summary>
        /// <param name="index">Переменная для выбора нужного объекта.</param>
        /// <returns>true - если объект по индексу найден, false - если не найден.</returns>
        public bool CheckElementPresent(int index)
        {
            try
            {
                webDriver.FindElement(By.XPath("(//input[@name='selected[]'])[" + (index + 1) + "]"));
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        /// <summary>
        /// Метод генерирующий случайные символы в диапазоне латинского алфавита.
        /// </summary>
        /// <param name="size">Кол-во сгенерированных символов.</param>
        /// <param name="lowerCase">Если true, то маленькие буквы, если false - большие.</param>
        /// <returns>Возвращает преобразованное значение данного экземпляра в текст.</returns>
        public static string GenerateRandomString(int size, bool lowerCase = true)
        {
            StringBuilder builder = new StringBuilder();

            char l;

            for (int i = 0; i < size; i++)
            {
                l = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * rnd.NextDouble() + 65)));
                builder.Append(l);
            }

            if (lowerCase)
                return builder.ToString().ToLower();

            return builder.ToString();
        }

        public ContactData ContactDataRandom()
        {
            ContactData generateContacntData = new ContactData();
            generateContacntData.BirthDay = generateContacntData.RandomAllYear(rnd).Day.ToString();
            generateContacntData.BirthMonth = ((EnumClass.EnumMonths)generateContacntData.RandomAllYear(rnd).Month).ToString();
            generateContacntData.YearOfBirth = generateContacntData.RandomAllYear(rnd).Year.ToString();
            generateContacntData.AnniversDay = generateContacntData.RandomAllYear(rnd).Day.ToString();
            generateContacntData.AnniversMonth = ((EnumClass.EnumMonths)generateContacntData.RandomAllYear(rnd).Month).ToString();
            generateContacntData.YearOfAnnivers = generateContacntData.RandomAllYear(rnd).Year.ToString();
            generateContacntData.FirstName = GenerateRandomString(10);
            generateContacntData.MiddleName = GenerateRandomString(10);
            generateContacntData.LastName = GenerateRandomString(10);
            generateContacntData.NickName = GenerateRandomString(10);
            generateContacntData.Company = GenerateRandomString(10);
            generateContacntData.Title = GenerateRandomString(10);
            generateContacntData.Address = GenerateRandomString(10);
            generateContacntData.HomePhone = GenerateRandomString(10);
            generateContacntData.MobilePhone = GenerateRandomString(10);
            generateContacntData.WorkPhone = GenerateRandomString(10);
            generateContacntData.Fax = GenerateRandomString(10);
            generateContacntData.Email = GenerateRandomString(10);
            generateContacntData.Email2 = GenerateRandomString(10);
            generateContacntData.Email3 = GenerateRandomString(10);
            generateContacntData.Homepage = GenerateRandomString(10);
            generateContacntData.SecondaryAddress = GenerateRandomString(10);
            generateContacntData.HomeAddress = GenerateRandomString(10);
            generateContacntData.Notes = GenerateRandomString(10);

            return generateContacntData;
        }

        public ContactData ContactDataRandomJson(string fileName)
        {
            ContactData generateContacntData = new ContactData
            {
                BirthDay = ContactDataFromJsonFile(fileName).First().BirthDay,
                BirthMonth = ContactDataFromJsonFile(fileName).First().BirthMonth,
                YearOfBirth = ContactDataFromJsonFile(fileName).First().YearOfBirth,
                AnniversDay = ContactDataFromJsonFile(fileName).First().AnniversDay,
                AnniversMonth = ContactDataFromJsonFile(fileName).First().AnniversMonth,
                YearOfAnnivers = ContactDataFromJsonFile(fileName).First().YearOfAnnivers,
                FirstName = ContactDataFromJsonFile(fileName).First().FirstName,
                MiddleName = ContactDataFromJsonFile(fileName).First().MiddleName,
                LastName = ContactDataFromJsonFile(fileName).First().LastName,
                NickName = ContactDataFromJsonFile(fileName).First().NickName,
                Company = ContactDataFromJsonFile(fileName).First().Company,
                Title = ContactDataFromJsonFile(fileName).First().Title,
                Address = ContactDataFromJsonFile(fileName).First().Address,
                HomePhone = ContactDataFromJsonFile(fileName).First().HomePhone,
                MobilePhone = ContactDataFromJsonFile(fileName).First().MobilePhone,
                WorkPhone = ContactDataFromJsonFile(fileName).First().WorkPhone,
                Fax = ContactDataFromJsonFile(fileName).First().Fax,
                Email = ContactDataFromJsonFile(fileName).First().Email,
                Email2 = ContactDataFromJsonFile(fileName).First().Email2,
                Email3 = ContactDataFromJsonFile(fileName).First().Email3,
                Homepage = ContactDataFromJsonFile(fileName).First().Homepage,
                SecondaryAddress = ContactDataFromJsonFile(fileName).First().SecondaryAddress,
                HomeAddress = ContactDataFromJsonFile(fileName).First().HomeAddress,
                Notes = ContactDataFromJsonFile(fileName).First().Notes
            };
            return generateContacntData;
        }

        public ContactData ContactDataRandomXML(string fileName)
        {
            ContactData generateContacntData = new ContactData
            {
                BirthDay = ContactDataFromXmlFile(fileName).First().BirthDay,
                BirthMonth = ContactDataFromXmlFile(fileName).First().BirthMonth,
                YearOfBirth = ContactDataFromXmlFile(fileName).First().YearOfBirth,
                AnniversDay = ContactDataFromXmlFile(fileName).First().AnniversDay,
                AnniversMonth = ContactDataFromXmlFile(fileName).First().AnniversMonth,
                YearOfAnnivers = ContactDataFromXmlFile(fileName).First().YearOfAnnivers,
                FirstName = ContactDataFromXmlFile(fileName).First().FirstName,
                MiddleName = ContactDataFromXmlFile(fileName).First().MiddleName,
                LastName = ContactDataFromXmlFile(fileName).First().LastName,
                NickName = ContactDataFromXmlFile(fileName).First().NickName,
                Company = ContactDataFromXmlFile(fileName).First().Company,
                Title = ContactDataFromXmlFile(fileName).First().Title,
                Address = ContactDataFromXmlFile(fileName).First().Address,
                HomePhone = ContactDataFromXmlFile(fileName).First().HomePhone,
                MobilePhone = ContactDataFromXmlFile(fileName).First().MobilePhone,
                WorkPhone = ContactDataFromXmlFile(fileName).First().WorkPhone,
                Fax = ContactDataFromXmlFile(fileName).First().Fax,
                Email = ContactDataFromXmlFile(fileName).First().Email,
                Email2 = ContactDataFromXmlFile(fileName).First().Email2,
                Email3 = ContactDataFromXmlFile(fileName).First().Email3,
                Homepage = ContactDataFromXmlFile(fileName).First().Homepage,
                SecondaryAddress = ContactDataFromXmlFile(fileName).First().SecondaryAddress,
                HomeAddress = ContactDataFromXmlFile(fileName).First().HomeAddress,
                Notes = ContactDataFromXmlFile(fileName).First().Notes
            };
            return generateContacntData;
        }

        public GroupData GroupDataRandom()
        {
            GroupData generateGroup = new GroupData
            {
                GroupName = GenerateRandomString(10),
                GroupHeader = GenerateRandomString(10),
                GroupFooter = GenerateRandomString(10)
            };
            return generateGroup;
        }

        public GroupData GroupDataRandomJson(string fileName)
        {
            GroupData generateGroup = new GroupData
            {
                GroupName = GroupDataFromJsonFile(fileName).First().GroupName,
                GroupHeader = GroupDataFromJsonFile(fileName).First().GroupHeader,
                GroupFooter = GroupDataFromJsonFile(fileName).First().GroupFooter
            };
            return generateGroup;
        }

        public GroupData GroupDataRandomXML(string fileName)
        {
            GroupData generateGroup = new GroupData
            {
                GroupName = GroupDataFromXmlFile(fileName).First().GroupName,
                GroupHeader = GroupDataFromXmlFile(fileName).First().GroupHeader,
                GroupFooter = GroupDataFromXmlFile(fileName).First().GroupFooter
            };
            return generateGroup;
        }

        public Dictionary<string, string> ContactDataList()
        {
            ContactData generateContacntData = new ContactData();

            generateContacntData.BirthDay = generateContacntData.RandomAllYear(rnd).Day.ToString();
            generateContacntData.BirthMonth = ((EnumClass.EnumMonths)generateContacntData.RandomAllYear(rnd).Month).ToString();
            generateContacntData.YearOfBirth = generateContacntData.RandomAllYear(rnd).Year.ToString();
            generateContacntData.AnniversDay = generateContacntData.RandomAllYear(rnd).Day.ToString();
            generateContacntData.AnniversMonth = ((EnumClass.EnumMonths)generateContacntData.RandomAllYear(rnd).Month).ToString();
            generateContacntData.YearOfAnnivers = generateContacntData.RandomAllYear(rnd).Year.ToString();
            generateContacntData.FirstName = GenerateRandomString(10);
            generateContacntData.MiddleName = GenerateRandomString(10);
            generateContacntData.LastName = GenerateRandomString(10);
            generateContacntData.NickName = GenerateRandomString(10);
            generateContacntData.Company = GenerateRandomString(10);
            generateContacntData.Title = GenerateRandomString(10);
            generateContacntData.Address = GenerateRandomString(10);
            generateContacntData.HomePhone = GenerateRandomString(10);
            generateContacntData.MobilePhone = GenerateRandomString(10);
            generateContacntData.WorkPhone = GenerateRandomString(10);
            generateContacntData.Fax = GenerateRandomString(10);
            generateContacntData.Email = GenerateRandomString(10);
            generateContacntData.Email2 = GenerateRandomString(10);
            generateContacntData.Email3 = GenerateRandomString(10);
            generateContacntData.Homepage = GenerateRandomString(10);
            generateContacntData.SecondaryAddress = GenerateRandomString(10);
            generateContacntData.HomeAddress = GenerateRandomString(10);
            generateContacntData.Notes = GenerateRandomString(10);

            var elemAndProp = new Dictionary<string, string>
            {
                { "email",  generateContacntData.Email },
                { "email2",  generateContacntData.Email2 },
                { "email3",  generateContacntData.Email3 },
                { "firstname",  generateContacntData.FirstName },
                { "middlename",  generateContacntData.MiddleName },
                { "lastname",  generateContacntData.LastName },
                { "nickname",  generateContacntData.NickName },
                { "company",  generateContacntData.Company },
                { "title",  generateContacntData.Title },
                { "address",  generateContacntData.Address },
                { "home",  generateContacntData.HomePhone },
                { "mobile",  generateContacntData.MobilePhone },
                { "work",  generateContacntData.WorkPhone },
                { "fax",  generateContacntData.Fax },
                { "homepage",  generateContacntData.Homepage },
                { "bday",  generateContacntData.BirthDay },
                { "bmonth",  generateContacntData.BirthMonth },
                { "byear",  generateContacntData.YearOfBirth },
                { "aday",  generateContacntData.AnniversDay },
                { "amonth",  generateContacntData.AnniversMonth },
                { "ayear",  generateContacntData.YearOfAnnivers },
                { "address2",  generateContacntData.SecondaryAddress },
                { "phone2",  generateContacntData.HomeAddress },
                { "notes",  generateContacntData.Notes },
            };

            return elemAndProp;
        }
    }
}
