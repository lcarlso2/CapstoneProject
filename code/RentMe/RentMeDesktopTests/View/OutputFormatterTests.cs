using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharedCode.Model;
using SharedCode.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentMeDesktopTests.View
{
    [TestClass]
    public class OutputFormatterTests
    {

        [TestMethod()]
        public void GenerateHistoryOfInventoryItemOneRentalTest()
        {
            var itemOne = new RentalItem
            {
                Title = "Lord of the Rings",
                Category = "Adventure",
                MemberId = 1,
                MemberEmail = "testEmail@email.com",
                RentalDate = new DateTime(2020, 1, 1, 0, 0, 0),
                ReturnDate = new DateTime(2020, 1, 1, 0, 0, 0),
                RentalId = 1,
                Status = "Ordered",
                UpdateDateTime = new DateTime(2020, 1, 1, 0, 0, 0)
            };
            var items = new List<RentalItem>();
            items.Add(itemOne);

            var formatter = new OutputFormatter();
            var expectedOutput = "Title: Lord of the Rings" + "     Category: Adventure" + Environment.NewLine + "Member ID: 1" + "     Member Email: testEmail@email.com" + Environment.NewLine + 
                Environment.NewLine +
                "Date Rented: " + new DateTime(2020, 1, 1, 0, 0, 0) + "     Return Date: " + new DateTime(2020, 1, 1, 0, 0, 0) + Environment.NewLine + Environment.NewLine + "Status: Ordered" +
                "     Update Date: " + new DateTime(2020, 1, 1, 0, 0, 0) + Environment.NewLine;

            Assert.AreEqual(expectedOutput, formatter.GenerateHistoryOfInventoryItem(items));
        }

        [TestMethod()]
        public void GenerateHistoryOfInventoryItemTwoRentalsTest()
        {
            var itemOne = new RentalItem
            {
                Title = "Lord of the Rings",
                Category = "Adventure",
                MemberId = 1,
                MemberEmail = "testEmail@email.com",
                RentalDate = new DateTime(2020, 1, 1, 0, 0, 0),
                ReturnDate = new DateTime(2020, 1, 1, 0, 0, 0),
                RentalId = 1,
                Status = "Ordered",
                UpdateDateTime = new DateTime(2020, 1, 1, 0, 0, 0)
            };

            var itemTwo = new RentalItem
            {
                Title = "Harry Potter",
                Category = "Adventure",
                MemberId = 2,
                MemberEmail = "testEmail@email.com",
                RentalDate = new DateTime(2020, 1, 1, 0, 0, 0),
                ReturnDate = new DateTime(2020, 1, 1, 0, 0, 0),
                RentalId = 2,
                Status = "Ordered",
                UpdateDateTime = new DateTime(2020, 1, 1, 0, 0, 0)
            };
            var items = new List<RentalItem>();
            items.Add(itemOne);
            items.Add(itemTwo);

            var formatter = new OutputFormatter();
            var expectedOutput = "Title: Lord of the Rings" + "     Category: Adventure" + Environment.NewLine + "Member ID: 1" + "     Member Email: testEmail@email.com" + Environment.NewLine +
                Environment.NewLine +
                "Date Rented: " + new DateTime(2020, 1, 1, 0, 0, 0) + "     Return Date: " + new DateTime(2020, 1, 1, 0, 0, 0) + Environment.NewLine + Environment.NewLine + "Status: Ordered" +
                "     Update Date: " + new DateTime(2020, 1, 1, 0, 0, 0) + Environment.NewLine +
                "------------------------------------------------------------------------" + Environment.NewLine + Environment.NewLine +
                "Title: Harry Potter" + "     Category: Adventure" + Environment.NewLine + "Member ID: 2" + "     Member Email: testEmail@email.com" + Environment.NewLine + Environment.NewLine +
                "Date Rented: " + new DateTime(2020, 1, 1, 0, 0, 0) + "     Return Date: " + new DateTime(2020, 1, 1, 0, 0, 0) + Environment.NewLine + Environment.NewLine + "Status: Ordered" +
                "     Update Date: " + new DateTime(2020, 1, 1, 0, 0, 0) + Environment.NewLine;

            Assert.AreEqual(expectedOutput, formatter.GenerateHistoryOfInventoryItem(items));
        }


        [TestMethod()]
        public void GenerateHistoryOfInventoryItemOneRentalMoreThanOneUpdateTest()
        {
            var itemOne = new RentalItem
            {
                Title = "Lord of the Rings",
                Category = "Adventure",
                MemberId = 1,
                MemberEmail = "testEmail@email.com",
                RentalDate = new DateTime(2020, 1, 1, 0, 0, 0),
                ReturnDate = new DateTime(2020, 1, 1, 0, 0, 0),
                RentalId = 1,
                Status = "Ordered",
                UpdateDateTime = new DateTime(2020, 1, 1, 0, 0, 0)
            };

            var itemTwo = new RentalItem
            {
                Title = "Lord of the Rings",
                Category = "Adventure",
                MemberId = 1,
                MemberEmail = "testEmail@email.com",
                RentalDate = new DateTime(2020, 1, 1, 0, 0, 0),
                ReturnDate = new DateTime(2020, 1, 1, 0, 0, 0),
                RentalId = 1,
                Status = "Shipped",
                UpdateDateTime = new DateTime(2020, 1, 2, 0, 0, 0)
            };
            var items = new List<RentalItem>();
            items.Add(itemOne);
            items.Add(itemTwo);

            var formatter = new OutputFormatter();
            var expectedOutput = "Title: Lord of the Rings" + "     Category: Adventure" + Environment.NewLine + "Member ID: 1" + "     Member Email: testEmail@email.com" + Environment.NewLine +
                Environment.NewLine +
                "Date Rented: " + new DateTime(2020, 1, 1, 0, 0, 0) + "     Return Date: " + new DateTime(2020, 1, 1, 0, 0, 0) + Environment.NewLine + Environment.NewLine + "Status: Ordered" +
                "     Update Date: " + new DateTime(2020, 1, 1, 0, 0, 0) + Environment.NewLine + "Status: Shipped" +
                "     Update Date: " + new DateTime(2020, 1, 2, 0, 0, 0) + Environment.NewLine;
                

            Assert.AreEqual(expectedOutput, formatter.GenerateHistoryOfInventoryItem(items));
        }

        [TestMethod()]
        public void GenerateEmployeeHistoryTest()
        {
            var itemOne = new RentalItem
            {
                Title = "Lord of the Rings",
                Category = "Adventure",
                RentalDate = new DateTime(2020, 1, 1, 0, 0, 0),
                ReturnDate = new DateTime(2020, 1, 1, 0, 0, 0),
                RentalId = 1,
                Status = "Ordered",
                UpdateDateTime = new DateTime(2020, 1, 1, 0, 0, 0),
                EmployeeUsername = "cb"
            };

            var itemTwo = new RentalItem
            {
                Title = "Lord of the Rings",
                Category = "Adventure",
                RentalDate = new DateTime(2020, 1, 1, 0, 0, 0),
                ReturnDate = new DateTime(2020, 1, 1, 0, 0, 0),
                RentalId = 1,
                Status = "Shipped",
                UpdateDateTime = new DateTime(2020, 1, 2, 0, 0, 0),
                EmployeeUsername = "cb"

            };
            var items = new List<RentalItem>();
            items.Add(itemOne);
            items.Add(itemTwo);

            var formatter = new OutputFormatter();
            var expectedOutput = "Employee Username: cb" + Environment.NewLine + Environment.NewLine + "Title: Lord of the Rings" + "     Category: Adventure" + Environment.NewLine +
                Environment.NewLine +
                "Date Rented: " + new DateTime(2020, 1, 1, 0, 0, 0) + "     Return Date: " + new DateTime(2020, 1, 1, 0, 0, 0) + Environment.NewLine + Environment.NewLine + "Status: Ordered" +
                "     Update Date: " + new DateTime(2020, 1, 1, 0, 0, 0) + Environment.NewLine + "Status: Shipped" +
                "     Update Date: " + new DateTime(2020, 1, 2, 0, 0, 0) + Environment.NewLine;


            Assert.AreEqual(expectedOutput, formatter.GenerateEmployeeHistory(items));
        }
    }
}
