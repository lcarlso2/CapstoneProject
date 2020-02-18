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
    public class AccountsController : Controller
    {
	    private readonly IRentalDal rentalDal;


	    private readonly ICustomerDal customerDal;

		/// <summary>
		/// Creates a new accounts controller with the desired dals
		/// </summary>
		/// <param name="customerDal">The customer dal for communication</param>
		/// <param name="rentalDal">the rentalDal dal for communication</param>
		/// @precondition none
		/// @postcondition the controller is created with the input dals
		public AccountsController(ICustomerDal customerDal, IRentalDal rentalDal)
	    {
		    this.customerDal = customerDal;
		    this.rentalDal = rentalDal;
	    }

	    /// <summary>
	    /// Creates a new default accounts controller
	    /// </summary>
	    /// @precondition none
	    /// @postcondition the controller is created with CustomerDals and RentalDals.
	    [ActivatorUtilitiesConstructor]
	    public AccountsController()
	    {
		    this.customerDal = new CustomerDal();
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
	    /// <returns>The rental history page</returns>
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
		/// The register action result
		/// </summary>
		/// <returns>The register page</returns>
		[HttpGet]
	    public IActionResult Register()
	    {
		    return View("Register");
	    }

	    /// <summary>
	    /// The http post for the register page
	    /// </summary>
	    /// <param name="customer">the customer being registered</param>
	    /// <returns> Registers the customer</returns>
	    [HttpPost]
	    [ValidateAntiForgeryToken]
	    public IActionResult Register(RegisteringCustomer customer)
	    {
		    if (ModelState.IsValid)
		    {
			    try
			    {
				    this.customerDal.RegisterCustomer(customer);
			    }
			    catch (Exception ex)
			    {

				    ViewBag.ErrorMessage = ex.Message;
				    return View(customer);
			    }

			    ModelState.Clear();
			    ViewBag.SuccessMessage = "You're Registered!";

			    return View("Register", new RegisteringCustomer());
		    }
		    else
		    {
			    return View(customer);
		    }

	    }
    }
}