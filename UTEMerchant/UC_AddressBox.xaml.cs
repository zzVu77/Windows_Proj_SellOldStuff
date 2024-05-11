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
    /// Interaction logic for UC_AddressBox.xaml
    /// </summary>
    public partial class UC_AddressBox : UserControl
    {
        private DeliveryAddress _deliveryAddress;

        public UC_AddressBox()
        {
            InitializeComponent();
        }

        public UC_AddressBox(DeliveryAddress deliveryAddress) : this()
        {
            _deliveryAddress = deliveryAddress;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            // Check null
            if (_deliveryAddress == null) return;
            tbRecipientName.Text = _deliveryAddress.RecipientName;
            tbRecipientPhone.Text = _deliveryAddress.RecipientPhone;
            tbStreetAddress.Text = _deliveryAddress.StreetAddress;
            tbDivision.Text = $"{_deliveryAddress.Ward}, {_deliveryAddress.District}, {_deliveryAddress.City}";
        }

        public void SetData (DeliveryAddress deliveryAddress)
        {
            _deliveryAddress = deliveryAddress;
            UserControl_Loaded(this, new RoutedEventArgs());
        }



        public bool CheckNull => _deliveryAddress == null;

        public DeliveryAddress DeliveryAddress => _deliveryAddress;
    }
}
