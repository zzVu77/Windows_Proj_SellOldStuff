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

namespace UTEMerchant
{
    /// <summary>
    /// Interaction logic for UCPurchased.xaml
    /// </summary>
    public partial class UCPurchased : UserControl
    {
        public UCPurchased()
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

        private void UC_ItemView_ItemClicked(object sender, EventArgs e)
        {
            if (sender is UC_ItemView clickedItem)
            {
                UC_ItemDetail detail = new UC_ItemDetail();
                // If the panel is empty, a new UC_ItemDetail is will be added
                if (dpSelectedItemDetailedInformation.Children.Count == 0)
                {
                    dpSelectedItemDetailedInformation.Children.Add(detail);
                }
                // If there exist one UC_ItemDetail but not the same as the recently clicked one then the old one will be replaced by the new one
                else if (dpSelectedItemDetailedInformation.Children.Count == 1 && dpSelectedItemDetailedInformation.Children[0] != detail)
                {
                    // Will have error because there is current no properties in UC_ItemDetail to recognize each other
                    dpSelectedItemDetailedInformation.Children[0] = detail;
                }
                // If there exist one UC_ItemDetai but the same as the recently clicked one the the existing one will be removed
                else if (dpSelectedItemDetailedInformation.Children.Count == 1 && dpSelectedItemDetailedInformation.Children[0] == detail)
                {
                    dpSelectedItemDetailedInformation.Children.Remove(detail);
                }
            }
        }

        //private void svItemsList_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        //{
        //    var clickedElement = e.OriginalSource as FrameworkElement;

        //    // Find the UC_ItemView parent
        //    var clickedItem = clickedElement.FindAncestor<UC_ItemView>();

        //    if (clickedItem != null)
        //    {
        //        // Do something with clickedItem
        //    }
        //}
    }
}
