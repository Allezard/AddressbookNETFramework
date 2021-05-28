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

        public BaseHelper(IWebDriver webDriver)
        {
            this.webDriver = webDriver;
        }

        public static IEnumerable<ContactData> ContactDataFromJsonFile()
        {
            var path = File.ReadAllText(@"C:\Users\Professional\source\repos\AddressbookNETFramework\AddressbookNETFramework\TestDataFolder\contacts.json");
            var fileJson = JsonConvert.DeserializeObject<List<ContactData>>(path);
            return fileJson;
        }

        public static IEnumerable<ContactData> ContactDataFromXmlFile()
        {
            IEnumerable<ContactData> xmlData;
            var a = new XmlSerializer(typeof(List<ContactData>));
            using (StreamReader reader = new StreamReader(@"C:\Users\Professional\source\repos\AddressbookNETFramework\AddressbookNETFramework\TestDataFolder\contacts.xml"))
            {
                xmlData = (List<ContactData>)a.Deserialize(reader);
            }
            return xmlData;
        }

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
            generateContacntData.BirthDay = generateContacntData.RandomBirthYear(rnd).Day.ToString();
            generateContacntData.BirthMonth = ((EnumClass.EnumMonths)generateContacntData.RandomBirthYear(rnd).Month).ToString();
            generateContacntData.YearOfBirth = generateContacntData.RandomBirthYear(rnd).Year.ToString();
            generateContacntData.AnniversDay = generateContacntData.RandomAnniversYear(rnd).Day.ToString();
            generateContacntData.AnniversMonth = ((EnumClass.EnumMonths)generateContacntData.RandomAnniversYear(rnd).Month).ToString();
            generateContacntData.YearOfAnnivers = generateContacntData.RandomAnniversYear(rnd).Year.ToString();
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

        public ContactData ContactDataRandomJson()
        {
            ContactData generateContacntData = new ContactData
            {
                BirthDay = ContactDataFromJsonFile().First().BirthDay,
                BirthMonth = ContactDataFromJsonFile().First().BirthMonth,
                YearOfBirth = ContactDataFromJsonFile().First().YearOfBirth,
                AnniversDay = ContactDataFromJsonFile().First().AnniversDay,
                AnniversMonth = ContactDataFromJsonFile().First().AnniversMonth,
                YearOfAnnivers = ContactDataFromJsonFile().First().YearOfAnnivers,
                FirstName = ContactDataFromJsonFile().First().FirstName,
                MiddleName = ContactDataFromJsonFile().First().MiddleName,
                LastName = ContactDataFromJsonFile().First().LastName,
                NickName = ContactDataFromJsonFile().First().NickName,
                Company = ContactDataFromJsonFile().First().Company,
                Title = ContactDataFromJsonFile().First().Title,
                Address = ContactDataFromJsonFile().First().Address,
                HomePhone = ContactDataFromJsonFile().First().HomePhone,
                MobilePhone = ContactDataFromJsonFile().First().MobilePhone,
                WorkPhone = ContactDataFromJsonFile().First().WorkPhone,
                Fax = ContactDataFromJsonFile().First().Fax,
                Email = ContactDataFromJsonFile().First().Email,
                Email2 = ContactDataFromJsonFile().First().Email2,
                Email3 = ContactDataFromJsonFile().First().Email3,
                Homepage = ContactDataFromJsonFile().First().Homepage,
                SecondaryAddress = ContactDataFromJsonFile().First().SecondaryAddress,
                HomeAddress = ContactDataFromJsonFile().First().HomeAddress,
                Notes = ContactDataFromJsonFile().First().Notes
            };
            return generateContacntData;
        }

        public ContactData ContactDataRandomXML()
        {
            ContactData generateContacntData = new ContactData
            {
                BirthDay = ContactDataFromXmlFile().First().BirthDay,
                BirthMonth = ContactDataFromXmlFile().First().BirthMonth,
                YearOfBirth = ContactDataFromXmlFile().First().YearOfBirth,
                AnniversDay = ContactDataFromXmlFile().First().AnniversDay,
                AnniversMonth = ContactDataFromXmlFile().First().AnniversMonth,
                YearOfAnnivers = ContactDataFromXmlFile().First().YearOfAnnivers,
                FirstName = ContactDataFromXmlFile().First().FirstName,
                MiddleName = ContactDataFromXmlFile().First().MiddleName,
                LastName = ContactDataFromXmlFile().First().LastName,
                NickName = ContactDataFromXmlFile().First().NickName,
                Company = ContactDataFromXmlFile().First().Company,
                Title = ContactDataFromXmlFile().First().Title,
                Address = ContactDataFromXmlFile().First().Address,
                HomePhone = ContactDataFromXmlFile().First().HomePhone,
                MobilePhone = ContactDataFromXmlFile().First().MobilePhone,
                WorkPhone = ContactDataFromXmlFile().First().WorkPhone,
                Fax = ContactDataFromXmlFile().First().Fax,
                Email = ContactDataFromXmlFile().First().Email,
                Email2 = ContactDataFromXmlFile().First().Email2,
                Email3 = ContactDataFromXmlFile().First().Email3,
                Homepage = ContactDataFromXmlFile().First().Homepage,
                SecondaryAddress = ContactDataFromXmlFile().First().SecondaryAddress,
                HomeAddress = ContactDataFromXmlFile().First().HomeAddress,
                Notes = ContactDataFromXmlFile().First().Notes
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
    }
}
