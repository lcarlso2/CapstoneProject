using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MySql.Data.MySqlClient;
using RentMe.DAL;
using RentMe.Models;
using SharedCode.DAL;

namespace RentMeTests.DalTests.CustomerDalTest
{

    /// <summary>
    /// The test class for updating an email for a customer 
    /// </summary>
    [TestClass()]
    public class UpdateEmailTests
    {
        [TestMethod()]
        public void UpdateEmail()
        {
            var customer = new Member { Email = "email@email.com" };
            var testEmail = "test@test.com";
            var customerDal = new MemberDal();

            Assert.AreEqual(1, customerDal.Authenticate(customer.Email, "Password"));

            customerDal.UpdateEmail(customer.Email, testEmail);

            Assert.AreEqual(1, customerDal.Authenticate(testEmail, "Password"));

            customerDal.UpdateEmail(testEmail, customer.Email);
        }

    }
}

