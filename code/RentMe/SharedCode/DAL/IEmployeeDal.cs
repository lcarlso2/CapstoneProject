using System;
using System.Collections.Generic;
using System.Text;
using SharedCode.Model;

namespace SharedCode.DAL
{
	/// <summary>
	/// The interface for all employee database actions
	/// </summary>
	public interface IEmployeeDal
	{

		/// <summary>
		/// Adds an employee to the database
		/// </summary>
		/// <param name="employee">The employee being added</param>
		/// <param name="password">The password of the employee</param>
		/// @precondition none
		/// @postcondition the employee is added
		void AddEmployee(Employee employee, string password);

		/// <summary>
		/// Removes the employee with the given username from the database
		/// </summary>
		/// <param name="username">the username of the employee being removed</param>
		/// @precondition none
		/// @postcondition the employee is removed
		void RemoveEmployee(string username);


		/// <summary>
		/// Gets the employees
		/// </summary>
		/// <returns>the employees </returns>
		List<Employee> GetEmployees(Employee currentEmployee);

		/// <summary>
		/// Authenticates the specified username.
		/// </summary>
		/// <param name="username">The username.</param>
		/// <param name="password">The password.</param>
		/// <returns>1 if the employee is valid otherwise 0</returns>
		int Authenticate(string username, string password);

		/// <summary>
		/// Gets the current user.
		/// </summary>
		/// <param name="username">The username.</param>
		/// <param name="password">The password.</param>
		/// <returns>The current user</returns>
		Employee GetCurrentUser(string username, string password);
	}
}
