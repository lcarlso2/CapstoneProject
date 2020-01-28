﻿using RentMeDesktop.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace RentMeDesktop.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class UpdateRentalPage : Page
    {
        public MainPageViewModel ViewModel;
        public ObservableCollection<string> statusTypes = new ObservableCollection<string>();
        /// <summary>
        /// Creates an upate rental page
        /// </summary>
        public UpdateRentalPage()
        {
            this.InitializeComponent();
            this.statusTypes.Add("Pending");
            this.statusTypes.Add("Shipped");
            this.statusTypes.Add("Delivered");
            this.statusTypes.Add("Returned");
            this.statusTypes.Add("Late");

            this.ViewModel = new MainPageViewModel();
            this.DataContext = this.ViewModel;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var oldViewModel = (MainPageViewModel)e.Parameter;
            this.ViewModel.SelectedRental = oldViewModel.SelectedRental;
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            this.ViewModel.UpdateRentalItem();
            Frame.Navigate(typeof(MainPage));
        }
    }
}
