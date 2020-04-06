using Microsoft.VisualStudio.TestTools.UnitTesting;
using RentMe.DAL;
using RentMe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RentMeTests.DalTests.MemberDalTest
{
    /// <summary>
    /// The test class for updating an email for a customer 
    /// </summary>
    [TestClass()]
    public class UpdateBlacklistStatusTests
    {
        [TestMethod()]
        public void UpdateBlacklistStatus()
        { 
            var memberDal = new MemberDal();
            var members = memberDal.GetAllMembers();
            var member = members.Where(member => member.MemberId == 15).First();

            var originalStatus = member.IsBlacklisted;
            var newStatus = -1;
            if(originalStatus == 1)
            {
                newStatus = 0;

            } else if(originalStatus == 0)
            {
                newStatus = 1;
            }

            memberDal.UpdateBlacklistStatus(15);

            members = memberDal.GetAllMembers();
            member = members.Where(member => member.MemberId == 15).First();

            Assert.AreEqual(newStatus, member.IsBlacklisted);
        }
    }
}
