using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EmployeesWeb.Services.Entities
{
    public class EmployeeDto
    {
        public int id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string hireDate { get; set; }
        public string department { get; set; }
        private EmployeeDto()
        {

        }

        public static EmployeeDto Build(int id, string FirstName, string LastName, string HireDate, string department)
        {
            return new EmployeeDto
            {
                id = id,
                firstName = FirstName,
                lastName = LastName,
                hireDate = HireDate,
                department = department
            };
        }
    }
}
