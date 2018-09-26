using SnowQueen.TestTask.DataAccess.Services;
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

namespace SnowQueen.TestTask.DataComparer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // TODO: Call WCF-service

            using (var service = 
                ProductsServiceFactory.CreateWithFileRepository(ConfigurationManager.AppSettings["FileStoragePath"]))
            {
                var productsFromFile = service.GetAllProducts();
            }
        }
    }
}
