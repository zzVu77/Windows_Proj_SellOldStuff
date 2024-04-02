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
    /// Interaction logic for WinDeltailItem.xaml
    /// </summary>
    public partial class WinDeltailItem : Window
    {
        Item info;
        List<User> users = new List<User>();
        user_DAO user_dao = new user_DAO();
        public WinDeltailItem()
        {
            InitializeComponent();
        }
        public WinDeltailItem(Item item)
        {
            info = item;
            var user_dao = new user_DAO();
            users = user_dao.Load();
            InitializeComponent();
            SetDefaultValue();

        }
        
        private void SetDefaultValue()
        {
            txblItemName.Text = info.Name;
            txbOriginalPrice.Text = info.OriginalPrice.ToString()+" $";
            txbBughtDate.Text=info.Bought_date.ToShortDateString();
            txbType.Text = info.Type.ToString();
            txbConditon.Text = info.Status.ToString()+" %";
            txtItemPrice.Text = info.Price.ToString() + " $";
            var resourceUri = new Uri(info.ImagePath, UriKind.RelativeOrAbsolute);
            imgItem.Source = new BitmapImage(resourceUri);
            // Tạo một FlowDocument
            FlowDocument flowDoc = new FlowDocument();
            // Thêm một Paragraph chứa văn bản mặc định vào FlowDocument
            Paragraph paragraph = new Paragraph(new Run(info.Description));
            flowDoc.Blocks.Add(paragraph);
            // Gán FlowDocument cho RichTextBox
            rtbDetailDescription.Document = flowDoc;
        }

       
    }
}
