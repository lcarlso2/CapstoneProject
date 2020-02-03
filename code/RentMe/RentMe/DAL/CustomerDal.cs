using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using RentMe.Models;
using SharedCode.DAL;

namespace RentMe.DAL
{
    /// <summary>
	/// The customer dal class responsible for communicating with the DB for customer related things 
	/// </summary>
	public class CustomerDal
    {

        /// <summary>
        /// Authenticates the specified customer.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        public static int Authenticate(string email, string password)
        {
            var validUser = 0;
            try
            {
                var conn = DbConnection.GetConnection();
                using (conn)
                {
                    conn.Open();
                    var query = "select count(*) from member, user where member.memberID = user.userID and email = @email and password = @password";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
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
        /// Registers the customer.
        /// </summary>
        /// <param name="customer">The customer to register.</param>
        public static void RegisterCustomer(RegisteringCustomer customer)
        {

            try
            {
                var conn = DbConnection.GetConnection();
                using (conn)
                {
                    conn.Open();

                    using (var transaction = conn.BeginTransaction())
                    {

                        var query = "insert into user(fname, lname, password) values (@fname, @lname, @password)";

                        using (var cmd = new MySqlCommand(query, conn))
                        {
                            cmd.Transaction = transaction;

                            cmd.Parameters.Add("@fname", MySqlDbType.VarChar);
                            cmd.Parameters["@fname"].Value = customer.First;

                            cmd.Parameters.Add("@lname", MySqlDbType.VarChar);
                            cmd.Parameters["@lname"].Value = customer.Last;

                            cmd.Parameters.Add("@password", MySqlDbType.VarChar);
                            cmd.Parameters["@password"].Value = customer.Password;

                            if (cmd.ExecuteNonQuery() != 1)
                            {
                                transaction.Rollback();
                            }

                            cmd.Parameters.Clear();
                            cmd.CommandText =
                                "insert into member(memberID, address, email, state, zip) values (last_insert_id(), @address, @email, @state, @zip)";


                            cmd.Parameters.Add("@address", MySqlDbType.VarChar);
                            cmd.Parameters.Add("@email", MySqlDbType.VarChar);
                            cmd.Parameters.Add("@state", MySqlDbType.VarChar);
                            cmd.Parameters.Add("@zip", MySqlDbType.VarChar);


                            cmd.Parameters["@address"].Value = customer.Address;
                            cmd.Parameters["@email"].Value = customer.Email;
                            cmd.Parameters["@state"].Value = customer.State;
                            cmd.Parameters["@zip"].Value = customer.Zip;

                            if (cmd.ExecuteNonQuery() != 1)
                            {
                                transaction.Rollback();
                            }
                            transaction.Commit();
                        }
                        conn.Close();
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
    }
}
