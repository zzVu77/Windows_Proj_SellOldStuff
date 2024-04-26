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
        CheckValid check = new CheckValid();
        Item_DAO dao = new Item_DAO();
        private int IdSeller;
        public WinNewItem()
        {
            InitializeComponent();
        }
        public WinNewItem(int idSeller) : this()
        {
            IdSeller = idSeller;
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

        //private void Button_MouseDown(object sender, MouseButtonEventArgs e)
        //{

        //    ComboBoxItem typeItem = (ComboBoxItem)cbType.SelectedItem;

        //    dao.AddItem(new Item(Int32.Parse(txtID.Text.ToString()), 
        //                txtName.Text.ToString(), 
        //                rtbConditonDescription.ToString(),
        //                float.Parse(txtOriginalPrice.Text.ToString()),
        //                float.Parse(txtPrice.Text.ToString()),
        //                image_path,
        //                DateTime.Parse(txtBoughtDate.Text.ToString()),
        //                txtCondition.Text.ToString(),
        //                typeItem.Content.ToString(), 1));
        //}

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ComboBoxItem typeItem = cbType.SelectedItem as ComboBoxItem;
            string text_detail = new TextRange(rtbDetailDescription.Document.ContentStart, rtbDetailDescription.Document.ContentEnd).Text;
            string text_Condition = new TextRange(rtbConditonDescription.Document.ContentStart, rtbConditonDescription.Document.ContentEnd).Text;
            
            // Perform validation checks
            if (typeItem == null ||
                //string.IsNullOrWhiteSpace(txtID.Text) ||
                string.IsNullOrWhiteSpace(txtBoughtDate.Text) ||
                string.IsNullOrWhiteSpace(txtName.Text) ||
                string.IsNullOrWhiteSpace(txtCondition.Text) ||
                string.IsNullOrWhiteSpace(txtOriginalPrice.Text) ||
                string.IsNullOrWhiteSpace(txtPrice.Text) ||
                string.IsNullOrWhiteSpace(text_detail) ||
                string.IsNullOrWhiteSpace(text_Condition) ||
                string.IsNullOrWhiteSpace(image_path) ||
                !check.IsDateFormatValid(txtBoughtDate.Text) ||           // Check if bought date has valid format
                !check.IsDateValid(txtBoughtDate.Text) ||                 // Check if bought date is a valid date
                !check.IsNumeric(txtCondition.Text) ||                    // Check if condition is numeric
                !check.IsNumeric(txtOriginalPrice.Text) ||                // Check if original price is numeric
                !check.IsNumeric(txtPrice.Text)                       // Check if price is numeric
               )
            {
                // Show error message if any of the conditions fail
                System.Windows.MessageBox.Show("Invalid information !!!");
            }
            else
            {

                
                Item_DAO dao = new Item_DAO();
                DateTime today = DateTime.Now;
                dao.Add(new Item(0, txtName.Text.ToString(),
                    float.Parse(txtPrice.Text.ToString()),
                    float.Parse(txtOriginalPrice.Text.ToString()), typeItem.Content.ToString(),
                    DateTime.Parse(txtBoughtDate.Text.ToString()),
                    text_Condition, Int32.Parse(txtCondition.Text.ToString())
                    , image_path, false, text_detail, IdSeller, today)
                             );
                this.Close();

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
