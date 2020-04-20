using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using RentMeDesktop.Utility;
using RentMeDesktop.View;
using SharedCode.DAL;
using SharedCode.Model;

namespace RentMeDesktop.ViewModel
{
	/// <summary>
	/// The view model for the employee management page
	///      Inherits from BaseViewModel
	/// </summary>
	public class EmployeeManagementViewModel : BaseViewModel
	{

		private string fName;

		private string lName;

		private string username;

		private string password;

		private string confirmedPassword;

		private bool isManager;

		private string employeeSearchTerm;

		private ObservableCollection<Employee> employees;

		private Employee selectedEmployee;

		private IEmployeeDal employeeDal;

		private ObservableCollection<RentalItem> selectedEmployeeHistory;

		private bool canEmployeeHistoryBeClicked;

		private bool canAddBeClicked;


		public bool CanAddBeClicked
		{
			get => this.canAddBeClicked;
			set
			{
				this.canAddBeClicked = value;
				this.OnPropertyChanged();

			}
		}

		/// <summary>
		/// Gets or sets the employee search term
		/// </summary>
		public string EmployeeSearchTerm
		{
			get => this.employeeSearchTerm;
			set
			{
				this.employeeSearchTerm = value;
				try
				{

					if (string.IsNullOrEmpty(this.EmployeeSearchTerm))
					{
						this.Employees = new ObservableCollection<Employee>(this.employeeDal.GetEmployees(this.CurrentEmployee));
					}
					else
					{
						this.Employees = new ObservableCollection<Employee>(this.employeeDal.GetEmployees(this.CurrentEmployee));
					}
				}
				catch (Exception)
				{
					DbError.showErrorWindow();
				}

				this.OnPropertyChanged();
			}
		}

		/// <summary>
		/// Gets or sets the First name 
		/// </summary>
		public string FName
		{
			get => this.fName;
			set
			{
				this.fName = value;
				this.CanAddBeClicked = !string.IsNullOrEmpty(this.FName) && !string.IsNullOrEmpty(this.LName) && !string.IsNullOrEmpty(this.Username) && !string.IsNullOrEmpty(this.Password) && (this.Password == this.ConfirmedPassword);
				this.OnPropertyChanged();
			}
		}

		/// <summary>
		/// Gets or sets the last name
		/// </summary>
		public string LName
		{
			get => this.lName;
			set
			{
				this.lName = value;
				this.CanAddBeClicked = !string.IsNullOrEmpty(this.FName) && !string.IsNullOrEmpty(this.LName) && !string.IsNullOrEmpty(this.Username) && !string.IsNullOrEmpty(this.Password) && (this.Password == this.ConfirmedPassword);
				this.OnPropertyChanged();
			}
		}

		/// <summary>
		/// Gets or sets the username
		/// </summary>
		public string Username
		{
			get => this.username;
			set
			{
				this.username = value;
				this.CanAddBeClicked = !string.IsNullOrEmpty(this.FName) && !string.IsNullOrEmpty(this.LName) && !string.IsNullOrEmpty(this.Username) && !string.IsNullOrEmpty(this.Password) && (this.Password == this.ConfirmedPassword);
				this.OnPropertyChanged();
			}
		}

		/// <summary>
		/// Gets or sets the password
		/// </summary>
		public string Password
		{
			get => this.password;
			set
			{
				this.password = value;
				this.CanAddBeClicked = !string.IsNullOrEmpty(this.FName) && !string.IsNullOrEmpty(this.LName) && !string.IsNullOrEmpty(this.Username) && !string.IsNullOrEmpty(this.Password) && (this.Password == this.ConfirmedPassword);
				this.OnPropertyChanged();
			}
		}

        /// <summary>
        /// Gets or sets the confirmed password.
        /// </summary>
        /// <value>
        /// The confirmed password.
        /// </value>
        public string ConfirmedPassword
		{
			get => this.confirmedPassword;
			set
			{
				this.confirmedPassword = value;
				this.CanAddBeClicked = !string.IsNullOrEmpty(this.FName) && !string.IsNullOrEmpty(this.LName) && !string.IsNullOrEmpty(this.Username) && !string.IsNullOrEmpty(this.Password) && (this.Password == this.ConfirmedPassword);
				this.OnPropertyChanged();
			}
		}
		/// <summary>
		/// Gets or sets the isManager 
		/// </summary>
		public bool IsManager
		{
			get => this.isManager;
			set
			{
				this.isManager = value;
				this.OnPropertyChanged();
			}
		}

