using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RentMeEmployee.DAL;
using RentMeEmployee.Models;

namespace RentMeEmployee.Controllers
{
    /// <summary>
    /// The home controller for the application 
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// true if the logged in person is a manager otherwise false
        /// </summary>
        public static bool isManager = false;

        /// <summary>
        /// The current employee logged in
        /// </summary>
        public static Employee CurrentEmployee;

        /// <summary>
        /// The action results for managing employees
        /// </summary>
        /// <returns>the view for managing employees</returns>
        public IActionResult ManageEmployees()
        {
            List<NewEmployee> employees = new List<NewEmployee>();
            try
            {
                employees = EmployeeDal.GetEmployees(CurrentEmployee);
            } catch (Exception ex)
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
                EmployeeDal.RemoveEmployee(username);
            } catch (Exception ex)
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
        public IActionResult AddEmployee(NewEmployee employee)
        {
            try
            {
                EmployeeDal.AddEmployee(employee);

                ViewBag.SuccessMessage = "Employee added!";
            } catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Uh-oh...something went wrong";
            }

            ModelState.Clear();
            return View(new NewEmployee());
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
            } else
            {
                return View();
            }
            
        }  

        /// <summary>
        /// The action result for the get request from the login page 
        /// </summary>
        /// <returns>the login page</returns>
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// The action result for the post request from the login page
        /// </summary>
        /// <param name="employee">the employee being logged in</param>
        /// <returns>the login page</returns>
        [HttpPost]
        public IActionResult Login(Employee employee)
        {
            try
            {
                if (ModelState.IsValid && EmployeeDal.Authenticate(employee.Username, employee.Password) == 1)
                {
                    if (EmployeeDal.IsManager(employee.Username) == 0)
                    {
                        isManager = false;
                    }
                    else
                    {
                        isManager = true;
                    }
                    CurrentEmployee = employee;
                    return RedirectToAction("EmployeeLanding");
                }
            }
            catch (Exception ex)
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
            List<BorrowedItem> items = new List<BorrowedItem>();

            try
            {
                items = RentalDal.RetrieveAllBorrowedItems();
            } catch (Exception ex)
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
            BorrowedItem item = new BorrowedItem();
            try
            {
                item = RentalDal.RetrieveAllBorrowedItems().Where(currentItem => currentItem.TransactionId == id).First();
            } catch (Exception ex)
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
        public IActionResult ConfirmedUpdate(BorrowedItem borrowedItem)
        {
            try
            {
                RentalDal.UpdateStatus(borrowedItem.TransactionId, borrowedItem.Status);
            } catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Uh-oh something went wrong";
                return View("UpdateStatus");
            }

            return RedirectToAction("EmployeeLanding");
        }

        /// <summary>
		/// The error action result
		/// </summary>
		/// <returns>The error page</returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
