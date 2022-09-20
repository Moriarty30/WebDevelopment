namespace WebDev.Services
{
    public class UsersService
    {
        private string BaseUrl { get; }

        private HttpClient _httpClient;
        public UsersService(string baseUrl)
        {
            BaseUrl = baseUrl;
        }



    }
}