using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentMeDesktop.Model
{
    /// <summary>
    /// The borrowed item class
    /// </summary>
    public class BorrowedItem
    {

        /// <summary>
        /// Gets or sets the transaction id
        /// </summary>
        public int TrasactionId { get; set; }

        /// <summary>
        /// Gets or sets the item id
        /// </summary>
        public int ItemId { get; set; }

        /// <summary>
        /// Gets or sets the customer email
        /// </summary>
        public string CustomerEmail { get; set; }

        /// <summary>
        /// Gets or sets the rental date
        /// </summary>
        public DateTime RentalDate { get; set; }

        /// <summary>
        /// Gets or sets the return date
        /// </summary>
        public DateTime ReturnDate { get; set; }

        /// <summary>
        /// Gets or sets the status
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// The borrowed string info
        /// </summary>
        public string BorrowedItemInfo => $"Transaction ID: {this.TrasactionId}, Item ID: {this.ItemId}, Customer Email: {this.CustomerEmail}, Rental Date: {this.RentalDate}, Return Date: {this.ReturnDate}, Status: {this.Status}";

        /// <summary>
        /// Creates a new default borrowed item
        /// </summary>
        public BorrowedItem()
        {

        }
    }
}
