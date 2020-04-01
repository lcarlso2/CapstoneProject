using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using RentMeDesktop.ViewModel;
using SharedCode.Model;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace RentMeDesktop.View
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class AddInventoryItemPage : Page
	{
		/// <summary>
		/// The view model
		/// </summary>
		public InventoryViewModel ViewModel;

		public AddInventoryItemPage()
		{
			this.InitializeComponent();
			this.ViewModel = new InventoryViewModel();
			this.DataContext = this.ViewModel;
		}

		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			var oldViewModel = (InventoryViewModel)e.Parameter;
			this.ViewModel.CurrentEmployee = oldViewModel?.CurrentEmployee;
			this.categoryComboBox.ItemsSource = InventoryItem.CategoryOptions;
			this.typeComboBox.ItemsSource = InventoryItem.TypeOptions;
			this.conditionComboBox.ItemsSource = InventoryItem.ConditionOptions;

		}

		private void cancelButton_Click(object sender, RoutedEventArgs e)
		{
			Frame.Navigate(typeof(ViewInventoryPage), this.ViewModel);
		}

		private async void addButton_Click(object sender, RoutedEventArgs e)
		{
			var dialog = new ContentDialog
			{
				Title = "Confirm",
				Content = $"Are you sure you want to add this item?",
				CloseButtonText = "Cancel",
				PrimaryButtonText = "Confirm"
			};
			var result = await dialog.ShowAsync();
			if (result == ContentDialogResult.Primary)
			{
				try
				{
					this.ViewModel.AddInventoryItem();
					Frame.Navigate(typeof(ViewInventoryPage), this.ViewModel);
				}
				catch (Exception)
				{
					DbError.showErrorWindow();
				}
			}
			
		}
	}
}
