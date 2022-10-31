using System.ComponentModel.DataAnnotations;

namespace EmployeesWeb.Application.Models
{

    public enum DepartmentType
    {
        IT,
        Audit,
        Finance
    }
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "El firstName es obligatorio")]
        public string? FirstName { get; set; }
        [Required(ErrorMessage = "El lastName es obligatorio")]
        public string? LastName { get; set; }
        [Required(ErrorMessage = "El hireDate es obligatorio")]
        public DateTime HireDate { get; set; }
        [Required(ErrorMessage = "El department es obligatorio")]
        [RegularExpression("IT|Finance|Audit", ErrorMessage = "Invalid Department")]
        public string? Department { get; set; }
    }
}