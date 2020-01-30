using System;
using System.ComponentModel.DataAnnotations;

namespace RentMeSharedCode.Model
{
    public class RentalItem
    {
        [Display(Name = "Transaction ID")]
        public int TransactionId { get; set; }

        [Display(Name = "Item ID")]
        public int ItemId { get; set; }

        [Display(Name = "Customer Email")]
        public string CustomerEmail { get; set; }

        [Display(Name = "Rental Date")]
        public DateTime RentalDate { get; set; }

        [Display(Name = "Return Date")]
        public DateTime ReturnDate { get; set; }

        [Display(Name = "Order Status")]
        public string Status { get; set; }

   

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
        public string RentalItemInfo => $"Transaction ID: {this.TransactionId}, Item ID: {this.ItemId}, Customer Email: {this.CustomerEmail}, Rental Date: {this.RentalDate}, Return Date: {this.ReturnDate}, Status: {this.Status}";



    }
}
