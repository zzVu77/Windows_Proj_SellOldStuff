using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
using System.Xml.Linq;

namespace UTEMerchant
{
    /// <summary>
    /// Interaction logic for UC_PurchasingUI.xaml
    /// </summary>
    public partial class UC_PurchasingUI : UserControl
    {
        private Item_DAO Item_dao = new Item_DAO();
        List<Item> items = new List<Item>();
        public UC_PurchasingUI()
        {
            InitializeComponent();
            wpItemsList.Children.Clear();
            Item_dao.Load();
            items = Item_dao.items;
            foreach (Item item in items)
            {
                UC_ItemView uc_item = new UC_ItemView(item);
                uc_item.ItemClicked += OnItemButtonAddToCartClicked;
                uc_item.MouseLeftButtonDown += wpItemsList_MouseLeftButtonDown;
                wpItemsList.Children.Add(uc_item);
            }
            
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

        private void imgFilter_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (dpRightSideBar.Visibility == Visibility.Collapsed)
            {
                if (uc_ShoppingCart.Visibility == Visibility.Visible) uc_ShoppingCart.Visibility = Visibility.Collapsed;
                dpRightSideBar.Visibility = Visibility.Visible;
            }
            else dpRightSideBar.Visibility = Visibility.Collapsed;
        }
        

        private void OnItemButtonAddToCartClicked(object sender, EventArgs e)
        {
            // The button in UC_ItemView was clicked!
            if (sender is UC_ItemView clickedItemView)
            {
                UC_ShoppingCartItemView uc_ShoppingCartItemView = new UC_ShoppingCartItemView(clickedItemView.info);
                uc_ShoppingCart.spItems.Children.Add(uc_ShoppingCartItemView);
            }
        }

        private void wpItemsList_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is UC_ItemView clickedItem)
            {                
                UC_ItemDetail itemDetail = new UC_ItemDetail(clickedItem.info);
                // If the panel is empty, a new UC_ItemDetail is will be added
                if (dpSelectedItemDetailedInformation.Children.Count == 0)
                {
                    dpSelectedItemDetailedInformation.Children.Add(itemDetail);
                }
                // If there exist one UC_ItemDetail but not the same as the recently clicked one, then the old one will be replaced by the new one
                else if (dpSelectedItemDetailedInformation.Children.Count == 1 && dpSelectedItemDetailedInformation.Children[0] != itemDetail)
                {
                    dpSelectedItemDetailedInformation.Children.Clear();
                    dpSelectedItemDetailedInformation.Children.Add(itemDetail);

                }
                // If there exist one UC_ItemDetail but the same as the recently clicked one, then the existing one will be removed
                else if (dpSelectedItemDetailedInformation.Children.Count == 1 && dpSelectedItemDetailedInformation.Children[0] == itemDetail)
                {
                    dpSelectedItemDetailedInformation.Children.Remove(itemDetail);
                }
            }
        }

        private void ClickedItem_ItemClicked(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        public void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            InitializeComponent();
            wpItemsList.Children.Clear();
            Item_dao.Load();
            items = Item_dao.items;
            foreach (Item item in items)
            {
                UC_ItemView uc_item = new UC_ItemView(item);
                uc_item.MouseLeftButtonDown += wpItemsList_MouseLeftButtonDown;
                wpItemsList.Children.Add(uc_item);
            }
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            this.wpItemsList.Children.Clear();
            Item_DAO dAO = new Item_DAO();
            dAO.Load();
            List<Item> itemsRefreshh = dAO.items;
            foreach (Item item in itemsRefreshh)
            {
                UC_ItemView uc_item = new UC_ItemView(item);
                uc_item.MouseLeftButtonDown += wpItemsList_MouseLeftButtonDown;
                wpItemsList.Children.Add(uc_item);
            }
        }

        private void imgShoppingCart_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (uc_ShoppingCart.Visibility == Visibility.Collapsed )
            {
                if(dpRightSideBar.Visibility == Visibility.Visible) dpRightSideBar.Visibility = Visibility.Collapsed;
                uc_ShoppingCart.Visibility = Visibility.Visible;
               
            }
            else uc_ShoppingCart.Visibility = Visibility.Collapsed;
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
