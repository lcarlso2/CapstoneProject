using RentMeDesktop.View;
using RentMeDesktop.ViewModel;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace RentMeDesktop
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
            var oldViewModel = (BaseViewModel) e.Parameter;
            this.ViewModel.CurrentEmployee = oldViewModel?.CurrentEmployee;
            
            try
            {
                this.ViewModel.GetBorrowedItems();
            }
            catch (Exception ex)
            {
                DBError.showErrorWindow();
            }
        }

        private void manageEmployeesButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(EmployeeManagementPage));
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
