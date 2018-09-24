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
using SnowQueen.TestTask.DataAccess;
using SnowQueen.TestTask.DataAccess.Services;

namespace SnowQueen.TestTask.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ProductViewModel product = new ProductViewModel();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = product;
        }

        private void AddProduct(object sender, RoutedEventArgs e)
        {
            using (var repository = new FileRepository<DataAccess.Entities.Product>(ConfigurationManager.AppSettings["FileStoragePath"]))
            {
                new ProductsService(repository).SaveProduct(product.ToDto());
            }
        }
    }
}
