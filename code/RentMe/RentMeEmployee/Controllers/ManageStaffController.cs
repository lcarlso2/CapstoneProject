using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using SharedCode.DAL;
using SharedCode.Model;

namespace RentMeEmployee.Controllers
{
	/// <summary>
	/// The ManageStaffController responsible for all pages related to managing staff
	/// </summary>
    public class ManageStaffController : Controller
    {
	    private readonly IEmployeeDal employeeDal;

		/// <summary>
		/// Creates a new default ManageStaff controller 
		/// </summary>
		[ActivatorUtilitiesConstructor]
	    public ManageStaffController()
	    {
		    this.employeeDal = new EmployeeDal();
	    }

	    /// <summary>
	    /// Creates a new controller with the given dals passed in
	    /// </summary>
	    /// <param name="employeeDal">The employee dal to be passed in</param>
	    /// @precondition none
	    /// @postcondition getEmployeeDal() == employeeDal
	    public ManageStaffController(IEmployeeDal employeeDal)
	    {
		    this.employeeDal = employeeDal;
	    }


		/// <summary>
		/// The action results for managing employees
		/// </summary>
		/// <returns>the view for managing employees</returns>
		public IActionResult ManageEmployees()
	    {
		    List<Employee> employees = new List<Employee>();
		    try
		    {
			    employees = this.employeeDal.GetEmployees(HomeController.CurrentEmployee);
		    }
		    catch (Exception)
		    {
			    ViewBag.ErrorMessage = "Uh-oh something went wrong";
		    }

		    return View(employees);
	    }


		/// <summary>
		/// The Delete employee action result
		/// </summary>
		/// <param name="username">the username of the employee being deleted</param>
		/// <returns>the manage employee view</returns>
		public IActionResult DeleteEmployee(string username)
	    {
		    try
		    {
			    this.employeeDal.RemoveEmployee(username);
		    }
		    catch (Exception)
		    {
			    ViewBag.ErrorMessage = "Uh-oh something went wrong";
		    }

		    return RedirectToAction("ManageEmployees");
	    }

	    /// <summary>
	    /// The get request for the add employee page
	    /// </summary>
	    /// <returns>the add employee page</returns>
	    [HttpGet]
	    public IActionResult AddEmployee()
	    {
		    return View();
	    }

	    /// <summary>
	    /// The post request for the add employee page
	    /// </summary>
	    /// <param name="employee">the employee being added</param>
	    /// <returns>the add employee page</returns>
	    [HttpPost]
	    public IActionResult AddEmployee(Employee employee)
	    {
		    if (ModelState.IsValid)
		    {
			    try
			    {
				    this.employeeDal.AddEmployee(employee, employee.Password);

				    ViewBag.SuccessMessage = "Employee added!";
			    }
			    catch (Exception)
			    {
				    ViewBag.ErrorMessage = "Uh-oh...something went wrong";
			    }

			    ModelState.Clear();
		    }
		    else
		    {
			    return View(employee);
			}

		    return View(new Employee());
	    }

    }
}