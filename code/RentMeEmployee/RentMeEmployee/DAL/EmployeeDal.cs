﻿using System;
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
        public static List<Employee> GetEmployees(Employee currentEmployee)
        {
            List<Employee> employees = new List<Employee>();
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



                                var employee = new Employee { FName = fName, LName = lName, Username = username, IsManager = isManager == 1};
                        

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
