using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
		private readonly IRentalDal rentalDal;
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
        /// The possible status for the current rental item
        /// </summary>
        public static List<SelectListItem> Statuses = new List<SelectListItem>();

        /// <summary>
        /// Creates a new default home controller 
        /// </summary>
        [ActivatorUtilitiesConstructor]
        public HomeController()
        {
	       this.rentalDal = new RentalDal();
           this.employeeDal = new EmployeeDal();
        }

        /// <summary>
        /// Creates a new home controller with the desired dals
        /// </summary>
        /// <param name="rentalDal">the rental dal</param>
        /// <param name="employeeDal">the employee dal</param>
        public HomeController(IRentalDal rentalDal, IEmployeeDal employeeDal)
        {
	        this.rentalDal = rentalDal;
	        this.employeeDal = employeeDal;
        }

        /// <summary>
        /// The action result for filtering the rentals by status
        /// </summary>
        /// <returns>The given view based on the desired status</returns>
        public IActionResult StatusFilter(string status)
        {

	        List<RentalItem> rentals = new List<RentalItem>();
	        try
	        {

		        if (status.Equals("All"))
		        {
			        rentals = new List<RentalItem>(this.rentalDal.RetrieveAllRentedItems());
		        }
		        else
		        {
			        rentals = new List<RentalItem>(this.rentalDal.RetrieveSelectRentedItems(status));
		        }
	        }
	        catch (Exception)
	        {
		        ViewBag.Error = "Uh-oh something went wrong";
		        return View("EmployeeLanding", rentals);
            }

	        return View("EmployeeLanding", rentals);

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
                employees = this.employeeDal.GetEmployees(CurrentEmployee);
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
            return View(new Employee());
        }


        /// <summary>
        /// The action result for the index page
        /// </summary>
        /// <returns>the index page</returns>
        public IActionResult Index()
		{
			if (CurrentEmployee != null)
			{
				return RedirectToAction("EmployeeLanding");
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
        /// <returns>the login page</returns>
        [HttpPost]
        public IActionResult Login(BaseEmployee employee)
        {
            try
            {

                if (ModelState.IsValid && this.employeeDal.Authenticate(employee.Username, employee.Password) == 1)
                {

                    CurrentEmployee = this.employeeDal.GetCurrentUser(employee.Username, employee.Password);
                    IsManager = CurrentEmployee.IsManager;

                    return RedirectToAction("EmployeeLanding");
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
        /// The employee landing page
        /// </summary>
        /// <returns>the employee landing page</returns>
        public IActionResult EmployeeLanding()
        {
            List<RentalItem> items = new List<RentalItem>();

            try
            {
	            items = this.rentalDal.RetrieveAllRentedItems();
            }
            catch (Exception)
            {
                ViewBag.ErrorMessage = "Uh-oh something went wrong";
            }


            return View(items);
        }

        /// <summary>
        /// The update status action result
        /// </summary>
        /// <param name="id">the id of the order being updated</param>
        /// <returns>the update status page</returns>
		public IActionResult UpdateStatus(int? id)
        {
            RentalItem item = new RentalItem();
            try
            {
                Statuses.Clear();
                item = this.rentalDal.RetrieveAllRentedItems().First(currentItem => currentItem.RentalId == id);
                var statuses = RentalItem.GetPossibleStatuses(item.Status);
                foreach (var current in statuses)
                {
	                Statuses.Add(new SelectListItem(current, current));
                }
            }
            catch (Exception)
            {
                ViewBag.ErrorMessage = "Uh-oh something went wrong";
            }


            return View(item);
        }

        /// <summary>
        /// The http post for the confirm update action
        /// </summary>
        /// <param name="borrowedItem">the borrowed item</param>
        /// <returns>the employee landing page</returns>
        [HttpPost]
        public IActionResult ConfirmedUpdate(RentalItem borrowedItem)
        {
            try
            {
                this.rentalDal.UpdateStatus(borrowedItem.RentalId, borrowedItem.Status, CurrentEmployee.EmployeeId);
            }
            catch (Exception)
            {
                ViewBag.ErrorMessage = "Uh-oh something went wrong";
                return View("UpdateStatus");
            }

            return RedirectToAction("EmployeeLanding");
        }

   

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}

	}
}
