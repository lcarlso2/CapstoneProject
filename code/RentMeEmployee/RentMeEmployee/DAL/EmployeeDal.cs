using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using RentMeEmployee.Models;

namespace RentMeEmployee.DAL
{
    /// <summary>
    /// The employee dal class repsonsible for communicating with the DB for employee related things 
    /// </summary>
    public class EmployeeDal
    {


        /// <summary>
        /// Adds an employee to the database
        /// </summary>
        /// <param name="employee">The employee being added</param>
        /// <param name="password">The password of the employee</param>
        public static void AddEmployee(NewEmployee employee)
        {
            try
            {
                var conn = DBConnection.GetConnection();
                using (conn)
                {
                    conn.Open();
                    var query = "insert into Employee(Username, Password, FName, LName, Manager) values (@username, @password, @fName, @lName, @manager)";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.Add("@username", MySqlDbType.VarChar);
                        cmd.Parameters["@username"].Value = employee.Username;

                        cmd.Parameters.Add("@password", MySqlDbType.VarChar);
                        cmd.Parameters["@password"].Value = employee.Password;

                        cmd.Parameters.Add("@fName", MySqlDbType.VarChar);
                        cmd.Parameters["@fName"].Value = employee.FName;

                        cmd.Parameters.Add("@lName", MySqlDbType.VarChar);
                        cmd.Parameters["@lName"].Value = employee.LName;

                        cmd.Parameters.Add("@manager", MySqlDbType.Int32);
                        cmd.Parameters["@manager"].Value = employee.IsManager;

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

        /// <summary>
        /// Removes the employee from the database
        /// </summary>
        /// <param name="employee">the employee being removed</param>
        public static void RemoveEmployee(string employeeUsername)
        {
            try
            {
                var conn = DBConnection.GetConnection();
                using (conn)
                {
                    conn.Open();
                    var query = "delete from Employee where Username = @username";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.Add("@username", MySqlDbType.VarChar);
                        cmd.Parameters["@username"].Value = employeeUsername;

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

        /// <summary>
        /// Gets the employees
        /// </summary>
        /// <returns>the employees </returns>
        public static List<NewEmployee> GetEmployees(Employee currentEmployee)
        {
            List<NewEmployee> employees = new List<NewEmployee>();
            try
            {
                var conn = DBConnection.GetConnection();
                using (conn)
                {
                    conn.Open();
                    var query = "select * from Employee";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            var usernameOrdinal = reader.GetOrdinal("Username");
                            var fNameOrdinal = reader.GetOrdinal("FName");
                            var lNameOrdinal = reader.GetOrdinal("LName");
                            var isManagerOrdinal = reader.GetOrdinal("Manager");

                            while (reader.Read())
                            {
                                var username = reader[usernameOrdinal] == DBNull.Value ? "null" : reader.GetString(usernameOrdinal);
                                var fName = reader[fNameOrdinal] == DBNull.Value ? "null" : reader.GetString(fNameOrdinal);
                                var lName = reader[lNameOrdinal] == DBNull.Value ? "null" : reader.GetString(lNameOrdinal);
                                var isManager = reader.GetInt32(isManagerOrdinal);



                                var employee = new NewEmployee { FName = fName, LName = lName, Username = username, IsManager = isManager == 1};
                        

                                if (employee.Username != currentEmployee.Username)
                                {
                                    employees.Add(employee);
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

            return employees;
        }

    }
}
