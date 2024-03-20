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
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(image_path);
                bitmap.EndInit();
                imgItem.Source = bitmap;
                // Xử lý đường dẫn đã chọn ở đây
            }
        }

        private void rtbDescription_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_MouseDown(object sender, MouseButtonEventArgs e)
        {

            ComboBoxItem typeItem = (ComboBoxItem)cbType.SelectedItem;
            Item_DAO dao = new Item_DAO();
            dao.add(new Item(Int32.Parse(txtID.Text.ToString()), txtName.Text.ToString(), rtbDescription.ToString(),
                float.Parse(txtOriginalPrice.Text.ToString()), float.Parse(txtPrice.Text.ToString()),
                image_path, DateTime.Parse(txtBoughtDate.Text.ToString()), txtStatus.Text.ToString(), typeItem.Content.ToString(), 1));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        { 
            if (new CheckValid().IsNumeric(txtID.Text.ToString()) && new CheckValid().IsDateFormatValid(txtBoughtDate.Text.ToString()) 
                && new CheckValid().IsDateValid(txtBoughtDate.Text.ToString()) && !string.IsNullOrEmpty(txtID.Text.ToString()) && 
                !string.IsNullOrEmpty(txtBoughtDate.Text.ToString()))
            {
                ComboBoxItem typeItem = (ComboBoxItem)cbType.SelectedItem;
                Item_DAO dao = new Item_DAO();
                dao.add(new Item(Int32.Parse(txtID.Text.ToString()), txtName.Text.ToString(), rtbDescription.Document.ToString(),
                    float.Parse(txtOriginalPrice.Text.ToString()), float.Parse(txtPrice.Text.ToString()),
                    image_path, DateTime.Parse(txtBoughtDate.Text.ToString()), txtStatus.Text.ToString(), typeItem.Content.ToString(), 1));
            }
            else
            {
                System.Windows.MessageBox.Show("Invalid information !!!");
                if (!(new CheckValid().IsNumeric(txtID.Text.ToString())) || string.IsNullOrEmpty(txtID.Text.ToString()))
                    txtID.Background = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                if (!new CheckValid().IsDateFormatValid(txtBoughtDate.Text.ToString())
                || !new CheckValid().IsDateValid(txtBoughtDate.Text.ToString()) || string.IsNullOrEmpty(txtBoughtDate.Text.ToString()))
                    txtBoughtDate.Background = new SolidColorBrush(Color.FromRgb(255, 0, 0));
               

            }
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
