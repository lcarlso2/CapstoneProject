using System;
using System.Collections.Generic;
using System.Text;
using SharedCode.DAL;
using SharedCode.Model;

namespace SharedCode.TestObjects
{
	/// <summary>
	/// The mock rental dal for testing purposes
	/// </summary>
	public class MockRentalDal : IRentalDal
	{

		public bool ThrowError { get; set; }

		public List<RentalItem> RetrieveAllRentedItems()
		{
			var items = new List<RentalItem>{new RentalItem{RentalId = 1, Status = "Ordered"}};
			if (this.ThrowError)
			{
				throw new Exception();
			}
			return items;
		}
		public List<RentalItem> RetrieveAllRentalsByCustomer(string email)
		{
			var items = new List<RentalItem> { new RentalItem { RentalId = 1, Status = "Ordered", RentalDate = DateTime.Now, Title = "Test"} };
			if (this.ThrowError)
			{
				throw new Exception();
			}
			return items;
		}


		public List<RentalItem> RetrieveSelectRentedItems(string selectedStatus)
		{
			var items = new List<RentalItem>();
			if (this.ThrowError)
			{
				throw new Exception();
			}
			return items;
		}


		public int UpdateStatus(int transactionId, string status, int employeeId)
		{
			if (this.ThrowError)
			{
				throw new Exception();
			}

			return 1;
		}

	}
}
