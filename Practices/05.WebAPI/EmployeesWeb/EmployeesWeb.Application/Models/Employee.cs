using System.ComponentModel.DataAnnotations;

namespace EmployeesWeb.Application.Models
{
    public class Employee
    {
        [Key]
        public int id { get; set; }
        [Required(ErrorMessage = "El email es obligatorio")]
        public string firstName { get; set; }
        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string lastName { get; set; }
        [Required(ErrorMessage = "El nombre de usuario es obligatorio")]
        public String hireDate { get; set; }
        [Required(ErrorMessage = "El password es obligatorio")]
        public string department { get; set; }
    }
}
