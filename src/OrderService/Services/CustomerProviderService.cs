namespace OrderService.Services
{
    public interface ICustomerProviderService
    {
        public Task<bool> ValidateCustomer(string id);
    }

    public class CustomerProviderService : ICustomerProviderService
    {
        private readonly HttpClient httpClient;
        private readonly ILogger<CustomerProviderService> logger;

        public CustomerProviderService(HttpClient _httpClient, ILogger<CustomerProviderService> _logger)
        {
            httpClient = _httpClient;
            logger = _logger;
        }

        public async Task<bool> ValidateCustomer(string id)
        {
            try
            {
                var response = httpClient.GetAsync($"\\Customer\\Validate\\{id}");

                string result = await response.Result.Content.ReadAsStringAsync();

                return result == "true";
            }
            catch (Exception ex)
            {
                logger.LogError($"Error while validating customer. Error Message: {ex.Message}");
                throw;
            }
        }
    }
}
