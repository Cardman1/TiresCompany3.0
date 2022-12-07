using Microsoft.Win32;
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
using System.Windows.Shapes;
using TiresCompany.Model;

namespace TiresCompany.View
{
    /// <summary>
    /// Логика взаимодействия для UpdateProductPage.xaml
    /// </summary>
    public partial class UpdateProductPage : Page
    {
        Core db = new Core();
        Product product;
        public UpdateProductPage()
        {
            InitializeComponent();
            TypeComboBox.ItemsSource = db.context.ProductType.ToList();
            product = new Product();
            DataContext = product;
        }
        public UpdateProductPage(Product product)
        {
            InitializeComponent();
            TypeComboBox.ItemsSource = db.context.ProductType.ToList();
            DataContext = product;
        }
     
        /// <summary>
        /// Изменение изображения
        /// </summary>
        private void ChangeImageButtonClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog winDialog = new OpenFileDialog();
            winDialog.InitialDirectory = @"c:\\";
            if (winDialog.ShowDialog() == true)
            {
                Uri sourse = new Uri(winDialog.FileName);
                ProductImage.Source = new BitmapImage(sourse);
            }
        }
    }
}
