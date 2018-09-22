using System;
using System.Collections.Generic;

namespace SnowQueen.TestTask.DataAccess
{
    public interface IRepository<TEntity>
    {
        void Create(TEntity item);

        IEnumerable<TEntity> Get();
    }
}
