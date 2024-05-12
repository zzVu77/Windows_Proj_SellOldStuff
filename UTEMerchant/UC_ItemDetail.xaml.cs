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

namespace UTEMerchant
{
    /// <summary>
    /// Interaction logic for UC_ItemDetail.xaml
    /// </summary>
    public partial class UC_ItemDetail : UserControl
    {
        Item info;
        List<User> users = new List<User>();
        User_DAO user_dao = new User_DAO();
        public UC_ItemDetail()
        {
            InitializeComponent();
            SetDefaultText();
        }
        public UC_ItemDetail(Item item)
        {
            info = item;
            var user_dao= new User_DAO();
            users = user_dao.Load();
            InitializeComponent();
            SetDefaultText();
        }
        private void SetDefaultText()
        {
            txblItemName.Text= info.name;
            txbOriginalPrice.Text = info.original_price.ToString()+" $";
            txblBoughtDate.Text = info.bought_date.ToString();

            foreach (User user in users) 
            {
                if (user.Id_user == info.SellerID)
                {
                    txblContactValue.Text = user.phone;
                    break;
                }
            }
            txblItemPrice.Text = info.price.ToString()+" $";
            txblTypeValue.Text = info.type.ToString();
            txblStatus.Text = info.condition.ToString();
            var resourceUri = new Uri(info.image_path, UriKind.RelativeOrAbsolute);
            imgItemPic.Source = new BitmapImage(resourceUri);
            // Tạo một FlowDocument
            FlowDocument flowDoc = new FlowDocument();
            // Thêm một Paragraph chứa văn bản mặc định vào FlowDocument
            Paragraph paragraph = new Paragraph(new Run(info.condition_description));
            flowDoc.Blocks.Add(paragraph);
            // Gán FlowDocument cho RichTextBox
            rtbDescription.Document = flowDoc;
        }
    }
}
