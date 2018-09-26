using System;
using System.Collections.Generic;

namespace SnowQueen.TestTask.DataAccess.Repositories
{
    public interface IRepository<TEntity> : IDisposable
    {
        void Create(TEntity item);

        IEnumerable<TEntity> Get();
    }
}
