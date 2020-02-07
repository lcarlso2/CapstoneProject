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
	public class EmployeeDal : IEmployeeDal
	{

 
        /// <summary>
        /// Adds an employee to the database
        /// </summary>
        /// <param name="employee">The employee being added</param>
        /// <param name="password">The password of the employee</param>
        [Obsolete("Use the non static add employee now")]
        public static void OldAddEmployee(Employee employee, string password)
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
		                    cmd.Parameters["@fname"].Value = employee.FirstName;

		                    cmd.Parameters.Add("@lname", MySqlDbType.VarChar);
		                    cmd.Parameters["@lname"].Value = employee.LastName;

		                    cmd.Parameters.Add("@password", MySqlDbType.VarChar);
		                    cmd.Parameters["@password"].Value = password;

		                    if (cmd.ExecuteNonQuery() != 1)
		                    {
			                    transaction.Rollback();
		                    }

                            cmd.Parameters.Clear();
                            cmd.CommandText =
	                            "insert into employee(employeeID, isManager, username) values (last_insert_id(), @isManager, @username)";


                            cmd.Parameters.Add("@isManager", MySqlDbType.Int32);
                            cmd.Parameters.Add("@username", MySqlDbType.VarChar);


                            cmd.Parameters["@isManager"].Value = employee.IsManager;
                            cmd.Parameters["@username"].Value = employee.Username;

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

        public void AddEmployee(Employee employee, string password)
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
                            cmd.Parameters["@fname"].Value = employee.FirstName;

                            cmd.Parameters.Add("@lname", MySqlDbType.VarChar);
                            cmd.Parameters["@lname"].Value = employee.LastName;

                            cmd.Parameters.Add("@password", MySqlDbType.VarChar);
                            cmd.Parameters["@password"].Value = password;

                            if (cmd.ExecuteNonQuery() != 1)
                            {
                                transaction.Rollback();
                            }

                            cmd.Parameters.Clear();
                            cmd.CommandText =
                                "insert into employee(employeeID, isManager, username) values (last_insert_id(), @isManager, @username)";


                            cmd.Parameters.Add("@isManager", MySqlDbType.Int32);
                            cmd.Parameters.Add("@username", MySqlDbType.VarChar);


                            cmd.Parameters["@isManager"].Value = employee.IsManager;
                            cmd.Parameters["@username"].Value = employee.Username;

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

        /// <summary>
        /// Removes the employee with the given username from the database
        /// </summary>
        /// <param name="username">the username of the employee being removed</param>
        [Obsolete("Use the non static remove employee now")]
        public static void OldRemoveEmployee(string username)
        {
            try
            {
                var conn = DbConnection.GetConnection();
                using (conn)
                {
                    conn.Open();
                    var query = "delete from user where userID = (select employeeID from employee where username = @username)";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.Add("@username", MySqlDbType.VarChar);
                        cmd.Parameters["@username"].Value = username;

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


        public void RemoveEmployee(string username)
        {
	        try
	        {
		        var conn = DbConnection.GetConnection();
		        using (conn)
		        {
			        conn.Open();
			        var query = "delete from user where userID = (select employeeID from employee where username = @username)";
			        using (var cmd = new MySqlCommand(query, conn))
			        {
				        cmd.Parameters.Add("@username", MySqlDbType.VarChar);
				        cmd.Parameters["@username"].Value = username;

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
        [Obsolete("Use the non static search employees now")]
        public static List<Employee> OldSearchEmployees(Employee currentEmployee, string searchTerm)
        {
            List<Employee> employees = new List<Employee>();
            try
            {
                var conn = DbConnection.GetConnection();
                using (conn)
                {
                    conn.Open();
                    var query = "select * from Employee where Username like concat('%', @searchTerm, '%') or concat(FName, LName) like concat('%', @searchTerm, '%')";
                    using (var cmd = new MySqlCommand(query, conn))
                    {

                        cmd.Parameters.Add("@searchTerm", MySqlDbType.VarChar);
                        cmd.Parameters["@searchTerm"].Value = searchTerm;

                        using (var reader = cmd.ExecuteReader())
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

        public List<Employee> SearchEmployees(Employee currentEmployee, string searchTerm)
        {
            List<Employee> employees = new List<Employee>();
            try
            {
                var conn = DbConnection.GetConnection();
                using (conn)
                {
                    conn.Open();
                    var query = "select * from Employee where Username like concat('%', @searchTerm, '%') or concat(FName, LName) like concat('%', @searchTerm, '%')";
                    using (var cmd = new MySqlCommand(query, conn))
                    {

                        cmd.Parameters.Add("@searchTerm", MySqlDbType.VarChar);
                        cmd.Parameters["@searchTerm"].Value = searchTerm;

                        using (var reader = cmd.ExecuteReader())
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
        [Obsolete("Use the non static get employees now")]
        public static List<Employee> OldGetEmployees(Employee currentEmployee)
        {
            var employees = new List<Employee>();
            try
            {
                var conn = DbConnection.GetConnection();
                using (conn)
                {
                    conn.Open();
                    var query = "select * from employee, user where employeeID = userID";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        using (var reader = cmd.ExecuteReader())
                        {
                            var usernameOrdinal = reader.GetOrdinal("username");
                            var fNameOrdinal = reader.GetOrdinal("fname");
                            var lNameOrdinal = reader.GetOrdinal("lname");
                            var isManagerOrdinal = reader.GetOrdinal("isManager");

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

        public List<Employee> GetEmployees(Employee currentEmployee)
        {
            var employees = new List<Employee>();
            try
            {
                var conn = DbConnection.GetConnection();
                using (conn)
                {
                    conn.Open();
                    var query = "select * from employee, user where employeeID = userID";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        using (var reader = cmd.ExecuteReader())
                        {
                            var usernameOrdinal = reader.GetOrdinal("username");
                            var fNameOrdinal = reader.GetOrdinal("fname");
                            var lNameOrdinal = reader.GetOrdinal("lname");
                            var isManagerOrdinal = reader.GetOrdinal("isManager");

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
        [Obsolete("Use the non static authenticate now")]
        public static int OldAuthenticate(string username, string password)
        {
            var validUser = 0;
            try
            {
                var conn = DbConnection.GetConnection();
                using (conn)
                {
                    conn.Open();
                    var query = "select count(*) from user, employee where userID = employeeID and username = @username and Password = @password";
                    using (var cmd = new MySqlCommand(query, conn))
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

        public int Authenticate(string username, string password)
        {
	        var validUser = 0;
	        try
	        {
		        var conn = DbConnection.GetConnection();
		        using (conn)
		        {
			        conn.Open();
			        var query = "select count(*) from user, employee where userID = employeeID and username = @username and Password = @password";
			        using (var cmd = new MySqlCommand(query, conn))
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
        [Obsolete("Use the non static get current user now")]
        public static Employee OldGetCurrentUser(string username, string password)
        {
            Employee currentUser = null;
            try
            {
                var conn = DbConnection.GetConnection();
                using (conn)
                {
                    conn.Open();
                    var query = "select * from user, employee where userID = employeeID and username = @username and password = @password";
                    using (var cmd = new MySqlCommand(query, conn))
                    {

                        cmd.Parameters.Add("@username", MySqlDbType.VarChar);
                        cmd.Parameters["@username"].Value = username;

                        cmd.Parameters.Add("@password", MySqlDbType.VarChar);
                        cmd.Parameters["@password"].Value = password;

                        using (var reader = cmd.ExecuteReader())
                        {
                            var fNameOrdinal = reader.GetOrdinal("fname");
                            var lNameOrdinal = reader.GetOrdinal("lname");
                            var isManagerOrdinal = reader.GetOrdinal("isManager");
                            var idOrdinal = reader.GetOrdinal("employeeID");



                            while (reader.Read())
                            {
                                var fname = reader[fNameOrdinal] == DBNull.Value
                                    ? "null"
                                    : reader.GetString(fNameOrdinal);

                                var lname = reader[lNameOrdinal] == DBNull.Value
                                    ? "null"
                                    : reader.GetString(lNameOrdinal);


                                var isManager = reader.GetInt32(isManagerOrdinal);

                                var id = reader.GetInt32(idOrdinal);


                                currentUser = new Employee(fname, lname)
                                {
                                    Username = username,
                                    EmployeeId = id
                                };


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


        public Employee GetCurrentUser(string username, string password)
        {
            Employee currentUser = null;
            try
            {
                var conn = DbConnection.GetConnection();
                using (conn)
                {
                    conn.Open();
                    var query = "select * from user, employee where userID = employeeID and username = @username and password = @password";
                    using (var cmd = new MySqlCommand(query, conn))
                    {

                        cmd.Parameters.Add("@username", MySqlDbType.VarChar);
                        cmd.Parameters["@username"].Value = username;

                        cmd.Parameters.Add("@password", MySqlDbType.VarChar);
                        cmd.Parameters["@password"].Value = password;

                        using (var reader = cmd.ExecuteReader())
                        {
                            var fNameOrdinal = reader.GetOrdinal("fname");
                            var lNameOrdinal = reader.GetOrdinal("lname");
                            var isManagerOrdinal = reader.GetOrdinal("isManager");
                            var idOrdinal = reader.GetOrdinal("employeeID");



                            while (reader.Read())
                            {
                                var fname = reader[fNameOrdinal] == DBNull.Value
                                    ? "null"
                                    : reader.GetString(fNameOrdinal);

                                var lname = reader[lNameOrdinal] == DBNull.Value
                                    ? "null"
                                    : reader.GetString(lNameOrdinal);


                                var isManager = reader.GetInt32(isManagerOrdinal);

                                var id = reader.GetInt32(idOrdinal);


                                currentUser = new Employee(fname, lname)
                                {
                                    Username = username,
                                    EmployeeId = id
                                };


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
