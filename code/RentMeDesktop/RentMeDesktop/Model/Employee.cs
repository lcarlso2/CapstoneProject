using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentMeDesktop.Model
{
   public class Employee
    {

        /// <summary>
        /// Gets or sets the username
        /// </summary>
        /// <value>
        /// The username
        /// </value>
        public string Username { get; set; }

   

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>
        /// The last name.
        /// </value>
        public string LastName { get; set; }


        /// <summary>
        /// Gets or sets the whether the employee is a manager
        /// </summary>
        /// <value>
        /// True if employee is manager, false if not
        /// </value>
        public bool IsManager { get; set; }

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
        public string EmployeeInfo => $"Name: {this.FirstName} {this.LastName} \nUsername: {this.Username} \nIs Manager: {this.IsManager} \n";
    }
}
