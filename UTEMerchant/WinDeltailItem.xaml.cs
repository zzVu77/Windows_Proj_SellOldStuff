﻿using System;
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
        private bool checkWishList=false;
        public Item info;
        private Seller seller;
        private int Id_user;
        private ImgPath_DAO ImgPath_DAO = new ImgPath_DAO();
        private WishList_DAO wishListDAO = new WishList_DAO();
        public bool check=false;
        private const string ImgRelativePath = "../../Img/";

        private static readonly string ExecutablePath = AppDomain.CurrentDomain.BaseDirectory;

        private static readonly string ImgFilePath = System.IO.Path.GetFullPath(System.IO.Path.Combine(ExecutablePath, ImgRelativePath));
        public WinDeltailItem()
        {
            InitializeComponent();


        }
        public WinDeltailItem(Item item, Seller seller,int id_user) : this()
        {
            this.info = item;
            this.seller = seller;
            Id_user = id_user;

            User user = new user_DAO().GetUserByID(seller.Id_user);
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(System.IO.Path.Combine(ImgFilePath, user.Image_Path), UriKind.RelativeOrAbsolute);
            bitmap.EndInit();
            imgSellerAvartar.ImageSource = bitmap;
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
            if (wishListDAO.CheckAddedItemInWishList(info.Item_Id, Id_user) == true)
            {
                spAddToWishList.Visibility = Visibility.Collapsed;
                spRemoveFromWishList.Visibility = Visibility.Visible;
                btnAddToWishList.Background = Brushes.Black;
                checkWishList = true;
            }
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(checkWishList==false)
            {
                DateTime now = DateTime.Now;
                wishListDAO.Add(new WishList(this.Id_user, info.Item_Id, now));
                checkWishList = true;
                spAddToWishList.Visibility = Visibility.Collapsed;
                spRemoveFromWishList.Visibility = Visibility.Visible;
                btnAddToWishList.Background = Brushes.Black;
            }    
            else
            {                
                wishListDAO.RemoveItem(info.Item_Id,this.Id_user);
                btnAddToWishList.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#E56B6F"));
                checkWishList = false;
                spAddToWishList.Visibility = Visibility.Visible;
                spRemoveFromWishList.Visibility = Visibility.Collapsed;
            }    

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (info == null) return;
            SetDefaultValue();
            List<ImgPath> imgPaths = ImgPath_DAO.GetListImagePathByItemID(this.info.Item_Id);
            dplImageSlide.Children.Clear();
            foreach (var i in imgPaths)
            {
                UC_ImageSlide imgs = new UC_ImageSlide(i.Path);
                imgs.ImageClicked += OnImageSlideClicked;
                dplImageSlide.Children.Add(imgs);
            }
            List<Item> recommendations;
            if (StaticValue.SELLER!=null)
            {

            recommendations = ItemRecommendationEngine.GetRecommendations(info, StaticValue.SELLER.SellerID, 30);

            }
            else recommendations = ItemRecommendationEngine.GetRecommendations(info, 0, 30);
            uni.Children.Clear();
            foreach (var item in recommendations)
            {
                UC_ItemView uc_Item = new UC_ItemView(item);
                uc_Item.ItemClicked += RelatedItem_Clicked;
                uc_Item.btnAddToCart.Visibility = Visibility.Collapsed;
                uni.Children.Add(uc_Item);
            }
        }

        private void RelatedItem_Clicked(object sender, RoutedEventArgs e)
        {
            if (sender is UC_ItemView uc_Item)
            {
                WinDeltailItem winDeltailItem = new WinDeltailItem(uc_Item.info,
                    new Seller_DAO().GetSeller(uc_Item.info.SellerID), Id_user);
                winDeltailItem.ShowDialog();
            }
        }

        private void imgItem_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            WinImageZoom winImageZoom = new WinImageZoom((Image)sender);
            winImageZoom.ShowDialog();
        }

        private void btnAddToCart_Click(object sender, RoutedEventArgs e)
        {
            check =true;
        }
    }
}
