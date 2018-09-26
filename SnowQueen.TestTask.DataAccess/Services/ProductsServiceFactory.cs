using SnowQueen.TestTask.DataAccess.Entities;
using SnowQueen.TestTask.DataAccess.Repositories;

namespace SnowQueen.TestTask.DataAccess.Services
{
    public static class ProductsServiceFactory
    {
        public static ProductsService CreateWithFileRepository(string filePath)
        {
            var repository = new FileRepository<Product>(filePath);
            return new ProductsService(repository);
        }

        public static ProductsService CreateWithDbRepository()
        {
            var repository = new DBRepository<Product>();
            return new ProductsService(repository);
        }
    }
}
