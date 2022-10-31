namespace EmployeesWeb.Services.Entities
{
    public class LoginDto
    {
        public string? Email { get; set; }
        public string? Password { get; set; }

        private LoginDto()
        {
        }

        public static LoginDto Build(string email, string password)
        {
            return new LoginDto
            {
                Email = email,
                Password = password
            };
        }
    }
}