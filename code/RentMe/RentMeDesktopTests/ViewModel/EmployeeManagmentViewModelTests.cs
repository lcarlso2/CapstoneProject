using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharedCode.TestObjects;
using RentMeDesktop.ViewModel;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedCode.Model;

namespace RentMeDesktopTests.ViewModel
{
    [TestClass]
    public class EmployeeManagmentViewModelTests
    {

        [TestMethod()]
        public void AddEmployeeTestValid()
        {
            var employeeDal = new MockEmployeeDal
            {
                ThrowError = false
            };
            var viewModel = new EmployeeManagementViewModel(employeeDal);


            employeeDal.employeeToAdd.FirstName = "Fname";
            employeeDal.employeeToAdd.LastName = "Lname";
            employeeDal.employeeToAdd.Username = "Username";
            employeeDal.employeeToAdd.Password = "Password";
            employeeDal.employeeToAdd.IsManager = true;

            var count = employeeDal.employeeList.Count;

            employeeDal.AddEmployee(employeeDal.employeeToAdd, employeeDal.employeeToAdd.Password);

            var countAfter = employeeDal.employeeList.Count;

            Assert.AreEqual(count + 1, countAfter);
        }

        [TestMethod()]
        [ExpectedException(typeof(Exception))]
        public void AddEmployeeTestInvalid()
        {
            var employeeDal = new MockEmployeeDal
            {
                ThrowError = false
            };
            var viewModel = new EmployeeManagementViewModel(employeeDal);


            employeeDal.employeeToAdd.FirstName = "Fname";
            employeeDal.employeeToAdd.LastName = "Lname";
            employeeDal.employeeToAdd.Username = "Username";
            employeeDal.employeeToAdd.Password = "Password";
            employeeDal.employeeToAdd.IsManager = true;

            var count = employeeDal.employeeList.Count;

            employeeDal.AddEmployee(employeeDal.employeeToAdd, "Invalid");
        }

        [TestMethod()]
        public void RemoveEmployeeTestValid()
        {
            var employeeDal = new MockEmployeeDal
            {
                ThrowError = false
            };
            var viewModel = new EmployeeManagementViewModel(employeeDal);


            employeeDal.employeeToAdd.FirstName = "Fname";
            employeeDal.employeeToAdd.LastName = "Lname";
            employeeDal.employeeToAdd.Username = "Username";
            employeeDal.employeeToAdd.Password = "Password";
            employeeDal.employeeToAdd.IsManager = true;

            employeeDal.AddEmployee(employeeDal.employeeToAdd, employeeDal.employeeToAdd.Password);

            var count = employeeDal.employeeList.Count;

            employeeDal.RemoveEmployee(employeeDal.employeeToAdd.Username);

            var countAfter = employeeDal.employeeList.Count;

            Assert.AreEqual(count - 1, countAfter);
        }

        [TestMethod()]
        [ExpectedException(typeof(Exception))]
        public void RemoveEmployeeTestInvalid()
        {
            var employeeDal = new MockEmployeeDal
            {
                ThrowError = false
            };
            var viewModel = new EmployeeManagementViewModel(employeeDal);


            employeeDal.employeeToAdd.FirstName = "Fname";
            employeeDal.employeeToAdd.LastName = "Lname";
            employeeDal.employeeToAdd.Username = "Username";
            employeeDal.employeeToAdd.Password = "Password";
            employeeDal.employeeToAdd.IsManager = true;

            employeeDal.AddEmployee(employeeDal.employeeToAdd, employeeDal.employeeToAdd.Password);

            var count = employeeDal.employeeList.Count;

            employeeDal.RemoveEmployee("Invalid");
        }

        [TestMethod()]
        [ExpectedException(typeof(NullReferenceException))]
        public void GetEmployeeHistoryIsInValid()
        {
            var employeeDal = new MockEmployeeDal
            {
                ThrowError = false
            };
            var viewModel = new EmployeeManagementViewModel(employeeDal);
            viewModel.RetrieveEmployeeHistory();
        }
    }

}
