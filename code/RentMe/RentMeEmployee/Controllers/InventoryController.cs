using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.DependencyInjection;
using SharedCode.DAL;
using SharedCode.Model;

namespace RentMeEmployee.Controllers
{
	/// <summary>
	/// The inventoryController responsible for all pages that manage inventory 
	/// </summary>
    public class InventoryController : Controller
    {

	    private readonly IInventoryDal inventoryDal;

	    /// <summary>
		/// Creates a new default inventory controller 
		/// </summary>
		[ActivatorUtilitiesConstructor]
	    public InventoryController()
	    {
		    this.inventoryDal = new InventoryDal();
	    }

	    /// <summary>
	    /// Creates a new controller with the given dals passed in
	    /// </summary>
	    /// <param name="inventoryDal">The inventory dal to be passed in</param>
	    /// @precondition none
	    /// @postcondition getInventoryDal() == inventoryDal
	    public InventoryController(IInventoryDal inventoryDal)
	    {
		    this.inventoryDal = inventoryDal;
	    }

		/// <summary>
		/// Returns a formatted output string for an inventory items history
		/// </summary>
		/// <param name="id">The id of the inventory item to get the history of</param>
		/// <returns>Returns a formatted output string for an inventory items history</returns>
		public IActionResult InventoryItemHistory(int id)
	    {
		    try
		    {
			    var inventoryItems = this.inventoryDal.GetItemHistorySummary(id);

			    return View("ItemHistory", inventoryItems);

		    }
		    catch (Exception)
		    {
			    ViewBag.ErrorMessage = "Uh-oh something went wrong";
			    return View("ViewInventory");
		    }

	    }

	    /// <summary>
	    /// The action results for managing employees
	    /// </summary>
	    /// <returns>the view for managing employees</returns>
	    public IActionResult ViewInventory()
	    {
		    List<InventoryItem> inventory = new List<InventoryItem>();
		    try
		    {
			    inventory = this.inventoryDal.GetInventoryItems();
		    }
		    catch (Exception)
		    {
			    ViewBag.ErrorMessage = "Uh-oh something went wrong";
		    }

		    return View(inventory);
	    }

		/// <summary>
		/// Gets the view for adding an inventory item
		/// </summary>
		/// <returns> The view for adding an inventory item</returns>
		[HttpGet]
	    public IActionResult AddInventory()
	    {
		    return View();
	    }

		/// <summary>
		/// The http post action for adding an inventory item
		/// </summary>
		/// <param name="item">the item being added</param>
		/// <returns>the Add inventory page with either a success or error message</returns>
		/// @precondition none
		/// @postcondition the item is added, or an error is displayed if something went wrong
		[HttpPost]
		public IActionResult AddInventory(InventoryItem item)
		{
			if (ModelState.IsValid)
			{
				try
				{
					this.inventoryDal.AddInventoryItem(item);
				}
				catch (Exception ex)
				{
					ViewBag.ErrorMessage = "Uh-oh, something bad happened";
					return View(item);
				}
			}
			else
			{
				return View(item);
			}
			ModelState.Clear();
			ViewBag.SuccessMessage = "Item Added!";
			return View(new InventoryItem());

		}

	}
}