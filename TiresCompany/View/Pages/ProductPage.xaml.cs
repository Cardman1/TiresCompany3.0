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
using TiresCompany.Model;


namespace TiresCompany.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для ProductPage.xaml
    /// </summary>
    public partial class ProductPage : Page
    {
        Core db = new Core();
        List<ProductType> productTypes;
        bool reverseType;
        int countPage = 0;
        //количество элементов на странице
        int countElement = 10;
        int page = 1;
        public ProductPage()
        {
            InitializeComponent();
            //сортировка 
            List<string> sotrTypeList = new List<string>()
            {
                "наименование","остаток на складе","стоимость"
            };
            SortComboBox.ItemsSource = sotrTypeList;
            //фильтрация
            productTypes = new List<ProductType>
            {
                new ProductType()
                {
                    ID=0,
                    Title="Все типы"
                }
            };
            productTypes.AddRange(db.context.ProductType.ToList());
            FilterComboBox.ItemsSource = productTypes;
            UpdateUI();
        }

        private void UpdateUI()
        {
            if (GetRows().Count > 10)
            {
                DisplayPagination(page);
                /*
                 * 0..9 на странице 1 (1-1)*10
                    10..19 на странице 2 (2-1)*10
                    20..29 на странице 3 (3-1)*10
                    30..39 на странице 4 (4-1)*10
                 */
                List<Product> displayProduct = GetRows().Skip((page-1)*countElement).Take(countElement).ToList();
            }
        }

        private List<Product> GetRows()
        {
            List<Product> arrayProduct = db.context.Product.ToList();
            return arrayProduct;
            

        }
        /// <summary>
        /// формирование количество страниц для вывода в пагинации
        /// </summary>
        /// <returns></returns>
        private int GetPagesCount()
        {
            
            int count = GetRows().Count;
            //формирование строк для постраничного вывода
            if (count > countElement)
            {
                //количество страниц в пагинации
                countPage = Convert.ToInt32(Math.Ceiling(count * 1.0 / countElement));
            }
            return countPage;
        }
        /// <summary>
        /// Вывод кнопок пагинации
        /// </summary>
        public void DisplayPagination(int page)
        {
            List<PageItem> source = new List<PageItem>();
            for (int i = 1; i <= GetPagesCount(); i++)
            {
                source.Add(new PageItem(i, i == page));
            }
            //кнопка страниц
            PaginationListView.ItemsSource = source;
            //если последующая/предыдущая недоступны, то кнопки "<" ">" становятся невидимыми
            PrevTextBlock.Visibility = (page <= 1 ? Visibility.Hidden : Visibility.Visible);
            NextTextBlock.Visibility = (page >= GetPagesCount() ? Visibility.Hidden : Visibility.Visible);
        }
        /// <summary>
        /// Переход на предущую страницу
        /// </summary>
       public void PrevTextBlockMouseLeftButtonDown (object sender, MouseButtonEventArgs e)
        {
            if (page <=1)
            {
                page = 1;
                PrevTextBlock.Visibility = Visibility.Hidden;
            }
            else
            {
                page = 1;
                PrevTextBlock.Visibility = Visibility.Visible;
            }
            UpdateUI();
        }
       
    /// <summary>
    /// Выбор активной страницы
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void PaginationTextBlockMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock activePage = sender as TextBlock;
            page = Convert.ToInt32(activePage.Text);
            UpdateUI();
        }
        private void SearchTextBoxGotFocus(object sender, RoutedEventArgs e)
        {
            SearchTextBox.Text = "";
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateUI();
        }

        private void FilterComboBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateUI();
        }

        private void ReverseButtonClick(object sender, RoutedEventArgs e)
        {
            reverseType = !reverseType;
            UpdateUI();
        }

        private void SortComboBoxSelectionChanget(object sender, SelectionChangedEventArgs e)
        {
            UpdateUI();
        }

        private void AddProductButtonClick(object sender, RoutedEventArgs e)
        {
            Product item = ProductListView.SelectedItem as Product;
            if (item == null)
            {
                this.NavigationService.Navigate(new UpdateProductPage());
            }
            else
            {
                this.NavigationService.Navigate(new UpdateProductPage(item));
            }
            
        }
        /// <summary>
        /// Изменение кнопки "Добавить" на "Редактировать"
        /// </summary>

        private void ProductListViewSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AddProductButton.Content = "Редактировать товар";
        }

        /// <summary>
        /// Переход на следущую страницу
        /// </summary>
        private void AddProductButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

           
        }

        private void NextTextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (page >= GetPagesCount())
            {
                page = GetPagesCount();
                NextTextBlock.Visibility = Visibility.Visible;
            }
            else
            {
                page += 1;
                NextTextBlock.Visibility = Visibility.Visible;
            }
            UpdateUI();
        }
    }
}
