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

        private IRentalDal rentalDal;
        private IEmployeeDal employeeDal;


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
        /// Initializes a new instance of the <see cref="BaseViewModel"/> class.
        /// </summary>
        /// @postcondition this.rentalDal is created as a new RentalDal
        /// @postcondition this.employeeDal is created as a new EmployeeDal
        public BaseViewModel()
        {
            this.rentalDal = new RentalDal();
            this.employeeDal = new EmployeeDal();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseViewModel"/> class.
        /// </summary>
        /// <param name="rentalDal">The rental dal.</param>
        /// <param name="employeeDal">The employee dal.</param>
        /// @postcondition this.rentalDal is set to the rental dal that is passed in
        /// @postcondition this.employeeDal is set to the employee dal that is passed in
        public BaseViewModel(IRentalDal rentalDal, IEmployeeDal employeeDal)
        {
            this.rentalDal = rentalDal;
            this.employeeDal = employeeDal;
        }
        /// <summary>
        /// Gets the borrowed items.
        /// </summary>
        public void GetRentalItems()
        {
            try
            {
                this.RentalItems = new ObservableCollection<RentalItem>(this.rentalDal.RetrieveAllRentedItems());
            } catch (Exception ex)
            {
                throw ex;
            }
           
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
                isValidEmployee = this.employeeDal.Authenticate(username, password) == VALID_EMPLOYEE;
                if (isValidEmployee)
                {
                    this.CurrentEmployee = this.employeeDal.GetCurrentUser(username, password);
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
