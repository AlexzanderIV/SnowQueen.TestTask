using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace SnowQueen.TestTask.DataComparer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IEnumerable<ProductViewModel> filteredProductsFromFile;

        public MainWindow()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            var errorMessage = string.Empty;

            // This needs to filter out products from the File.
            IEnumerable<ProductViewModel> productsFromDb = null;

            // Call WCF-service to get products from the DB.
            try
            {
                productsFromDb = ProductsProvider.GetProductsFromDb().ToList();
                dgProductsFromDb.ItemsSource = productsFromDb;
            }
            catch (Exception ex)
            {
                tblErrorMessageDb.Text = $"An error has occured while calling the WCF service: {ex.Message}{Environment.NewLine}";
            }

            // Read products from the File.
            try
            {
                filteredProductsFromFile = ProductsProvider.GetFilteredProductsFromFile(productsFromDb).ToList();
                dgProductsFromFile.ItemsSource = filteredProductsFromFile;
            }
            catch (Exception ex)
            {
                tblErrorMessageFile.Text = $"An error has occured while loading from file: {ex.Message}{Environment.NewLine}";
            }
        }

        private void ReloadData(object sender, RoutedEventArgs e)
        {
            var button = ((Button)sender);
            button.IsEnabled = false;

            LoadData();

            button.IsEnabled = true;
        }

        private void AddProductsToDb(object sender, RoutedEventArgs e)
        {
            var button = ((Button)sender);
            button.IsEnabled = false;

            ProductsProvider.AddProductsToDb(filteredProductsFromFile);
            LoadData();

            button.IsEnabled = true;
        }
    }
}
