using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace SharedCode.Model
{
    /// <summary>
    /// The rental item class
    /// </summary>
    public class RentalItem : InventoryItem
    {
        /// <summary>
        /// Gets or sets the member id that rented the item
        /// </summary>
        /// <value>
        ///The member id
        /// </value>
        [Display(Name = "Member ID")]
        public int MemberId { get; set; }

        /// <summary>
        /// Gets or sets the member email that rented the item
        /// </summary>
        /// <value>
        /// The member email
        /// </value>
        [Display(Name = "Member Email")]
        public string MemberEmail { get; set; }

        /// <summary>
        /// Gets or sets the rental ID
        /// </summary>
        /// <value>
        ///The rental ID
        /// </value>
        [Display(Name = "Rental ID")]
        public int RentalId { get; set; }


        /// <summary>
        /// Gets or sets the rental date
        /// </summary>
        /// <value>
        ///The rental date
        /// </value>
        [Display(Name = "Rental Date")]
        public DateTime RentalDate { get; set; }

        /// <summary>
        /// Gets or sets the return date
        /// </summary>
        ///<value>
        ///The return date
        /// </value>
        [Display(Name = "Return Date")]
        public DateTime ReturnDate { get; set; }


        /// <summary>
        /// Gets or sets the status
        /// </summary>
        /// <value>
        ///The status
        /// </value>
        [Display(Name = "Order Status")]
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the date time for when the status of a rental item is updated
        /// </summary>
        /// <value>
        ///The update date time
        /// </value>
         [Display(Name = "Update Date")]
        public DateTime UpdateDateTime {get; set; }


        /// <summary>
        /// Creates a new rental item 
        /// </summary>
        public RentalItem()
        {

        }

        /// <summary>
        /// The rental string info
        /// </summary>
        public string RentalItemInfo => $"Rental ID: {this.RentalId}, Inventory ID: {this.InventoryId}, Category: {this.Category}, Title: {this.Title}\nMember ID: {this.MemberId} Member Email: {this.MemberEmail}\nRental Date: {this.RentalDate}, Return Date: {this.ReturnDate}, Status: {this.Status}\n";

        /// <summary>
        /// Gets the list of possible statuses 
        /// </summary>
        /// <param name="currentStatus">the current status</param>
        /// <returns>the list of possible statuses</returns>
        public static List<string> GetPossibleStatuses(string currentStatus)
        {
            List<string> statuses;


            if (currentStatus.Equals("Ordered"))
            {
                statuses = new List<string>{ "Ordered", "Shipped" };

            } else if (currentStatus.Equals("Shipped"))
            {

	            statuses = new List<string> { "Shipped", "Returned" };

            } 
            else
            {

	            statuses = new List<string> { "Returned" };

            }

            return statuses;
        }

        /// <summary>
        /// Gets the status id given the string status
        /// </summary>
        /// <param name="status">the status</param>
        /// <returns>the number equivalent of the status</returns>
        public static int GetStatusId(string status)
        {
	        int statusId;

	        if (status.Equals("Ordered"))
	        {
		        statusId = 1;
	        }
	        else if (status.Equals("Shipped"))
	        {
		        statusId = 2;
	        }
	        else
	        {
		        statusId = 4;
	        }

	        return statusId;

        }




    }
}
