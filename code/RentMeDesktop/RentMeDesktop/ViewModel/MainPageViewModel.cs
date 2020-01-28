using RentMeDesktop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentMeDesktop.ViewModel
{
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
    }
}
