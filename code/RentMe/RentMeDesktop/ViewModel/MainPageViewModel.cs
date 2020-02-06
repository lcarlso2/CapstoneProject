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
	/// </summary>
	public class MainPageViewModel : BaseViewModel
	{
		private RentalItem selectedRental;

		private bool canUpdateBeClicked;

		private ObservableCollection<string> statusFilters;

		private string selectedStatusFilter;

		/// <summary>
		/// Creates a new main page view model
		/// </summary>
		public MainPageViewModel()
		{
			this.SelectedStatusFilter = "All";
			this.StatusFilters = new ObservableCollection<string>{"All", "Ordered", "Shipped", "Returned"};
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
				try
				{
					if (this.SelectedStatusFilter.Equals("All"))
					{
						this.RentalItems = new ObservableCollection<RentalItem>(RentalDal.RetrieveAllRentedItems());
					}
					else
					{
						this.RentalItems =
							new ObservableCollection<RentalItem>(
								RentalDal.RetrieveSelectRentedItems(this.SelectedStatusFilter));
					}
				}
				catch (Exception)
				{
					DbError.showErrorWindow();
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
		/// Updates the rental item
		/// </summary>
		public void UpdateRentalItem()
		{
			try
			{
				RentalDal.UpdateStatus(this.SelectedRental.RentalId, this.SelectedRental.Status, this.CurrentEmployee.EmployeeId);
			}
			catch (Exception)
			{
				DbError.showErrorWindow();
			}

		}

    }
}
