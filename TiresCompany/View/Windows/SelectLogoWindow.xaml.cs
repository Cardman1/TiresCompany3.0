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

namespace TiresCompany.View.Windows
{
    /// <summary>
    /// Логика взаимодействия для SelectLogoWindow.xaml
    /// </summary>
    public partial class SelectLogoWindow : Window
    {
        public SelectLogoWindow()
        {
            InitializeComponent();
        }

       
        /// <summary>
        /// Выбор изображения
        /// </summary>
        
        private void AttachButtonClick(object sender, RoutedEventArgs e)
        {
            SelectLogoWindow win = new SelectLogoWindow();
           if (win.ShowDialog()==true)
            {

            }
        }
    }
}
