using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
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
	public sealed partial class Login : Page
	{
		/// <summary>
		/// The view model
		/// </summary>
		public BaseViewModel ViewModel;

		public Login()
		{
			this.InitializeComponent();
			this.ViewModel = new BaseViewModel();
			this.DataContext = this.ViewModel;
		}

		private void loginButton_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				if (this.ViewModel.ValidateLoginCredentials(this.usernameTextBox.Text, this.passwordBox.Password))
				{
					Frame.Navigate(typeof(MainPage), this.ViewModel);
				}
				else
				{
					this.usernameTextBox.BorderBrush = new SolidColorBrush(Colors.Red);
					this.passwordBox.BorderBrush = new SolidColorBrush(Colors.Red);
					this.usernameTextBox.PlaceholderText = "Invalid Username";
					this.passwordBox.PlaceholderText = "Invalid Password";
				}
			}
			catch (Exception)
			{
				DbError.showErrorWindow();
			}
		}

		private void usernameTextBox_TextChanged(object sender, TextChangedEventArgs e)
		{
			this.usernameTextBox.BorderBrush = new SolidColorBrush(Colors.Gray);
			this.usernameTextBox.PlaceholderText = "Username";
		}

		private void passwordBox_PasswordChanged(object sender, RoutedEventArgs e)
		{
			this.passwordBox.PlaceholderText = "Password";
			this.passwordBox.BorderBrush = new SolidColorBrush(Colors.Gray);
		}
    }
}
