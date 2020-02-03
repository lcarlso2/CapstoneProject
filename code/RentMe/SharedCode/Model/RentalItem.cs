using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace SharedCode.Model
{
	/// <summary>
	/// The rental item class
	/// </summary>
	public class RentalItem
	{

        [Display(Name = "Member ID")]
        public int MemberId { get; set; }

        [Display(Name = "Member Email")]
        public string MemberEmail { get; set; }

        [Display(Name = "Rental ID")]
        public int RentalId { get; set; }


        [Display(Name = "Rental Date")]
        public DateTime RentalDate { get; set; }

        [Display(Name = "Return Date")]
        public DateTime ReturnDate { get; set; }

        [Display(Name = "Inventory ID")]
        public int InventoryId { get; set; }

        [Display(Name = "Category")]
        public string Category { get; set; }

        [Display(Name = "Title")]
        public string Title { get; set; }


        [Display(Name = "Order Status")]
        public string Status { get; set; }


        /// <summary>
        /// Creates a new rental item 
        /// </summary>
        public RentalItem()
        {

        }

        public void setStatus(string status)
        {
            this.Status = status;
        }

        /// <summary>
        /// The rental string info
        /// </summary>
        public string RentalItemInfo => $"Rental ID: {this.RentalId}, Inventory ID: {this.InventoryId}, Category: {this.Category}, Title: {this.Title}\nMember ID: {this.MemberId} Member Email: {this.MemberEmail}\nRental Date: {this.RentalDate}, Return Date: {this.ReturnDate}, Status: {this.Status}\n";


    }
}
