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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UTEMerchant
{
    /// <summary>
    /// Interaction logic for UC_FeedbackForDashBoard.xaml
    /// </summary>
    public partial class UC_FeedbackForDashBoard : UserControl
    {
        private User client;
        private Item item;
        private CustomerReview customerReview;
        private const string ImgRelativePath = "../../Img/";

        private static readonly string ExecutablePath = AppDomain.CurrentDomain.BaseDirectory;

        private static readonly string ImgFilePath = System.IO.Path.GetFullPath(System.IO.Path.Combine(ExecutablePath, ImgRelativePath));
        public UC_FeedbackForDashBoard()
        {
            InitializeComponent();
        }

        public UC_FeedbackForDashBoard(Item item, User client, CustomerReview customerReview) : this()
        {
            this.item = item;
            this.client = client;
            this.customerReview = customerReview;
            SetValue();

        }
        private void SetValue()
        {
            txblFeedback_ClientName.Text = this.client.Name;
            txblFeedBack_Content.Text = this.customerReview.ReviewText;
            txblFeedback_ItemName.Text = this.item.Name.ToString();
            txblFeedback_RateDate.Text = this.customerReview.ReviewDate.ToShortDateString();
            txblFeedback_ItemPrice.Text = this.item.Price.ToString() + " $";
            ucFeedbackStar.SetRating(customerReview.Rating);

            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(System.IO.Path.Combine(ImgFilePath, this.client.Image_Path), UriKind.RelativeOrAbsolute);
            bitmap.EndInit();
            imgFeedback_ClientAvatar.ImageSource = bitmap;

            BitmapImage bitmap1 = new BitmapImage();
            bitmap1.BeginInit();
            bitmap1.UriSource = new Uri(this.item.Image_Path, UriKind.RelativeOrAbsolute);
            bitmap1.EndInit();
            imgFeedback_Item.Source = bitmap1;

        }
    }
}
