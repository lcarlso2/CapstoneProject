using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharedCode.DAL;
using SharedCode.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentMeTests.SharedCodeTests.DalTests.EmployeeDalTests
{
    /// <summary>
    /// The test class for get employee history
    /// </summary>
    [TestClass()]
    public class GetEmployeeHistoryTests
    {

        [TestMethod()]
        public void GetEmployeeHistoryValidTest()
        {
            var employeeDal = new EmployeeDal();
            var result = employeeDal.GetEmployeeHistory(4);
            Assert.AreEqual(0, result.Count);
        }
    }
}
