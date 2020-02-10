using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedCode.DAL;
using SharedCode.Model;

namespace RentMeDesktop.ViewModel
{
    /// <summary>
    /// The view model for the view inventory page
    /// </summary>
    public class InventoryViewModel : BaseViewModel
    {
        private IInventoryDal inventoryDal;
        private ObservableCollection<InventoryItem> inventory;

        private InventoryItem selectedInventoryItem;

        /// <summary>
        /// The selected inventory item
        /// </summary>
        public InventoryItem SelectedInventoryItem
        {
            get => this.selectedInventoryItem;
            set
            {
                this.selectedInventoryItem = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// The entire inventory for the app
        /// </summary>
        public ObservableCollection<InventoryItem> Inventory
        {
            get => this.inventory;
            set
            {
                this.inventory = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InventoryViewModel"/> class.
        /// </summary>
        /// @postcondition this.inventoryDal is created as a new InventoryDal
        public InventoryViewModel()
        {
            this.inventoryDal = new InventoryDal();
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="InventoryViewModel"/> class.
        /// </summary>
        /// <param name="inventoryDal">The inventory dal.</param>
        /// /// @postcondition this.inventoryDal is set to the inventory dal that is passed in
        public InventoryViewModel(IInventoryDal inventoryDal)
        {
            this.inventoryDal = inventoryDal;
        }

        /// <summary>
        /// Retrieves all inventory items from the db
        /// </summary>
        /// @postcondition this.Inventory == all inventory items from db
        public void RetrieveAllInventoryItems()
        {
            this.Inventory = new ObservableCollection<InventoryItem>(this.inventoryDal.GetInventoryItems());
        }

    }
}
