using System.ComponentModel.DataAnnotations;

namespace RentMeEmployee.Models
{
    public class Employee
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name ="First name")]
        public string FName { get; set; }

        [Display(Name = "Last name")]
        public string LName { get; set; }

        [Display(Name = "Is Manager")]
        public bool IsManager { get; set; }
    }
}
