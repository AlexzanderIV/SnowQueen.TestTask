using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using SnowQueen.TestTask.DataAccess.Entities;

namespace SnowQueen.TestTask.DataAccess.Repositories
{
    public class FileRepository<TEntity> : IRepository<TEntity>
        where TEntity: Entity, new()
    {
        private const string PropertiesDelimiter = ";";

        private readonly string _filePath;

        private List<TEntity> _entities;

        public FileRepository(string filePath)
        {
            _filePath = filePath;
            _entities = Load().OrderBy(x => x.Id).ToList();
        }

        public IEnumerable<TEntity> Get()
        {
            return _entities;
        }
        
        public void Create(TEntity newEntity)
        {
            if (newEntity == null)
            {
                return;
            }

            newEntity.Id = GetLastEntityId() + 1;

            // Just sanity check to verify that we have no dupliated Ids
            if (_entities.Any(r => r.Id == newEntity.Id))
            {
                return;
            }

            _entities.Add(newEntity);
            Save();
        }

        public TEntity GetById(int id)
        {
            return _entities.Find(r => r.Id == id);
        }

        public void Delete(TEntity entity)
        {
            if (entity == null)
            {
                return;
            }
            _entities.Remove(entity);
            Save();
        }

        /// <summary>
        /// Load entities data from file.
        /// </summary>
        private IEnumerable<TEntity> Load()
        {
            if (!File.Exists(_filePath))
            {
                return new List<TEntity>();
            }

            using (var fileStream = new FileStream(_filePath, FileMode.Open))
            {
                using (var reader = new StreamReader(fileStream))
                {
                    List<TEntity> entities = new List<TEntity>();
                    while (!reader.EndOfStream)
                    {
                        var fileData = reader.ReadLine();
                        var entity = DeserializeFromString(fileData);
                        if (entity != null)
                        {
                            entities.Add(entity);
                        }
                    }
                    return entities;
                }
            }
        }

        /// <summary>
        /// Save the whole entities data to file.
        /// </summary>
        private void Save()
        {
            if (_entities == null || _entities.Count < 1)
            {
                return;
            }

            var fileDirectory = Path.GetDirectoryName(_filePath);
            if (!Directory.Exists(fileDirectory))
            {
                Directory.CreateDirectory(fileDirectory);
            }

            var lines = new List<string>(_entities.Count);
            foreach (var product in _entities)
            {
                lines.Add(SerializeToString(product));
            }

            File.WriteAllLines(_filePath, lines.ToArray());
        }

        private string SerializeToString(TEntity entity)
        {
            if (entity == null)
            {
                return string.Empty;
            }

            var properties = entity.GetType().GetProperties();

            var sb = new StringBuilder(string.Empty);
            foreach (var property in properties)
            {
                var value = property.GetValue(entity);
                // Id field should goes first.
                if (property.Name.Equals("Id", StringComparison.OrdinalIgnoreCase))
                {
                    sb.Insert(0, $"{value}{PropertiesDelimiter}");
                }
                else
                {
                    sb.Append($"{value}{PropertiesDelimiter}");
                }
            }
            
            return sb.ToString();
        }

        private TEntity DeserializeFromString(string data)
        {
            if (string.IsNullOrEmpty(data))
            {
                return null;
            }

            var dataArray = data.Split(new[] { PropertiesDelimiter }, StringSplitOptions.RemoveEmptyEntries);

            var entity = new TEntity();
            
            var properties = entity.GetType().GetProperties();

            var idProperty = properties.FirstOrDefault(p => p.Name.Equals("Id", StringComparison.OrdinalIgnoreCase));
            int? idValue = null;
            // Id field should goes first.
            if (idProperty != null)
            {
                idValue = Int32.Parse(dataArray[0]);
            }

            var dataArrayIndex = 1;
            for (var propertyIndex = 0; propertyIndex < dataArray.Length; propertyIndex++)
            {
                if (idValue.HasValue && properties[propertyIndex].Name == idProperty.Name)
                {
                    properties[propertyIndex].SetValue(entity, idValue);
                }
                else
                {
                    var propertyType = properties[propertyIndex].PropertyType;
                    var value = Convert.ChangeType(dataArray[dataArrayIndex++], propertyType);
                    properties[propertyIndex].SetValue(entity, value);
                }
            }
            
            return entity;
        }

        private int GetLastEntityId()
        {
            return _entities.LastOrDefault()?.Id ?? 0;
        }

        public void Dispose()
        {
            // TODO: Add disposal logic if needed.
        }
    }  
}
