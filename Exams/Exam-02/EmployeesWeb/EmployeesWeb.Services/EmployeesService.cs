using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using EmployeesWeb.Services.Entities;
using Newtonsoft.Json.Converters;

namespace EmployeesWeb.Services
{
    public class EmployeesService
    {
        private readonly HttpClient _httpClient;
       
        public EmployeesService(string baseUrl)
        {
            _httpClient = new HttpClient();

            SetupHttpConnection(_httpClient, baseUrl);
        }
        private static void SetupHttpConnection(HttpClient httpClient, string baseUrl)
        {
            //Passing service base url  
            httpClient.BaseAddress = new Uri(baseUrl);

            httpClient.DefaultRequestHeaders.Clear();

            //Define request data format  
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        public async Task<List<EmployeeDto>> GetEmployees()
        {
            var employeesList = new List<EmployeeDto>();

            // Sending request to find web api REST service resource to Get All Users using HttpClient
            HttpResponseMessage response = await _httpClient.GetAsync("/Employees");

            // Checking the response is successful or not which is sent using HttpClient
            if (response.IsSuccessStatusCode)
            {
                // Storing the content response recieved from web api
                var responseContent = response.Content.ReadAsStringAsync().Result;

                //Deserializing the response recieved from web api and storing into the Employee list
                employeesList = JsonConvert.DeserializeObject<List<EmployeeDto>>(responseContent, new IsoDateTimeConverter { DateTimeFormat = "dd/MM/yyyy" });
            }

#pragma warning disable CS8603 // Posible tipo de valor devuelto de referencia nulo
            return employeesList;
#pragma warning restore CS8603 // Posible tipo de valor devuelto de referencia nulo
        }

        public async Task<EmployeeDto> GetEmployeeById(int id)
        {
            EmployeeDto? employee = null;

            // Sending request to find web api REST service resource to Get All Users using HttpClient
            HttpResponseMessage response = await _httpClient.GetAsync($"/Employees/{id}");

            // Checking the response is successful or not which is sent using HttpClient
            if (response.IsSuccessStatusCode)
            {
                // Storing the content response recieved from web api
                var responseContent = response.Content.ReadAsStringAsync().Result;

                //Deserializing the response recieved from web api and storing into the Employee list
                employee = JsonConvert.DeserializeObject<EmployeeDto>(responseContent, new IsoDateTimeConverter { DateTimeFormat = "dd/MM/yyyy" });
            }

#pragma warning disable CS8603 // Posible tipo de valor devuelto de referencia nulo
            return employee;
#pragma warning restore CS8603 // Posible tipo de valor devuelto de referencia nulo
        }

        public async Task<EmployeeDto> AddEmployee(EmployeeDto employee)
        {
            EmployeeDto? employeeDtoResponse = null;

            StringContent content = new(JsonConvert.SerializeObject(employee), Encoding.UTF8, "application/json");

            // Sending request to find web api REST service resource to Add an User using HttpClient
            HttpResponseMessage response = await _httpClient.PostAsync($"/Employees", content);

            // Checking the response is successful or not which is sent using HttpClient
            if (response.IsSuccessStatusCode)
            {
                // Storing the content response recieved from web api
                var responseContent = response.Content.ReadAsStringAsync().Result;

                //Deserializing the response recieved from web api and storing into the Employee list
                employeeDtoResponse = JsonConvert.DeserializeObject<EmployeeDto>(responseContent, new IsoDateTimeConverter { DateTimeFormat = "dd/MM/yyyy" });
            }

#pragma warning disable CS8603 // Posible tipo de valor devuelto de referencia nulo
            return employeeDtoResponse;
#pragma warning restore CS8603 // Posible tipo de valor devuelto de referencia nulo
        }

        public async Task<EmployeeDto> UpdateEmployee(EmployeeDto employee)
        {
            EmployeeDto? employeeDtoResponse = null;

            StringContent content = new(JsonConvert.SerializeObject(employee), Encoding.UTF8, "application/json");

            // Sending request to find web api REST service resource to Add an User using HttpClient
            HttpResponseMessage response = await _httpClient.PutAsync($"/Employees/{employee.Id}", content);

            // Checking the response is successful or not which is sent using HttpClient
            if (response.IsSuccessStatusCode)
            {
                // Storing the content response recieved from web api
                var responseContent = response.Content.ReadAsStringAsync().Result;

                //Deserializing the response recieved from web api
                employeeDtoResponse = JsonConvert.DeserializeObject<EmployeeDto>(responseContent, new IsoDateTimeConverter { DateTimeFormat = "dd/MM/yyyy" });
            }

#pragma warning disable CS8603 // Posible tipo de valor devuelto de referencia nulo
            return employeeDtoResponse;
#pragma warning restore CS8603 // Posible tipo de valor devuelto de referencia nulo
        }

        public async Task<EmployeeDto> DeleteEmployee(int id)
        {
            EmployeeDto? employeeDtoResponse = null;

            // Sending request to find web api REST service resource to Delete the User using HttpClient
            HttpResponseMessage response = await _httpClient.DeleteAsync($"/Employees/{id}");

            // Checking the response is successful or not which is sent using HttpClient
            if (response.IsSuccessStatusCode)
            {
                // Storing the content response recieved from web api
                var responseContent = response.Content.ReadAsStringAsync().Result;

                // Deserializing the response recieved from web api
                employeeDtoResponse = JsonConvert.DeserializeObject<EmployeeDto>(responseContent, new IsoDateTimeConverter { DateTimeFormat = "dd/MM/yyyy" });
            }

#pragma warning disable CS8603 // Posible tipo de valor devuelto de referencia nulo
            return employeeDtoResponse;
#pragma warning restore CS8603 // Posible tipo de valor devuelto de referencia nulo
        }
    }
}