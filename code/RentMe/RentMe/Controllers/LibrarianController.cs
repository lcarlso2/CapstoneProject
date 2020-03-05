using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using RentMe.DAL;
using RentMe.Models;
using SharedCode.DAL;
using SharedCode.Model;

namespace RentMe.Controllers
{
	/// <summary>
	/// The controller responsible for the librarian pages 
	/// </summary>
    public class LibrarianController : Controller
    {
	    private readonly ILibrarianDal librarianDal;

	    private readonly IRentalDal rentalDal;


		/// <summary>
		/// The current selected members email
		/// </summary>
	    public static string CurrentMemberEmail;

		/// <summary>
		/// The default constructor for the librarian page which instantiates the default
		/// librarian dal and rental dal
		/// </summary>
		/// @precondition none
		/// @postcondition the librarian controller is created with the default dals
		[ActivatorUtilitiesConstructor]
		public LibrarianController()
	    {
			this.librarianDal = new LibrarianDal();
			this.rentalDal = new RentalDal();
	    }

		/// <summary>
		/// The constructor for the librarian page where you pass in the desired librarian dal and rental dal
		/// </summary>
		/// <param name="librarianDal"> the librarian dal being passed in</param>
		/// <param name="rentalDal"> the rentalDal dal being passed in</param>
		/// @precondition none
		/// @postcondition the librarian controller is created with the desired dals
		public LibrarianController(ILibrarianDal librarianDal, IRentalDal rentalDal)
		{
			this.librarianDal = librarianDal;
			this.rentalDal = rentalDal;
		}

		/// <summary>
		/// The action result for viewing all members
		/// </summary>
		/// <returns>the all members page with either the members fetched from the db or an error message with an empty list if
		/// something went wrong when communicating with the db</returns>
	    public IActionResult ViewAllMembers()
	    {
		    try
		    {
			    var members = this.librarianDal.GetAllMembers();
			    return View("AllMembers", members);
		    }
		    catch (Exception)
		    {
			    ViewBag.Error = "Uh-oh.. something went wrong";
				return View("AllMembers", new List<RegisteringMember>());
			}
            
	    }

		/// <summary>
		/// The member history page of the member with the selected id
		/// </summary>
		/// <param name="email">the email of the member</param>
		/// <returns>the member history page with the history of the member or an error message with an empty list if
		/// something went wrong when communicating with he db</returns>
		public IActionResult ViewMemberHistory(string email)
		{
			CurrentMemberEmail = email;
		    try
		    {
			    var rentals = this.rentalDal.RetrieveAllRentalsByCustomer(email);
			    return View("MemberHistory", rentals);
			}
		    catch (Exception)
		    {
			    ViewBag.Error = "Uh-oh.. something went wrong";
			    return View("MemberHistory", new List<RentalItem>());
			}
	    }

	}
}