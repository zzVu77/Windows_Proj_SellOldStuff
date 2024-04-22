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
    /// Interaction logic for WinSellerInterface.xaml
    /// </summary>
    public partial class WinSellerInterface : Window
    {
        private User User { get; set; }
        private Seller Seller { get; set; }
        UC_StartSelling uc_StartSelling;
        UC_RegistrationComplete uc_RegistrationComplete;
        UC_SellerRegistration uc_SellerRegistration;
        UC_SellerUI uc_SellerUI;
        //UC_DeliveryUI uc_Delivery;
        public WinSellerInterface(User user) : this()
        {

            this.User = user;
            // Tạo một BitmapImage từ đường dẫn ảnh
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(user.Image_Path.ToString(), UriKind.RelativeOrAbsolute);
            bitmap.EndInit();
            imgUserAvatar.ImageSource = bitmap;

            txbName.Text = user.Name;
            uc_PurchasingUI.Id_user = user.Id_user;
            List<Seller> sellers = new List<Seller>();
            sellers = new Seller_DAO().Load();
            foreach (Seller seller in sellers)
            {
                if (seller.Id_user == this.User.Id_user)
                {
                    this.Seller = seller;
                }    
            }

            if (Seller != null)
            {
                this.uc_SellerUI = new UC_SellerUI(Seller.SellerID);
                
            }
            else this.uc_SellerUI = new UC_SellerUI();
            grdSellerUI.Children.Add(uc_SellerUI);
            uc_SellerUI.Visibility = Visibility.Collapsed;
            uc_SellerUI.HorizontalAlignment = HorizontalAlignment.Stretch;
            uc_SellerUI.VerticalAlignment = VerticalAlignment.Stretch;
            uc_SellerUI.Background = Brushes.Transparent;
            //this.uc_Delivery = new UC_DeliveryUI(user.Id_user);


            //grdSellerUI.Children.Add(uc_Delivery);

            uc_PurchasingUI.Visibility = Visibility.Visible;
            //uc_Delivery.Visibility = Visibility.Visible;

            /* uc_Delivery.VerticalAlignment = VerticalAlignment.Bottom;
             uc_Delivery.Height = 735;
             uc_Delivery.Margin = new Thickness(0, 20, 0, 15);*/
            uc_Delivery.Id_user = user.Id_user;
            uc_Delivery.Visibility = Visibility.Collapsed;
            
            
        }
        public WinSellerInterface()
        {
            InitializeComponent();


            
            this.uc_StartSelling = new UC_StartSelling();
            this.uc_RegistrationComplete = new UC_RegistrationComplete();
            this.uc_SellerRegistration = new UC_SellerRegistration();
            
          
            grdSellerUI.Children.Add(uc_StartSelling);
            grdSellerUI.Children.Add(uc_RegistrationComplete);
            grdSellerUI.Children.Add(uc_SellerRegistration);
            

            uc_StartSelling.Visibility=Visibility.Collapsed;
            uc_RegistrationComplete.Visibility=Visibility.Collapsed;
            uc_SellerRegistration.Visibility=Visibility.Collapsed;
           





            uc_StartSelling.btnStartSelling.Click += OnStartTradingButtonClicked;
            uc_SellerRegistration.btnDone.Click += OnDoneRegistrationButtonClicked;
            uc_RegistrationComplete.btnRefresh.Click += OnRefreshButtonClicked;
        }

        private bool IsMaximize = false;
        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                if (IsMaximize)
                {
                    this.WindowState = WindowState.Normal;
                    this.Width = 1080;
                    this.Height = 720;

                    IsMaximize = false;
                }
                else
                {
                    this.WindowState = WindowState.Maximized;

                    IsMaximize = true;
                }
            }
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void UC_SellerUI_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void mnuitPurchase_Click(object sender, RoutedEventArgs e)
        {
            grdSellerUI.Visibility = Visibility.Collapsed;
            uc_Delivery.Visibility = Visibility.Collapsed;
            uc_PurchasingUI.Visibility = Visibility.Visible;
        }

        private void mnuitStock_Click(object sender, RoutedEventArgs e)
        {
            uc_SellerUI.Visibility = Visibility.Collapsed;
            uc_Delivery.Visibility = Visibility.Collapsed;
            uc_PurchasingUI.Visibility = Visibility.Collapsed;
            uc_SellerRegistration.Visibility = Visibility.Collapsed;
            uc_RegistrationComplete.Visibility = Visibility.Collapsed;
            uc_StartSelling.Visibility = Visibility.Collapsed;

            // If the user is registered as a seller
            if (3 > 2)
            {                
                grdSellerUI.Visibility = Visibility.Visible;
                uc_SellerUI.Visibility = Visibility.Visible;
            }
            // If the user haven't registered to be a seller
            else if (1 > 2)
            {
                grdSellerUI.Visibility = Visibility.Visible;
                uc_StartSelling.Visibility = Visibility.Visible;
            }
        }


        private void mnuitOrder_Click(object sender, RoutedEventArgs e)
        {
            grdSellerUI.Visibility = Visibility.Collapsed;
            uc_PurchasingUI.Visibility = Visibility.Collapsed;
            uc_Delivery.Visibility = Visibility.Visible;
        }

        // This method is called when the user clicks the Start Trading button in the Start Selling UC
        private void OnStartTradingButtonClicked(object sender, EventArgs e)
        {
            if (sender is Button)
            {
                uc_StartSelling.Visibility = Visibility.Collapsed;
                uc_SellerRegistration.Visibility = Visibility.Visible;
            }
        }

        // This method is called when the user clicks the Done button in the Seller Registration UC
        private void OnDoneRegistrationButtonClicked(object sender, EventArgs e)
        {
            if (sender is Button)
            {
                uc_SellerRegistration.Visibility = Visibility.Collapsed;
                uc_RegistrationComplete.Visibility = Visibility.Visible;
            }
        }

        // This method is called when the user clicks the Refresh List button in the Registration Complete UC
        private void OnRefreshButtonClicked(object sender, EventArgs e)
        {
            if (sender is Button )
            {

                uc_RegistrationComplete.Visibility = Visibility.Collapsed;
                uc_SellerUI.Visibility = Visibility.Visible;
            }
        }

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {

            this.Close();
        }

        private void uc_Delivery_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            uc_Delivery.Load();
        }
    }
}