		/// <summary>
		/// Gets or sets the selected employee history
		/// </summary>
		public ObservableCollection<RentalItem> SelectedEmployeeHistory {
			get => this.selectedEmployeeHistory;
			set
			{
				this.selectedEmployeeHistory = value;
				this.OnPropertyChanged();
			}
		}

		/// <summary>
		/// Gets or sets the view employee history button can be clicked
		/// </summary>
		/// @precondition none
		/// @postcondition the canAddItemBeClicked is set
		public bool CanEmployeeHistoryBeClicked
		{
			get => this.canEmployeeHistoryBeClicked;
			set
			{
				this.canEmployeeHistoryBeClicked = value;
				this.OnPropertyChanged();
			}
		}

		/// <summary>
		/// The relay command for removing an employee
		/// </summary>
		public RelayCommand RemoveCommand { get; set; }


		/// <summary>
		/// Gets or sets the selected employee
		/// </summary>
		public Employee SelectedEmployee
		{
			get => this.selectedEmployee;
			set
			{
				this.selectedEmployee = value;
				this.CanEmployeeHistoryBeClicked = (this.SelectedEmployee != null);
				this.OnPropertyChanged();
				this.RemoveCommand.OnCanExecuteChanged();
			}
		}

		/// <summary>
		/// Gets or sets the employees
		/// </summary>
		public ObservableCollection<Employee> Employees
		{
			get => this.employees;
			set
			{
				this.employees = value;
				this.OnPropertyChanged();
			}
		}


		/// <summary>
		/// Creates a new employee management view model
		/// </summary>
		/// @precondition none
		/// @postcondition Created EmployeeManagmentViewModel with new RelayCommands and EmployeeDal
		public EmployeeManagementViewModel()
		{
			this.RemoveCommand = new RelayCommand(removeEmployee, canRemoveEmployee);
			this.employeeDal = new EmployeeDal();

		}
		/// <summary>
		/// Creates a new employee management view model
		/// <param name="employeeDal">the dal to use for the class. For testing purposes</param>
		/// </summary>
		/// @precondition none
		/// @postcondition Created EmployeeManagmentViewModel with new RelayCommands and EmployeeDal equal to employeeDal
		public EmployeeManagementViewModel(IEmployeeDal employeeDal)
		{
			this.RemoveCommand = new RelayCommand(removeEmployee, canRemoveEmployee);
			this.employeeDal = employeeDal;

		}

		/// <summary>
		/// Sets all rental items associated with selected employee from the db
		/// </summary>
		/// @precondition none
		/// @postcondition selectedEmployeeHistory == all items associated with selected employee from the db
		public void RetrieveEmployeeHistory()
		{
			var historyItems = this.employeeDal.GetEmployeeHistory(this.SelectedEmployee.EmployeeId);
			this.SelectedEmployeeHistory = new ObservableCollection<RentalItem>(historyItems);
		}


		/// <summary>
		/// Sets the employees from the db
		/// </summary>
		/// <param name="currentEmployee">the current employee logged in</param>
		/// @precondition none
		/// @postcondition this.Employees equal to the employee list from the database
		public void RetrieveEmployees(Employee currentEmployee)
		{
			this.Employees = new ObservableCollection<Employee>(this.employeeDal.GetEmployees(currentEmployee));
		}

		/// <summary>
		/// Adds an employee to the database 
		/// </summary>
		public void AddEmployee()
		{
			var employeeToAdd = new Employee(this.FName, this.LName, this.Username, this.IsManager);
			this.employeeDal.AddEmployee(employeeToAdd, this.Password);
			this.resetFields();
		}


		private bool canRemoveEmployee(object obj)
		{
			return this.SelectedEmployee != null;
		}

		private async void removeEmployee(object obj)
		{
			ContentDialog dialog = new ContentDialog();
			dialog.Title = "Confirm";
			dialog.Content = "Are you sure you want to remove this employee?";
			dialog.CloseButtonText = "Cancel";
			dialog.PrimaryButtonText = "Confirm";
			var result = await dialog.ShowAsync();
			if (result == ContentDialogResult.Primary)
			{
				try
				{
					this.employeeDal.RemoveEmployee(this.SelectedEmployee.Username);
					this.Employees = new ObservableCollection<Employee>(this.employeeDal.GetEmployees(this.CurrentEmployee));
				}
				catch (Exception)
				{
					DbError.showErrorWindow();
				}
			}

		}


		private void resetFields()
		{
			this.FName = null;
			this.LName = null;
			this.Username = null;
			this.Password = null;
			this.ConfirmedPassword = null;
			this.IsManager = false;
		}
	}
}
