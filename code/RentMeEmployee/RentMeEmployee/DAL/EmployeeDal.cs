using System;
using MySql.Data.MySqlClient;

namespace RentMeEmployee.DAL
{
    /// <summary>
    /// The employee dal class repsonsible for communicating with the DB for employee related things 
    /// </summary>
    public class EmployeeDal
    {

        /// <summary>
        /// Authenticates the specified employee.
        /// </summary>
        /// <param name="username">The user name.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        public static int Authenticate(string username, string password)
        {
            var validUser = 0;
            try
            {
                var conn = DBConnection.GetConnection();
                using (conn)
                {
                    conn.Open();
                    var query = "select count(*) from Employee where Username = @Username and Password = @Password";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.Add("@Username", MySqlDbType.VarChar);
                        cmd.Parameters["@Username"].Value = username;

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
        /// Checks to see if the employee is a manager.
        /// </summary>
        /// <param name="username">The user name.</param>
        /// <returns></returns>
        public static int IsManager(string username)
        {
            var manager = 0;
            try
            {
                var conn = DBConnection.GetConnection();
                using (conn)
                {
                    conn.Open();
                    var query = "select Manager from Employee where Username = @Username";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.Add("@Username", MySqlDbType.VarChar);
                        cmd.Parameters["@Username"].Value = username;


                        manager = Convert.ToInt32(cmd.ExecuteScalar());
                    }
                    conn.Close();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return manager;
        }
    }
}
