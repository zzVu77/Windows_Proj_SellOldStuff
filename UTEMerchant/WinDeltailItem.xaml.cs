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
        private Seller seller;
        private int Id_user;
        private ImgPath_DAO ImgPath_DAO = new ImgPath_DAO();
        public WinDeltailItem()
        {
            InitializeComponent();
        }
        public WinDeltailItem(Item item, Seller seller,int id_user)
        {
            this.info = item;
            this.seller = seller;
            Id_user = id_user;
            //var user_dao = new user_DAO();
            //users = user_dao.Load();
            InitializeComponent();
            SetDefaultValue();
            List<ImgPath> imgPaths = ImgPath_DAO.GetListImagePathByItemID(this.info.Item_Id);
            dplImageSlide.Children.Clear();
            foreach (var i in imgPaths)
            {
                UC_ImageSlide imgs = new UC_ImageSlide(i.Path);
                imgs.ImageClicked += OnImageSlideClicked;
                dplImageSlide.Children.Add(imgs);
            }


        }

        private void OnImageSlideClicked(object sender, RoutedEventArgs e)
        {
            if(sender is UC_ImageSlide img)
            {
                string imgpath = img.imgPath;
                var resourceUri = new Uri(imgpath, UriKind.RelativeOrAbsolute);
                imgItem.Source = new BitmapImage(resourceUri);
            }
        }
        
        private void SetDefaultValue()
        {
            txblItemName.Text = info.Name;
            txbOriginalPrice.Text = info.Original_Price.ToString()+" $";
            txbBughtDate.Text=info.Bought_date.ToShortDateString();
            txbType.Text = info.Type.ToString();
            txbConditon.Text = info.Condition.ToString()+" %";
            txtItemPrice.Text = info.Price.ToString() + " $";
            txbSellerName.Text = seller.ShopName;
            txbSellerContact.Text = seller.Phone;
            txbSellerAddress.Text = seller.Ward +", "+ seller.District + ", " + seller.City;

            
            var resourceUri = new Uri(info.Image_Path, UriKind.RelativeOrAbsolute);
            imgItem.Source = new BitmapImage(resourceUri);
            // Tạo một FlowDocument
            FlowDocument flowDoc = new FlowDocument();
            // Thêm một Paragraph chứa văn bản mặc định vào FlowDocument
            Paragraph paragraph = new Paragraph(new Run(info.Detail_description));
            flowDoc.Blocks.Add(paragraph);
            // Gán FlowDocument cho RichTextBox
            rtbDetailDescription.Document = flowDoc;

            // Tạo một FlowDocument
            FlowDocument flowDoc1 = new FlowDocument();
            // Thêm một Paragraph chứa văn bản mặc định vào FlowDocument
            Paragraph paragraph1 = new Paragraph(new Run(info.Condition_Description));
            flowDoc1.Blocks.Add(paragraph1);
            // Gán FlowDocument cho RichTextBox
            rtbConditonDescription.Document = flowDoc1;


        }

        private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnBuyNow_Click(object sender, RoutedEventArgs e)
        {
            WinBuyingInterface winBuyingInterface = new WinBuyingInterface(this.info, Id_user);
            this.Hide();
            winBuyingInterface.ShowDialog();
            this.ShowDialog();
        }

        private void Ellipse_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Item_DAO item_DAO = new Item_DAO();
            WinShopView winShopView = new WinShopView(seller, item_DAO.GetItemsBySellerID(seller.SellerID));
            winShopView.ShowDialog();
            this.Show();
            this.BringIntoView();
        }

        private void txbSellerName_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Ellipse_MouseLeftButtonDown(sender, e);
        }
        private void iconClose_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void iconClose_MouseUp_1(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
    }
}
