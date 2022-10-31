//using EmployeesWeb.Application.Mappers;
using Microsoft.AspNetCore.Mvc;
using EmployeesWeb.Application.Models;
using EmployeesWeb.Application.Config;
using EmployeesWeb.Services;
using Microsoft.Extensions.Options;
using EmployeesWeb.Services.Entities;

namespace EmployeesWeb.Application.Controllers
{
    public class EmployeesController : Controller
    {
        private static List<Employee>? _employeeList;
        private readonly ApiConfiguration _apiConfiguration;
        private readonly EmployeesService employeesService;
        public EmployeesController(IOptions<ApiConfiguration> apiConfiguration)
        {
            _apiConfiguration = apiConfiguration.Value;
#pragma warning disable CS8604 // Posible argumento de referencia nulo
            employeesService = new EmployeesService(_apiConfiguration.ApiEmployeesUrl);
#pragma warning restore CS8604 // Posible argumento de referencia nulo
        }

        // GET: EmployeesController
        public async Task<ActionResult> Index()
        {
            ViewData["IsUserLogged"] = HttpContext.Session.GetString("IsUserLogged");;

            IList<EmployeeDto> employees = await employeesService.GetEmployees();

            _employeeList = employees.Select(employeeDto => MapperToEmployee(employeeDto)).ToList();

            return View(_employeeList);
        }

        // GET: EmployeesController/Details
        public async Task<ActionResult> Details(int id)
        {
            ViewData["IsUserLogged"] = HttpContext.Session.GetString("IsUserLogged");

            EmployeeDto employeeDto = await employeesService.GetEmployeeById(id);

            var employee = MapperToEmployee(employeeDto);

            return View(employee);
        }

        // GET: EmployeesController/Create
        public ActionResult Create()
        {
            ViewData["IsUserLogged"] = HttpContext.Session.GetString("IsUserLogged");

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

        // GET: EmployeesController/Edit
        public async Task<ActionResult> Edit(int id)
        {
            var employeeFound = await employeesService.GetEmployeeById(id);

            if (employeeFound == null)
            {
                return NotFound();
            }

            ViewData["IsUserLogged"] = HttpContext.Session.GetString("IsUserLogged");

            var employee = MapperToEmployee(employeeFound);

            return View(employee);
        }

        // POST: EmployeesController/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Employee employee)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var employeeModified = await employeesService.UpdateEmployee(MapperToEmployeeDto(employee));

                    return RedirectToAction(nameof(Index));
                }
                return View(employee);
            }
            catch
            {
                return View();
            }
        }

        // GET: EmployeesController/Delete
        public async Task<ActionResult> Delete(int id)
        {
            var employeeFound = await employeesService.GetEmployeeById(id);

            if (employeeFound == null)
            {
                return NotFound();
            }

            ViewData["IsUserLogged"] = HttpContext.Session.GetString("IsUserLogged");

            var employee = MapperToEmployee(employeeFound);

            return View(employee);
        }

        // POST: EmployeesController/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Employee employee)
        {
            try
            {
                var employeeFound = await employeesService.GetEmployeeById(employee.Id);

                if (employeeFound == null)
                {
                    return View();
                }

                var employeeDeleted = await employeesService.DeleteEmployee(employee.Id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        private static Employee MapperToEmployee(EmployeeDto employeeDto)
        {
            return new Employee
            {
                Id = employeeDto.Id,
                FirstName = employeeDto.FirstName,
                LastName = employeeDto.LastName,
                HireDate = employeeDto.HireDate,
                Department = employeeDto.Department
            };
        }

        private static EmployeeDto MapperToEmployeeDto(Employee employee)
        {
#pragma warning disable CS8604 // Posible argumento de referencia nulo
            return EmployeeDto.Build(
              id: employee.Id,
              firstName: employee.FirstName,
              lastName: employee.LastName,
              hireDate: employee.HireDate,
              department: employee.Department
            );
#pragma warning restore CS8604 // Posible argumento de referencia nulo
        }

    }
}
