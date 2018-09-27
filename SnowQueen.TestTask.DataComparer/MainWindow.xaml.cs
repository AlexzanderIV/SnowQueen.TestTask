using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SnowQueen.TestTask.DataAccess.Services;
using SnowQueen.TestTask.DataComparer.ProductsWebService;

namespace SnowQueen.TestTask.DataComparer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ProductsWebServiceClient _wcfClient;

        public MainWindow()
        {
            InitializeComponent();
            _wcfClient = new ProductsWebServiceClient("BasicHttpBinding_IProductsWebService");
            LoadData();
        }

        private void LoadData()
        {
            var errorMessage = string.Empty;

            // Call WCF-service to get products from the DB.
            try
            {
                var productDataContracts = _wcfClient.GetProducts();
                dgProductsFromDb.ItemsSource = productDataContracts.Select(p => new ProductViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    Amount = p.Amount
                });
            }
            catch (Exception ex)
            {
                tblErrorMessageDb.Text = $"An error has occured while calling the WCF service: {ex.Message}{Environment.NewLine}";
            }

            try
            {
                using (var service =
                ProductsServiceFactory.CreateWithFileRepository(ConfigurationManager.AppSettings["FileStoragePath"]))
                {
                    var productsFromFile = service.GetAllProducts();
                    dgProductsFromFile.ItemsSource = productsFromFile.Select(p => new ProductViewModel
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Price = p.Price,
                        Amount = p.Amount
                    });
                }
            }
            catch (Exception ex)
            {
                tblErrorMessageFile.Text = $"An error has occured while loading from file: {ex.Message}{Environment.NewLine}";
            }
        }

        private void ReloadData(object sender, RoutedEventArgs e)
        {
            LoadData();
        }
    }
}
