using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using RentMe.DAL;
using RentMe.Models;

namespace RentMe.Controllers
{
    public class LibrarianController : Controller
    {
	    private readonly ILibrarianDal librarianDal;

	    [ActivatorUtilitiesConstructor]
		public LibrarianController()
	    {
			this.librarianDal = new LibrarianDal();
	    }

		public LibrarianController(ILibrarianDal librarianDal)
		{
			this.librarianDal = librarianDal;
		}

	    public IActionResult ViewAllMembers()
	    {
		    try
		    {
			    var members = this.librarianDal.GetAllMembers();
			    return View("AllMembers", members);
		    }
		    catch (Exception)
		    {
			    ViewBag.Error = "Uh-oh.. something went wrong";
				return View("AllMembers", new List<RegisteringMember>());
			}
            
	    }

	    public IActionResult ViewMemberHistory(int id)
	    {

		    return View("MemberHistory");
	    }

	}
}