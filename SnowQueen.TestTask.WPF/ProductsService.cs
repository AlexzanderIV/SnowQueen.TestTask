using SnowQueen.TestTask.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnowQueen.TestTask.WPF
{
    public class ProductsService
    {
        public void SaveProduct()
        {
            var testProduct = new ProductViewModel
            {
                Name = "Test product",
                Price = 24.99M,
                Amount = 3
            };

            //DBRepository<DataAccess.Entities.Product>.TestTask(testProduct.Name, testProduct.Price, testProduct.Amount);
            var products = new FileRepository<DataAccess.Entities.Product> ("/testFile.txt").Get();
        }


    }
}
