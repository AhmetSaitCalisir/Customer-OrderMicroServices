using CustomerService.Data;
using CustomerService.Models;
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

        /// <summary>
        /// Müşteri yaratılan end point
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        #region Create
        [HttpPost]
        public async Task<IActionResult> Create(CustomerModel customer)
        {
            try
            {
                var customerId = await _customerService.CreateCustomer(customer);
                return Ok(customerId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        #endregion

        /// <summary>
        /// Müşteri düzenlenen end point
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        #region Update
        [HttpPut]
        public IActionResult Update(CustomerModel customer)
        {
            try
            {
                var isCustomerUpdated = _customerService.UpdateCustomer(customer);
                if (isCustomerUpdated)
                {
                    return Ok(isCustomerUpdated);
                }
                return BadRequest(isCustomerUpdated);
            }
            catch (Exception ex)
            {
                if (ex.Message == "Customer_Not_Found")
                {
                    return NotFound(customer.Id);
                }
                return BadRequest(ex.Message);
            }
        }
        #endregion

        /// <summary>
        /// İd ile müşteri silinen end point
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        #region Delete
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            try
            {
                var isCustomerDeleted = _customerService.DeleteCustomer(id);
                if (isCustomerDeleted)
                {
                    return Ok(isCustomerDeleted);
                }
                return BadRequest(isCustomerDeleted);
            }
            catch (Exception ex)
            {
                if (ex.Message == "Customer_Not_Found")
                {
                    return NotFound(id);
                }
                return BadRequest(ex.Message);
            }
        }
        #endregion

        /// <summary>
        /// Bütüm müşterilerin listesini döndüren end point
        /// </summary>
        /// <returns></returns>
        #region GetAll
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var customers = _customerService.GetCustomers();
                return Ok(customers);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        /// <summary>
        /// İd'si verilen kullanıcıyı döndüren fonksiyon
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        #region Get
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            try
            {
                var customer = _customerService.GetCustomer(id);
                return Ok(customer);
            }
            catch (Exception ex)
            {
                if (ex.Message == "Customer_Not_Found")
                {
                    return NotFound(id);
                }
                return BadRequest(ex.Message);
            }
        }
        #endregion
    }
}
