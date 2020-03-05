using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace RentMe.Controllers
{
    public class LibrarianController : Controller
    {

	    public IActionResult ViewAllUsers()
	    {
		    return View("AllUsers");
	    }

    }
}