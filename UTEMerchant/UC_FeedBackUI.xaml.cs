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
    /// Interaction logic for UC_FeedBackUI.xaml
    /// </summary>
    public partial class UC_FeedBackUI : UserControl
    {
        private User client;
        private Item item;
        private CustomerReview customerReview; 
        public UC_FeedBackUI()
        {
            InitializeComponent();
        }

        public UC_FeedBackUI(Item item, User client, CustomerReview customerReview) : this()
        {
            this.item = item;
            this.client = client;
            this.customerReview = customerReview;
            SetValue();
            
        }
        private void SetValue()
        {
            txblFeedback_ClientName.Text = this.client.name;
            txblFeedBack_Content.Text= this.customerReview.ReviewText;
            txblFeedback_ItemName.Text = this.item.name.ToString();
            txblFeedback_RateDate.Text = this.customerReview.ReviewDate.ToShortDateString();
            txblFeedback_ItemPrice.Text = this.item.price.ToString()+" $";
            ucFeedbackStar.SetRating(customerReview.Rating);
            
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(this.client.Image_path, UriKind.RelativeOrAbsolute);;
            bitmap.EndInit();
            imgFeedback_ClientAvatar.ImageSource = bitmap;

            BitmapImage bitmap1 = new BitmapImage();
            bitmap1.BeginInit();
            bitmap1.UriSource = new Uri(this.item.image_path, UriKind.RelativeOrAbsolute);
            bitmap1.EndInit();
            imgFeedback_Item.Source = bitmap1;

        }

     
    }
}
