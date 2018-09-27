using SnowQueen.TestTask.DataAccess.Services;
using SnowQueen.TestTask.DataComparer.ProductsWebService;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace SnowQueen.TestTask.DataComparer
{
    public static class ProductsProvider
    {
        private static ProductsWebServiceClient WcfClient = new ProductsWebServiceClient("BasicHttpBinding_IProductsWebService");

        public static void AddProductsToDb(IEnumerable<ProductViewModel> products)
        {
            // If there are no products to add, then do nothing.
            if (products == null || !products.Any())
            {
                return;
            }

            var dataContracts = products.Select(p => new ProductDataContract
            {
                Name = p.Name,
                Price = p.Price,
                Amount = p.Amount
            }).ToArray();
            WcfClient.AddSeveralProducts(dataContracts);
        }

        public static IEnumerable<ProductViewModel> GetProductsFromDb()
        {
            var productDataContracts = WcfClient.GetProducts();
            return productDataContracts.Select(p => new ProductViewModel
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                Amount = p.Amount
            });
        }

        public static IEnumerable<ProductViewModel> GetFilteredProductsFromFile(IEnumerable<ProductViewModel> productsFromDb)
        {
            var productsFromFile = GetProductsFromFile();

            if (productsFromDb == null || !productsFromDb.Any())
            {
                return productsFromFile;
            }          

            return productsFromFile.Where(p => !productsFromDb.Contains(p, new ProductsEqualityComparer()));
        }

        private static IEnumerable<ProductViewModel> GetProductsFromFile()
        {
            using (var service =
                ProductsServiceFactory.CreateWithFileRepository(ConfigurationManager.AppSettings["FileStoragePath"]))
            {
                var productsFromFile = service.GetAllProducts();
                return productsFromFile.Select(p => new ProductViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    Amount = p.Amount
                });

            }
        }
    }

    class ProductsEqualityComparer : IEqualityComparer<ProductViewModel>
    {
        public ProductsEqualityComparer()
        {
        }

        public bool Equals(ProductViewModel x, ProductViewModel y)
        {
            if (x == null && y == null)
            {
                return true;
            }
            else if (x == null || y == null)
            {
                return false;
            }
            else if ((string.IsNullOrEmpty(x.Name) || x.Name.Equals(y.Name, StringComparison.OrdinalIgnoreCase)) &&
                     x.Price == y.Price &&
                     x.Amount == y.Amount)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public int GetHashCode(ProductViewModel obj)
        {
            return obj.Id.GetHashCode();
        }
    }
}
