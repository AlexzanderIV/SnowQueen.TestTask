using System;
using System.Collections.Generic;
using System.Linq;
using SnowQueen.TestTask.DataAccess;
using SnowQueen.TestTask.DataAccess.Dtos;
using SnowQueen.TestTask.DataAccess.Entities;
using SnowQueen.TestTask.DataAccess.Services;

namespace SnowQueen.TestTask.WCFService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ProductsWebService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select ProductsWebService.svc or ProductsWebService.svc.cs at the Solution Explorer and start debugging.
    public class ProductsWebService : IProductsWebService
    {
        /// <summary>
        /// Save product to DB.
        /// </summary>
        /// <param name="productDto">Product DTO to save to DB.</param>
        public void AddProduct(ProductDto productDto)
        {
            if (productDto == null)
            {
                throw new ArgumentNullException(nameof(productDto));
            }

            try
            {
                using (var repository = new DBRepository<Product>())
                {
                    new ProductsService(repository).SaveProduct(productDto);
                }
            }
            catch (Exception ex)
            {
                var tmp = ex;
                throw;
            }
        }

        /// <summary>
        /// Save several products to DB.
        /// </summary>
        /// <param name="productDtos">List of product DTOs to save to DB.</param>
        public void AddSeveralProducts(IEnumerable<ProductDto> productDtos)
        {
            if (productDtos == null)
            {
                throw new ArgumentNullException(nameof(productDtos));
            }

            using (var repository = new DBRepository<Product>())
            {
                var service = new ProductsService(repository);

                foreach (var productDto in productDtos)
                {
                    service.SaveProduct(productDto);
                }
            }
        }

        /// <summary>
        /// Get all products from DB.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ProductDto> GetProducts()
        {
            using (var repository = new DBRepository<Product>())
            {
                return new ProductsService(repository).GetAllProducts().Select(p => new ProductDto
                {
                    Name = p.Name,
                    Price = p.Price,
                    Amount = p.Amount
                });
            }
        }
    }
}
