using CustomerService.Data;
using CustomerService.Entities;
using CustomerService.Models;

namespace CustomerService.Services
{
    public interface ICustomerService
    {
        public Task<string> CreateCustomer(CustomerModel customer);
        public bool UpdateCustomer(CustomerModel customer);
        public bool DeleteCustomer(string id);
        public List<Customer> GetCustomers();
        public Customer GetCustomer(string id);
    }

    public class CustomerService : ICustomerService
    {
        private readonly DataContext _context;
        private readonly ILogger<CustomerService> logger;

        public CustomerService(DataContext context, ILogger<CustomerService> _logger)
        {
            _context = context;
            logger = _logger;
        }

        /// <summary>
        /// Müşteri yaratan fonksiyon
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        #region CreateCustomer
        public async Task<string> CreateCustomer(CustomerModel _customer)
        {
            try
            {
                Customer customer = new Customer()
                {
                    AddressId = _customer.AddressId,
                    CreatedAt = DateTime.UtcNow,
                    Email = _customer.Email,
                    Id = Guid.NewGuid().ToString(),
                    Name = _customer.Name,
                    UpdatedAt = DateTime.UtcNow,
                };
                await _context.Customers.AddAsync(customer);
                await _context.SaveChangesAsync();
                return customer.Id;
            }
            catch (Exception ex)
            {
                logger.LogError($"Error while creating customer. Error Message: {ex.Message}");
                throw;
            }
        }
        #endregion

        /// <summary>
        /// Müşteri düzenleyen fonksiyon
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        #region UpdateCustomer
        public bool UpdateCustomer(CustomerModel _customer)
        {
            try
            {
                var currentCustomer = _context.Customers.Where(c => c.Id == _customer.Id)
                                                         .FirstOrDefault();

                if (currentCustomer == null)
                {
                    throw new Exception("Customer_Not_Found");
                }

                currentCustomer.UpdatedAt = DateTime.UtcNow;
                currentCustomer.AddressId = _customer.AddressId;
                currentCustomer.Email = _customer.Email;
                currentCustomer.Name = _customer.Name;

                _context.Update(currentCustomer);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                logger.LogError($"Error while updating customer with id of {_customer.Id}. Error Message: {ex.Message}");
                throw;
            }
        }
        #endregion

        /// <summary>
        /// Müşteri silen fonksiyon
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        #region DeleteCustomer
        public bool DeleteCustomer(string id)
        {
            try
            {
                var currentCustomer = _context.Customers.Where(c => c.Id == id)
                                                         .FirstOrDefault();

                if (currentCustomer == null)
                {
                    throw new Exception("Customer_Not_Found");
                }

                _context.Customers.Remove(currentCustomer);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                logger.LogError($"Error while deleting customer with id of {id}. Error Message: {ex.Message}");
                throw;
            }
        }
        #endregion

        /// <summary>
        /// Tüm müşterileri listeleyen fonksiyon
        /// </summary>
        /// <returns></returns>
        #region GetCustomers
        public List<Customer> GetCustomers()
        {
            try
            {
                return _context.Customers.ToList();
            }
            catch (Exception ex)
            {
                logger.LogError($"Error while getting customers. Error Message: {ex.Message}");
                throw;
            }
        }
        #endregion

        /// <summary>
        /// Id'si verilen müşteriyi veren fonksiyon
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        #region GetCustomer
        public Customer GetCustomer(string id)
        {
            try
            {
                var customer = _context.Customers.Where(c => c.Id == id).FirstOrDefault();

                if (customer == null)
                {
                    throw new Exception("Customer_Not_Found");
                }

                return customer;
            }
            catch (Exception ex)
            {
                logger.LogError($"Error while getting customer with id of {id}. Error Message: {ex.Message}");
                throw;
            }
        }
        #endregion
    }
}
