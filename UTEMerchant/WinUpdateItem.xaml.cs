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
    /// Interaction logic for WinUpdateItem.xaml
    /// </summary>
    public partial class WinUpdateItem : Window
    {
        private string mainImgPath;
        List<string> selectedFilePath = new List<string>();
        Item_DAO dao = new Item_DAO();
        ImgPath_DAO ImgPath_DAO = new ImgPath_DAO();
        Item item;
        CheckValid check = new CheckValid();
        public WinUpdateItem()
        {
            InitializeComponent();
        }
        public WinUpdateItem(Item x):this()
        {
            this.item = x;
            SetDefault();
        }
        public void SetDefault()
        {
            txtName.Text = item.Name;
            txtOriginalPrice.Text = item.Original_Price.ToString();
            txtPrice.Text=item.Price.ToString();
            cbType.SelectedItem=item.Type;
            
            for(int i=0;i<cbType.Items.Count;i++)
            {
                ComboBoxItem comboBoxItem = cbType.Items[i] as ComboBoxItem;
                string st = comboBoxItem.Content.ToString();
                if (comboBoxItem != null && st == item.Type)
                {
                    cbType.SelectedIndex = i;
                    break;
                }
            }    
            txtCondition.Text = item.Condition.ToString();
            txtBoughtDate.Text=item.Bought_date.ToShortDateString();

            var resourceUri = new Uri(item.Image_Path, UriKind.RelativeOrAbsolute);
            imgItem.Source = new BitmapImage(resourceUri);
            // Tạo một FlowDocument
            FlowDocument flowDoc = new FlowDocument();
            // Thêm một Paragraph chứa văn bản mặc định vào FlowDocument
            Paragraph paragraph = new Paragraph(new Run(item.Detail_description));
            flowDoc.Blocks.Add(paragraph);
            // Gán FlowDocument cho RichTextBox
            rtbDetailDescription.Document = flowDoc;
            // Tạo một FlowDocument
            FlowDocument flowDoc1 = new FlowDocument();
            // Thêm một Paragraph chứa văn bản mặc định vào FlowDocument
            Paragraph paragraph1 = new Paragraph(new Run(item.Condition_Description));
            flowDoc1.Blocks.Add(paragraph1);
            // Gán FlowDocument cho RichTextBox
            rtbConditonDescription.Document = flowDoc1;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Filter = "Images (*.jpg,*.png)|*.jpg;*.png";
            openFileDialog.Multiselect = true;
            if (openFileDialog.ShowDialog() == true)
            {
                selectedFilePath = openFileDialog.FileNames.ToList();
                mainImgPath = selectedFilePath[0];
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(mainImgPath);
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
                string.IsNullOrWhiteSpace(mainImgPath) ||
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

                Item product = new Item();
                product.Item_Id = this.item.Item_Id;
                product.Name = txtName.Text;
                product.Price = float.Parse(txtPrice.Text.ToString());
                product.Original_Price = float.Parse(txtOriginalPrice.Text.ToString());
                product.Bought_date = DateTime.Parse(txtBoughtDate.Text.ToString());
                product.Condition = Int32.Parse(txtCondition.Text.ToString());
                product.Condition_Description = text_Condition;
                product.Detail_description = text_detail;
                product.Image_Path = mainImgPath;
                product.Type = typeItem.Content.ToString();
                
                dao.UpdateItem(product);
                if (selectedFilePath != null)
                {
                    ImgPath_DAO.DeleteImgPaths(this.item.Item_Id);
                    foreach (var x in selectedFilePath)
                    {
                        ImgPath imgPath = new ImgPath(this.item.Item_Id, x);
                        ImgPath_DAO.Add(imgPath);
                    }
                }
                //List<ImgPath> list = ImgPath_DAO.Load();
                this.Close();

            }
        }
    }
}
