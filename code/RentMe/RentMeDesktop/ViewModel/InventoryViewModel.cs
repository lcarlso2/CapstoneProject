using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedCode.Model;

namespace RentMeDesktop.ViewModel
{
    /// <summary>
    /// The view model for the view inventory page
    /// </summary>
    class InventoryViewModel
    {
        private ObservableCollection<InventoryItem> inventory;

        private InventoryItem selectedInventoryItem;

        /// <summary>
        /// The selected inventory item
        /// </summary>
        public InventoryItem SelectedInventoryItem;

        /// <summary>
        /// The entire inventory for the app
        /// </summary>
        public ObservableCollection<InventoryItem> Inventory;

    }
}
