﻿using MySql.Data.MySqlClient;
using RentMe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentMe.DAL
{
	/// <summary>
	/// The class responsible for borrowing items 
	/// </summary>
	public class BorrowDal
	{

		public static void RentItem(Customer customer, MediaModel media)
		{
			try
			{
				var conn = DBConnection.GetConnection();
				using (conn)
				{
					conn.Open();
					using (var transaction = conn.BeginTransaction())
					{


						var query = "insert into BorrowedItem(ItemId, CustomerEmail, RentalDate, ReturnDate) values (@itemID, @customerEmail, @rentalDate, @returnDate)";

						using (var cmd = new MySqlCommand(query, conn))
						{
							cmd.Transaction = transaction;

							cmd.Parameters.Add("@itemID", MySqlDbType.Int32);
							cmd.Parameters["@itemID"].Value = media.Id;

							cmd.Parameters.Add("@customerEmail", MySqlDbType.VarChar);
							cmd.Parameters["@customerEmail"].Value = customer.Email;

							cmd.Parameters.Add("@rentalDate", MySqlDbType.DateTime);
							cmd.Parameters["@rentalDate"].Value = DateTime.Now;

							cmd.Parameters.Add("@returnDate", MySqlDbType.DateTime);
							cmd.Parameters["@returnDate"].Value = DateTime.Now.AddDays(14);

							if (cmd.ExecuteNonQuery() != 1)
							{
								transaction.Rollback();
							}

							cmd.Parameters.Clear();
							cmd.Parameters.Add("@id", MySqlDbType.Int32);

							cmd.CommandText = "update Media set Qty = Qty - 1 where Id = @id;";
					
							cmd.Parameters["@id"].Value = media.Id;

							if (cmd.ExecuteNonQuery() != 1)
							{
								transaction.Rollback();
							}

							transaction.Commit();
						}
						conn.Close();
					}
				}
			} catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}