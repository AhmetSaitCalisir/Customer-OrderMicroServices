using Microsoft.AspNetCore.Mvc;
using OrderService.Models;
using OrderService.Services;

namespace OrderService.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        /// <summary>
        /// Sipariş yaratılan end point
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        #region Create
        [HttpPost]
        public async Task<IActionResult> Create(OrderModel order)
        {
            try
            {
                var orderId = await _orderService.CreateOrder(order);
                return Ok(orderId);
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
        /// Sipariş düzenlenen end point
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        #region Update
        [HttpPut]
        public IActionResult Update(OrderModel order)
        {
            try
            {
                var isOrderUpdated = _orderService.UpdateOrder(order);

                return Ok(isOrderUpdated);
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
        /// İd ile sipariş silinen end point
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        #region Delete
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            try
            {
                var isOrderDeleted = _orderService.DeleteOrder(id);

                return Ok(isOrderDeleted);
            }
            catch (Exception ex)
            {
                if (ex.Message == "Order_Not_Found")
                {
                    return NotFound(id);
                }
                return BadRequest(ex.Message);
            }
        }
        #endregion

        /// <summary>
        /// Bütüm siparişleri listesini döndüren end point
        /// </summary>
        /// <returns></returns>
        #region GetAll
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var orders = _orderService.GetOrders();
                return Ok(orders);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        /// <summary>
        /// İd'si verilen siparişi döndüren end point
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        #region Get
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            try
            {
                var order = _orderService.GetOrder(id);
                return Ok(order);
            }
            catch (Exception ex)
            {
                if (ex.Message == "Order_Not_Found")
                {
                    return NotFound(id);
                }
                return BadRequest(ex.Message);
            }
        }
        #endregion

        /// <summary>
        /// Sipariş durumunu güncelleyen end point
        /// </summary>
        /// <param name="orderStatus"></param>
        /// <returns></returns>
        #region ChangeOrderStatus
        [HttpPut("changestatus")]
        public IActionResult ChangeOrderStatus(OrderStatusModel orderStatus)
        {
            try
            {
                var isOrderUpdated = _orderService.ChangeStatus(orderStatus);

                return Ok(isOrderUpdated);
            }
            catch (Exception ex)
            {
                if (ex.Message == "Order_Not_Found")
                {
                    return NotFound(orderStatus.Id);
                }
                return BadRequest(ex.Message);
            }
        }
        #endregion
    }
}
