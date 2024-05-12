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
    /// Interaction logic for WinAddressCustomization.xaml
    /// </summary>
    public partial class WinAddressCustomization : Window
    {
        private readonly bool _isEditing = false;

        private DeliveryAddress _deliveryAddress;

        public WinAddressCustomization()
        {
            InitializeComponent();
        }

        public WinAddressCustomization(bool isEditing = false) : this()
        {
            _deliveryAddress = new DeliveryAddress();
            _isEditing = isEditing;
            btnDeleteAddress.Visibility = Visibility.Collapsed;
        }

        public WinAddressCustomization(DeliveryAddress deliveryAddress, bool isEditing = true) : this()
        {
            _deliveryAddress = deliveryAddress;
            _isEditing = isEditing;
        }

        private void BtnSave_OnClick(object sender, RoutedEventArgs e)
        {
            _deliveryAddress.RecipientName = txtRecipientName.Text;
            _deliveryAddress.RecipientPhone = txtRecipientPhone.Text;
            _deliveryAddress.City = cbCity.Text;
            _deliveryAddress.District = cbDistrict.Text;
            _deliveryAddress.Ward = cbWard.Text;
            _deliveryAddress.StreetAddress = txtDetails.Text;
            if (cbSetAsDefault.IsChecked != null) _deliveryAddress.DefaultAddress = cbSetAsDefault.IsChecked.Value;
            else _deliveryAddress.DefaultAddress = false;

            if (_isEditing)
            {
                // Update the delivery address
                new DeliveryAddress_DAO().UpdateDeliveryAddress(_deliveryAddress);
            }
            else
            {
                // Create a new delivery address
                new DeliveryAddress_DAO().AddDeliveryAddress(_deliveryAddress.RecipientName,
                    _deliveryAddress.StreetAddress, _deliveryAddress.RecipientPhone, _deliveryAddress.City,
                    _deliveryAddress.District, _deliveryAddress.Ward, StaticValue.USER.Id_user,
                    _deliveryAddress.DefaultAddress);
            }
            

            // don't need to check if the address is valid or not
            // because the user can input any address they want
            // the address will be checked when the user place the order
            // so just return the address
            DialogResult = true;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (_deliveryAddress != null)
            {
                txtRecipientName.Text = _deliveryAddress.RecipientName;
                txtRecipientPhone.Text = _deliveryAddress.RecipientPhone;
                txtDetails.Text = _deliveryAddress.StreetAddress;
                cbCity.Text = _deliveryAddress.City;
                cbDistrict.Text = _deliveryAddress.District;
                cbWard.Text = _deliveryAddress.Ward;
                cbSetAsDefault.IsChecked = _deliveryAddress.DefaultAddress;
            }
        }

        public string RecipientName => _deliveryAddress.RecipientName;
        public string RecipientPhone => _deliveryAddress.RecipientPhone;
        public string Address => _deliveryAddress.StreetAddress;
        public string City => _deliveryAddress.City;
        public string District => _deliveryAddress.District;
        public string Ward => _deliveryAddress.Ward;

        private void BtnDeleteAddress_OnClick(object sender, RoutedEventArgs e)
        {
            new DeliveryAddress_DAO().RemoveDeliveryAddress(_deliveryAddress.ID);
            DialogResult = true;
        }
    }
}
