﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RentMe.DAL;
using RentMe.Models;

namespace RentMe.Controllers
{
	public class HomeController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}

		public IActionResult About()
		{
			ViewData["Message"] = "Your application description page.";

			return View();
		}

        
		public IActionResult Browse()
		{
			
            List<MediaModel> media = new List<MediaModel>
            {
                new MediaModel { Title = "Lord of the Rings", Category = "Book" },
                new MediaModel { Title = "Little Women", Category = "Book"  },
                new MediaModel { Title = "Star Wars", Category = "Movie"  }
            };

            return View(media);
		}

		public IActionResult Contact()
		{
			ViewData["Message"] = "Your contact page.";

			return View();
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[HttpGet]
		public IActionResult Register()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Register(RegisteringCustomer customer)
		{

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

		[HttpGet]
		public IActionResult Login()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Login(Customer customer)
		{
			if (ModelState.IsValid)
			{
				ModelState.Clear();
           
                return RedirectToAction("Browse");
				
			} 
			else
			{
				return View("Index");
			}
		}


		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
