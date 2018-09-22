using SnowQueen.TestTask.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace SnowQueen.TestTask.DataAccess
{
    public class FileRepository<TEntity> : IRepository<TEntity>, IDisposable
        where TEntity: Entity
    {
        private readonly string _fileName;

        public FileRepository(string fileName)
        {
            _fileName = fileName;
        }

        public IEnumerable<TEntity> Get()
        {

        }
        
        public void Create(TEntity item)
        {
            
        }

        public void Dispose()
        {
            
        }

        // TODO: DELETE. TEST SAVE TO DB
        public static void TestTask(string name, decimal price, int amount)
        {
            using (var repo = new DBRepository<Product>())
            {
                var product = new Product
                {
                    Name = name,
                    Price = price,
                    Amount = amount
                };
                repo.Create(product);
            }
        }
    }
}
