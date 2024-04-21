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
        private string image_path;
        public UC_Profile()
        {
            InitializeComponent();            
        }

        public void SetDefault(User user)
        {
            distinctCities = address_dao.Load();
            cbPickupCity.Items.Clear();
            foreach (Address address in distinctCities)
            {
                cbPickupCity.Items.Add(address.City);
            }
            txtFullName.Text = user.Name;
            txtUserID.Text = user.Id_user.ToString();
            txtUserEmail.Text=user.Email;
            txtUserPhoneNumber.Text = user.Phone.ToString();
            txtUserCity.Text = user.City;
            txtUserDistrict.Text = user.District;
            txtUserWard.Text = user.Ward;

            var resourceUri = new Uri(user.Image_Path, UriKind.RelativeOrAbsolute);
            imgUserPhoto.Source = new BitmapImage(resourceUri);

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
            cbPickupDistrict.Visibility = Visibility.Visible;
            txtUserDistrict.Visibility = Visibility.Collapsed;

            for (int i = 0; i < distinctCities.Count; i++)
            {
                if (distinctCities[i].City==txtUserCity.Text)
                {
                    cbPickupCity.SelectedIndex = i;
                    break;
                }    
            }

            for (int i = 0; i < distinctCities.Count; i++)
            {
                if (distinctCities[i].District == txtUserDistrict.Text)
                {
                    cbPickupDistrict.SelectedIndex = i; //Chỗ này bị lỗi tại trong combobox của District chỉ có mỗi Nha Trang, Fix lại chỗ này sau
                    break;
                }
            }

            btnSave.Visibility = Visibility.Visible;

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            string selectedValue = cbPickupCity.SelectedItem.ToString();
            txtUserCity.Text = selectedValue;

            selectedValue = cbPickupDistrict.SelectedItem.ToString();
            txtUserDistrict.Text = selectedValue;

            cbPickupCity.Visibility = Visibility.Collapsed;
            txtUserCity.Visibility = Visibility.Visible;
            cbPickupDistrict.Visibility = Visibility.Collapsed;
            txtUserDistrict.Visibility = Visibility.Visible;

            btnSave.Visibility = Visibility.Collapsed;
        }

        private void btnChangePhoto_Click(object sender, RoutedEventArgs e)
        {
            string image_path;
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Filter = "Tất cả các tệp (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                string selectedFilePath = openFileDialog.FileName;
                image_path = selectedFilePath;
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(image_path);
                bitmap.EndInit();
                imgUserPhoto.Source = bitmap;
                // Xử lý đường dẫn đã chọn ở đây
            }
        }
    }
}
