using MySql.Data.MySqlClient;
using RentMe.Models;
using SharedCode.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentMe.DAL
{
    /// <summary>
    /// The member dal class responsible for communicating with the DB for member related things 
    /// </summary>
    public class MemberDal : IMemberDal
	{

        /// <summary>
        /// Adds an address to the db for the given member 
        /// </summary>
        /// <param name="address"> the address being added</param>
        /// <param name="member"> the member the address is being added to</param>
        /// @precondition none
        /// @postcondition the address is added to the db
        public void AddAddress(Address address, Member member)
		{
            try
            {
                var conn = DbConnection.GetConnection();
                using (conn)
                {
                    conn.Open();

                    using var transaction = conn.BeginTransaction();
                    var query = "insert into address(memberID, address, state, zip) values ((select memberID from member where email = @memberEmail), @address, @state, @zip)";

                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Transaction = transaction;

                        cmd.Parameters.AddWithValue("@memberEmail", member.Email);
                        cmd.Parameters.AddWithValue("@address", address.StreetAddress);
                        cmd.Parameters.AddWithValue("@state", address.State);
                        cmd.Parameters.AddWithValue("@zip", address.Zip);


                        if (cmd.ExecuteNonQuery() != 1)
                        {
                            transaction.Rollback();
                        }

                        transaction.Commit();
                    }
                    conn.Close();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// Gets the addresses stored for the given member 
        /// </summary>
        /// <param name="member"> the member the addresses are being gotten for</param>
        /// <returns>the addresses for the given member</returns>
        public List<Address> GetAddresses(Member member)
		{
			var addresses = new List<Address>();
			try
			{
				var conn = DbConnection.GetConnection();
				using (conn)
				{
					conn.Open();
					var query = "select * from address where memberID = (select memberID from member where email = @memberEmail)";

                    using (var cmd = new MySqlCommand(query, conn))
					{
                        cmd.Parameters.AddWithValue("@memberEmail", member.Email);

						using var reader = cmd.ExecuteReader();
						var addressIdOrdinal = reader.GetOrdinal("addressID");
						var streetAddressOrdinal = reader.GetOrdinal("address");
						var stateOrdinal = reader.GetOrdinal("state");
						var zipOrdinal = reader.GetOrdinal("zip");



						while (reader.Read())
						{
							var addressId = reader.GetInt32((addressIdOrdinal));

							var streetAddress = reader[streetAddressOrdinal] == DBNull.Value
								? "null"
								: reader.GetString(streetAddressOrdinal);

							var state = reader[stateOrdinal] == DBNull.Value
								? "null"
								: reader.GetString(stateOrdinal);

							var zip = reader[zipOrdinal] == DBNull.Value
								? "null"
								: reader.GetString(zipOrdinal);



							var address = new Address { AddressId = addressId, StreetAddress = streetAddress, State = state, Zip = zip};

							addresses.Add(address);
						}
					}
					conn.Close();
				}

			}
			catch (Exception ex)
			{
				throw ex;
			}

            return addresses;
		}

        /// <summary>
        /// Authenticates the customer's sign in on the database
        /// </summary>
        /// <param name="email">the customers email</param>
        /// <param name="password">the customer password</param>
        /// <returns>1 if the customer is valid, otherwise 0 and the customer is invalid</returns>
        /// @precondition none
        /// @postcondition the customer is signed in or an error is thrown if something goes wrong on the database
        public int Authenticate(string email, string password)
        {
	        var validUser = 0;
	        try
	        {
		        var conn = DbConnection.GetConnection();
		        using (conn)
		        {
			        conn.Open();
			        var query = "select count(*) from member, user where member.memberID = user.userID and email = @email and password = @password";
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

        /// <summary>
        /// Registers the member on the database 
        /// </summary>
        /// <param name="member">The member being registered </param>
        /// @precondition none
        /// @postcondition the member is registered or an error is thrown if something goes wrong on the database
        public void RegisterMember(RegisteringMember member)
        {

            try
            {
                var conn = DbConnection.GetConnection();
                using (conn)
                {
                    conn.Open();

                    using var transaction = conn.BeginTransaction();
                    var query = "insert into user(fname, lname, password) values (@fname, @lname, @password)";

                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Transaction = transaction;

                        cmd.Parameters.Add("@fname", MySqlDbType.VarChar);
                        cmd.Parameters["@fname"].Value = member.First;

                        cmd.Parameters.Add("@lname", MySqlDbType.VarChar);
                        cmd.Parameters["@lname"].Value = member.Last;

                        cmd.Parameters.Add("@password", MySqlDbType.VarChar);
                        cmd.Parameters["@password"].Value = member.Password;

                        if (cmd.ExecuteNonQuery() != 1)
                        {
                            transaction.Rollback();
                        }

                        cmd.Parameters.Clear();
                        cmd.CommandText =
                            "insert into member(memberID, email) values (last_insert_id(), @email)";

                        cmd.Parameters.Add("@email", MySqlDbType.VarChar);
                        cmd.Parameters["@email"].Value = member.Email;
         

                        if (cmd.ExecuteNonQuery() != 1)
                        {
                            transaction.Rollback();
                        }

                        cmd.Parameters.Clear();
                        cmd.CommandText = "insert into address(memberID, address, state, zip) values " +
                                          "((select memberID from member where email = @memberEmail), @address, @state, @zip)";

                        cmd.Parameters.AddWithValue("@memberEmail", member.Email);
                        cmd.Parameters.AddWithValue("@address", member.Address.StreetAddress);
                        cmd.Parameters.AddWithValue("@state", member.Address.State);
                        cmd.Parameters.AddWithValue("@zip", member.Address.Zip);

                        if (cmd.ExecuteNonQuery() != 1)
                        {
	                        transaction.Rollback();
                        }
                        transaction.Commit();
                    }
                    conn.Close();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        /// <summary>
		/// Updates the customers email that has a matching original email
		/// </summary>
		/// <param name="originalEmail"> The current logged in members email</param>
		/// <param name="updatedEmail"> The email to update for the customer</param>
		/// @precondition none
		/// @postcondition the email is updated
        public void UpdateEmail(string originalEmail, string updatedEmail)
        {
            try
            {
                var conn = DbConnection.GetConnection();
                using (conn)
                {
                    conn.Open();

                    using var transaction = conn.BeginTransaction();
                    var query = "update member set email = @updatedEmail where email = @originalEmail";

                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Transaction = transaction;

                        cmd.Parameters.AddWithValue("@updatedEmail", updatedEmail);
                        cmd.Parameters.AddWithValue("@originalEmail", originalEmail);


                        if (cmd.ExecuteNonQuery() != 1)
                        {
                            transaction.Rollback();
                        }

                        transaction.Commit();
                    }
                    conn.Close();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
