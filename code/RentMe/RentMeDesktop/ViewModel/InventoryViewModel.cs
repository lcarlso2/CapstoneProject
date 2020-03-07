using System;
using System.Collections.ObjectModel;

using SharedCode.DAL;
using SharedCode.Model;

namespace RentMeDesktop.ViewModel
{
    /// <summary>
    /// The view model for the view inventory page
    ///      Inherits from BaseViewModel
    /// </summary>
    public class InventoryViewModel : BaseViewModel
    {
        private IInventoryDal inventoryDal;

        private ObservableCollection<InventoryItem> inventory;

        private InventoryItem selectedInventoryItem;

        private bool canItemHistoryBeClicked;

        private bool canAddItemBeClicked;

        private string title;

        private string category;

        private string type;

        private string condition;

        private ObservableCollection<RentalItem> selectedItemHistory;

        /// <summary>
        /// Gets or sets the add item can be clicked
        /// </summary>
        /// @precondition none
        /// @postcondition the canAddItemBeClicked is set
        public bool CanAddItemBeClicked
        {
	        get => this.canAddItemBeClicked;
	        set
	        {
		        this.canAddItemBeClicked = value;
                this.OnPropertyChanged();
	        }
        }

        /// <summary>
        /// Gets or sets the title 
        /// </summary>
        /// @precondition none
        /// @postcondition the title is set
        public string Title
        {
	        get => this.title;
	        set
	        {
		        this.title = value;
		        this.CanAddItemBeClicked = this.canAddBeClicked();
		        this.OnPropertyChanged();
	        }
        }

        /// <summary>
        /// Gets or sets the category 
        /// </summary>
        /// @precondition none
        /// @postcondition the category is set
        public string Category
        {
	        get => this.category;
	        set
	        {
		        this.category = value;
		        this.CanAddItemBeClicked = this.canAddBeClicked();
                this.OnPropertyChanged();
	        }
        }

        /// <summary>
        /// Gets or sets the type 
        /// </summary>
        /// @precondition none
        /// @postcondition the type is set
        public string Type
        {
	        get => this.type;
	        set
	        {
		        this.type = value;
		        this.CanAddItemBeClicked = this.canAddBeClicked();
                this.OnPropertyChanged();
	        }
        }

        /// <summary>
        /// Gets or sets the condition 
        /// </summary>
        /// @precondition none
        /// @postcondition the condition is set
        public string Condition
        {
	        get => this.condition;
	        set
	        {
		        this.condition = value;
		        this.CanAddItemBeClicked = this.canAddBeClicked();
                this.OnPropertyChanged();
	        }
        }

        /// <summary>
        /// The selected inventory item
        /// </summary>
        public InventoryItem SelectedInventoryItem
        {
            get => this.selectedInventoryItem;
            set
            {
                this.selectedInventoryItem = value;
                this.CanItemHistoryBeClicked = (this.SelectedInventoryItem != null);
                this.OnPropertyChanged();
            }
        }

        /// <summary>
		/// Gets or sets the selected item history
		/// </summary>
		public ObservableCollection<RentalItem> SelectedItemHistory
        {
            get => this.selectedItemHistory;
            set
            {
                this.selectedItemHistory = value;
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
        /// Gets or sets property depending on if update can be clicked
        /// </summary>
        /// <value>
        /// True if a rental has been selected and the rental hasn't been returned otherwise false
        /// </value>
        public bool CanItemHistoryBeClicked
        {
            get => this.canItemHistoryBeClicked;
            set
            {
                this.canItemHistoryBeClicked = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InventoryViewModel"/> class.
        /// </summary>
        /// @procondition none
        /// @postcondition this.inventoryDal is created as a new InventoryDal
        public InventoryViewModel()
        {
            this.inventoryDal = new InventoryDal();
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="InventoryViewModel"/> class.
        /// </summary>
        /// <param name="inventoryDal">The inventory dal.</param>
        /// @postcondition this.inventoryDal is set to the inventory dal that is passed in
        public InventoryViewModel(IInventoryDal inventoryDal)
        {
            this.inventoryDal = inventoryDal;
        }

        /// <summary>
        /// Retrieves all inventory items from the db
        /// </summary>
		/// @precondition none
        /// @postcondition this.Inventory == all inventory items from db
        public void RetrieveAllInventoryItems()
        {
            try
            {
                this.Inventory = new ObservableCollection<InventoryItem>(this.inventoryDal.GetInventoryItems());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Gets a detailed history summary of the selected inventory item
        /// </summary>
        /// @precondition none
		/// @postcondition selected item history == rental items from the db
        public void GetSelectedItemHistorySummary()
        {
            var summaryItems = this.inventoryDal.GetItemHistorySummary(this.SelectedInventoryItem.InventoryId);
            this.SelectedItemHistory = new ObservableCollection<RentalItem>(summaryItems);
        }


        /// <summary>
        /// Adds an inventory item
        /// </summary>
        /// @precondition none
        /// @postcondition the item is added or an error is thrown if something went wrong with connecting to the db
        public void AddInventoryItem()
        {
	        var item = new InventoryItem
	        {
                Title = this.Title,
                Category = this.Category,
                Type = this.Type,
                Condition = this.Condition
	        };

            this.inventoryDal.AddInventoryItem(item);
            this.resetFields();
        }

        private void resetFields()
        {
	        this.Title = null;
	        this.Category = null;
	        this.Type = null;
	        this.Condition = null;
        }
        /// <summary>
        /// Removes the currently selected item from the inventory
        /// </summary>
        /// @precondition none
        /// @postcondition the item is removed or an error is thrown if something went wrong with connecting to the db
        public void RemoveInventoryItem()
        {
            this.inventoryDal.RemoveInventoryItem(this.SelectedInventoryItem.InventoryId);
        }

        private bool canAddBeClicked()
        {
	        return !string.IsNullOrEmpty(this.Title) && !string.IsNullOrEmpty(this.Category) &&
	               !string.IsNullOrEmpty(this.Type) && !string.IsNullOrEmpty(this.Condition);
        }
    }
}
