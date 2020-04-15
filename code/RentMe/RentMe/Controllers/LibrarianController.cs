using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using RentMe.DAL;
using RentMe.Models;
using SharedCode.DAL;
using SharedCode.Model;

namespace RentMe.Controllers
{
    /// <summary>
    /// The controller responsible for the librarian pages 
    /// </summary>
    public class LibrarianController : Controller
    {
        private readonly IMemberDal memberDal;

        private readonly IRentalDal rentalDal;


        /// <summary>
        /// The current selected members email
        /// </summary>
        public static string CurrentMemberEmail;

        /// <summary>
        /// The current selected rentals id
        /// </summary>
        public static int CurrentRentalId;

        /// <summary>
        /// The default constructor for the librarian page which instantiates the default
        /// member dal and rental dal
        /// </summary>
        /// @precondition none
        /// @postcondition the librarian controller is created with the default dals
        [ActivatorUtilitiesConstructor]
        public LibrarianController()
        {
            this.memberDal = new MemberDal();
            this.rentalDal = new RentalDal();
        }

        /// <summary>
        /// The constructor for the librarian page where you pass in the desired member dal and rental dal
        /// </summary>
        /// <param name="memberDal"> the member dal being passed in</param>
        /// <param name="rentalDal"> the rentalDal dal being passed in</param>
        /// @precondition none
        /// @postcondition the librarian controller is created with the desired dals
        public LibrarianController(IMemberDal memberDal, IRentalDal rentalDal)
        {
            this.memberDal = memberDal;
            this.rentalDal = rentalDal;
        }

        /// <summary>
        /// The action result for viewing all members
        /// </summary>
        /// <returns>the all members page with either the members fetched from the db or an error message with an empty list if
        /// something went wrong when communicating with the db</returns>
        public IActionResult ViewAllMembers()
        {
            ViewBag.Filter = "All";
            try
            {
                var members = this.memberDal.GetAllMembers();
                return View("AllMembers", members);
            }
            catch (Exception)
            {
                ViewBag.Error = "Uh-oh.. something went wrong";
                return View("AllMembers", new List<RegisteringMember>());
            }

        }

        /// <summary>
        /// The member history page of the member with the selected id
        /// </summary>
        /// <param name="email">the email of the member</param>
        /// <returns>the member history page with the history of the member or an error message with an empty list if
        /// something went wrong when communicating with he db</returns>
        public IActionResult ViewMemberHistory(string email)
        {
            CurrentMemberEmail = email;
            try
            {
                var rentals = this.rentalDal.RetrieveAllRentalsByCustomer(email);
                var member = this.memberDal.GetAllMembers().First(current => current.Email.Equals(email));
                ViewBag.IsBlacklisted = member.IsBlacklisted;
                return View("MemberHistory", rentals);
            }
            catch (Exception)
            {
                ViewBag.Error = "Uh-oh.. something went wrong";
                return View("MemberHistory", new List<RentalItem>());
            }
        }

        /// <summary>
        /// The member history page with the selected member now blacklisted
        /// </summary>
        /// <param name="id">the id of the member to blacklist</param>
        /// <returns>The all members page if a valid action. Else returns to the all members page if an error occured.</returns>
		public IActionResult BlacklistMember(int id)
        {
            ViewBag.Filter = "All";
            try
            {
                this.memberDal.UpdateBlacklistStatus(id);

                var members = this.memberDal.GetAllMembers();
                return View("AllMembers", members);
            }
            catch (Exception)
            {
                ViewBag.Error = "Uh-oh.. something went wrong";
                return View("AllMembers", new List<RegisteringMember>());
            }
        }

        /// <summary>
        /// The view for the details of a given rental
        /// </summary>
        /// <param name="id">the rental id the details are being displayed for</param>
        /// <returns>the detail for the given rental id or the detail view with an error message if something went wrong</returns>
        public IActionResult RentalDetails(int id)
        {
            CurrentRentalId = id;
            try
            {
                var rentalDetails = this.rentalDal.RetrieveHistoryForRentalTransaction(id);
                return View("RentalDetails", rentalDetails);
            }
            catch (Exception)
            {
                ViewBag.Error = "Uh-oh.. something went wrong";
                return View("RentalDetails", new List<RentalItem>());
            }

        }

        /// <summary>
        /// Returns the all member page with a filtered list of members based on the filter
        /// </summary>
        /// <param name="filter">the desired filter</param>
        /// <returns>The all member page with either all of the members or the overdue members depending on the selected filter. If an error
        /// occurs then the allmember page is returned with an empty list of members and an error message</returns>
        public IActionResult MemberFilter(string filter)
        {
            try
            {
                if (filter.Equals("All"))
                {
                    ViewBag.Filter = filter;
                    var members = this.memberDal.GetAllMembers();
                    return View("AllMembers", members);
                }
                else
                {
                    ViewBag.Filter = filter;
                    var members = this.memberDal.GetOverdueMembers();
                    return View("AllMembers", members);
                }
            }
            catch (Exception)
            {
                ViewBag.Filter = filter;
                ViewBag.Error = "Uh-oh.. something went wrong";
                return View("AllMembers", new List<RegisteringMember>());
            }

        }

    }
}