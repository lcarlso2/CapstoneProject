using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace RentMe.Models
{
	/// <summary>
	/// The class responsible for keeping track of the registering customer
	/// </summary>
	public class RegisteringMember : Member
	{
		/// <summary>
		/// Gets or sets the members id
		/// </summary>
		/// <value>
		///The customer id
		/// </value>
		public int MemberId { get; set; }

		/// <summary>
		/// Gets or sets the first name
		/// </summary>
		/// @precondition none
		/// @postcondition the first name is set
		[Required]
		[Display(Name = "First Name")]
		public string First { get; set; }


		/// <summary>
		/// Gets or sets the last name
		/// </summary>
		/// @precondition none
		/// @postcondition the last name is set
		[Required]
		[Display(Name = "Last Name")]
		public string Last { get; set; }


		/// <summary>
		/// Gets or sets the confirm email
		/// </summary>
		/// @precondition none
		/// @postcondition the confirm email is set
		[Required]
		[Display(Name = "Confirm Email")]
		[DataType(DataType.EmailAddress)]
		[Compare("Email")]
		public string ConfirmEmail { get; set; }

		/// <summary>
		/// Gets or sets the confirm password
		/// </summary>
		/// @precondition none
		/// @postcondition the confirm password is set
		[Required]
		[Display(Name = "Confirm Password")]
		[DataType(DataType.Password)]
		[Compare("Password")]
		public string ConfirmPassword { get; set; }


		/// <summary>
		/// Gets or sets the Address
		/// </summary>
		/// @precondition none
		/// @postcondition the address is set
		public Address Address { get; set; }

        /// <summary>
        /// Gets or sets the value for if a member is blacklisted
        /// </summary>
        /// @precondition none
        /// @postcondition the value is set for being blacklisted
        public int IsBlacklisted { get; set; }
	}
}
