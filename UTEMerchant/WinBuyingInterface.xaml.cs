using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
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
    /// Interaction logic for WinBuyingInterface.xaml
    /// </summary>
    public partial class WinBuyingInterface : Window
    {
        public event EventHandler ItemClicked;
        public Item info;
        List<User> users = new List<User>();
        User_DAO user_dao = new User_DAO();
        Address_DAO address_dao = new Address_DAO();
        private int Id_User;
        public WinBuyingInterface()
        {
            InitializeComponent();
        }
        public WinBuyingInterface(Item item, int id_user)
        {
            info = item;
            Id_User = id_user;
            var user_dao = new User_DAO();
            users = user_dao.Load();
            InitializeComponent();
            SetDefault();
        }
        private void SetDefault()
        {
            txblOrderItemName.Text = info.name;
            txblOrderOriginalPrice.Text = info.original_price.ToString()+"$";
            txblOrderPrice.Text = info.price.ToString() + "$";
            txblConditon.Text = info.condition.ToString()+"%";
            txblTotalValue.Text = info.price.ToString() + "$";
            var resourceUri = new Uri(info.image_path, UriKind.RelativeOrAbsolute);
            imgOrderItem.Source = new BitmapImage(resourceUri);
            List<Address> distinctCities = address_dao.Load();
            cbPickupCity.Items.Clear();
            foreach (Address address in distinctCities)
            {
                cbPickupCity.Items.Add(address.City);
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new PurchasedItem_DAO().RequestItems(new List<Item> { info }, Id_User, txtDeliveryAddress.Text, cbPickupWard.Text, cbPickupDistrict.Text, cbPickupCity.Text);
            this.Close();
        }

        private void cbPickupCity_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbPickupCity.SelectedItem != null)
            {
                string selectedCity = cbPickupCity.SelectedItem.ToString();

                // Filter districts based on selected city
                var filteredDistricts = address_dao.GetDistricts(selectedCity);

                // Update district ComboBox
                if (cbPickupCity.Items != null)
                    cbPickupDistrict.Items.Clear();
                foreach (var district in filteredDistricts)
                {
                    cbPickupDistrict.Items.Add(district);
                }
                cbPickupDistrict.SelectedIndex = -1;
            }
        }
    }
}
