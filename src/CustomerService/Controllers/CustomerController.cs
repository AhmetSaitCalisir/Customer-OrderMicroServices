using CustomerService.Data;
using CustomerService.Services;
using Microsoft.AspNetCore.Mvc;

namespace CustomerService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly ICustomerService _customerService;

        public CustomerController(DataContext context, ICustomerService customerService)
        {
            _context = context;
            _customerService = customerService;
        }
    }
}
