using System.Collections.Generic;
using SnowQueen.TestTask.DataAccess.Dtos;
using SnowQueen.TestTask.DataAccess.Entities;

namespace SnowQueen.TestTask.DataAccess.Services
{
    public class ProductsService
    {
        private readonly IRepository<Product> _repository;

        public ProductsService(IRepository<Product> repository)
        {
            _repository = repository;
        }

        public void SaveProduct(ProductDto productDto)
        {
            var product = new Product
            {
                Name = productDto.Name,
                Price = productDto.Price,
                Amount = productDto.Amount
            };

            _repository.Create(product);
        }

        public IEnumerable<Product> GetAllProducts()
        {
            var products = _repository.Get();
            return products;
        }
    }
}
