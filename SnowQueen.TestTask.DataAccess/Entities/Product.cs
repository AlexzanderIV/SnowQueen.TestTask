namespace SnowQueen.TestTask.DataAccess.Entities
{
    public class Product : Entity
    {
        public string Name { get; set; }

        public decimal Price { get; set; }

        public int Amount { get; set; }
    }
}