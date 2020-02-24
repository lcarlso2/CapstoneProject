using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using RentMe.DAL;
using RentMe.Models;

namespace RentMe.Controllers
{
    /// <summary>
    /// The BorrowController responsible for all pages that have to do with borrowing 
    /// </summary>
    public class BorrowController : Controller
    {
	    private readonly IBorrowDal borrowDal;

	    private readonly IMediaDal mediaDal;

	    public static Media SelectedItem;

	    /// <summary>
	    /// Creates a new borrow controller with the desired dals
	    /// </summary>
	    /// <param name="borrowDal">the borrow dal for communication</param>
	    /// <param name="mediaDal">the media dal for communication</param>
	    /// @precondition none
	    /// @postcondition the controller is created with the input dals
	    public BorrowController(IBorrowDal borrowDal, IMediaDal mediaDal)
	    {
		    this.borrowDal = borrowDal;
		    this.mediaDal = mediaDal;
	    }

	    /// <summary>
	    /// Creates a new default borrow controller
	    /// </summary>
	    /// @precondition none
	    /// @postcondition the controller is created with new borrowDals and MediaDals.
	    [ActivatorUtilitiesConstructor]
	    public BorrowController()
	    {
		    this.borrowDal = new BorrowDal();
		    this.mediaDal = new MediaDal();
	    }


        /// <summary>
        /// The confirm borrow action result
        /// </summary>
        /// <param name="id">the id of the item being borrowed</param>
        /// <returns>The confirm borrow screen or if an error occurs redirected to the browse action</returns>
        public IActionResult ConfirmBorrow(int? id)
	    {
		    try
		    {
		
			    var media = this.mediaDal.RetrieveAllMedia().First(currentMedia => currentMedia.InventoryId == id);
			

			    SelectedItem = media;

			    return View(new ConfirmBorrowObject());
		    }
		    catch (Exception)
		    {
			    ViewBag.Error = "Uh-oh.. something went wrong";
			    return RedirectToAction("Browse");
		    }

	    }

        /// <summary>
        /// The confirmed borrow action result
        /// </summary>
        /// <param name="id">the id of the item being borrowed</param>
        /// <returns>Returns to the browse page if the rental was confirmed, otherwise stays at the confirm page if an error occurs</returns>
        [HttpPost]
        public IActionResult ConfirmedBorrow(ConfirmBorrowObject item)
        {
	        if (ModelState.IsValid)
	        {
		        try
		        {
			        this.borrowDal.BorrowItem(HomeController.CurrentUser, SelectedItem);

			        return RedirectToAction("Browse");
		        }
		        catch (NullReferenceException ex)
		        {
			        ViewBag.ErrorMessage = ex.Message;
			        ViewBag.Error = "Please log in again.";
			        return View("ConfirmBorrow", item);
		        }
		        catch (Exception ex)
		        {
			        ViewBag.ErrorMessage = ex.Message;
			        ViewBag.Error = ex.Message;
			        return View("ConfirmBorrow", item);
		        }
	        }

	        ViewBag.Error = "Please select a shipping address";
	        return View("ConfirmBorrow", item);
        }

        /// <summary>
        /// The browse action
        /// </summary>
        /// <returns>The browse page</returns>
        public IActionResult Browse()
        {
            try
            {
                var media = this.mediaDal.RetrieveAllMedia();
                ViewBag.Type = "Type";
                ViewBag.Category = "Category";
                return View("Browse", media);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                ViewBag.Error = "Uh-oh.. something went wrong";
                return View(new List<Media>());
            }

        }




        /// <summary>
        /// The type filter action
        /// </summary>
        /// <param name="type">the type to filter the media by</param>
        /// <returns>The browse page with filtered items</returns>
        public IActionResult TypeFilter(string type)
        {
            List<Media> media = new List<Media>();
            try
            {


                if (type == "All")
                {
                    media = this.mediaDal.RetrieveAllMedia();
                }
                else
                {
                    media = this.mediaDal.RetrieveMediaByType(type);
                }
            }
            catch (Exception)
            {
                ViewBag.Error = "Uh-oh something went wrong";
                return View("Browse");
            }

            ViewBag.Category = "Category";
            ViewBag.Type = type;
            return View("Browse", media);

        }



        /// <summary>
        /// The type category action
        /// </summary>
        /// <param name="category">the category to filter the media by</param>
        /// <returns>The browse page with filtered items</returns>
        public IActionResult CategoryFilter(string category)
        {
            List<Media> media = new List<Media>();
            try
            {
                if (category == "All")
                {
                    media = this.mediaDal.RetrieveAllMedia();
                }
                else
                {
                    media = this.mediaDal.RetrieveMediaByCategory(category);
                }
            }
            catch (Exception)
            {
                ViewBag.Error = "Uh-oh something went wrong";
                return View("Browse");
            }

            ViewBag.Type = "Type";
            ViewBag.Category = category;
            return View("Browse", media);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddAddress(Address address)
        {
	        var item = new ConfirmBorrowObject
	        {
		        AddressId = 2
	        };
            
	        if (ModelState.IsValid)
	        {

		        HomeController.CurrentUser.Addresses.Add(address);

		        return View("ConfirmBorrow", item);
	        }
	        ViewBag.Error = "Invalid Address";
	        return View("ConfirmBorrow", item);
            
        }

        public IActionResult AddAddress()
        {
	        return PartialView("AddAddress");
        }


    }
}