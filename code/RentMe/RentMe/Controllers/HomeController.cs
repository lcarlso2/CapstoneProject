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

	    private readonly IMemberDal memberDal;

	    private readonly ILibrarianDal librarianDal;

	    /// <summary>
        /// The current user that is logged into the system.
        /// If there is no-one logged in, the user is null.
        /// </summary>
        public static Member CurrentUser;

	    public static Librarian CurrentLibrarian;

        /// <summary>
        /// Creates a new home controller with the desired dals
        /// </summary>
        /// <param name="memberDal">The member dal for communication</param
        /// <param name="librarianDal">The librarian dal for communication</param>
        /// @precondition none
        /// @postcondition the controller is created with the input dals
        public HomeController(IMemberDal memberDal, ILibrarianDal librarianDal)
        {
	        this.memberDal = memberDal;
	        this.librarianDal = librarianDal;
        }

        /// <summary>
        /// Creates a new default home controller with the default customer dal and librarian dal
        /// </summary>
        /// @precondition none
        /// @postcondition the controller is created with the CustomerDal and the librarian dal
        [ActivatorUtilitiesConstructor]
        public HomeController()
        {
	        this.memberDal = new MemberDal();
            this.librarianDal = new LibrarianDal();
        }

        /// <summary>
        /// The index action result
        /// </summary>
        /// <returns>the browse page if someone is logged in, otherwise the index page if no one is logged in.</returns>
        /// @precondition none
        /// @postcondition none
        public IActionResult Index()
        {
            if (CurrentUser != null || CurrentLibrarian != null)
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
            CurrentLibrarian = null;
            return RedirectToAction("Index");
        }




        /// <summary>
        /// The http post for the action result login
        /// </summary>
        /// <param name="user">the user logging in</param>
        /// <returns>The browse page if the user is signed in otherwise stays on the index page if the login is invalid or an error occurs</returns>
        /// @precondition state of system is that of no one logged in
        /// @postcondition If the log in is successful then the state of the system changes to that of a valid member
        [HttpPost]
        public IActionResult Login(Member user)
        {
            try
            {
                if (ModelState.IsValid)
                {
	                if (this.memberDal.Authenticate(user.Email, user.Password) == 1)
	                {
		                CurrentUser = new Member {Email = user.Email, Password = user.Password};
		                return RedirectToAction("LibrariansChoice", "Borrow");
	                } 
	                if (this.librarianDal.Authenticate(user.Email, user.Password) == 1)
	                {
                        CurrentLibrarian = new Librarian { Email = user.Email, Password = user.Password };
                        return RedirectToAction("LibrariansChoice", "Borrow");
                    }
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
