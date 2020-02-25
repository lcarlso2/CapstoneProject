using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using RentMe.DAL;
using RentMe.Models;
using SharedCode.DAL;

namespace RentMe.Controllers
{
    /// <summary>
    /// The home controller responsible for the page communication of the Rent Me Application (Member Side).
    /// </summary>
    public class HomeController : Controller
    {

	    private readonly ICustomerDal customerDal;

	    /// <summary>
        /// The current user that is logged into the system.
        /// If there is no-one logged in, the user is null.
        /// </summary>
        public static Customer CurrentUser;

        /// <summary>
        /// Creates a new home controller with the desired dals
        /// </summary>
        /// <param name="customerDal">The customer dal for communication</param>
        /// @precondition none
        /// @postcondition the controller is created with the input dals
        public HomeController(ICustomerDal customerDal)
        {
	        this.customerDal = customerDal;
        }

        /// <summary>
        /// Creates a new default home controller
        /// </summary>
        /// @precondition none
        /// @postcondition the controller is created with the CustomerDal
        [ActivatorUtilitiesConstructor]
        public HomeController()
        {
	        this.customerDal = new CustomerDal();
        }

        /// <summary>
        /// The index action result
        /// </summary>
        /// <returns>the browse page if someone is logged in, otherwise the index page if no one is logged in.</returns>
        /// @precondition none
        /// @postcondition none
        public IActionResult Index()
        {
            if (CurrentUser != null)
            {
                return RedirectToAction("Browse", "Borrow");
            }
            return View("Index");
        }


        /// <summary>
        /// The signout action result
        /// </summary>
        /// <returns>The index page and the state of the system changes to no one being logged in</returns>
        /// @precondition none
        /// @postcondition the current user is set to null
        public IActionResult Signout()
        {
            CurrentUser = null;

            return RedirectToAction("Index");
        }




        /// <summary>
        /// The http post for the action result login
        /// </summary>
        /// <param name="customer">the customer logging in</param>
        /// <returns>The browse page if the customer is signed in otherwise stays on the index page if the login is invalid or an error occurs</returns>
        /// @precondition state of system is that of no one logged in
        /// @postcondition If the log in is successful then the state of the system changes to that of a valid member
        [HttpPost]
        public IActionResult Login(Customer customer)
        {
            try
            {
                if (ModelState.IsValid && this.customerDal.Authenticate(customer.Email, customer.Password) == 1)
                {
                    CurrentUser = new Customer { Email = customer.Email, Password = customer.Password };
                    return RedirectToAction("Browse", "Borrow");
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                ViewBag.Error = "Whoops, try again. Something went wrong.";
                return View("Index");
            }

            ViewBag.Error = "Invalid login";
            return View("Index");
        }


        /// <summary>
        /// The error page
        /// </summary>
        /// <returns>the error page</returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
