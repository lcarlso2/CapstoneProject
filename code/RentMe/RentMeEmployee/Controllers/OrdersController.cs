using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.DependencyInjection;
using SharedCode.DAL;
using SharedCode.Model;

namespace RentMeEmployee.Controllers
{
	/// <summary>
	/// The OrdersController responsible for all order pages
	/// </summary>
    public class OrdersController : Controller
    {

	    private readonly IRentalDal rentalDal;

	    /// <summary>
	    /// The possible status for the current rental item
	    /// </summary>
	    public static List<SelectListItem> Statuses = new List<SelectListItem>();

		/// <summary>
		/// Creates a new default order controller with the dals
		/// </summary>
		/// @precondition none
		/// @postcondition the controller is created
		[ActivatorUtilitiesConstructor]
		public OrdersController()
	    {
			this.rentalDal = new RentalDal();
	    }

		/// <summary>
		/// Creates a new  order controller with the given dals
		/// </summary>
		/// <param name="rentalDal">the rental dal</param>
		/// @precondition none
		/// @postcondition the controller is created
		public OrdersController(IRentalDal rentalDal)
	    {
		    this.rentalDal = rentalDal;
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

			ViewBag.Status = status;
			return View("EmployeeLanding", rentals);

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

			ViewBag.Status = "Select Status";
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
				this.rentalDal.UpdateStatus(borrowedItem.RentalId, borrowedItem.Status, HomeController.CurrentEmployee.EmployeeId);
			}
			catch (Exception)
			{
				ViewBag.ErrorMessage = "Uh-oh something went wrong";
				return View("UpdateStatus");
			}

			return RedirectToAction("EmployeeLanding");
		}

	}
}