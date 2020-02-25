using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.DependencyInjection;
using RentMeEmployee.Models;
using SharedCode.DAL;
using SharedCode.Model;

namespace RentMeEmployee.Controllers
{
	/// <summary>
	/// The home controller for the application 
	/// </summary>
	public class HomeController : Controller
	{
		private readonly IEmployeeDal employeeDal;
   

		/// <summary>
        /// The current employee logged in
        /// </summary>
        public static Employee CurrentEmployee = null;

        /// <summary>
        /// True if the current user is a manager
        /// </summary>
        public static bool IsManager;


        /// <summary>
        /// Creates a new default home controller 
        /// </summary>
        [ActivatorUtilitiesConstructor]
        public HomeController()
        {
	        this.employeeDal = new EmployeeDal();
        }

        /// <summary>
        /// Creates a new controller with the given dals passed in
        /// </summary>
        /// <param name="employeeDal">The employee dal to be passed in</param>
        /// @precondition none
        /// @postcondition getEmployeeDal() == employeeDal
        public HomeController(IEmployeeDal employeeDal)
        {
	        this.employeeDal = employeeDal;
        }

        /// <summary>
        /// The action result for the index page
        /// </summary>
        /// <returns>the index page or the employee landing page if the currentemployee is not null</returns>
        public IActionResult Index()
		{
			if (CurrentEmployee != null)
			{
				return RedirectToAction("EmployeeLanding", "Orders");
			}
			else
			{
				return View();
			}
        }

        /// <summary>
        /// The sign out action result
        /// </summary>
        /// <returns>The index page</returns>
        /// @precondition none
        /// @postcondition the current employee is set to null
        public IActionResult SignOut()
        {
            CurrentEmployee = null;

            return RedirectToAction("Index");
        }

        /// <summary>
        /// The action result for the get request from the login page 
        /// </summary>
        /// <returns>the login page</returns>
        [HttpGet]
        public IActionResult Login()
        {
            return View("Index");
        }

        /// <summary>
        /// The action result for the post request from the login page
        /// </summary>
        /// <param name="employee">the employee being logged in</param>
        /// <returns>the login page. If the credentials provided are valid then the state of the
        /// system changes to a valid user being logged in</returns>
        [HttpPost]
        public IActionResult Login(BaseEmployee employee)
        {
            try
            {

                if (ModelState.IsValid && this.employeeDal.Authenticate(employee.Username, employee.Password) == 1)
                {

                    CurrentEmployee = this.employeeDal.GetCurrentUser(employee.Username, employee.Password);
                    IsManager = CurrentEmployee.IsManager;

                    return RedirectToAction("EmployeeLanding", "Orders");
                }
            }
            catch (Exception)
            {
                ViewBag.Error = "Whoops, try again. Something went wrong.";
                return View("Index");
            }

            ViewBag.Error = "Invalid login";
            return View("Index");
        }

        
        /// <summary>
        /// The error page
        /// </summary>
        /// <returns>The error page </returns>
		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}

	}
}
