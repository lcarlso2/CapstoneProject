using RentMeDesktop.DAL;
using RentMeDesktop.Model;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace RentMeDesktop.ViewModel
{
    public class BaseViewModel : INotifyPropertyChanged 
    {

        private const int VALID_EMPLOYEE = 1;

        private Employee currentEmployee;

        private ObservableCollection<BorrowedItem> borrowedItems;


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
        public ObservableCollection<BorrowedItem> BorrowedItems
        {
            get => this.borrowedItems;
            set
            {
                this.borrowedItems = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets the borrowed items.
        /// </summary>
        public void GetBorrowedItems()
        {
            this.BorrowedItems = new ObservableCollection<BorrowedItem>(BorrowedItemDAL.RetrieveAllBorrowedItems());
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


        public bool ValidateLoginCredentials(string username, string password)
        {
            bool isValidEmployee;

            try
            {
                isValidEmployee = EmployeeDAL.Authenticate(username, password) == VALID_EMPLOYEE;
                this.CurrentEmployee = EmployeeDAL.GetCurrentUser(username, password);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return isValidEmployee;
        }

    }
}
