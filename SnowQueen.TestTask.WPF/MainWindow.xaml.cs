using System;
using System.Collections.Generic;
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
        public MainWindow()
        {
            InitializeComponent();

            var testProduct = new ProductViewModel
            {
                Name = "Test product",
                Price = 24.99M,
                Amount = 3
            };

            using (var repository = new FileRepository<DataAccess.Entities.Product>("DB/testFile.txt"))
            {
                // TODO: DELETE
                var products = repository.Get();

                new ProductsService(repository).SaveProduct(testProduct.ToDto());
            }
        }
    }
}
