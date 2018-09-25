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
using SnowQueen.TestTask.WPF.Repository;

namespace SnowQueen.TestTask.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ProductViewModel product;

        public MainWindow()
        {
            InitializeComponent();
            UpdateBinding();
        }

        private void AddProduct(object sender, RoutedEventArgs e)
        {
            try
            {
                tblResult.Text = string.Empty;

                // TODO: Call WCF-service.

                // Save product to file.
                using (var repository = FileRepositoryFactory<DataAccess.Entities.Product>.Create())
                {
                    new ProductsService(repository).SaveProduct(product.ToDto());
                }

                tblResult.Text = "Product successfully added.";
                tblResult.Foreground = new SolidColorBrush(Colors.Green);
                // Clear form.
                UpdateBinding();
            }
            catch (Exception ex)
            {
                tblResult.Text = $"An error has occured: {ex.Message}";
                tblResult.Foreground = new SolidColorBrush(Colors.Red);
            }
        }

        private void UpdateBinding()
        {
            product = new ProductViewModel();
            DataContext = product;
        }
    }
}
