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
        private DeliveryAddress _deliveryAddress;
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
                _deliveryAddress = new DeliveryAddress_DAO().GetUserDefaultAddress(StaticValue.USER.Id_user);
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
            if (ucAddressBox.CheckNull)
            {
                MessageBox.Show("Please supply your delivery address.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                List<Item> items = new List<Item>();
                foreach (KeyValuePair<Seller, List<Item>> pair in _items)
                {
                    items.AddRange(pair.Value);
                }
                new PurchasedItem_DAO().RequestItems(items, StaticValue.USER.Id_user, $"{_deliveryAddress}, {_deliveryAddress.Ward}, {_deliveryAddress.District}, {_deliveryAddress.City}", _deliveryAddress.RecipientName, _deliveryAddress.RecipientPhone);
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

        private void grdDeliveryAddress_Loaded(object sender, RoutedEventArgs e)
        {
            if (_deliveryAddress != null)
            {
                ucAddressBox.SetData(_deliveryAddress);
                ucAddressBox.Visibility = Visibility.Visible;
                tbSelectDeliveryAddress.Visibility = Visibility.Collapsed;
            }
            else
            {
                ucAddressBox.Visibility = Visibility.Collapsed;
                tbSelectDeliveryAddress.Visibility = Visibility.Visible;
            }
        }

        private void tbSelectDeliveryAddress_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            WinAddressOptions winAddressOptions = new WinAddressOptions(StaticValue.USER.Id_user);
            winAddressOptions.ShowDialog();

            if (winAddressOptions.DialogResult == true)
            {
                _deliveryAddress = winAddressOptions.SelectedDeliveryAddress;
                grdDeliveryAddress_Loaded(this.grdDeliveryAddress, new RoutedEventArgs());
            }
        }

        private void ucAddressBox_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (_deliveryAddress == null) return;
            WinAddressOptions winAddressOptions = new WinAddressOptions(StaticValue.USER.Id_user, _deliveryAddress.ID);
            winAddressOptions.ShowDialog();

            if (winAddressOptions.DialogResult == true)
            {
                _deliveryAddress = winAddressOptions.SelectedDeliveryAddress;
                grdDeliveryAddress_Loaded(this.grdDeliveryAddress, new RoutedEventArgs());
            }
        }
    }
}
