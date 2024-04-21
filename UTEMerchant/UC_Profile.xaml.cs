using HandyControl.Controls;
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
    /// Interaction logic for UC_Profile.xaml
    /// </summary>
    public partial class UC_Profile : UserControl
    {
        Address_DAO address_dao = new Address_DAO();
        List<Address> distinctCities;
        public UC_Profile()
        {
            InitializeComponent();
            SetDefault();
        }

        public void SetDefault()
        {
            distinctCities = address_dao.Load();
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
                string selectedCity = cbPickupCity.SelectedItem.ToString();

                // Filter districts based on selected city
                var filteredDistricts = address_dao.getdistrict(selectedCity);

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

        private void cbPickupDistrict_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }  
              

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            cbPickupCity.Visibility = Visibility.Visible;
            txtUserCity.Visibility = Visibility.Collapsed;
            
            for (int i = 0; i < distinctCities.Count; i++)
            {
                if (distinctCities[i].City==txtUserCity.Text)
                {
                    cbPickupCity.SelectedIndex = i;
                    break;
                }    
            }
            
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            string selectedValue = cbPickupCity.SelectedItem.ToString();
            txtUserCity.Text = selectedValue;
            cbPickupCity.Visibility = Visibility.Collapsed;
            txtUserCity.Visibility = Visibility.Visible;
        }
    }
}
