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
        //private readonly User _user;
        private double? _totalPrice;
        private bool _placeOrderSuccessful = false;


        public WinPlaceOrder()
        {
            InitializeComponent();
        }

        public WinPlaceOrder(Dictionary<Seller, List<Item>> items, double TotalPrice) : this()
        {
            this._items = items;
            this._totalPrice = TotalPrice;
            
        }

        public bool IsPlaceOrderComplete => _placeOrderSuccessful;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _placeOrderSuccessful = false;
            if (_totalPrice != null)
            {
                tbTotal.Text = "$" + _totalPrice.Value.ToString("F");
            }

            if (StaticValue.USER != null)
            {
                tbDeliveryAddress.Text = $" {StaticValue.USER.Ward}, {StaticValue.USER.District}, {StaticValue.USER.City}";
                tbDeliveryEmail.Text = StaticValue.USER.Email;
                tbDeliveryName.Text = StaticValue.USER.Name;
                tbDeliveryPhone.Text = StaticValue.USER.Phone;

            } 
                
            if (_items != null)
            {
                foreach (KeyValuePair<Seller, List<Item>> entry in _items)
                {
                    AddItems(entry.Key, entry.Value);
                }

            }
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

        //private void cbShippingChanel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    if (cbShippingChanel.SelectedIndex == 0)
        //    {
        //        _shippingFee = 5;
        //        tbShippingCost.Text = "$5.00";
        //        tbTotal.Text = "$" + CalculateTotalPrice().ToString("F");
        //    }
        //    else if (cbShippingChanel.SelectedIndex == 1)
        //    {
        //        _shippingFee = 10;
        //        tbShippingCost.Text = "$10.00";
        //        tbTotal.Text = "$" + CalculateTotalPrice().ToString("F");
        //    }
        //    else
        //    {
        //        _shippingFee = 0;
        //        tbShippingCost.Text = "$0.00";
        //        tbTotal.Text = "$" + CalculateTotalPrice().ToString("F");
        //    }
        //}

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Check if the user has entered details for delivery address
            if (tbDeliveryAddress.Text == "")
            {
                MessageBox.Show("Please enter your delivery address.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                List<Item> items = new List<Item>();
                foreach (KeyValuePair<Seller, List<Item>> pair in _items)
                {
                    items.AddRange(pair.Value);
                }
                new PurchasedItem_DAO().RequestItems(items, StaticValue.USER.Id_user,tbDeliveryAddress.Text, tbDeliveryName.Text, tbDeliveryPhone.Text,tbDeliveryEmail.Text);
            }

            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while placing the order. Please try again later.", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }

            finally
            {
                _placeOrderSuccessful = true;
                this.Close();
            }

        }

        private void tbDeliveryAddress_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Split the address into city, district, ward, and details and remove any leading/trailing spaces
            string[] addressParts = tbDeliveryAddress.Text.Split(',');

            int len = addressParts.Length;
            for (int i = len - 1; i >= 0; i--)
            {
                if (addressParts[i] != string.Empty) addressParts[i] = addressParts[i].Trim();
            }

            string city = addressParts[--len];
            string district = addressParts[--len];
            string ward = addressParts[--len];

            // Join the rest of the array
            string details = string.Join(",", addressParts.Take(len));

            WinAddressCustomization winAddressCustomization =
                new WinAddressCustomization(city, district, ward, details);
            if (winAddressCustomization.ShowDialog() == true)
            {
                var address = $"{winAddressCustomization.Details}, {winAddressCustomization.Ward}, {winAddressCustomization.District}, {winAddressCustomization.City}";
                tbDeliveryAddress.Text = address;
            }

        }
    }
}
