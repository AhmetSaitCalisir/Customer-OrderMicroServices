using CustomerService.Data;
using CustomerService.Entities;

namespace CustomerService.Services
{
    public class CustomerService
    {
        private readonly DataContext _context;

        public CustomerService(DataContext context) 
        {
            _context = context;
        }

        public async Task<string> CreateCustomer(Customer customer)
        {
            customer.Id = Guid.NewGuid().ToString();
            customer.CreatedAt = DateTime.Now;
            customer.UpdatedAt = DateTime.Now;
            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();
            return customer.Id;
        }

        public async Task<bool> UpdateCustomer(Customer customer)
        {
            var currentCustomer = _context.Customers.Where(c => c.Id == customer.Id)
                                                         .FirstOrDefault();

            if (currentCustomer != null)
            {
                return false;
            }

            customer.CreatedAt = currentCustomer.CreatedAt;
            customer.UpdatedAt = DateTime.Now;

            _context.Update(customer);
            _context.SaveChanges();
            return true;
        }

        public async Task<bool> DeleteCustomer(string id)
        {
            var currentCustomer = _context.Customers.Where(c => c.Id == id)
                                                         .FirstOrDefault();

            if (currentCustomer != null)
            {
                return false;
            }

            _context.Customers.Remove(currentCustomer);
            _context.SaveChanges();
            return true;
        }

        public async Task<List<Customer>> GetCustomers()
        {
            return  _context.Customers.ToList();
        }

        public async Task<Customer> GetCustomer(string id)
        {
            var customer = _context.Customers.Where(c => c.Id == id).FirstOrDefault();

            if (customer == null) throw new Exception("CustomerNotFound");

            return customer;
        }
    }
}
