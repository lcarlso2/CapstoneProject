using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace SharedCode.Model
{
    /// <summary>
	/// The base employee class
	/// </summary>
	public class BaseEmployee
	{

        /// <summary>
        /// Gets or sets the username
        /// </summary>
        /// <value>
        /// The username
        /// </value>
        [Required]
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the password
        /// </summary>
        /// /// <value>
        /// The password
        /// </value>
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
