using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RentMe.DAL;
using RentMe.Models;

namespace RentMe.Controllers
{
	/// <summary>
	/// The home controller responsible for the page communication 
	/// </summary>
	public class HomeController : Controller
	{
		/// <summary>
		/// The current user that is logged in
		/// </summary>
		public static Customer CurrentUser;

		private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}

		/// <summary>
		/// The index action result
		/// </summary>
		/// <returns>the browse page if someone is logged in, otherwise the index page</returns>
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
		/// <returns>The confirm borrow screen</returns>
		public IActionResult ConfirmBorrow(int? id)
		{

			Media media = MediaDal.RetrieveAllMedia().First(currentMedia => currentMedia.InventoryId == id);
			return View(media);
		}

		/// <summary>
		/// The confirmed borrow action result
		/// </summary>
		/// <param name="id">the id of the item being borrowed</param>
		/// <returns>Returns to the browse page if the rental was confirmed, otherwise stays at the confirm page</returns>
		public IActionResult ConfirmedBorrow(int? id)
		{
			Media media = MediaDal.RetrieveAllMedia().First(currentMedia => currentMedia.InventoryId == id);

			try
			{
				BorrowDal.OldBorrowItem(CurrentUser, media);

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
				List<Media> media = MediaDal.RetrieveAllMedia();

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
		/// <returns>The browse page with filtered items</returns>
		public IActionResult TypeFilter(string type)
		{
			List<Media> media = new List<Media>();
			if (type == "All")
			{
				media = MediaDal.RetrieveAllMedia();
			}
			else
			{
				media = MediaDal.RetrieveMediaByType(type);
			}

			return View("Browse", media);

		}

		/// <summary>
		/// The type category action
		/// </summary>
		/// <returns>The browse page with filtered items</returns>
		public IActionResult CategoryFilter(string category)
		{
			List<Media> media = new List<Media>();
			if (category == "All")
			{
				media = MediaDal.RetrieveAllMedia();
			}
			else
			{
				media = MediaDal.RetrieveMediaByCategory(category);
			}

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
			Debug.WriteLine(HttpContext.User);
			if (ModelState.IsValid)
			{
				try
				{
					CustomerDal.RegisterCustomer(customer);
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
		/// <returns>The browse page if the customer is signed in</returns>
		[HttpPost]
		public IActionResult Login(Customer customer)
		{
			try
			{
				if (ModelState.IsValid && CustomerDal.Authenticate(customer.Email, customer.Password) == 1)
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


		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
