using RentMeDesktop.DAL;
using RentMeDesktop.Model;
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

		public Employee SelectedEmployee { get => this.selectedEmployee; 
			set
			{
				this.selectedEmployee = value;
				this.OnPropertyChanged();
			} 
		}

		public ObservableCollection<Employee> Employees { 
			get => this.employees;
			set
			{
				this.employees = value;
				this.OnPropertyChanged();
			} 
		}

		public void RetrieveEmployees(Employee currentEmployee)
		{
			this.Employees = new ObservableCollection<Employee>(EmployeeDAL.GetEmployees(currentEmployee));
		}
	}
}
