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
        List<string> selectedFilePath = new List<string>();
        CheckValid check = new CheckValid();
        Item_DAO dao = new Item_DAO();
        ImgPath_DAO ImgPath_DAO = new ImgPath_DAO();
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
            openFileDialog.Filter = "Images (*.jpg,*.png)|*.jpg;*.png";
            openFileDialog.Multiselect = true;
            if (openFileDialog.ShowDialog() == true)
            {
                selectedFilePath = openFileDialog.FileNames.ToList();
                image_path = selectedFilePath.First();
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(image_path);
                bitmap.EndInit();
                imgItem.Source = bitmap;
                // Xử lý đường dẫn đã chọn ở đây
            }
        }

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
                    , selectedFilePath[0], false, text_detail, IdSeller, today));
                int itemID = dao.GetTheMaximumItem_ID();
                foreach (var x in selectedFilePath)
                {
                    ImgPath imgPath = new ImgPath(itemID, x);
                    ImgPath_DAO.Add(imgPath);
                }
                List<ImgPath> list = ImgPath_DAO.Load();
                this.Close();

            }
        }

    }
}
