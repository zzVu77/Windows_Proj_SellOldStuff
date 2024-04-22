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
    /// Interaction logic for UC_SellerProfile.xaml
    /// </summary>
    public partial class UC_SellerProfile : UserControl
    {
        Address_DAO address_dao = new Address_DAO();
        List<Address> distinctCities;
        private string image_path;
        public UC_SellerProfile()
        {
            InitializeComponent();
        }
        public void SetDefault(User user, Seller seller)
        {
            distinctCities = address_dao.Load();
            cbPickupCity.Items.Clear();
            foreach (Address address in distinctCities)
            {
                cbPickupCity.Items.Add(address.City);
            }
            txtSellerShopName.Text = seller.ShopName;
            txtSellerID.Text = seller.Id_user.ToString();
            txtSellerShopEmail.Text = user.Email;
            txtSellerPhoneNumber.Text = seller.Phone.ToString();
            txtSellerCity.Text = seller.City;
            txtSellerDistrict.Text = seller.District;
            txtSellerWard.Text = seller.Ward;

            var resourceUri = new Uri(user.Image_Path, UriKind.RelativeOrAbsolute);
            imgSellerPhoto.Source = new BitmapImage(resourceUri);

        }

        private void ChangeTextBoxBackGroundEdit()
        {
            txtSellerShopName.Background = Brushes.White;
            txtSellerShopEmail.Background = Brushes.White;
            txtSellerPhoneNumber.Background = Brushes.White;
            txtSellerWard.Background = Brushes.White;
        }
        private void ChangeTextBoxBackGroundSave()
        {
            txtSellerShopName.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#f4f4f4"));
            txtSellerShopEmail.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#f4f4f4"));
            txtSellerPhoneNumber.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#f4f4f4"));
            txtSellerWard.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#f4f4f4"));
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
            txtSellerCity.Visibility = Visibility.Collapsed;
            cbPickupDistrict.Visibility = Visibility.Visible;
            txtSellerDistrict.Visibility = Visibility.Collapsed;

            txtSellerShopName.IsReadOnly = false;
              txtSellerShopEmail.IsReadOnly= false;
            txtSellerPhoneNumber.IsReadOnly= false;
                   txtSellerWard.IsReadOnly= false;

            ChangeTextBoxBackGroundEdit();

            for (int i = 0; i < distinctCities.Count; i++)
            {
                if (distinctCities[i].City == txtSellerCity.Text)
                {
                    cbPickupCity.SelectedIndex = i;
                    break;
                }
            }

            for (int i = 0; i < distinctCities.Count; i++)
            {
                if (distinctCities[i].District == txtSellerDistrict.Text)
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
            txtSellerCity.Text = selectedValue;

            selectedValue = cbPickupDistrict.SelectedItem.ToString();
            txtSellerDistrict.Text = selectedValue;

            txtSellerShopName.IsReadOnly = true;
            txtSellerShopEmail.IsReadOnly = true;
            txtSellerPhoneNumber.IsReadOnly = true;
            txtSellerWard.IsReadOnly = true;

            ChangeTextBoxBackGroundSave();

            cbPickupCity.Visibility = Visibility.Collapsed;
            txtSellerCity.Visibility = Visibility.Visible;
            cbPickupDistrict.Visibility = Visibility.Collapsed;
            txtSellerDistrict.Visibility = Visibility.Visible;

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
                imgSellerPhoto.Source = bitmap;
                // Xử lý đường dẫn đã chọn ở đây
            }
        }

        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (this.Visibility == Visibility.Collapsed)
            {
                btnSave.Visibility = Visibility.Collapsed;
            }
        }
    }
}
