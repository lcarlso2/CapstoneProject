using MySql.Data.MySqlClient;
using RentMeDesktop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentMeDesktop.DAL
{
    public class EmployeeDAL
    {
        /// <summary>
        /// Authenticates the specified username.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        public static int Authenticate(string username, string password)
        {
            var validUser = 0;
            try
            {
                var conn = DbConnection.GetConnection();
                using (conn)
                {
                    conn.Open();
                    var query = "select count(*) from Employee where Username = @username and Password = @password";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.Add("@username", MySqlDbType.VarChar);
                        cmd.Parameters["@username"].Value = username;

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
        /// Gets the current user.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <returns>The current users first and last name</returns>
        public static Employee GetCurrentUser(string username, string password)
        {
            Employee currentUser = null;
            try
            {
                var conn = DbConnection.GetConnection();
                using (conn)
                {
                    conn.Open();
                    var query = "select FName, LName, Manager from Employee where Username = @username and Password = @password";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {

                        cmd.Parameters.Add("@username", MySqlDbType.VarChar);
                        cmd.Parameters["@username"].Value = username;

                        cmd.Parameters.Add("@password", MySqlDbType.VarChar);
                        cmd.Parameters["@password"].Value = password;

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            var fNameOrdinal = reader.GetOrdinal("FName");
                            var lNameOrdinal = reader.GetOrdinal("LName");
                            var isManagerOrdinal = reader.GetOrdinal("Manager");



                            while (reader.Read())
                            {
                                var fname = reader[fNameOrdinal] == DBNull.Value
                                    ? "null"
                                    : reader.GetString(fNameOrdinal);

                                var lname = reader[lNameOrdinal] == DBNull.Value
                                    ? "null"
                                    : reader.GetString(lNameOrdinal);


                                var isManager = reader.GetInt32(isManagerOrdinal);


                                currentUser = new Employee(fname, lname);


                                if (isManager == 1)
                                {
                                    currentUser.IsManager = true;
                                }
                            }
                        }

                    }
                    conn.Close();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return currentUser;

        }

    }
}
