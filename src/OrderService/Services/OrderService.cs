using OrderService.Data;
using OrderService.Entities;
using OrderService.Models;
using Microsoft.EntityFrameworkCore;

namespace OrderService.Services
{
    public interface IOrderService
    {
        public Task<string> CreateOrder(OrderModel order);
        public Task<bool> UpdateOrder(OrderModel order);
        public bool DeleteOrder(string id);
        public List<Order> GetOrders();
        public Order GetOrder(string id);
        public bool ChangeStatus(OrderStatusModel orderStatus);
    }
    public class OrderService : IOrderService
    {
        private readonly DataContext _context;
        private readonly ILogger<OrderService> logger;
        private readonly ICustomerProviderService _customerProviderService;

        public OrderService(DataContext context, ILogger<OrderService> _logger, ICustomerProviderService customerProviderService)
        {
            _context = context;
            logger = _logger;
            _customerProviderService = customerProviderService;
        }

        /// <summary>
        /// İd'si verilen siparişin durumunu güncelleyen fonksiyon.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        #region ChangeStatus
        public bool ChangeStatus(OrderStatusModel orderStatus)
        {
            try
            {
                var currentOrder = _context.Orders.Where(c => c.Id == orderStatus.Id)
                                                         .FirstOrDefault();

                if (currentOrder == null)
                {
                    throw new Exception("Order_Not_Found");
                }

                currentOrder.Status = orderStatus.Status;
                currentOrder.UpdatedAt = DateTime.Now;

                _context.Update(currentOrder);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                logger.LogError($"Error while updating order status with id of {orderStatus.Id}. Error Message: {ex.Message}");
                throw;
            }
        }
        #endregion

        /// <summary>
        /// Sipariş oluşturan fonksiyon
        /// </summary>
        /// <param name="_order"></param>
        /// <returns></returns>
        #region CreateOrder
        public async Task<string> CreateOrder(OrderModel _order)
        {
            try
            {
                bool isCustomerValid = await _customerProviderService.ValidateCustomer(_order.CustomerId);

                if (!isCustomerValid)
                {
                    throw new Exception("Customer_Not_Valid");
                }

                Order order = new Order()
                {
                    AddressId = _order.AddressId,
                    CustomerId = _order.CustomerId,
                    Price = _order.Price,
                    ProductId = _order.ProductId,
                    Quantity = _order.Quantity,
                    Status = _order.Status,
                    CreatedAt = DateTime.UtcNow,
                    Id = Guid.NewGuid().ToString(),
                    UpdatedAt = DateTime.UtcNow,
                };
                await _context.Orders.AddAsync(order);
                await _context.SaveChangesAsync();
                return order.Id;
            }
            catch (Exception ex)
            {
                logger.LogError($"Error while creating order. Error Message: {ex.Message}");
                throw;
            }
        }
        #endregion

        /// <summary>
        /// Id'si verilen siparişi silen fonksiyon
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        #region DeleteOrder
        public bool DeleteOrder(string id)
        {
            try
            {
                var currentOrder = _context.Orders.Where(c => c.Id == id)
                                                         .FirstOrDefault();

                if (currentOrder == null)
                {
                    throw new Exception("Order_Not_Found");
                }

                _context.Orders.Remove(currentOrder);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                logger.LogError($"Error while deleting order with id of {id}. Error Message: {ex.Message}");
                throw;
            }
        }
        #endregion

        /// <summary>
        /// Id'si verilen siparişi döndüren fonksiyon
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        #region GetOrder
        public Order GetOrder(string id)
        {
            try
            {
                var order = _context.Orders.Where(c => c.Id == id)
                    .Include(x => x.Address)
                    .Include(x => x.Customer)
                    .FirstOrDefault();

                if (order == null)
                {
                    throw new Exception("Order_Not_Found");
                }

                return order;
            }
            catch (Exception ex)
            {
                logger.LogError($"Error while getting order with id of {id}. Error Message: {ex.Message}");
                throw;
            }
        }
        #endregion

        /// <summary>
        /// Bütün siparişleri listeleyen fonksiyon
        /// </summary>
        /// <returns></returns>
        #region GetOrders
        public List<Order> GetOrders()
        {
            try
            {
                return _context.Orders.ToList();
            }
            catch (Exception ex)
            {
                logger.LogError($"Error while getting orders. Error Message: {ex.Message}");
                throw;
            }
        }
        #endregion

        /// <summary>
        /// Siparişi güncelleyen fonksiyon
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        #region UpdateOrder
        public async Task<bool> UpdateOrder(OrderModel order)
        {
            try
            {
                bool isCustomerValid = await _customerProviderService.ValidateCustomer(order.CustomerId);

                if (!isCustomerValid)
                {
                    throw new Exception("Customer_Not_Valid");
                }

                var currentOrder = _context.Orders.Where(c => c.Id == order.Id)
                                                         .FirstOrDefault();

                if (currentOrder == null)
                {
                    throw new Exception("Order_Not_Found");
                }

                currentOrder.Quantity = order.Quantity;
                currentOrder.Status = order.Status;
                currentOrder.AddressId = order.AddressId;
                currentOrder.ProductId = order.ProductId;
                currentOrder.CustomerId = order.CustomerId;
                currentOrder.Price = order.Price;
                currentOrder.Status = order.Status;
                currentOrder.UpdatedAt = DateTime.Now;

                _context.Update(currentOrder);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                logger.LogError($"Error while updating order with id of {order.Id}. Error Message: {ex.Message}");
                throw;
            }
        }
        #endregion
    }
}
