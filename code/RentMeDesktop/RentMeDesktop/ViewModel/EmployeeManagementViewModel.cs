using RentMeDesktop.DAL;
using RentMeDesktop.Model;
using RentMeDesktop.Utility;
using RentMeDesktop.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentMeDesktop.ViewModel
{


	/// <summary>
	/// The view model for the employee managment page
	/// </summary>
	public class EmployeeManagementViewModel : BaseViewModel
	{

		private string fName { get; set; }

		private string lName { get; set; }

		private string username { get; set; }

		private string password { get; set; }

		private bool isManager { get; set; }

		private string employeeSearchTerm { get; set; }

		private ObservableCollection<Employee> employees;

		private Employee selectedEmployee;

		/// <summary>
		/// Gets or sets the employee search term
		/// </summary>
		public string EmployeeSearchTerm { 
			get => this.employeeSearchTerm; 
			set
			{
				this.employeeSearchTerm = value;
				try
				{
					
					if (string.IsNullOrEmpty(this.EmployeeSearchTerm))
					{
						this.Employees = new ObservableCollection<Employee>(EmployeeDAL.GetEmployees(this.CurrentEmployee));
					}
					else
					{
						this.Employees = new ObservableCollection<Employee>(EmployeeDAL.SearchEmployees(this.CurrentEmployee, this.EmployeeSearchTerm));
					}
				} catch (Exception ex)
				{
					DBError.showErrorWindow();
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
				this.OnPropertyChanged();
				this.AddCommand.OnCanExecuteChanged();
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
				this.OnPropertyChanged();
				this.AddCommand.OnCanExecuteChanged();
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
				this.OnPropertyChanged();
				this.AddCommand.OnCanExecuteChanged();
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
				this.OnPropertyChanged();
				this.AddCommand.OnCanExecuteChanged();
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
		/// Creates a new employee management view model
		/// </summary>
		public EmployeeManagementViewModel()
		{
			this.RemoveCommand = new RelayCommand(removeEmployee, canRemoveEmployee);
			this.AddCommand = new RelayCommand(addEmployee, canAddEmployee);
			
		}


		private bool canRemoveEmployee(object obj)
		{
			return this.SelectedEmployee != null;
		}

		private void removeEmployee(object obj)
		{
			try
			{
				EmployeeDAL.RemoveEmployee(this.SelectedEmployee);
				this.Employees = new ObservableCollection<Employee>(EmployeeDAL.GetEmployees(this.CurrentEmployee));
			} catch(Exception ex)
			{
				DBError.showErrorWindow();
			}
		}

		private bool canAddEmployee(object obj)
		{
			return !String.IsNullOrEmpty(this.FName) && !String.IsNullOrEmpty(this.LName) && !String.IsNullOrEmpty(this.Username) && !String.IsNullOrEmpty(this.Password);
		}

		private void addEmployee(object obj)
		{
			var employeeToAdd = new Employee(this.FName, this.LName, this.Username, this.IsManager);
			EmployeeDAL.AddEmployee(employeeToAdd, this.Password);
			this.resetFields();
		}

		private void resetFields()
		{
			this.FName = null;
			this.LName = null;
			this.Username = null;
			this.Password = null;
			this.IsManager = false;
		}
		/// <summary>
		/// The relay command for removing an employee
		/// </summary>
		public RelayCommand RemoveCommand { get; set; }

		/// <summary>
		/// The relay command for adding an employee
		/// </summary>
		public RelayCommand AddCommand { get; set; }

		/// <summary>
		/// Gets or sets the selected employee
		/// </summary>
		public Employee SelectedEmployee { 
			get => this.selectedEmployee; 
			set
			{
				this.selectedEmployee = value;
				this.OnPropertyChanged();
				this.RemoveCommand.OnCanExecuteChanged();
			} 
		}

		/// <summary>
		/// Gets or sets the employees
		/// </summary>
		public ObservableCollection<Employee> Employees { 
			get => this.employees;
			set
			{
				this.employees = value;
				this.OnPropertyChanged();
			} 
		}

		/// <summary>
		/// Sets the employees from the db
		/// </summary>
		/// <param name="currentEmployee">the current employee logged in</param>
		public void RetrieveEmployees(Employee currentEmployee)
		{
			this.Employees = new ObservableCollection<Employee>(EmployeeDAL.GetEmployees(currentEmployee));
		}
	}
}
