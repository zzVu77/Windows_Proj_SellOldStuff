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
    /// Interaction logic for WinBuyingInterface.xaml
    /// </summary>
    public partial class WinBuyingInterface : Window
    {
        public event EventHandler ItemClicked;
        public Item info;
        List<User> users = new List<User>();
        user_DAO user_dao = new user_DAO();
        List<Address> addresses;
        public WinBuyingInterface()
        {
            InitializeComponent();
        }
        public WinBuyingInterface(Item item)
        {
            info = item;
            var user_dao = new user_DAO();
            users = user_dao.Load();
            InitializeComponent();
            SetDefault();
        }
        private void SetDefault()
        {
            txblOrderItemName.Text = info.Name;
            txblOrderOriginalPrice.Text = info.Original_Price.ToString()+"$";
            txblOrderPrice.Text = info.Price.ToString() + "$";
            txblConditon.Text = info.Condition.ToString()+"%";
            txblTotalValue.Text = info.Price.ToString() + "$";
            var resourceUri = new Uri(info.Image_Path, UriKind.RelativeOrAbsolute);
            imgOrderItem.Source = new BitmapImage(resourceUri);

            addresses = new DB_Connection().LoadData<Address>("Address");
            var distinctCities = addresses.Select(a => a.City).Distinct().ToList();
            
            cbPickupCity.Items.Clear();
            foreach (var city in distinctCities)
            {
                cbPickupCity.Items.Add(city);

            }
        }

        private void cbPickupCity_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbPickupCity.SelectedItem != null)
            {
                string selectedCity = cbPickupCity.SelectedItem.ToString();

                // Filter districts based on selected city
                var filteredDistricts = addresses.Where(a => a.City == selectedCity)
                                                .Select(a => a.District)
                                                .Distinct().ToList()
                                                ;

                // Update district ComboBox
                cbPickupDistrict.Items.Clear();
                cbPickupDistrict.ItemsSource = filteredDistricts;
                cbPickupDistrict.SelectedIndex = -1; 
            }
        }
    }
}
