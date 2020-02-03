using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace SharedCode.Model
{
	/// <summary>
	/// The employee class
	/// </summary>
	public class Employee : BaseEmployee
	{
        
        /// <summary>
        /// Gets or sets the employee id
        /// </summary>
        /// <value>
        /// The employee ID
        /// </value>
        public int EmployeeId { get; set; }

        /// <summary>
        /// Gets or sets the confirm password
        /// </summary>
        /// <value>
        /// The confirm password
        /// </value>
        [Required]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }


        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        [Required]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>
        /// The last name.
        /// </value>
        [Required]
        public string LastName { get; set; }


        /// <summary>
        /// Gets or sets the whether the employee is a manager
        /// </summary>
        /// <value>
        /// True if employee is manager, false if not
        /// </value>
        [Display(Name = "Is Manager")]
        public bool IsManager { get; set; }

        /// <summary>
        /// Gets the employee info string
        /// </summary>
        public string EmployeeInfo => $"Name: {this.FirstName} {this.LastName} \nUsername: {this.Username} \nIs Manager: {this.IsManager} \n";

        /// <summary>
        /// The default constructor for the employee
        /// </summary>
        public Employee()
        {
            this.FirstName = "";
            this.LastName = "";
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Employee"/> class.
        /// </summary>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastName">The last name.</param>
        public Employee(string firstName, string lastName)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.IsManager = false;
        }

        /// <summary>
        /// Initializes a new instance of the employee
        /// </summary>
        /// <param name="firstName"> the first name </param>
        /// <param name="lastName"> the last name</param>
        /// <param name="username"> the username</param>
        /// <param name="isManager"> true or false if the employee is a manager or not</param>
        public Employee(string firstName, string lastName, string username, bool isManager)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Username = username;
            this.IsManager = isManager;
        }
    }
}
