using CustomerService.Data;
using CustomerService.Entities;

namespace CustomerService.Services
{
    public interface ICustomerService
    {
        public Task<string> CreateCustomer(Customer customer);
        public bool UpdateCustomer(Customer customer);
        public bool DeleteCustomer(string id);
        public List<Customer> GetCustomers();
        public Customer GetCustomer(string id);
    }

    public class CustomerService : ICustomerService
    {
        private readonly DataContext _context;

        public CustomerService(DataContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Müşteri yaratan fonksiyon
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        #region CreateCustomer
        public async Task<string> CreateCustomer(Customer customer)
        {
            try
            {
                customer.Id = Guid.NewGuid().ToString();
                customer.CreatedAt = DateTime.Now;
                customer.UpdatedAt = DateTime.Now;
                await _context.Customers.AddAsync(customer);
                await _context.SaveChangesAsync();
                return customer.Id;
            }
            catch (Exception)
            {
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
        public bool UpdateCustomer(Customer customer)
        {
            try
            {
                var currentCustomer = _context.Customers.Where(c => c.Id == customer.Id)
                                                         .FirstOrDefault();

                if (currentCustomer == null)
                {
                    throw new Exception("Customer_Not_Found");
                }

                customer.CreatedAt = currentCustomer.CreatedAt;
                customer.UpdatedAt = DateTime.Now;

                _context.Update(customer);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
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
            catch (Exception)
            {
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
            catch (Exception)
            {
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
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}
