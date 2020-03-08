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
	/// The AccountsController responsible for all pages that have to do with customer accounts 
	/// </summary>
    public class AccountsController : Controller
    {
	    private readonly IRentalDal rentalDal;


	    private readonly IMemberDal memberDal;

		/// <summary>
		/// Creates a new accounts controller with the desired dals
		/// </summary>
		/// <param name="memberDal">The customer dal for communication</param>
		/// <param name="rentalDal">the rentalDal dal for communication</param>
		/// @precondition none
		/// @postcondition the controller is created with the input dals
		public AccountsController(IMemberDal memberDal, IRentalDal rentalDal)
	    {
		    this.memberDal = memberDal;
		    this.rentalDal = rentalDal;
	    }

		/// <summary>
		/// Creates a new default accounts controller with the default memberDal and rental dals 
		/// </summary>
		/// @precondition none
		/// @postcondition the controller is created with memberDal and RentalDals.
		[ActivatorUtilitiesConstructor]
	    public AccountsController()
	    {
		    this.memberDal = new MemberDal();
		    this.rentalDal = new RentalDal();
	    }

		/// <summary>
		/// The rental history filter action
		/// </summary>
		/// <param name="type">the type to filter the rentals by</param>
		/// <returns>The rental history page with filtered items or the browse page if an error occurs</returns>
		public IActionResult RentalFilter(string type)
	    {
		    List<RentalItem> media = new List<RentalItem>();
		    List<RentalItem> sortedMedia = new List<RentalItem>();

		    try
		    {
			    media = this.rentalDal.RetrieveAllRentalsByCustomer(HomeController.CurrentUser.Email);

			    if (type == "Date")
			    {
				    sortedMedia = media.OrderBy(item => item.RentalDate).ToList();
			    }
			    else if (type == "Status")
			    {
				    sortedMedia = media.OrderBy(item => item.Status).ToList();
			    }
			    else if (type == "Title")
			    {
				    sortedMedia = media.OrderBy(item => item.Title).ToList();
			    }
			    else
			    {
				    sortedMedia = media;
			    }
		    }
		    catch (Exception)
		    {
			    ViewBag.Error = "Uh-oh something went wrong";
			    return RedirectToAction("Browse", "Borrow");
		    }

		    return View("RentalHistory", sortedMedia);

	    }

	    /// <summary>
	    /// The rental history action
	    /// </summary>
	    /// <returns>The rental history page with the list of rented items or an error if something went wrong</returns>
	    /// @precondition none
	    /// @postcondition the rental history page is shown 
	    public IActionResult RentalHistory()
	    {
		    try
		    {
			    var rentals = this.rentalDal.RetrieveAllRentalsByCustomer(HomeController.CurrentUser.Email);

			    return View(rentals);
		    }
		    catch (Exception ex)
		    {
			    ViewBag.ErrorMessage = ex.Message;
			    ViewBag.Error = "Uh-oh.. something went wrong";
			    return View(new List<RentalItem>());
		    }

	    }

		/// <summary>
		/// The profile action
		/// </summary>
		/// <returns>The profile page with the customer information or an error if something went wrong</returns>
		/// @precondition none
		/// @postcondition the profile page is shown 
		public IActionResult Profile()
		{
			try
			{
				HomeController.CurrentUser.Addresses = this.memberDal.GetAddresses(HomeController.CurrentUser);

				return View("Profile");

			}
			catch (Exception)
			{
				ViewBag.Error = "Uh-oh.. something went wrong";
				return View("Profile");
			}

		}

		/// <summary>
		/// The register action result
		/// </summary>
		/// <returns>The register page</returns>
		/// @precondition none
		/// @postcondition the register page is shown
		[HttpGet]
	    public IActionResult Register()
	    {
		    return View("Register");
	    }

		/// <summary>
		/// The http post for the register page
		/// </summary>
		/// <param name="member">the member being registered</param>
		/// <returns> Registers the member and shows a success message or shows an error message. If
		/// invalid data is entered then messages are also displayed to notify the user</returns>
		/// @precondition none
		/// @postcondition the member is added to the DB
		[HttpPost]
	    [ValidateAntiForgeryToken]
	    public IActionResult Register(RegisteringMember member)
	    {
		    if (ModelState.IsValid)
		    {
			    try
			    {
				    this.memberDal.RegisterMember(member);
			    }
			    catch (Exception ex)
			    {

				    ViewBag.ErrorMessage = ex.Message;
				    return View(member);
			    }

			    ModelState.Clear();
			    ViewBag.SuccessMessage = "You're Registered!";

			    return View("Register", new RegisteringMember());
		    }
		    else
		    {
			    return View(member);
		    }

	    }

		/// <summary>
		/// Adds an address and returns the profile page
		/// </summary>
		/// <param name="address"> the address being added</param>
		/// <returns>the profile page</returns>
		/// @precondition none
		/// @postcondition the address is added or an error is displayed if something went wrong 
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult AddAddress(Address address)
		{
			if (ModelState.IsValid)
			{
				try
				{
					this.memberDal.AddAddress(address, HomeController.CurrentUser);

					HomeController.CurrentUser.Addresses = this.memberDal.GetAddresses(HomeController.CurrentUser);

					return View("Profile");
				}
				catch (Exception)
				{
					ViewBag.Error = "Uh-oh something went wrong";
					return View("Profile");
				}
			}
			ViewBag.Error = "Invalid Address";
			return View("Profile");

		}

		/// <summary>
		/// Gets the partial view for adding an address
		/// </summary>
		/// <returns>The partial view for adding an address</returns>
		/// @precondition none
		/// @postcondition page being showed is the partial view add address
		public IActionResult AddAddress()
		{
			return PartialView("AddAddress");
		}

		/// <summary>
		/// Updates the members email that has a matching original email
		/// </summary>
		/// <param name="member"> The member object submitted by the form</param>
		/// @precondition none
		/// @postcondition the email is updated
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult UpdateEmail(Member member)
		{
			if (!string.IsNullOrEmpty(member.Email))
			{
				this.memberDal.UpdateEmail(HomeController.CurrentUser.Email, member.Email);
				HomeController.CurrentUser.Email = member.Email;
				
			}
			return View("Profile");

		}


        public IActionResult RemoveAddress(string address)
        {

            if (!string.IsNullOrEmpty(address))
            {
                this.memberDal.RemoveAddress(address, HomeController.CurrentUser.Email);
                var addressParts = address.Split(' ');
                var streetAddress = "";
                for (var i = 0; i < (addressParts.Length - 2); i++)
                {
                    streetAddress += addressParts[i] + " ";
                }
                var state = addressParts[addressParts.Length - 2];
                var zip = addressParts[addressParts.Length - 1];
                streetAddress = streetAddress.Trim();

				foreach (var add in HomeController.CurrentUser.Addresses)
                {
                    if (add.StreetAddress == streetAddress && add.State == state && add.Zip == zip)
                    {
                        HomeController.CurrentUser.Addresses.Remove(add);
                        break;
                    }
                }

			}
            return View("Profile");
        }
	}
}