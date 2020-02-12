using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RentMeDesktop.Utility;
using RentMeDesktop.View;
using SharedCode.DAL;
using SharedCode.Model;

namespace RentMeDesktop.ViewModel
{
    /// <summary>
    /// The main page viewmodel 
    ///     Inherits from BaseViewModel
    /// </summary>
    public class MainPageViewModel : BaseViewModel
    {
        private RentalItem selectedRental;

        private bool canUpdateBeClicked;

        private ObservableCollection<string> statusFilters;

        private string selectedStatusFilter;

        private IRentalDal rentalDal;

        /// <summary>
        /// Creates a new main page view model
        /// </summary>
        /// @precondition none
        /// @postcondition Created mainPageViewModel with new RentalDal this.StatusFilters populated 
        ///     and this.SelectedStatusFilter set to all
        public MainPageViewModel()
        {
            this.SelectedStatusFilter = "All";
            this.StatusFilters = new ObservableCollection<string> { "All", "Ordered", "Shipped", "Returned" };
            this.rentalDal = new RentalDal();
        }

        /// <summary>
		/// Creates a main page view model with the given rentalDal
		/// </summary>
		/// <param name="rentalDal">The rental dal to be used for tests</param>
        /// @precondition none
        /// @postcondition Created mainPageViewModel with new RentalDal this.StatusFilters populated 
        ///     and this.SelectedStatusFilter set to all
		public MainPageViewModel(IRentalDal rentalDal)
        {
            this.SelectedStatusFilter = "All";
            this.StatusFilters = new ObservableCollection<string> { "All", "Ordered", "Shipped", "Returned" };
            this.rentalDal = rentalDal;
        }
        /// <summary>
        /// Gets or sets the rental filters
        /// </summary>
        /// <value>
        /// The rental filters
        /// </value>
        public ObservableCollection<string> StatusFilters
		{
			get => this.statusFilters;
			set
			{
				this.statusFilters = value;
				this.OnPropertyChanged();
			}
		}

        /// <summary>
        /// Gets or sets the selected rental filter
        /// </summary>
        /// <value>
        ///	The selected rental filter
        /// </value>
        public string SelectedStatusFilter
        {
            get => this.selectedStatusFilter;
            set
            {
                this.selectedStatusFilter = value;
                if (this.rentalDal != null)
                {
                    try
                    {
                        if (this.SelectedStatusFilter.Equals("All"))
                        {
                            this.RentalItems =
                                new ObservableCollection<RentalItem>(this.rentalDal.RetrieveAllRentedItems());
                        }
                        else
                        {
                            this.RentalItems =
                                new ObservableCollection<RentalItem>(
                                    this.rentalDal.RetrieveSelectRentedItems(this.SelectedStatusFilter));
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
                this.OnPropertyChanged();
            }
        }


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
                this.CanUpdateBeClicked = (this.SelectedRental != null) && !this.SelectedRental.Status.Equals("Returned");
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets property depending on if update can be clicked
        /// </summary>
        /// <value>
        /// True if a rental has been selected and the rental hasn't been returned otherwise false
        /// </value>
        public bool CanUpdateBeClicked
        {
            get => this.canUpdateBeClicked;
            set
            {
                this.canUpdateBeClicked = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Updates the rental item in the db with the selected status
        /// </summary>
        public int UpdateRentalItem()
        {
            var rowsAffected = -1;
            try
            {
                rowsAffected = this.rentalDal.UpdateStatus(this.SelectedRental.RentalId, this.SelectedRental.Status, this.CurrentEmployee.EmployeeId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return rowsAffected;
        }

    }
}
