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

        private async void detailsButton_Click(object sender, RoutedEventArgs e)
        {
            var content = this.ViewModel.GetSelectedItemDetailSummary();
            ContentDialog dialog = new ContentDialog();
            dialog.Title = "Details";
            if (String.IsNullOrEmpty(content))
            {
                dialog.Content = "It seems there is no information here";
            } else
            {
                dialog.Content = new ScrollViewer()
                {
                    Content = new TextBlock() { Text = content}
                };
            }
            
            dialog.CloseButtonText = "Close";
            var result = await dialog.ShowAsync();
        }
    }
}
