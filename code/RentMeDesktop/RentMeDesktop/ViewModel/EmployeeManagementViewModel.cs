using RentMeDesktop.DAL;
using RentMeDesktop.Model;
using RentMeDesktop.Utility;
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
		private ObservableCollection<Employee> employees;

		private Employee selectedEmployee;

		/// <summary>
		/// Creates a new employee management view model
		/// </summary>
		public EmployeeManagementViewModel()
		{
			this.RemoveCommand = new RelayCommand(removeEmployee, canRemoveEmployee);
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

			}
		}

		/// <summary>
		/// The relay command for removing an employee
		/// </summary>
		public RelayCommand RemoveCommand { get; set; }

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
