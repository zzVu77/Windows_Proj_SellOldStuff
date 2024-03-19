using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Wpf.Ui.Controls;

namespace UTEMerchant
{
    /// <summary>
    /// Interaction logic for WinNewItem.xaml
    /// </summary>
    public partial class WinNewItem : Window
    {
        private string image_path;
        public WinNewItem()
        {
            InitializeComponent();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Filter = "Tất cả các tệp (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                string selectedFilePath = openFileDialog.FileName;
                image_path = selectedFilePath;
                // Xử lý đường dẫn đã chọn ở đây
            }
        }

        private void rtbDescription_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_MouseDown(object sender, MouseButtonEventArgs e)
        {

            ComboBoxItem typeItem = (ComboBoxItem)cbType.SelectedItem;

            new Item_DAO().add(new Item(Int32.Parse(txtID.Text.ToString()), txtName.Text.ToString(), rtbDescription.ToString(),
                float.Parse(txtOriginalPrice.Text.ToString()), float.Parse(txtPrice.Text.ToString()),
                image_path, DateTime.Parse(txtBoughtDate.Text.ToString()), txtStatus.Text.ToString(), typeItem.Content.ToString(), 1));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ComboBoxItem typeItem = (ComboBoxItem)cbType.SelectedItem;

            new Item_DAO().add(new Item(Int32.Parse(txtID.Text.ToString()), txtName.Text.ToString(), rtbDescription.ToString(),
                float.Parse(txtOriginalPrice.Text.ToString()), float.Parse(txtPrice.Text.ToString()),
                image_path, DateTime.Parse(txtBoughtDate.Text.ToString()), txtStatus.Text.ToString(), typeItem.Content.ToString(), 1));
        }

        /*private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }*/

        //private void ToggleButton_Click(object sender, RoutedEventArgs e)
        //{
        //    var toggleButton = sender as ToggleButton;
        //    if (toggleButton != null)
        //    {
        //        var comboBox = toggleButton.TemplatedParent as ComboBox;
        //        if (comboBox != null)
        //        {
        //            comboBox.IsDropDownOpen = !comboBox.IsDropDownOpen;
        //        }
        //    }
        //}
    }
}
