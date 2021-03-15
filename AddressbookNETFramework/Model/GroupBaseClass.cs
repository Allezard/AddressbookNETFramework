using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace AddressbookNETFramework.Model
{
    public class GroupBaseClass : BaseClass
    {
        [TearDown]
        public void CompareGroupsUI_DB()
        {
            if (PERFORM_LONG_UI_CHECKS)
            {
                List<GroupData> fromUI = app.Groups.GetGroupList();
                List<GroupData> fromDB = GroupData.GetAll();
                fromUI.Sort();
                fromDB.Sort();
                Assert.AreEqual(fromUI, fromDB);
                Console.Out.WriteLine("Список групп fromUI: " + fromUI.Count + "\n" + "Список групп fromDB: " + fromDB.Count);
            }
        }
    }
}
