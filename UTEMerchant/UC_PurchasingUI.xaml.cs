using System;
using System.Collections.Generic;
using System.Globalization;
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
using System.Windows.Media.Effects;
using System.Windows.Media.Animation;
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
        private readonly Item_DAO _itemDao = new Item_DAO();
        private readonly Seller_DAO _sellerDao = new Seller_DAO();
        List<Item> _items = new List<Item>();
        public int IdUser { get; set; }


        public UC_PurchasingUI()
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

        private void imgFilter_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (dpRightSideBar.Visibility == Visibility.Collapsed)
            {
                dpRightSideBar.Visibility = Visibility.Visible;
            }
            else dpRightSideBar.Visibility = Visibility.Collapsed;
        }


        private void OnItemButtonAddToCartClicked(object sender, RoutedEventArgs e)
        {
            // The button in UC_ItemView was clicked!
            if (sender is UC_ItemView clickedItemView)
            {
                Item clickedItem = clickedItemView.info;

                // Find existing seller view, otherwise create a new one
                var sellerView = uc_ShoppingCart.spItems.Children.OfType<UC_ShoppingCartItemsView>()
                    .FirstOrDefault(seller => seller.GetSeller().SellerID == clickedItem.SellerID);

                if (sellerView == null)
                {
                    sellerView = new UC_ShoppingCartItemsView(_sellerDao.GetSeller(clickedItem.SellerID));
                    uc_ShoppingCart.spItems.Children.Add(sellerView);
                }

                // Check if the item is already in the cart
                if (sellerView.spItems.Children.OfType<UC_ItemInShoppingCartItemsView>()
                    .Any(item => item.GetItem().Item_Id == clickedItem.Item_Id))
                {
                    return;
                }
                sellerView.AddItem(clickedItem);
                sellerView.spItems.Children.OfType<UC_ItemInShoppingCartItemsView>().Last().TogItemChecked += RecalculateTotalPrice;
                sellerView.spItems.Children.OfType<UC_ItemInShoppingCartItemsView>().Last().TogItemUnchecked += RecalculateTotalPrice;
                uc_ShoppingCart.CheckCart();
            }
        }

        private void wpItemsList_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is UC_ItemView clickedItem)
            {
                Seller seller = _sellerDao.GetSeller(clickedItem.info.SellerID);
                WinDeltailItem winDeltailItem = new WinDeltailItem(clickedItem.info, seller, IdUser);
                winDeltailItem.ShowDialog();
                wpItemsList.Children.Clear();

                _items = _itemDao.Load();
                _items.Sort((item1, item2) => item1.Sale_Status.CompareTo(item2.Sale_Status));
                foreach (Item item in _items)
                {
                    UC_ItemView uc_item = new UC_ItemView(item);
                    uc_item.MouseLeftButtonDown += wpItemsList_MouseLeftButtonDown;
                    wpItemsList.Children.Add(uc_item);
                }
            }
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            this.wpItemsList.Children.Clear();
            
            foreach (Item item in new Item_DAO().Load())
            {
                UC_ItemView uc_item = new UC_ItemView(item);
                uc_item.MouseLeftButtonDown += wpItemsList_MouseLeftButtonDown;
                wpItemsList.Children.Add(uc_item);

            }
        }

        public void imgShoppingCart_MouseDown(object sender, MouseButtonEventArgs e)
        {
            grdPurchasingInterface.IsEnabled = false;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            grdPurchasingInterface.IsEnabled = true;
        }


        private void uc_ShoppingCart_Loaded(object sender, RoutedEventArgs e)
        {
            uc_ShoppingCart.CheckCart();
        }

        private void RecalculateTotalPrice(object sender, RoutedEventArgs e)
        {
            if (sender is UC_ItemInShoppingCartItemsView item)
            {
                double total = Double.Parse(tbTotalPriceValue.Text);

                if (item.togItem.IsChecked == true)
                {
                    total += item.GetItem().Price;
                }
                else
                {
                    total -= item.GetItem().Price;
                }

                tbTotalPriceValue.Text = total.ToString(CultureInfo.CurrentCulture);
            }
        }

        private void btnCheckout_Click(object sender, RoutedEventArgs e)
        {
            Dictionary<Seller, List<Item>> items = new Dictionary<Seller, List<Item>>();
            foreach (UC_ShoppingCartItemsView sellerView in uc_ShoppingCart.spItems.Children.OfType<UC_ShoppingCartItemsView>())
            {
                items.Add(sellerView.GetSeller(), sellerView.GetSelectedItems());
            }

            WinPlaceOrder winPlaceOrder = new WinPlaceOrder(items, new user_DAO().GetUserByID(IdUser));
            winPlaceOrder.ShowDialog();

            if (winPlaceOrder.IsPlaceOrderComplete)
            {
                uc_ShoppingCart.spItems.Children.Clear();
                tbTotalPriceValue.Text = "0";
            }
        }

        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            InitializeComponent();
            wpItemsList.Children.Clear();

            _items = _itemDao.Load();
            _items.Sort((item1, item2) => item1.Sale_Status.CompareTo(item2.Sale_Status));
            foreach (Item item in _items)
            {
                UC_ItemView uc_item = new UC_ItemView(item);
                uc_item.MouseLeftButtonDown += wpItemsList_MouseLeftButtonDown;
                wpItemsList.Children.Add(uc_item);
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            wpItemsList.Children.Clear();
            _items = _itemDao.Load();
            _items.Sort((item1, item2) => item1.Sale_Status.CompareTo(item2.Sale_Status));
            foreach (Item item in _items)
            {
                UC_ItemView uc_item = new UC_ItemView(item);
                uc_item.ItemClicked += OnItemButtonAddToCartClicked;
                uc_ShoppingCart.CheckCart();
                uc_item.MouseLeftButtonDown += wpItemsList_MouseLeftButtonDown;
                wpItemsList.Children.Add(uc_item);
            }
        }

        private void txtSearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            
            if (!string.IsNullOrWhiteSpace(txtSearchBox.Text))
            {
                List<Item> items = _itemDao.Search(txtSearchBox.Text);
                if (items.Count > 0)
                {
                    wpItemsList.Children.Clear();
                    foreach (Item item in items)
                    {
                        UC_ItemView uc_item = new UC_ItemView(item);
                        uc_item.ItemClicked += OnItemButtonAddToCartClicked;
                        uc_ShoppingCart.CheckCart();
                        uc_item.MouseLeftButtonDown += wpItemsList_MouseLeftButtonDown;
                        wpItemsList.Children.Add(uc_item);
                    }
                }
            }
            else
            {
                wpItemsList.Children.Clear();
                foreach (Item item in _items)
                {
                    UC_ItemView uc_item = new UC_ItemView(item);
                    uc_item.ItemClicked += OnItemButtonAddToCartClicked;
                    uc_ShoppingCart.CheckCart();
                    uc_item.MouseLeftButtonDown += wpItemsList_MouseLeftButtonDown;
                    wpItemsList.Children.Add(uc_item);
                }
            }
        }
    }
}
