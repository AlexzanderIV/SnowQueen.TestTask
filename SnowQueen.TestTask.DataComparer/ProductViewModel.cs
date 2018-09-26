using SnowQueen.TestTask.DataComparer.ProductsWebService;
using System.ComponentModel.DataAnnotations;

namespace SnowQueen.TestTask.DataComparer
{
    public class ProductViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public int Amount { get; set; }

        public ProductDataContract ToDataContract()
        {
            return new ProductDataContract
            {
                Name = Name,
                Price = Price,
                Amount = Amount
            };
        }
    }
}
