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
        bool isFirstClickProfile = true;
        bool isFirstClickStock = true;
        private User User { get; set; }
        private Seller Seller { get; set; }
        UC_StartSelling uc_StartSelling;
        UC_RegistrationComplete uc_RegistrationComplete;
        UC_SellerRegistration uc_SellerRegistration;
        //UC_DeliveryUI uc_Delivery;
        public WinSellerInterface(User user) : this()
        {
            this.User = user;
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
            
            uc_StartSelling.Visibility = Visibility.Collapsed;
            uc_RegistrationComplete.Visibility = Visibility.Collapsed;
            uc_SellerRegistration.Visibility = Visibility.Collapsed;

            uc_StartSelling.btnStartSelling.Click += OnStartTradingButtonClicked;
            uc_SellerRegistration.btnDone.Click += OnDoneRegistrationButtonClicked;
            uc_RegistrationComplete.btnRefresh.Click += OnRefreshButtonClicked;
            uc_BuyerProfile.SavedButtonClicked += UCUserProfile_SavedButtonClicked;
            uc_SellerProfile.SavedButtonClicked += UCSellerProfile_SavedButtonClicked;
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (User != null)
            {
                uc_PurchasingUI.IdUser = User.Id_user;
                List<Seller> sellers = new List<Seller>();
                sellers = new Seller_DAO().Load();
                foreach (Seller seller in sellers)
                {
                    if (seller.Id_user == this.User.Id_user)
                    {
                        this.Seller = seller;
                        break;
                    }
                }

                txbName.Text = User.Name;
                uc_PurchasingUI.Visibility = Visibility.Visible;
                uc_Delivery.SetUser(User);
                uc_SellerUI.SetSeller(Seller);
                uc_PendingOrderReview.SetSeller(Seller);
            }
            
        }

        private void mnuitPurchase_Click(object sender, RoutedEventArgs e)
        {
            CollapseAll();
            mnuitBuyerProfile.Visibility = Visibility.Collapsed;
            mnuitSellerProfile.Visibility = Visibility.Collapsed;
            mnuitPendingOrder.Visibility = Visibility.Collapsed;
            mnuitInventory.Visibility = Visibility.Collapsed;
            uc_PurchasingUI.Visibility = Visibility.Visible;

        }

        private void mnuitStock_Click(object sender, RoutedEventArgs e)
        {
            mnuitBuyerProfile.Visibility = Visibility.Collapsed;
            mnuitSellerProfile.Visibility = Visibility.Collapsed;
            if (isFirstClickStock)
            {
                mnuitPendingOrder.Visibility = Visibility.Visible;
                mnuitInventory.Visibility = Visibility.Visible;
                isFirstClickStock = false;
            }
            else
            {
                mnuitPendingOrder.Visibility = Visibility.Collapsed;
                mnuitInventory.Visibility = Visibility.Collapsed;
                isFirstClickStock = true;
            }
            // If the user is registered as a seller
            if (3 > 2)
            {

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
            CollapseAll();
            uc_Delivery.Visibility = Visibility.Visible;
            mnuitBuyerProfile.Visibility = Visibility.Collapsed;
            mnuitSellerProfile.Visibility = Visibility.Collapsed;
            mnuitPendingOrder.Visibility = Visibility.Collapsed;
            mnuitInventory.Visibility = Visibility.Collapsed;

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
            if (sender is Button)
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
            uc_Delivery.Reload();
        }

        private void mnuitBuyerProfile_Click(object sender, RoutedEventArgs e)
        {
            CollapseAll();
            uc_BuyerProfile.Visibility = Visibility.Visible;
            uc_BuyerProfile.SetDefault(User);
        }

        private void mnuitSellerProfile_Click(object sender, RoutedEventArgs e)
        {
            CollapseAll();
            uc_SellerProfile.Visibility = Visibility.Visible;
            uc_SellerProfile.SetDefault(User, Seller);
        }

        private void UCUserProfile_SavedButtonClicked(object sender, EventArgs e)
        {
            if (sender is UC_BuyerProfile clickedItemView)
            {
                // Tạo một BitmapImage từ đường dẫn ảnh
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(uc_BuyerProfile.image_path.ToString(), UriKind.RelativeOrAbsolute);
                bitmap.EndInit();
                imgUserAvatar.ImageSource = bitmap;
                txbName.Text = uc_BuyerProfile.txtUserFullName.Text;
            }

        }
        private void UCSellerProfile_SavedButtonClicked(object sender, EventArgs e)
        {

            if (sender is UC_SellerProfile clickedItemView)
            {
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(uc_SellerProfile.image_path.ToString(), UriKind.RelativeOrAbsolute);
                bitmap.EndInit();
                imgUserAvatar.ImageSource = bitmap;
            }
        }

        private void mnuitPendingOrder_Click(object sender, RoutedEventArgs e)
        {
            CollapseAll();
            grdSellerUI.Visibility = Visibility.Visible;
            uc_SellerUI.Visibility = Visibility.Collapsed;
            uc_PendingOrderReview.Visibility = Visibility.Visible;
        }

        private void mnuitInventory_Click(object sender, RoutedEventArgs e)
        {
            CollapseAll();
            grdSellerUI.Visibility = Visibility.Visible;
            uc_PendingOrderReview.Visibility = Visibility.Collapsed;
            uc_SellerUI.Visibility = Visibility.Visible;
        }

        private void CollapseAll()
        {
            grdSellerUI.Visibility = Visibility.Collapsed;
            uc_SellerUI.Visibility = Visibility.Collapsed;
            uc_Delivery.Visibility = Visibility.Collapsed;
            uc_PurchasingUI.Visibility = Visibility.Collapsed;
            uc_SellerRegistration.Visibility = Visibility.Collapsed;
            uc_RegistrationComplete.Visibility = Visibility.Collapsed;
            uc_StartSelling.Visibility = Visibility.Collapsed;
            uc_PendingOrderReview.Visibility = Visibility.Collapsed;
            uc_BuyerProfile.Visibility = Visibility.Collapsed;
            uc_SellerProfile.Visibility = Visibility.Collapsed;
        }

        private void mnuitProfile_Click(object sender, RoutedEventArgs e)
        {
            mnuitPendingOrder.Visibility = Visibility.Collapsed;
            mnuitInventory.Visibility = Visibility.Collapsed;
            if (isFirstClickProfile)
            {
                mnuitBuyerProfile.Visibility = Visibility.Visible;
                mnuitSellerProfile.Visibility = Visibility.Visible;
                isFirstClickProfile = false;
            }
            else
            {
                mnuitBuyerProfile.Visibility = Visibility.Collapsed;
                mnuitSellerProfile.Visibility = Visibility.Collapsed;
                isFirstClickProfile = true;
            }

        }

        
    }

}

