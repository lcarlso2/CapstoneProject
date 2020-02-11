using SharedCode.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharedCode.View
{
    /// <summary>
    /// Holds methods used for formatting and generating output for the view
    /// </summary>
    public class OutputFormatter
    {
        private const int DEFAULT_ID = -1;

        /// <summary>
        /// Generates a summary of an inventory items history
        /// </summary>
        /// <param name="rentalItems"></param>
        /// <returns></returns>
        public string GenerateHistoryOfInventoryItem(List<RentalItem> rentalItems)
        {
            StringBuilder sb = new StringBuilder();
            var rentalId = DEFAULT_ID;
            var isFirstItem = true;
            rentalItems = rentalItems.OrderBy(item => item.RentalDate).ToList();
            foreach (var rentalItem in rentalItems)
            {
                
                if (rentalId != rentalItem.RentalId)
                {
                    if(!isFirstItem)
                    {
                        sb.Append("------------------------------------------------------------------------" + Environment.NewLine + Environment.NewLine);
                    }
                    
                    sb.Append("Title: " + rentalItem.Title + "\tCategory: " + rentalItem.Category + Environment.NewLine + "Member ID: " + rentalItem.MemberId + "\tMember Email: " + rentalItem.MemberEmail + Environment.NewLine + Environment.NewLine +
                    "Date Rented: " + rentalItem.RentalDate + "\tDate Returned: " + rentalItem.ReturnDate + Environment.NewLine + Environment.NewLine);
                    rentalId = rentalItem.RentalId;
                    isFirstItem = false;
                } 

                sb.Append("Status: " + rentalItem.Status + "\tUpdate Date: " + rentalItem.UpdateDateTime + Environment.NewLine);
              
            }
            return sb.ToString();
        }

    }
}
