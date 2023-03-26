using OrderService.Data;

namespace OrderService.Services
{
    public interface IAddressService
    {
        public bool ValidateAddress(string id);
    }
    public class AddressService : IAddressService
    {
        private readonly DataContext _context;
        private readonly ILogger<AddressService> logger;
        public AddressService(DataContext context, ILogger<AddressService> _logger)
        {
            _context = context;
            logger = _logger;
        }

        /// <summary>
        /// Adresin var olup olmadığını kontrol eden fonksiyon
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        #region ValidateCustomer
        public bool ValidateAddress(string id)
        {
            try
            {
                var address = _context.Addresses.Where(c => c.Id == id).FirstOrDefault();

                return address != null;
            }
            catch (Exception ex)
            {
                logger.LogError($"Error while validating address with id of {id}. Error Message: {ex.Message}");
                throw;
            }
        }
        #endregion
    }
}
