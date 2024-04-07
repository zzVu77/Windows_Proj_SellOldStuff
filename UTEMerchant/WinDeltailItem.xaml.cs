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
        
        public Item info;
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
            txbOriginalPrice.Text = info.Original_Price.ToString()+" $";
            txbBughtDate.Text=info.Bought_date.ToShortDateString();
            txbType.Text = info.Type.ToString();
            txbConditon.Text = info.Condition.ToString()+" %";
            txtItemPrice.Text = info.Price.ToString() + " $";
            var resourceUri = new Uri(info.Image_Path, UriKind.RelativeOrAbsolute);
            imgItem.Source = new BitmapImage(resourceUri);
            // Tạo một FlowDocument
            FlowDocument flowDoc = new FlowDocument();
            // Thêm một Paragraph chứa văn bản mặc định vào FlowDocument
            Paragraph paragraph = new Paragraph(new Run(info.Condition_Description));
            flowDoc.Blocks.Add(paragraph);
            // Gán FlowDocument cho RichTextBox
            rtbDetailDescription.Document = flowDoc;
        }

        private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnBuyNow_Click(object sender, RoutedEventArgs e)
        {
            WinBuyingInterface winBuyingInterface = new WinBuyingInterface(this.info);
            this.Hide();
            winBuyingInterface.ShowDialog();
            this.ShowDialog();
        }
    }
}
