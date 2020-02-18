using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RentMe.DAL;
using RentMe.Models;
using SharedCode.DAL;
using SharedCode.Model;

namespace RentMe.Controllers
{
    /// <summary>
    /// The home controller responsible for the page communication of the Rent Me Application (Member Side).
    /// </summary>
    public class HomeController : Controller
    {

        private readonly IBorrowDal borrowDal;
        private readonly ICustomerDal customerDal;
        private readonly IMediaDal mediaDal;
        private readonly IRentalDal rentalDal;

        /// <summary>
        /// The current user that is logged into the system.
        /// If there is noone logged in, the user is null.
        /// </summary>
        public static Customer CurrentUser;

        /// <summary>
        /// Creates a new home controller with the desired dals
        /// </summary>
        /// <param name="borrowDal">the borrow dal for communication</param>
        /// <param name="customerDal">The customer dal for communication</param>
        /// <param name="mediaDal">the media dal for communication</param>
        /// @precondition none
        /// @postcondition the controller is created with the input dals
        public HomeController(IBorrowDal borrowDal, ICustomerDal customerDal, IMediaDal mediaDal, IRentalDal rentalDal)
        {
            this.borrowDal = borrowDal;
            this.customerDal = customerDal;
            this.mediaDal = mediaDal;
            this.rentalDal = rentalDal;
        }

        /// <summary>
        /// Creates a new default home controller
        /// </summary>
        /// @precondition none
        /// @postcondition the controller is created with new borrowDals, CustomerDals, MediaDals, and RentalDals.
        [ActivatorUtilitiesConstructor]
        public HomeController()
        {
            this.borrowDal = new BorrowDal();
            this.customerDal = new CustomerDal();
            this.mediaDal = new MediaDal();
            this.rentalDal = new RentalDal();
        }

        /// <summary>
        /// The index action result
        /// </summary>
        /// <returns>the browse page if someone is logged in, otherwise the index page if no one is logged in</returns>
        public IActionResult Index()
        {
            if (CurrentUser != null)
            {
                return RedirectToAction("Browse");
            }
            return View("Index");
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
                return View(media);
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
        public IActionResult ConfirmedBorrow(int? id)
        {
            Media media = this.mediaDal.RetrieveAllMedia().First(currentMedia => currentMedia.InventoryId == id);

            try
            {
                this.borrowDal.BorrowItem(CurrentUser, media);

                return RedirectToAction("Browse");
            }
            catch (NullReferenceException ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                ViewBag.Error = "Please log in again.";
                return View("ConfirmBorrow", media);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                ViewBag.Error = ex.Message;
                return View("ConfirmBorrow", media);
            }
        }

        /// <summary>
        /// The signout action result
        /// </summary>
        /// <returns>The index page</returns>
        public IActionResult Signout()
        {
            CurrentUser = null;

            return RedirectToAction("Index");
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
        /// The rental history action
        /// </summary>
        /// <returns>The rental history page</returns>
        public IActionResult RentalHistory()
        {
            try
            {
                var rentals = this.rentalDal.RetrieveAllRentalsByCustomer(CurrentUser.Email);

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
                media = this.rentalDal.RetrieveAllRentalsByCustomer(CurrentUser.Email);

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
                return View("Browse");
            }

            return View("RentalHistory", sortedMedia);

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



        /// <summary>
        /// The http post for the action result login
        /// </summary>
        /// <param name="customer">the customer logging in</param>
        /// <returns>The browse page if the customer is signed in otherwise stays on the index page if the login is invalid or an error occurs</returns>
        [HttpPost]
        public IActionResult Login(Customer customer)
        {
            try
            {
                if (ModelState.IsValid && this.customerDal.Authenticate(customer.Email, customer.Password) == 1)
                {
                    CurrentUser = new Customer { Email = customer.Email, Password = customer.Password };
                    return RedirectToAction("Browse");
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


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
