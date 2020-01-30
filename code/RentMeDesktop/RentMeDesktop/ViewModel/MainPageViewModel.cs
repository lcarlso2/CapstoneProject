using RentMeDesktop.View;
using RentMeSharedCode.DAL;
using RentMeSharedCode.Model;
using System;

namespace RentMeDesktop.ViewModel
{
    /// <summary>
    /// The main page viewmodel 
    /// </summary>
    public class MainPageViewModel : BaseViewModel 
    {
        private RentalItem selectedRental;

        private bool isRentalSelected;

        /// <summary>
        /// Gets or sets the selected borrowed item 
        /// </summary>
        /// <value>
        /// The selected borrowed item 
        /// </value>
        public RentalItem SelectedRental
        {
            get => this.selectedRental;
            set
            {
                this.selectedRental = value;
                this.IsRentalSelected = (this.selectedRental != null);
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets property depending on if a rental is selected
        /// </summary>
        /// <value>
        /// True if a rental has been selected and false if not
        /// </value>
        public bool IsRentalSelected
        {
            get => this.isRentalSelected;
            set
            {
                this.isRentalSelected = value;
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
                RentalDAL.UpdateStatus(this.SelectedRental.TransactionId, this.SelectedRental.Status);
            } catch (Exception ex)
            {
                DBError.showErrorWindow();
            }
           
        }


    }
}
