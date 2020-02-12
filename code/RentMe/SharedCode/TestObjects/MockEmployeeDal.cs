using System;
using System.Collections.Generic;
using System.Linq;
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

        public List<Employee> employeeList = new List<Employee>();
        public Employee employeeToAdd = new Employee();

        public bool ThrowError { get; set; }
        public int AuthenticateValueToReturn { get; set; }

        public void AddEmployee(Employee employee, string password)
        {
            if (employee.Password != password)
            {
                throw new Exception();
            }
            else
            {
                this.employeeList.Add(employee);
            }

        }


        public void RemoveEmployee(string username)
        {
            if (this.employeeList.Any(emp => emp.Username == username))
            {
                this.employeeList.RemoveAll(emp => emp.Username == username);

            }
            else
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
