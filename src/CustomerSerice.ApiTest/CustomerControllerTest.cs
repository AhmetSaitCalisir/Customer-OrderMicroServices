using AutoFixture;
using CustomerService.Controllers;
using CustomerService.Entities;
using CustomerService.Models;
using CustomerService.Services;
using Microsoft.AspNetCore.Mvc;

namespace CustomerSerice.ApiTest
{
    [TestClass]
    public class CustomerControllerTest
    {
        private Mock<ICustomerService> _customerService;
        private Fixture _fixture;
        private CustomerController _controller;

        public CustomerControllerTest()
        {
            _fixture = new Fixture();
            _customerService = new Mock<ICustomerService>();
            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList().ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        #region Get Customers Tests
        [TestMethod]
        public void Get_Customers_ReturnOk()
        {
            try
            {
                var customers = _fixture.CreateMany<Customer>(5).ToList();

                _customerService.Setup(s => s.GetCustomers()).Returns(customers);

                _controller = new CustomerController(_customerService.Object);

                var result = _controller.GetAll();

                var response = result as ObjectResult;

                Assert.AreEqual(200, response.StatusCode);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [TestMethod]
        public void Get_Customers_ReturnCustomerList()
        {
            try
            {
                var customers = _fixture.CreateMany<Customer>(5).ToList();

                _customerService.Setup(s => s.GetCustomers()).Returns(customers);

                _controller = new CustomerController(_customerService.Object);

                var result = _controller.GetAll();

                var response = result as ObjectResult;

                Assert.IsInstanceOfType(response.Value, typeof(List<Customer>));
            }
            catch (Exception)
            {
                throw;
            }
        }

        [TestMethod]
        public void Get_Customers_RetunBadRequest()
        {
            try
            {
                _customerService.Setup(s => s.GetCustomers()).Throws<Exception>();

                _controller = new CustomerController(_customerService.Object);

                var result = _controller.GetAll();

                var response = result as ObjectResult;

                Assert.AreEqual(400, response.StatusCode);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region Get Customer Tests
        [TestMethod]
        public void Get_Customer_ReturnOk()
        {
            try
            {
                var customer = _fixture.Create<Customer>();

                _customerService.Setup(s => s.GetCustomer(It.IsAny<string>())).Returns(customer);

                _controller = new CustomerController(_customerService.Object);

                var result = _controller.Get(Guid.NewGuid().ToString());

                var response = result as ObjectResult;

                Assert.AreEqual(200, response.StatusCode);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [TestMethod]
        public void Get_Customer_ReturnCustomer()
        {
            try
            {
                var customer = _fixture.Create<Customer>();

                _customerService.Setup(s => s.GetCustomer(It.IsAny<string>())).Returns(customer);

                _controller = new CustomerController(_customerService.Object);

                var result = _controller.Get(Guid.NewGuid().ToString());

                var response = result as ObjectResult;

                Assert.IsInstanceOfType(response.Value, typeof(Customer));
            }
            catch (Exception)
            {
                throw;
            }
        }

        [TestMethod]
        public void Get_Customer_ReturnNotFound()
        {
            try
            {
                _customerService.Setup(s => s.GetCustomer(It.IsAny<string>())).Throws(new Exception("Customer_Not_Found"));

                _controller = new CustomerController(_customerService.Object);

                var result = _controller.Get(Guid.NewGuid().ToString());

                var response = result as ObjectResult;

                Assert.AreEqual(404, response.StatusCode);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [TestMethod]
        public void Get_Customer_ReturnBadRequest()
        {
            try
            {
                _customerService.Setup(s => s.GetCustomer(It.IsAny<string>())).Throws<Exception>();

                _controller = new CustomerController(_customerService.Object);

                var result = _controller.Get(Guid.NewGuid().ToString());

                var response = result as ObjectResult;

                Assert.AreEqual(400, response.StatusCode);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region Create Customer Tests
        [TestMethod]
        public async Task Create_Customer_ReturnOk()
        {
            _customerService.Setup(s => s.CreateCustomer(It.IsAny<CustomerModel>())).ReturnsAsync(Guid.NewGuid().ToString());

            _controller = new CustomerController(_customerService.Object);

            var customer = _fixture.Create<CustomerModel>();

            var result = await _controller.Create(customer);

            var response = result as ObjectResult;

            Assert.AreEqual(200, response.StatusCode);
        }

        [TestMethod]
        public async Task Create_Customer_ReturnGuid()
        {
            _customerService.Setup(s => s.CreateCustomer(It.IsAny<CustomerModel>())).ReturnsAsync(Guid.NewGuid().ToString());

            _controller = new CustomerController(_customerService.Object);

            var customer = _fixture.Create<CustomerModel>();

            var result = await _controller.Create(customer);

            var response = result as ObjectResult;

            Assert.IsInstanceOfType(response.Value, typeof(string));
        }

        [TestMethod]
        public async Task Create_Customer_ReturnNotFound()
        {
            _customerService.Setup(s => s.CreateCustomer(It.IsAny<CustomerModel>())).ThrowsAsync(new Exception("Something_Not_Found"));

            _controller = new CustomerController(_customerService.Object);

            var customer = _fixture.Create<CustomerModel>();

            var result = await _controller.Create(customer);

            var response = result as ObjectResult;

            Assert.AreEqual(404, response.StatusCode);
        }

        [TestMethod]
        public async Task Create_Customer_ReturnBadRequest()
        {
            _customerService.Setup(s => s.CreateCustomer(It.IsAny<CustomerModel>())).ThrowsAsync(new Exception());

            _controller = new CustomerController(_customerService.Object);

            var customer = _fixture.Create<CustomerModel>();

            var result = await _controller.Create(customer);

            var response = result as ObjectResult;

            Assert.AreEqual(400, response.StatusCode);
        }
        #endregion

        #region Update Customer Tests
        [TestMethod]
        public void Update_Customer_ReturnOk()
        {
            _customerService.Setup(s => s.UpdateCustomer(It.IsAny<CustomerModel>())).Returns(true);

            _controller = new CustomerController(_customerService.Object);

            var customer = _fixture.Create<CustomerModel>();

            var result = _controller.Update(customer);

            var response = result as ObjectResult;

            Assert.AreEqual(200, response.StatusCode);
        }

        [TestMethod]
        public async Task Update_Customer_ReturnBoolean()
        {
            _customerService.Setup(s => s.UpdateCustomer(It.IsAny<CustomerModel>())).Returns(true);

            _controller = new CustomerController(_customerService.Object);

            var customer = _fixture.Create<CustomerModel>();

            var result = _controller.Update(customer);

            var response = result as ObjectResult;

            Assert.IsInstanceOfType(response.Value, typeof(bool));
        }

        [TestMethod]
        public void Update_Customer_ReturnNotFound()
        {
            _customerService.Setup(s => s.UpdateCustomer(It.IsAny<CustomerModel>())).Throws(new Exception("Something_Not_Found"));

            _controller = new CustomerController(_customerService.Object);

            var customer = _fixture.Create<CustomerModel>();

            var result = _controller.Update(customer);

            var response = result as ObjectResult;

            Assert.AreEqual(404, response.StatusCode);
        }

        [TestMethod]
        public void Update_Customer_ReturnBadRequest()
        {
            _customerService.Setup(s => s.UpdateCustomer(It.IsAny<CustomerModel>())).Throws(new Exception());

            _controller = new CustomerController(_customerService.Object);

            var customer = _fixture.Create<CustomerModel>();

            var result = _controller.Update(customer);

            var response = result as ObjectResult;

            Assert.AreEqual(400, response.StatusCode);
        }
        #endregion

        #region Delete Customer Tests
        [TestMethod]
        public void Delete_Customer_ReturnOk()
        {
            _customerService.Setup(s => s.DeleteCustomer(It.IsAny<string>())).Returns(true);

            _controller = new CustomerController(_customerService.Object);

            var result = _controller.Delete(Guid.NewGuid().ToString());

            var response = result as ObjectResult;

            Assert.AreEqual(200, response.StatusCode);
        }

        [TestMethod]
        public async Task Delete_Customer_ReturnBoolean()
        {
            _customerService.Setup(s => s.DeleteCustomer(It.IsAny<string>())).Returns(true);

            _controller = new CustomerController(_customerService.Object);

            var result = _controller.Delete(Guid.NewGuid().ToString());

            var response = result as ObjectResult;

            Assert.IsInstanceOfType(response.Value, typeof(bool));
        }

        [TestMethod]
        public void Delete_Customer_ReturnNotFound()
        {
            _customerService.Setup(s => s.DeleteCustomer(It.IsAny<string>())).Throws(new Exception("Something_Not_Found"));

            _controller = new CustomerController(_customerService.Object);

            var result = _controller.Delete(Guid.NewGuid().ToString());

            var response = result as ObjectResult;

            Assert.AreEqual(404, response.StatusCode);
        }

        [TestMethod]
        public void Delete_Customer_ReturnBadRequest()
        {
            _customerService.Setup(s => s.DeleteCustomer(It.IsAny<string>())).Throws(new Exception());

            _controller = new CustomerController(_customerService.Object);

            var result = _controller.Delete(Guid.NewGuid().ToString());

            var response = result as ObjectResult;

            Assert.AreEqual(400, response.StatusCode);
        }
        #endregion

        #region Validate Customer Tests
        [TestMethod]
        public void Validate_Customer_ReturnOk()
        {
            _customerService.Setup(s => s.ValidateCustomer(It.IsAny<string>())).Returns(true);

            _controller = new CustomerController(_customerService.Object);

            var result = _controller.Validate(Guid.NewGuid().ToString());

            var response = result as ObjectResult;

            Assert.AreEqual(200, response.StatusCode);
        }

        [TestMethod]
        public async Task Validate_Customer_ReturnBoolean()
        {
            _customerService.Setup(s => s.ValidateCustomer(It.IsAny<string>())).Returns(true);

            _controller = new CustomerController(_customerService.Object);

            var result = _controller.Validate(Guid.NewGuid().ToString());

            var response = result as ObjectResult;

            Assert.IsInstanceOfType(response.Value, typeof(bool));
        }

        [TestMethod]
        public void Validate_Customer_ReturnBadRequest()
        {
            _customerService.Setup(s => s.ValidateCustomer(It.IsAny<string>())).Throws(new Exception());

            _controller = new CustomerController(_customerService.Object);

            var result = _controller.Validate(Guid.NewGuid().ToString());

            var response = result as ObjectResult;

            Assert.AreEqual(400, response.StatusCode);
        }
        #endregion
    }
}
