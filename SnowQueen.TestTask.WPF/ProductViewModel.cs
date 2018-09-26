using SnowQueen.TestTask.WPF.ProductsWebService;
using System.ComponentModel.DataAnnotations;

namespace SnowQueen.TestTask.WPF
{
    public class ProductViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int Amount { get; set; }

        public DataAccess.Dtos.ProductDto ToDto()
        {
            return new DataAccess.Dtos.ProductDto
            {
                Name = Name,
                Price = Price,
                Amount = Amount
            };
        }

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
