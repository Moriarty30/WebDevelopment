using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeesWeb.Services.Entities
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime HireDate { get; set; }
        public string? Department { get; set; }
        
        private EmployeeDto()
        {

        }

        public static EmployeeDto Build(int id, string firstName, string lastName, DateTime hireDate, string department)
        {
            return new EmployeeDto
            {
                Id = id,
                FirstName = firstName,
                LastName = lastName,
                HireDate = hireDate,
                Department = department
            };
        }
    }
}