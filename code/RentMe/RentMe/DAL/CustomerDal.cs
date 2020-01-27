using MySql.Data.MySqlClient;
using RentMe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentMe.DAL
{
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
                var conn = DBConnection.GetConnection();
                using (conn)
                {
                    conn.Open();
                    var query = "select count(*) from customer where email = @email and password = @password";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.Add("@Email", MySqlDbType.VarChar);
                        cmd.Parameters["@Email"].Value = email;

                        cmd.Parameters.Add("@Password", MySqlDbType.VarChar);
                        cmd.Parameters["@Password"].Value = password;


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
                var conn = DBConnection.GetConnection();
                using (conn)
                {
                    conn.Open();
                    var query = "insert into Customer(Email, Password, FName, LName, Address, State, Zip) values(@Email, @Password, @FName, @LName, @Address, @State, @Zip)";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {


                        cmd.Parameters.Add("@Email", MySqlDbType.VarChar);
                        cmd.Parameters["@Email"].Value = customer.Email;

                        cmd.Parameters.Add("@Password", MySqlDbType.VarChar);
                        cmd.Parameters["@Password"].Value = customer.Password;

                        cmd.Parameters.Add("@FName", MySqlDbType.VarChar);
                        cmd.Parameters["@FName"].Value = customer.First;

                        cmd.Parameters.Add("@LName", MySqlDbType.VarChar);
                        cmd.Parameters["@LName"].Value = customer.Last;

                        cmd.Parameters.Add("@Address", MySqlDbType.VarChar);
                        cmd.Parameters["@Address"].Value = customer.Address;

                        cmd.Parameters.Add("@State", MySqlDbType.VarChar);
                        cmd.Parameters["@State"].Value = customer.State;

                        cmd.Parameters.Add("@Zip", MySqlDbType.VarChar);
                        cmd.Parameters["@Zip"].Value = customer.Zip;



                        cmd.ExecuteScalar();
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

