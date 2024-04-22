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

namespace UTEMerchant
{
    /// <summary>
    /// Interaction logic for WinPlaceOrder.xaml
    /// </summary>
    public partial class WinPlaceOrder : Window
    {
        private readonly Dictionary<Seller, List<Item>> _items;
        private readonly User _user;
        private double _subTotalPrice;
        private double _shippingFee;
        private double _totalPrice;
        private bool _placeOrderSuccessful = false;


        public WinPlaceOrder()
        {
            InitializeComponent();
        }

        public WinPlaceOrder(Dictionary<Seller, List<Item>> items, User user) : this()
        {
            this._items = items;
            _user = user;
        }

        public bool IsPlaceOrderComplete => _placeOrderSuccessful;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (_user != null) tbDeliveryAddress.Text = $"{_user.District} {_user.Ward} {_user.City}";
            if (_items != null)
            {
                foreach (KeyValuePair<Seller, List<Item>> entry in _items)
                {
                    AddItems(entry.Key, entry.Value);
                }
                _subTotalPrice = CalculateSubTotalPrice();
                tbSubTotal.Text = "$" + _subTotalPrice.ToString("F");
            }
            cbShippingChanel.SelectedValue = "Standard Shipping";
        }

        private void AddItems(Seller seller, List<Item> items)
        {
            UC_PlaceOrderItemsBox box = new UC_PlaceOrderItemsBox(seller);
            foreach (Item item in items)
            {
                box.AddItem(item);
            }

            if (spItems .Children.Count == 0)
            {
                spItems.Children.Add(box);
            }
            else if (spItems.Children.Count == 1)
            {
                if (spItems.Children[0] is UC_PlaceOrderItemsBox first) first.Margin = new Thickness(0, 0, 0, 10);
                box.Margin = new Thickness(0, 10, 0, 0);
                spItems.Children.Add(box);
            }
            else if (spItems.Children.Count > 1)
            {
                if (spItems.Children[spItems.Children.Count - 1] is UC_PlaceOrderItemsBox last) last.Margin = new Thickness(0,10,0,10);
                box.Margin = new Thickness(0, 10, 0, 0);
                spItems.Children.Add(box);
            }
        }

        private double CalculateSubTotalPrice()
        {
            double totalPrice = 0;
            
            foreach (KeyValuePair<Seller, List<Item>> entry in _items)
            {
                foreach (Item item in entry.Value)
                {
                    totalPrice += item.Price;
                }
            }

            return totalPrice;
        }

        private void cbShippingChanel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbShippingChanel.SelectedValue.ToString() == "Standard Shipping")
            {
                _shippingFee = 5;
                tbShippingCost.Text = "$5.00";
                tbTotal.Text = "$" + CalculateTotalPrice().ToString("F");
            }
            else if (cbShippingChanel.SelectedValue.ToString() == "Express Shipping")
            {
                _shippingFee = 10;
                tbShippingCost.Text = "$10.00";
                tbTotal.Text = "$" + CalculateTotalPrice().ToString("F");
            }
            else
            {
                _shippingFee = 0;
                tbShippingCost.Text = "$0.00";
                tbTotal.Text = "$" + CalculateTotalPrice().ToString("F");
            }
        }

        private double CalculateTotalPrice()
        {
            return _subTotalPrice + _shippingFee;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _placeOrderSuccessful = true;
            this.Close();
        }
    }
}
