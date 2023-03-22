using CustomerService.Data;
using CustomerService.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CustomerService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly DataContext _context;

        public CustomerController(DataContext context)
        {
            _context = context;
        }
    }
}
