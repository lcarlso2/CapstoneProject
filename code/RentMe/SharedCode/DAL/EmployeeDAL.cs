using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;
using SharedCode.Model;

namespace SharedCode.DAL
{
	/// <summary>
	/// The employee DAL responsible for communicating with the database for employee purposes
	/// </summary>
	public class EmployeeDal
	{

        /// <summary>
        /// Adds an employee to the database
        /// </summary>
        /// <param name="employee">The employee being added</param>
        /// <param name="password">The password of the employee</param>
        public static void AddEmployee(Employee employee, string password)
        {
            try
            {
                var conn = DbConnection.GetConnection();
                using (conn)
                {
                    conn.Open();
                    var query = "insert into Employee(Username, Password, FName, LName, Manager) values (@username, @password, @fName, @lName, @manager)";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.Add("@username", MySqlDbType.VarChar);
                        cmd.Parameters["@username"].Value = employee.Username;

                        cmd.Parameters.Add("@password", MySqlDbType.VarChar);
                        cmd.Parameters["@password"].Value = password;

                        cmd.Parameters.Add("@fName", MySqlDbType.VarChar);
                        cmd.Parameters["@fName"].Value = employee.FirstName;

                        cmd.Parameters.Add("@lName", MySqlDbType.VarChar);
                        cmd.Parameters["@lName"].Value = employee.LastName;

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
        public static void RemoveEmployee(Employee employee)
        {
            try
            {
                var conn = DbConnection.GetConnection();
                using (conn)
                {
                    conn.Open();
                    var query = "delete from Employee where Username = @username";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.Add("@username", MySqlDbType.VarChar);
                        cmd.Parameters["@username"].Value = employee.Username;

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
        /// Searches the employees and returns the one matching the search term
        /// </summary>
        /// <returns>the employees matching the term</returns>
        public static List<Employee> SearchEmployees(Employee currentEmployee, string searchTerm)
        {
            List<Employee> employees = new List<Employee>();
            try
            {
                var conn = DbConnection.GetConnection();
                using (conn)
                {
                    conn.Open();
                    var query = "select * from Employee where Username like concat('%', @searchTerm, '%') or concat(FName, LName) like concat('%', @searchTerm, '%')";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {

                        cmd.Parameters.Add("@searchTerm", MySqlDbType.VarChar);
                        cmd.Parameters["@searchTerm"].Value = searchTerm;

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



                                var employee = new Employee(fName, lName);
                                employee.Username = username;
                                employee.IsManager = isManager == 1;

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


        /// <summary>
        /// Gets the employees
        /// </summary>
        /// <returns>the employees </returns>
        public static List<Employee> GetEmployees(Employee currentEmployee)
        {
            List<Employee> employees = new List<Employee>();
            try
            {
                var conn = DbConnection.GetConnection();
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



                                var employee = new Employee(fName, lName);
                                employee.Username = username;
                                employee.IsManager = isManager == 1;

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
                    var query = "select e.FName, e.LName, e.Manager from Employee e where e.Username = @username and e.Password = @password";
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
