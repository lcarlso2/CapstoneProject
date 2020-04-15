using System;
using System.Collections.Generic;
using System.Linq;
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
        /// @precondition none
        /// @postcondition the employee is added or an error is thrown if something went wrong on the database
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
        /// @precondition none
        /// @postcondition the employee is removed or an error is thrown if something went wrong on the database 
        public void RemoveEmployee(string username)
        {
            try
            {
                var conn = DbConnection.GetConnection();
                using (conn)
                {
                    conn.Open();
                    var query = "update employee set isActive = 0 where username = @username;";
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
        /// Gets the employees
        /// </summary>
        /// <returns>the employee are returned or an error is thrown if something goes wrong on the database </returns>
        /// @precondition none
        /// @postcondition none
        public List<Employee> GetEmployees(Employee currentEmployee)
        {
            var employees = new List<Employee>();
            try
            {
                var conn = DbConnection.GetConnection();
                using (conn)
                {
                    conn.Open();
                    var query = "select * from employee, user where employeeID = userID and isActive = 1";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        using (var reader = cmd.ExecuteReader())
                        {
                            var usernameOrdinal = reader.GetOrdinal("username");
                            var idOrdinal = reader.GetOrdinal("employeeID");
                            var fNameOrdinal = reader.GetOrdinal("fname");
                            var lNameOrdinal = reader.GetOrdinal("lname");
                            var isManagerOrdinal = reader.GetOrdinal("isManager");

                            while (reader.Read())
                            {
                                var username = reader[usernameOrdinal] == DBNull.Value ? "null" : reader.GetString(usernameOrdinal);
                                var fName = reader[fNameOrdinal] == DBNull.Value ? "null" : reader.GetString(fNameOrdinal);
                                var lName = reader[lNameOrdinal] == DBNull.Value ? "null" : reader.GetString(lNameOrdinal);
                                var employeeId = reader.GetInt32(idOrdinal);
                                var isManager = reader.GetInt32(isManagerOrdinal);



                                var employee = new Employee(fName, lName);
                                employee.Username = username;
                                employee.IsManager = isManager == 1;
                                employee.EmployeeId = employeeId;

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
        /// <returns>1 if the employee is valid otherwise 0. Or throws an error if something goes wrong on the database</returns>
        public int Authenticate(string username, string password)
        {
            var validUser = 0;
            try
            {
                var conn = DbConnection.GetConnection();
                using (conn)
                {
                    conn.Open();
                    var query = "select count(*) from user, employee where userID = employeeID and username = @username and Password = @password and isActive = 1";
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
        /// <returns>The current users first and last name or an error if something goes wrong on the database</returns>
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

        /// <summary>
        /// Gets all actions that the given employee has down on the system
        /// </summary>
        /// <param name="employeeId">the given employee's id</param>
        /// <returns>the items the employee has updated</returns>
        public List<RentalItem> GetEmployeeHistory(int employeeId)
        {
            List<RentalItem> rentalItems = new List<RentalItem>();
            try
            {
                var conn = DbConnection.GetConnection();
                using (conn)
                {
                    conn.Open();
                    var query = "select e.username, s2.`condition`, r.rentalID, r.rentalDateTime, " +
                                "r.returnDateTime, r.inventoryID, category, title, status, s2.updateDateTime from rental_transaction r, " +
                                "employee e, media, inventory_item i, status_history s2, status where e.employeeId = @employeeId and " +
                                "e.employeeId = s2.employeeId and r.inventoryID = i.inventoryID and i.mediaID = media.mediaID and " +
                                "s2.rentalTransactionID = r.rentalID and s2.statusID = status.statusID";
                    using (var cmd = new MySqlCommand(query, conn))
                    {

                        cmd.Parameters.Add("@employeeId", MySqlDbType.Int32);
                        cmd.Parameters["@employeeId"].Value = employeeId;

                        using (var reader = cmd.ExecuteReader())
                        {
                            var usernameOrdinal = reader.GetOrdinal("username");
                            var rentalIdOrdinal = reader.GetOrdinal("rentalID");
                            var rentalDateTimeOrdinal = reader.GetOrdinal("rentalDateTime");
                            var returnDateTimeOrdinal = reader.GetOrdinal("returnDateTime");
                            var inventoryIdOrdinal = reader.GetOrdinal("inventoryID");
                            var categoryOrdinal = reader.GetOrdinal("category");
                            var titleOrdinal = reader.GetOrdinal("title");
                            var conditionOrdinal = reader.GetOrdinal("condition");
                            var statusOrdinal = reader.GetOrdinal("status");
                            var updateDateTimeOrdinal = reader.GetOrdinal("updateDateTime");

                            while (reader.Read())
                            {
                                var username = reader[usernameOrdinal] == DBNull.Value ? "null" : reader.GetString(usernameOrdinal);
                                var rentalID = reader.GetInt32(rentalIdOrdinal);
                                var rentalDateTime = reader.GetDateTime(rentalDateTimeOrdinal);
                                var returnDateTime = reader.GetDateTime(returnDateTimeOrdinal);
                                var inventoryID = reader.GetInt32(inventoryIdOrdinal);
                                var category = reader[categoryOrdinal] == DBNull.Value ? "null" : reader.GetString(categoryOrdinal);
                                var title = reader[titleOrdinal] == DBNull.Value ? "null" : reader.GetString(titleOrdinal);
                                var status = reader[statusOrdinal] == DBNull.Value ? "null" : reader.GetString(statusOrdinal);
                                var condition = reader[conditionOrdinal] == DBNull.Value ? "null" : reader.GetString(conditionOrdinal);
                                var updateDateTime = reader.GetDateTime(updateDateTimeOrdinal);


                                var rentalItem = new RentalItem
                                {
                                    RentalId = rentalID,
                                    RentalDate = rentalDateTime,
                                    ReturnDate = returnDateTime,
                                    InventoryId = inventoryID,
                                    Category = category,
                                    Title = title,
                                    Status = status,
                                    UpdateDateTime = updateDateTime,
                                    EmployeeUsername = username,
                                    Condition = condition
                                };

                                rentalItems.Add(rentalItem);
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

            return rentalItems.OrderByDescending(item => item.RentalDate).ThenByDescending(item => item.UpdateDateTime).ToList();
        }
    }
}
