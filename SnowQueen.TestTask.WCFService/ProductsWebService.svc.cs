using System;
using System.Collections.Generic;
using System.Linq;
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
        public void AddProduct(ProductDataContract product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            using (var repository = new DBRepository<Product>())
            {
                new ProductsService(repository).SaveProduct(ToDto(product));
            }
        }

        /// <summary>
        /// Save several products to DB.
        /// </summary>
        /// <param name="productDtos">List of product DTOs to save to DB.</param>
        public void AddSeveralProducts(IEnumerable<ProductDataContract> products)
        {
            if (products == null)
            {
                throw new ArgumentNullException(nameof(products));
            }

            using (var repository = new DBRepository<Product>())
            {
                var service = new ProductsService(repository);

                foreach (var product in products)
                {
                    service.SaveProduct(ToDto(product));
                }
            }
        }

        /// <summary>
        /// Get all products from DB.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ProductDataContract> GetProducts()
        {
            using (var repository = new DBRepository<Product>())
            {
                return new ProductsService(repository).GetAllProducts().Select(p => ToDataContract(p));
            }
        }

        private ProductDto ToDto(ProductDataContract dataContract)
        {
            return new ProductDto
            {
                Name = dataContract.Name,
                Price = dataContract.Price,
                Amount = dataContract.Amount
            };
        }

        private ProductDataContract ToDataContract(Product product)
        {
            return new ProductDataContract
            {
                Name = product.Name,
                Price = product.Price,
                Amount = product.Amount
            };
        }
    }
}
