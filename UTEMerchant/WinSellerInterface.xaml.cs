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
        public WinSellerInterface(User user)
        {
            this.User = user;
            InitializeComponent();
            txbName.Text = user.Name;
        }
        public WinSellerInterface()
        {  
            InitializeComponent();
            uc_StartSelling.btnStartSelling.Click += OnStartTradingButtonClicked;
            uc_SellerRegistration.btnDone.Click += OnDoneRegistrationClicked;
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

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void UC_SellerUI_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void mnuitPurchase_Click(object sender, RoutedEventArgs e)
        {
            grdSellerUI.Visibility = Visibility.Collapsed;
            foreach (var item in grdSellerUI.Children)
            {
                if (item is UserControl)
                {
                    UserControl uc = item as UserControl;
                    uc.Visibility = Visibility.Collapsed;
                }
            }
            uc_PurchasingUI.Visibility = Visibility.Visible;
        }

        private void mnuitStock_Click(object sender, RoutedEventArgs e)
        {
            //// If the user is registered as a seller
            //if (1 > 2)
            //{
                uc_PurchasingUI.Visibility = Visibility.Collapsed;
                uc_SellerRegistration.Visibility = Visibility.Collapsed;
                uc_RegistrationComplete.Visibility = Visibility.Collapsed;
                uc_StartSelling.Visibility = Visibility.Collapsed;

                grdSellerUI.Visibility = Visibility.Visible;
                uc_SellerUI.Visibility = Visibility.Visible;
            //}
            //// If the user haven't registered to be seller
            //else if (1 < 2)
            //{
            //    uc_PurchasingUI.Visibility = Visibility.Collapsed;
            //    uc_SellerRegistration.Visibility = Visibility.Collapsed;
            //    uc_RegistrationComplete.Visibility = Visibility.Collapsed;
            //    uc_StartSelling.Visibility = Visibility.Collapsed;

            //    grdSellerUI.Visibility = Visibility.Visible;
            //    uc_StartSelling.Visibility = Visibility.Visible;
            //}
            //// If the user have registered to be a seller but registration hasn't been approved
            //else if (1 == 2)
            //{
            //    uc_PurchasingUI.Visibility = Visibility.Collapsed;
            //    uc_SellerRegistration.Visibility = Visibility.Collapsed;
            //    uc_RegistrationComplete.Visibility = Visibility.Collapsed;
            //    uc_StartSelling.Visibility = Visibility.Collapsed;

            //    grdSellerUI.Visibility = Visibility.Visible;
            //    uc_RegistrationComplete.Visibility = Visibility.Visible;
            //}

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
        private void OnDoneRegistrationClicked(object sender, EventArgs e)
        {
            if (sender is Button)
            {
                uc_SellerRegistration.Visibility = Visibility.Collapsed;
                uc_RegistrationComplete.Visibility = Visibility.Visible;
            }
        }

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {

            this.Close();
        }
    }
}

