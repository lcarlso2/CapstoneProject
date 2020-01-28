using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentMeDesktop.Model
{
    public class BorrowedItem
    {

        public int TrasactionId { get; set; }

        public int ItemId { get; set; }

        public string CustomerEmail { get; set; }

        public DateTime RentalDate { get; set; }

        public DateTime ReturnDate { get; set; }

        public string Status { get; set; }

        public string BorrowedItemInfo => $"Transaction ID: {this.TrasactionId}, Item ID: {this.ItemId}, Customer Email: {this.CustomerEmail}, Rental Date: {this.RentalDate}, Return Date: {this.ReturnDate}, Status: {this.Status}";

        public BorrowedItem()
        {

        }
    }
}
