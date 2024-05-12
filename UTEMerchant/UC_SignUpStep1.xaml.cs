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
    /// Interaction logic for UC_SignUpStep1.xaml
    /// </summary>
    public partial class UC_SignUpStep1 : UserControl
    {
        public event EventHandler NextButtonClicked;
        public string selectedCity;
        public string selectedDistrict;
        Address_DAO address_dao = new Address_DAO();
        public UC_SignUpStep1()
        {
            InitializeComponent();
            List<Address> distinctCities = address_dao.Load();
            cbPickupCity.Items.Clear();
            foreach (Address address in distinctCities)
            {
                cbPickupCity.Items.Add(address.City);
            }
        }
        private void cbPickupCity_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbPickupCity.SelectedItem != null)
            {
                selectedCity = cbPickupCity.SelectedItem.ToString();

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
        private void btnNext_Click(object sender, RoutedEventArgs e)
        {

            NextButtonClicked?.Invoke(this, EventArgs.Empty);
        }

        private void cbPickupDistrict_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbPickupCity.SelectedItem != null)
            {
                selectedDistrict = cbPickupCity.SelectedItem.ToString();
            }
        }
    }
}
