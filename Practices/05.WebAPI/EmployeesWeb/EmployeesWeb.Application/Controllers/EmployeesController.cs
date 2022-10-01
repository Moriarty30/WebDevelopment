using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using EmployeesWeb.Application.Models;
using System.Linq;
using EmployeesWeb.Application.Config;
using EmployeesWeb.Services;
using Microsoft.Extensions.Options;
using EmployeesWeb.Services.Entities;

namespace EmployeesWeb.Application.Controllers
{
    public class EmployeesController : Controller
    {
        private static List<Employee> _employeeList;
        private static int numEmployees;
        private readonly ApiConfiguration _apiConfiguration;
        private EmployeesService employeesService;
        public EmployeesController(IOptions<ApiConfiguration> apiConfiguration)
        {
            _apiConfiguration = apiConfiguration.Value;
            employeesService = new EmployeesService(_apiConfiguration.ApiEmployeesUrl);
        }

        // GET: EmployeesController
        public async Task<ActionResult> Index()
        {
            IList<EmployeeDto> employees = await employeesService.GetEmployees();

            _employeeList = employees.Select(employeeDto => MapperToEmployee(employeeDto)).ToList();

            return View(_employeeList);
        }

        // GET: EmployeesController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            EmployeeDto employeeDto = await employeesService.GetEmployeeById(id);

            var employee = MapperToEmployee(employeeDto);

            return View(employee);
        }

        // GET: EmployeesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EmployeesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Employee employee)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var employeeAdded = await employeesService.AddEmployee(MapperToEmployeeDto(employee));
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: EmployeesController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var employeeFound = await employeesService.GetEmployeeById(id);

            if (employeeFound == null)
            {
                return NotFound();
            }

            var employee = MapperToEmployee(employeeFound);

            return View(employee);
        }

        // POST: EmployeesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Employee employee)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var employeeModified =  await employeesService.UpdateEmployee(MapperToEmployeeDto(employee));

                    return RedirectToAction(nameof(Index));
                }
                return View(employee);
            }
            catch
            {
                return View();
            }
        }

        // GET: EmployeesController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var employeeFound = await employeesService.GetEmployeeById(id);

            if (employeeFound == null)
            {
                return NotFound();
            }

            var employee = MapperToEmployee(employeeFound);

            return View(employee);
        }

        // POST: EmployeesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Employee employee)
        {
            try
            {
                var employeeFound = await employeesService.GetEmployeeById(employee.id);

                if (employeeFound == null)
                {
                    return View();
                }

                var employeeDeleted = await employeesService.DeleteEmployee(employee.id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        private Employee MapperToEmployee(EmployeeDto employeeDto)
        {
            return new Employee
            {
                id = employeeDto.id,
                firstName= employeeDto.firstName,
                lastName = employeeDto.lastName,
                hireDate = employeeDto.hireDate,
                department = employeeDto.department
            };
        }

        private EmployeeDto MapperToEmployeeDto(Employee employee)
        {
            return EmployeeDto.Build(
              id: employee.id,
              FirstName: employee.firstName,
              LastName: employee.lastName,
              HireDate: employee.hireDate,
              department: employee.department
            );
        }

    }
}
