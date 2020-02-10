using System;
using System.Collections.Generic;
using System.Text;
using SharedCode.DAL;
using SharedCode.Model;

namespace SharedCode.TestObjects
{
	/// <summary>
	/// The mock employee dal for testing purposes
	/// </summary>
	public class MockEmployeeDal : IEmployeeDal
	{

		/// <summary>
		/// The value to return for the authenticate method 
		/// </summary>
		public int AuthenticateValueToReturn { get; set; }

		public void AddEmployee(Employee employee, string password)
		{
			if (this.ThrowError)
			{
				throw new Exception();
			}
		}


		public void RemoveEmployee(string username)
		{
			if (this.ThrowError)
			{
				throw new Exception();
			}
		}


		public List<Employee> SearchEmployees(Employee currentEmployee, string searchTerm)
		{
			if (this.ThrowError)
			{
				throw new Exception();
			}

			return new List<Employee>();
		}

		public List<Employee> GetEmployees(Employee currentEmployee)
		{
			if (this.ThrowError)
			{
				throw new Exception();
			}

			return new List<Employee>();
		}

		/// <summary>
		/// Set to true if an error is needed for testing purposes
		/// </summary>
		public bool ThrowError { get; set; }

		public int Authenticate(string username, string password)
		{
			if (this.ThrowError)
			{
				throw new Exception();
			}

			return this.AuthenticateValueToReturn;
		}

		public Employee GetCurrentUser(string username, string password)
		{
			var employee = new Employee();

			if (this.ThrowError)
			{
				throw new Exception();
			}
			return employee;
		}
	}

}
