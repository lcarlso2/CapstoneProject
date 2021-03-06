﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using RentMe.Models;
using SharedCode.DAL;
using SharedCode.Model;

namespace RentMe.DAL
{
	public class LibrarianDal : ILibrarianDal
	{
		/// <summary>
		/// Authenticates the specified librarian.
		/// </summary>
		/// <param name="email">The email.</param>
		/// <param name="password">The password.</param>
		/// <returns>1 if the librarian is valid otherwise 0</returns>
		public int Authenticate(string email, string password)
		{
			var validUser = 0;
			try
			{
				var conn = DbConnection.GetConnection();
				using (conn)
				{
					conn.Open();
					var query = "select count(*) from librarian, user where librarian.librarianID = user.userID and email = @email and password = @password";
					using (var cmd = new MySqlCommand(query, conn))
					{
						cmd.Parameters.Add("@email", MySqlDbType.VarChar);
						cmd.Parameters["@email"].Value = email;

						cmd.Parameters.Add("@password", MySqlDbType.VarChar);
						cmd.Parameters["@password"].Value = password;


						validUser = Convert.ToInt32(cmd.ExecuteScalar());
					}
					conn.Close();
				}

			}
			catch (Exception ex)
			{
				throw ex;
			}

			return validUser;
		}

		
	}
}
