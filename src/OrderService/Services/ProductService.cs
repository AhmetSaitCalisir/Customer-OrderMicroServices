using OrderService.Data;

namespace OrderService.Services
{
    public interface IProductService
    {
        public bool ValidateProduct(string id);
    }
    public class ProductService : IProductService
    {
        private readonly DataContext _context;
        private readonly ILogger<ProductService> logger;
        public ProductService(DataContext context, ILogger<ProductService> _logger)
        {
            _context = context;
            logger = _logger;
        }

        /// <summary>
        /// Ürünün var olup olmadığını kontrol eden fonksiyon
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        #region ValidateProduct
        public bool ValidateProduct(string id)
        {
            try
            {
                var product = _context.Products.Where(c => c.Id == id).FirstOrDefault();

                return product != null;
            }
            catch (Exception ex)
            {
                logger.LogError($"Error while validating product with id of {id}. Error Message: {ex.Message}");
                throw;
            }
        }
        #endregion
    }
}
