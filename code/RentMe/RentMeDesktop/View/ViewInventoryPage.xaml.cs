﻿using System;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace RentMeDesktop.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ViewInventoryPage : Page
    {
        /// <summary>
        /// The view model
        /// </summary>
        public InventoryViewModel ViewModel;

        /// <summary>
        /// Initializes the data contect and the view model
        /// </summary>
        public ViewInventoryPage()
        {
            this.InitializeComponent();
            this.ViewModel = new InventoryViewModel();
            this.DataContext = this.ViewModel;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var oldViewModel = (BaseViewModel)e.Parameter;
            this.ViewModel.CurrentEmployee = oldViewModel?.CurrentEmployee;
            try
            {
                this.ViewModel.RetrieveAllInventoryItems();
            }
            catch (Exception)
            {
                DbError.showErrorWindow();
            }
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage), this.ViewModel);
        }

        private void itemHistoryButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(ItemHistoryPage), this.ViewModel);
        }

        private void addItemButton_Click(object sender, RoutedEventArgs e)
        {
	        Frame.Navigate(typeof(AddInventoryItemPage), this.ViewModel);
        }

        private async void removeItemButton_Click(object sender, RoutedEventArgs e)
        {
	        var dialog = new ContentDialog
	        {
		        Title = "Confirm",
		        Content = $"Are you sure you want to remove this item?",
		        CloseButtonText = "Cancel",
		        PrimaryButtonText = "Confirm"
	        };
	        var result = await dialog.ShowAsync();
	        if (result == ContentDialogResult.Primary)
	        {
		        try
		        {
			        this.ViewModel.RemoveInventoryItem();
			        this.ViewModel.RetrieveAllInventoryItems();
		        }
		        catch (Exception)
		        {
			        DbError.showErrorWindow();
		        }
	        }
        }
    }
}
