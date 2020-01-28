using System.ComponentModel.DataAnnotations;

namespace RentMeEmployee.Models
{
    public class Employee
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
