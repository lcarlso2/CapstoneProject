using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentMeEmployee.Models
{
    public class BorrowedItem
    {
        public int TrasactionId { get; set; }

        public int ItemId { get; set; }

        public string CustomerEmail { get; set; }
        
        public DateTime RentalDate { get; set; }

        public DateTime ReturnDate { get; set; }

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
