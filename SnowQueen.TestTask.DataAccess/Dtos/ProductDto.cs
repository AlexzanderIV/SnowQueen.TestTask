using System.Runtime.Serialization;

namespace SnowQueen.TestTask.DataAccess.Dtos
{
    //[DataContract]
    public class ProductDto
    {
        public string Name { get; set; }

        public decimal Price { get; set; }

        public int Amount { get; set; }
    }
}