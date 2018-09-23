using SnowQueen.TestTask.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.IO;
using System.Text;

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
    
    
    
    /// <summary>
    /// DTO для записи данных о тех. обслуживании автомобиля
    /// </summary>
    public class ServiceRecordDto
    {
        public int Id { get; set; }

        public string VINNumber { get; set; }

        public string Owner { get; set; }

        public int ProductionYear { get; set; }

        public DateTime LastServiceDate { get; set; }

        public DateTime WarrantyExpiresDate { get; set; }

        //public DateTime NextServiceDate =>
        //    LastServiceDate.AddDays(int.Parse(ConfigurationManager.AppSettings["ServiceIntervalInDays"]));

    }

    class ServiceRecord
    {
        public int Id { get; set; }
        /// <summary>
        /// VIN-номер автомобиля
        /// </summary>
        public string VINNumber { get; set; }
        /// <summary>
        /// Владелец автомобиля (ФИО)
        /// </summary>
        public string Owner { get; set; }
        /// <summary>
        /// Год выпуска автомобиля
        /// </summary>
        public int ProductionYear { get; set; }
        /// <summary> 
        /// Дата последнего ТО
        /// </summary>
        public DateTime LastServiceDate { get; set; }
        /// <summary> 
        /// Дата следующего ТО
        /// </summary>
        public DateTime NextServiceDate { get; set; }
        /// <summary> 
        /// Признак необходимости проейти ТО в ближайшее время
        /// </summary>
        public bool NeedServiceSoon =>
            (DateTime.UtcNow.Date >= NextServiceDate.AddDays(-1).Date &&
            DateTime.UtcNow.Date <= NextServiceDate.Date &&
            NextServiceDate.Date <= WarrantyExpiresDate.Date);
        /// <summary>
        /// Дата окончания гарантийного обслуживания
        /// </summary>
        public DateTime WarrantyExpiresDate { get; set; }

        public void CopyFrom(ServiceRecord newRecord)
        {
            VINNumber = newRecord.VINNumber;
            Owner = newRecord.Owner;
            ProductionYear = newRecord.ProductionYear;
            LastServiceDate = newRecord.LastServiceDate;
            WarrantyExpiresDate = newRecord.WarrantyExpiresDate;
        }

        public static ServiceRecord FromDto(ServiceRecordDto record)
        {
            return new ServiceRecord
            {
                Id = record.Id,
                VINNumber = record.VINNumber,
                Owner = record.Owner,
                ProductionYear = record.ProductionYear,
                LastServiceDate = record.LastServiceDate,
                WarrantyExpiresDate = record.WarrantyExpiresDate
            };
        }

        public static ServiceRecordDto ToDto(ServiceRecord record)
        {
            return new ServiceRecordDto
            {
                Id = record.Id,
                VINNumber = record.VINNumber,
                Owner = record.Owner,
                ProductionYear = record.ProductionYear,
                LastServiceDate = record.LastServiceDate,
                WarrantyExpiresDate = record.WarrantyExpiresDate
            };
        }


        private const string DELEMITER = ";";
        // TODO: Переделать на метод-расширение
        public static string SerializeToString(ServiceRecord record)
        {
            if (record == null)
                return string.Empty;

            var sb = new StringBuilder();
            sb.Append($"{record.Id}{DELEMITER}");
            sb.Append($"{record.VINNumber}{DELEMITER}");
            sb.Append($"{record.Owner}{DELEMITER}");
            sb.Append($"{record.ProductionYear}{DELEMITER}");
            sb.Append($"{record.LastServiceDate}{DELEMITER}");
            sb.Append($"{record.WarrantyExpiresDate}");
            return sb.ToString();
        }

        // TODO: Переделать на метод-расширение
        public static ServiceRecord DeserializeFromString(string data)
        {
            if (string.IsNullOrEmpty(data))
                return null;
            var dataArray = data.Split(new[] { DELEMITER }, StringSplitOptions.RemoveEmptyEntries);

            var record = new ServiceRecord
            {
                Id = Int32.Parse(dataArray?[0]),
                VINNumber = dataArray?[1],
                Owner = dataArray?[2],
                ProductionYear = Int32.Parse(dataArray?[3]),
                LastServiceDate = DateTime.Parse(dataArray?[4]),
                WarrantyExpiresDate = DateTime.Parse(dataArray?[5])
            };
            return record;
        }

    }

    class ServiceRecordRepository
    {
        private static string _filePath = "DB";
        private static string _fileName = Path.Combine(_filePath, "DB.txt");
        private static List<ServiceRecord> records;

        public ServiceRecordRepository()
        {
            records = Load().ToList();
        }

        public ServiceRecord Get(int id)
        {
            var record = records.Find(r => r.Id == id);
            return record;
        }

        public IEnumerable<ServiceRecord> GetAll()
        {
            return records;
        }

        public ServiceRecord Insert(ServiceRecord newRecord)
        {
            if (newRecord == null)
                return null;

            if (records.Any(r => r.Id == newRecord.Id))
                return null;

            records.Add(newRecord);
            Save();

            return newRecord;
        }

        public ServiceRecord Update(ServiceRecord newRecord)
        {
            var updatedRecord = Get(newRecord.Id);
            updatedRecord.CopyFrom(newRecord);
            Save();
            return updatedRecord;
        }

        public void Delete(ServiceRecord record)
        {
            if (record == null)
                return;
            records.Remove(record);
            Save();
        }

        /// <summary>
        /// Загрузка данных из файла (БД)
        /// </summary>
        private IEnumerable<ServiceRecord> Load()
        {
            if (!Directory.Exists(_filePath))
            {
                Directory.CreateDirectory(_filePath);
            }

            if (!File.Exists(_fileName))
            {
                return new List<ServiceRecord>();
            }

            var lines = File.ReadAllLines(_fileName);
            var result = new List<ServiceRecord>(lines.Length);
            foreach (var line in lines)
            {
                var record = ServiceRecord.DeserializeFromString(line);
                result.Add(record);
            }
            return result;
        }

        private void Save()
        {
            if (records == null || records.Count < 1)
                return;

            if (!Directory.Exists(_filePath))
            {
                Directory.CreateDirectory(_filePath);
            }

            //var text = new StringBuilder();
            //foreach (var record in records)
            //{
            //    text.Append(ServiceRecord.SerializeToString(record));
            //    text.Append("\n");
            //}

            //using (FileStream fileStream = new FileStream(_filePath, FileMode.OpenOrCreate))
            //{
            //    // преобразуем строку в байты
            //    byte[] input = Encoding.Default.GetBytes(text.ToString());
            //    // запись массива байтов в файл
            //    fileStream.Write(input, 0, input.Length);
            //}

            var lines = new List<string>(records.Count);
            foreach (var record in records)
            {
                lines.Add(ServiceRecord.SerializeToString(record));
            }

            File.WriteAllLines(_fileName, lines.ToArray());
        }

    }
}
