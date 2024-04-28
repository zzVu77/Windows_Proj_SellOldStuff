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
        private string image_path;
        List<string> selectedFilePath = new List<string>();
        Item_DAO dao = new Item_DAO();
        ImgPath_DAO ImgPath_DAO = new ImgPath_DAO();
        Item item;
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
    }
}
