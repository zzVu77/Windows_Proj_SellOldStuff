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
        private string _city;
        private string _district;
        private string _ward;
        private string _details;

        public WinAddressCustomization()
        {
            InitializeComponent();
        }

        public WinAddressCustomization(string city, string district, string ward, string details) : this()
        {
            _city = city;
            _district = district;
            _ward = ward;
            _details = details;
        }

        private void BtnSave_OnClick(object sender, RoutedEventArgs e)
        {
            _city = cbCity.Text;
            _district = cbDistrict.Text;
            _ward = cbWard.Text;
            _details = tbDetails.Text;
            
            // don't need to check if the address is valid or not
            // because the user can input any address they want
            // the address will be checked when the user place the order
            // so just return the address
            DialogResult = true;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (_city != null) cbCity.Text = _city;
            if (_district != null) cbDistrict.Text = _district;
            if (_ward != null) cbWard.Text = _ward;
            if (_details != null) tbDetails.Text = _details;
        }

        public string City => _city;
        public string District => _district;
        public string Ward => _ward;
        public string Details => _details;
    }
}
