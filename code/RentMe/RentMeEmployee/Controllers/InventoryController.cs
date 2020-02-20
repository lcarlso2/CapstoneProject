using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
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

	    public IActionResult AddInventory()
	    {
		    return View();
	    }

	}
}