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
    }
}
