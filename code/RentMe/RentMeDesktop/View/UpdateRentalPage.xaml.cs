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
using RentMeDesktop.ViewModel;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace RentMeDesktop.View
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class UpdateRentalPage : Page
	{
		/// <summary>
		/// The view model
		/// </summary>
		public MainPageViewModel ViewModel;

		/// <summary>
		/// The status type
		/// </summary>
		public ObservableCollection<string> StatusTypes = new ObservableCollection<string>();
		public UpdateRentalPage()
		{
			this.InitializeComponent();
			this.StatusTypes.Add("Ordered");
			this.StatusTypes.Add("Shipped");
			this.StatusTypes.Add("Delivered");
			this.StatusTypes.Add("Returned");
			this.StatusTypes.Add("Late");

			this.ViewModel = new MainPageViewModel();
			this.DataContext = this.ViewModel;
		}

		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			var oldViewModel = (MainPageViewModel)e.Parameter;
			this.ViewModel.CurrentEmployee = oldViewModel?.CurrentEmployee;
			this.ViewModel.SelectedRental = oldViewModel?.SelectedRental;
		}

		private void cancelButton_Click(object sender, RoutedEventArgs e)
		{
			Frame.Navigate(typeof(MainPage), this.ViewModel);
		}

		private void updateButton_Click(object sender, RoutedEventArgs e)
		{
			this.ViewModel.UpdateRentalItem();
			Frame.Navigate(typeof(MainPage), this.ViewModel);
		}
	}
}
