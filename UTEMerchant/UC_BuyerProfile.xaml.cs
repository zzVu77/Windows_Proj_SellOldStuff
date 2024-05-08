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
using System.Windows.Media;
using System.Xml.Linq;

namespace UTEMerchant
{
    /// <summary>
    /// Interaction logic for UC_BuyerProfile.xaml
    /// </summary>
    public partial class UC_BuyerProfile : UserControl
    {
        public event EventHandler SavedButtonClicked;
        //User user =new User();
        string selectedCity;
        string selectedDistrict;
        user_DAO user_dao = new user_DAO();
        Address_DAO address_dao = new Address_DAO();
        List<Address> distinctCities;
        public string image_path;
        public UC_BuyerProfile()
        {
            InitializeComponent();
        }

        public void SetDefault()
        {
            
            distinctCities = address_dao.Load();
            cbPickupCity.Items.Clear();
            foreach (Address address in distinctCities)
            {
                cbPickupCity.Items.Add(address.City);
            }
            txtUserFullName.Text = StaticValue.USER.Name;      
            txtUserID.Text = StaticValue.USER.Id_user.ToString();
            txtUserEmail.Text = StaticValue.USER.Email;
            txtUserPhoneNumber.Text = StaticValue.USER.Phone.ToString();
            txtUserCity.Text = StaticValue.USER.City;
            txtUserDistrict.Text = StaticValue.USER.District;
            txtUserWard.Text = StaticValue.USER.Ward;
            image_path = StaticValue.USER.Image_Path;
            var resourceUri = new Uri(image_path, UriKind.RelativeOrAbsolute);
            imgUserPhoto.Source = new BitmapImage(resourceUri);

        }

        private void ChangeTextBoxBackGroundEdit()
        {
            txtUserFullName.Background = Brushes.White;
            txtUserPhoneNumber.Background = Brushes.White;
            txtUserEmail.Background = Brushes.White;
            txtUserWard.Background = Brushes.White;

            txtUserFullName.IsReadOnly = false;
            txtUserPhoneNumber.IsReadOnly = false;
            txtUserEmail.IsReadOnly = false;
            txtUserWard.IsReadOnly = false;
        }
        private void ChangeTextBoxBackGroundSave()
        {
            txtUserFullName.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#f4f4f4"));
            txtUserPhoneNumber.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#f4f4f4"));
            txtUserEmail.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#f4f4f4"));
            txtUserWard.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#f4f4f4"));

            btnSave.Visibility = Visibility.Collapsed;
            txtUserFullName.IsReadOnly = true;
            txtUserPhoneNumber.IsReadOnly = true;
            txtUserEmail.IsReadOnly = true;
            txtUserWard.IsReadOnly = true;
            cbPickupCity.Visibility = Visibility.Collapsed;
            cbPickupDistrict.Visibility = Visibility.Collapsed;
            txtUserCity.Visibility = Visibility.Visible;
            txtUserDistrict.Visibility = Visibility.Visible;
        }
        private void cbPickupCity_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbPickupCity.SelectedItem != null)
            {
                selectedCity = cbPickupCity.SelectedItem.ToString();
                txtUserCity.Text = selectedCity;
                // Filter districts based on selected city
                var filteredDistricts = address_dao.getdistrict(selectedCity);

                // Update district ComboBox
                if (cbPickupCity.Items != null)
                {
                    cbPickupDistrict.Items.Clear();
                }
                foreach (var district in filteredDistricts)
                {
                    cbPickupDistrict.Items.Add(district);
                }
                cbPickupDistrict.SelectedIndex = -1;
            }
        }

        private void cbPickupDistrict_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbPickupDistrict.SelectedItem != null)
            {
                selectedDistrict = cbPickupDistrict.SelectedItem.ToString();
                txtUserDistrict.Text = selectedDistrict;
            }
        }



        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            cbPickupCity.Visibility = Visibility.Visible;
            txtUserCity.Visibility = Visibility.Collapsed;
            cbPickupDistrict.Visibility = Visibility.Visible;
            txtUserDistrict.Visibility = Visibility.Collapsed;
            ChangeTextBoxBackGroundEdit();

            txtUserFullName.IsReadOnly = false;          
            txtUserPhoneNumber.IsReadOnly = false;          
            txtUserEmail.IsReadOnly = false;           
            txtUserWard.IsReadOnly = false;          

            for (int i = 0; i < distinctCities.Count; i++)
            {
                if (distinctCities[i].City == txtUserCity.Text)
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
            /*string selectedValue = cbPickupCity.SelectedItem.ToString();
            txtUserCity.Text = selectedValue;*/

            ChangeTextBoxBackGroundSave();

            /*selectedValue = cbPickupDistrict.SelectedItem.ToString();
            txtUserDistrict.Text = selectedValue;*/

            cbPickupCity.Visibility = Visibility.Collapsed;
            txtUserCity.Visibility = Visibility.Visible;
            cbPickupDistrict.Visibility = Visibility.Collapsed;
            txtUserDistrict.Visibility = Visibility.Visible;
            btnSave.Visibility = Visibility.Collapsed;

            StaticValue.USER.Id_user = Int32.Parse(txtUserID.Text);
            if (string.IsNullOrWhiteSpace(txtUserFullName.Text) || string.IsNullOrWhiteSpace(txtUserEmail.Text) ||
                string.IsNullOrWhiteSpace(txtUserWard.Text) || string.IsNullOrWhiteSpace(txtUserPhoneNumber.Text)
                )
            {
                MessageBox.Show("Please complete all information");
                txtUserFullName.Text = StaticValue.USER.Name;
                txtUserEmail.Text = StaticValue.USER.Email;
                txtUserWard.Text = StaticValue.USER.Ward;
                txtUserPhoneNumber.Text = StaticValue.USER.Phone;
            }
            else
            {
                StaticValue.USER.Name = txtUserFullName.Text;
                StaticValue.USER.Email = txtUserEmail.Text;
                if (!string.IsNullOrEmpty(selectedCity))
                    StaticValue.USER.City = selectedCity;
                else StaticValue.USER.City = txtUserCity.Text;
                if (!string.IsNullOrEmpty(selectedDistrict))
                    StaticValue.USER.District = selectedDistrict;
                else StaticValue.USER.District = txtUserDistrict.Text;
                StaticValue.USER.Ward = txtUserWard.Text;
                StaticValue.USER.Phone = txtUserPhoneNumber.Text;

                StaticValue.USER.Image_Path = image_path;

                user_dao.UpdateUser(StaticValue.USER);
                SavedButtonClicked?.Invoke(this, EventArgs.Empty);
               
            }
        }

        private void btnChangePhoto_Click(object sender, RoutedEventArgs e)
        {

            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Filter = "Images (*.jpg,*.png)|*.jpg;*.png";
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
            btnSave.Visibility = Visibility.Visible;
        }

        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if(this.Visibility == Visibility.Collapsed)
            {
                ChangeTextBoxBackGroundSave();
            }    
        }
    }
}
