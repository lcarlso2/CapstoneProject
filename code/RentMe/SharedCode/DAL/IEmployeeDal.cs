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
		void AddEmployee(Employee employee, string password);

		/// <summary>
		/// Removes the employee with the given username from the database
		/// </summary>
		/// <param name="username">the username of the employee being removed</param>
		void RemoveEmployee(string username);

		/// <summary>
		/// Searches the employees and returns the one matching the search term
		/// </summary>
		/// <returns>the employees matching the term</returns>
		List<Employee> SearchEmployees(Employee currentEmployee, string searchTerm);

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
		/// <returns></returns>
		int Authenticate(string username, string password);

		/// <summary>
		/// Gets the current user.
		/// </summary>
		/// <param name="username">The username.</param>
		/// <param name="password">The password.</param>
		/// <returns>The current users first and last name</returns>
		Employee GetCurrentUser(string username, string password);
	}
}
