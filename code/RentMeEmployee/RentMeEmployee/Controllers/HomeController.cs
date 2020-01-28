﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RentMeEmployee.DAL;
using RentMeEmployee.Models;

namespace RentMeEmployee.Controllers
{
    public class HomeController : Controller
    {

        public static bool isManager = false;

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
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
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(Employee employee)
        {
            try
            {
                if (ModelState.IsValid && EmployeeDal.Authenticate(employee.UserName, employee.Password) == 1)
                {
                    if (EmployeeDal.IsManager(employee.UserName) == 0)
                    {
                        isManager = false;
                    }
                    else
                    {
                        isManager = true;
                    }
                    return RedirectToAction("EmployeeLanding");
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Whoops, try again. Something went wrong.";
                return View("Index");
            }

            ViewBag.Error = "Invalid login";
            return View("Index");
        }

        public IActionResult EmployeeLanding()
        {
            ViewData["Message"] = "Employee Landing Page.";

            List<BorrowedItem> items = RentalDal.RetrieveAllBorrowedItems();

            return View(items);
        }

		public IActionResult UpdateStatus(int? id)
        {

            BorrowedItem item = RentalDal.RetrieveAllBorrowedItems().Where(currentItem => currentItem.TrasactionId == id).First();

            return View(item);
        }

        public IActionResult ConfirmedUpdate(int? id, string status)
        {

            RentalDal.UpdateStatus(id, status);

            return RedirectToAction("EmployeeLanding");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
