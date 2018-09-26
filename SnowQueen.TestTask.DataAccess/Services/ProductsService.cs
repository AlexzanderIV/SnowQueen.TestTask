using System;
using System.Collections.Generic;
using SnowQueen.TestTask.DataAccess.Dtos;
using SnowQueen.TestTask.DataAccess.Entities;
using SnowQueen.TestTask.DataAccess.Repositories;

namespace SnowQueen.TestTask.DataAccess.Services
{
    public class ProductsService : IDisposable
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

        public void Dispose()
        {
            _repository.Dispose();
        }
    }
}