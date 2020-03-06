using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RentMe.DAL;

namespace RentMeTests.DalTests.MemberDalTest
{
	/// <summary>
	/// The test class for getting all members from the db 
	/// </summary>
	[TestClass()]
	public class GetAllMembersTests
	{

		[TestMethod()]
		public void GetAllMembersValidTest()
		{
			var memberDal = new MemberDal();
			var result = memberDal.GetAllMembers();

			Assert.AreEqual(2, result.Count);
		}

	}
}
