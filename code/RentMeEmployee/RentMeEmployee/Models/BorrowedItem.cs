using System;
using System.ComponentModel.DataAnnotations;

namespace RentMeEmployee.Models
{
    public class BorrowedItem
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

   

        public BorrowedItem()
        {

        }

        public void setStatus(string status)
        {
            this.Status = status;
        }


    }
}
