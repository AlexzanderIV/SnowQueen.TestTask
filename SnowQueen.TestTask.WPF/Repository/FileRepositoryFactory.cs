using SnowQueen.TestTask.DataAccess.Entities;
using System.Configuration;

namespace SnowQueen.TestTask.WPF.Repository
{
    public static class FileRepositoryFactory<TEntity>
        where TEntity : Entity, new()
    {
        public static FileRepository<TEntity> Create()
        {
            return new FileRepository<TEntity>(ConfigurationManager.AppSettings["FileStoragePath"]);
        }
    }
}
