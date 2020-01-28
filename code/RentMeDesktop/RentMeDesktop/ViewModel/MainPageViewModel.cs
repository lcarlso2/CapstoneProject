using RentMeDesktop.DAL;
using RentMeDesktop.Model;
using RentMeDesktop.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentMeDesktop.ViewModel
{
    /// <summary>
    /// The main page viewmodel 
    /// </summary>
    public class MainPageViewModel : BaseViewModel 
    {
        private BorrowedItem selectedRental;

        /// <summary>
        /// Gets or sets the selected borrowed item 
        /// </summary>
        /// <value>
        /// The selected borrowed item 
        /// </value>
        public BorrowedItem SelectedRental
        {
            get => this.selectedRental;
            set
            {
                this.selectedRental = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Updates the rental item
        /// </summary>
        public void UpdateRentalItem()
        {
            try
            {
                BorrowedItemDAL.UpdateBorrowedItem(this.SelectedRental);
            } catch (Exception ex)
            {
                DBError.showErrorWindow();
            }
           
        }


    }
}
