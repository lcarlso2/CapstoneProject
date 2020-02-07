using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using SharedCode.DAL;
using SharedCode.Model;

namespace RentMeDesktop.ViewModel
{
	/// <summary>
	/// The base view model class
	/// </summary>
	public class BaseViewModel : INotifyPropertyChanged
	{
        private const int VALID_EMPLOYEE = 1;

        private Employee currentEmployee;

        private ObservableCollection<RentalItem> rentalItems;


        /// <summary>
        /// Gets or sets the current employee
        /// </summary>
        /// <value>
        /// The current employee 
        /// </value>
        public Employee CurrentEmployee
        {
            get => this.currentEmployee;
            set
            {
                this.currentEmployee = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
		/// Gets or sets the borrowed items 
		/// </summary>
		/// <value>
		/// The borrow items 
		/// </value>
        public ObservableCollection<RentalItem> RentalItems
        {
            get => this.rentalItems;
            set
            {
                this.rentalItems = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets the borrowed items.
        /// </summary>
        public void GetRentalItems()
        {
            this.RentalItems = new ObservableCollection<RentalItem>(RentalDal.RetrieveAllRentedItems());
        }

        /// <summary>
        ///     Occurs when a property value changes.
        /// </summary>
        /// <returns> the event</returns>
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Validates the login credentials 
        /// </summary>
        /// <param name="username">the username</param>
        /// <param name="password">the password</param>
        /// <returns>True if the login was valid, otherwise false</returns>
        public bool ValidateLoginCredentials(string username, string password)
        {
            bool isValidEmployee;

            try
            {
                isValidEmployee = EmployeeDal.OldAuthenticate(username, password) == VALID_EMPLOYEE;
                if (isValidEmployee)
                {
                    this.CurrentEmployee = EmployeeDal.OldGetCurrentUser(username, password);
                    this.CurrentEmployee.Username = username;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return isValidEmployee;
        }
    }
}
