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
    public sealed partial class AddEmployeePage : Page
    {
        /// <summary>
        /// The view Model
        /// </summary>
        public EmployeeManagementViewModel ViewModel;
        public AddEmployeePage()
        {
            this.InitializeComponent();
            this.ViewModel = new EmployeeManagementViewModel();
            this.DataContext = this.ViewModel;
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(EmployeeManagementPage), this.ViewModel);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var oldViewModel = (BaseViewModel)e.Parameter;
            this.ViewModel.CurrentEmployee = oldViewModel?.CurrentEmployee;
        }
    }
}
