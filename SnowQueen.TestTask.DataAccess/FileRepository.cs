using SnowQueen.TestTask.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.IO;

namespace SnowQueen.TestTask.DataAccess
{
    public class FileRepository<TEntity> : IRepository<TEntity>
        where TEntity: Entity, new()
    {
        private const string PropertiesDelimiter = ";";

        private readonly string _fileName;

        public FileRepository(string fileName)
        {
            _fileName = fileName;
        }

        public IEnumerable<TEntity> Get()
        {
            using (var fileStream = new FileStream(_fileName, FileMode.Open))
            {
                using (var reader = new StreamReader(fileStream))
                {
                    List<TEntity> entities = new List<TEntity>();
                    while (!reader.EndOfStream)
                    {
                        var fileData = reader.ReadLine();
                        var entity = FromString(fileData);
                        if (entity != null)
                        {
                            entities.Add(entity);
                        }
                    }
                    return entities;
                }
            }
        }
        
        public void Create(TEntity item)
        {
            
        }

        private TEntity FromString(string fileData)
        {
            var properties = fileData.Split(new string[] { PropertiesDelimiter }, StringSplitOptions.RemoveEmptyEntries);
            if (typeof(TEntity) is Product)
            {
                var entity = new Product
                {
                    Id = int.Parse(properties[0]),
                    Name = properties[1],
                    Price = decimal.Parse(properties[2]),
                    Amount = int.Parse(properties[3])
                };
                return entity as TEntity;
            }
            return null;
        }

        public void Dispose()
        {
            
        }
    }
}
