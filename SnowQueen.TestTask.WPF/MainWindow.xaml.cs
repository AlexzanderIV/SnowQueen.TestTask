using System;
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
using SnowQueen.TestTask.WPF.ProductsWebService;
using SnowQueen.TestTask.WPF.Repository;

namespace SnowQueen.TestTask.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ProductViewModel _product;

        private readonly ProductsWebServiceClient _wcfClient;

        public MainWindow()
        {
            InitializeComponent();
            UpdateBinding();

            _wcfClient = new ProductsWebServiceClient("BasicHttpBinding_IProductsWebService");
        }

        private void AddProduct(object sender, RoutedEventArgs e)
        {
            tblResult.Text = string.Empty;

            bool isSuccess = false;
            string errorMessage = string.Empty;

            // First try to call WCF-service to add product to the DB.
            // If it will fail, then at least we try to save product to the file.
            try
            {
                _wcfClient.AddProduct(_product.ToDataContract());
                isSuccess = true;
            }
            catch (Exception ex)
            {
                errorMessage += $"An error has occured while calling the WCF service: {ex.Message}{Environment.NewLine}";
            }

            try
            {
                // Save product to file.
                using (var repository = FileRepositoryFactory<DataAccess.Entities.Product>.Create())
                {
                    new ProductsService(repository).SaveProduct(_product.ToDto());
                }
                isSuccess = true;
            }
            catch (Exception ex)
            {
                errorMessage += $"An error has occured while saving to file: {ex.Message}{Environment.NewLine}";
            }

            if (isSuccess)
            {
                var message = "Product successfully added.";

                // If product was succesfully saved (either to DB or to file), but we get some error,
                // then add errorMessag to output.
                if (!string.IsNullOrEmpty(errorMessage))
                {
                    message += $" BUT{Environment.NewLine}{errorMessage}";
                }

                tblResult.Text = message;
                tblResult.Foreground = new SolidColorBrush(Colors.Green);
            }
            else
            {
                tblResult.Text = errorMessage;
                tblResult.Foreground = new SolidColorBrush(Colors.Red);
            }
            
            // Clear form.
            UpdateBinding();
        }

        private void UpdateBinding()
        {
            _product = new ProductViewModel();
            DataContext = _product;
        }
    }
}
