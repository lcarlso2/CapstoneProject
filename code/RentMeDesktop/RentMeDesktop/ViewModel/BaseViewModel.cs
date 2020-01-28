using RentMeDesktop.DAL;
using RentMeDesktop.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace RentMeDesktop.ViewModel
{
    public class BaseViewModel : INotifyPropertyChanged
    {

        private const int VALID_EMPLOYEE = 1;

        private Employee currentEmployee;

        


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
                //this.CurrentEmployee = EmployeeDAL.GetCurrentUser(username, password);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return isValidEmployee;
        }

    }
}
