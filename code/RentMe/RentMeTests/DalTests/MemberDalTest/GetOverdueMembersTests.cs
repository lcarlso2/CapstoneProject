using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RentMe.DAL;

namespace RentMeTests.DalTests.MemberDalTest
{
	/// <summary>
	/// The test class for getting overdue members from the db 
	/// </summary>
	[TestClass()]
	public class GetOverdueMembersTests
	{
		[TestMethod()]
		public void GetOverdueMembersValidTest()
		{
			var memberDal = new MemberDal();
			var result = memberDal.GetOverdueMembers();

			Assert.AreEqual(1, result.Count);
		}
	}
}
