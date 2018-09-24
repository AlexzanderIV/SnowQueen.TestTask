using SnowQueen.TestTask.DataAccess;
using SnowQueen.TestTask.DataAccess.Dtos;
using SnowQueen.TestTask.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            //DBRepository<DataAccess.Entities.Product>.TestTask(testProduct.Name, testProduct.Price, testProduct.Amount);

            var products = _repository.Get();

            return products;
        }
    }
}
