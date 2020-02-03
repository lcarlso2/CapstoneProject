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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace RentMeDesktop.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        /// <summary>
        /// The view Model
        /// </summary>
        public MainPageViewModel ViewModel;

        public MainPage()
        {
            this.InitializeComponent();
            this.ViewModel = new MainPageViewModel();
            this.DataContext = this.ViewModel;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var oldViewModel = (BaseViewModel)e.Parameter;
            this.ViewModel.CurrentEmployee = oldViewModel?.CurrentEmployee;

            try
            {
                this.ViewModel.GetRentalItems();
            }
            catch (Exception ex)
            {
                DbError.showErrorWindow();
            }
        }

        private void manageEmployeesButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(EmployeeManagementPage), this.ViewModel);
        }

        private void updateRentalStatusButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(UpdateRentalPage), this.ViewModel);
        }

        private void logoutButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Login));
        }
    }
}
