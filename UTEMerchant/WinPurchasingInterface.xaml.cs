using System;
using System.Collections.Generic;
using System.Globalization;
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


namespace UTEMerchant
{
    /// <summary>
    /// Interaction logic for Purchasing_Interface.xaml
    /// </summary>
    public partial class Purchasing_Interface : Window
    {
        public Purchasing_Interface()
        {
            InitializeComponent();
        }

        private void XIcon_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtSearchBox.Text = "";
        }

        private void btnRelevance_Click(object sender, RoutedEventArgs e)
        {
            if (btnRelevance.Background == Brushes.Transparent)
            {
                btnRelevance.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#EAAC8B");
            }
            else btnRelevance.Background = Brushes.Transparent;
        }

        private void btnPopular_Click(object sender, RoutedEventArgs e)
        {
            if (btnPopular.Background == Brushes.Transparent)
            {
                btnPopular.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#EAAC8B");
            }
            else btnPopular.Background = Brushes.Transparent;
        }

        private void btnPrice_Click(object sender, RoutedEventArgs e)
        {
            if (btnPrice.Background == Brushes.Transparent)
            {
                btnPrice.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#EAAC8B");
            }
            else btnPrice.Background = Brushes.Transparent;
        }

        private void svItemsList_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            var Item = e.OriginalSource as FrameworkElement;
            UC_ItemDetail uC_ItemDetail = new UC_ItemDetail();
            if (Item != null && dpSelectedItemDetailedInformation.Children.Count != 1)
            {
                dpSelectedItemDetailedInformation.Children.Add(uC_ItemDetail);
            }
        }

        private void btnSellerMode_Click(object sender, RoutedEventArgs e)
        {
            WinSellerInterface winSellerInterface = new WinSellerInterface();
            Hide();
            winSellerInterface.ShowDialog();
            Show();
        }


        private void tbSeller_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Hide();
            var seller = new WinSellerInterface();
            seller.ShowDialog();
        }

        //public int RowCount
        //{
        //    get
        //    {
        //        // Calculate the number of rows based on the number of items and columns
        //        int itemCount = ugItems.Children.Count;
        //        int columns = ugItems.Columns;
        //        return (itemCount + columns - 1) / columns;
        //    }
        //}
    }
}
