﻿using CustomerService.Models;
using CustomerService.Services;
using Microsoft.AspNetCore.Mvc;

namespace CustomerService.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
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
                if (ex.Message.Contains("Not_Found"))
                {
                    return NotFound(ex.Message);
                }
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

                return Ok(isCustomerUpdated);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Not_Found"))
                {
                    return NotFound(ex.Message);
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

                return Ok(isCustomerDeleted);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Not_Found"))
                {
                    return NotFound(ex.Message);
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
        /// İd'si verilen müşteriyi döndüren end point
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
                    return NotFound(ex.Message);
                }
                return BadRequest(ex.Message);
            }
        }
        #endregion

        /// <summary>
        /// İd'si verilen müşterinin var olup olmadığını döndüren end point
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        #region Validate
        [HttpGet("validate/{id}")]
        public IActionResult Validate(string id)
        {
            try
            {
                var isCustomerExist = _customerService.ValidateCustomer(id);
                return Ok(isCustomerExist);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion
    }
}
