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
        public WinSellerInterface()
        {
            InitializeComponent();
            Item item1 = new Item(01, "Iphone 14 Pro Maxsdsdasdas", "RAM 8GB ROM 256GB", 1000, 600, "/Img/iPhone-14-Pro-Max-9907.jpg", new DateTime(2023, 11, 20), "95%", "Smart Phone");
            //productGrid.Items.Add(item1);
            //productGrid.Items.Add(item1);
            //productGrid.Items.Add(item1);
            //productGrid.Items.Add(item1);
            //productGrid.Items.Add(item1);
            //productGrid.Items.Add(item1);
            //productGrid.Items.Add(item1);
            //productGrid.Items.Add(item1);
            //productGrid.Items.Add(item1);
            //productGrid.Items.Add(item1);
            //productGrid.Items.Add(item1);
            //productGrid.Items.Add(item1);
            //productGrid.Items.Add(item1);
            //productGrid.Items.Add(item1);
            //productGrid.Items.Add(item1);
            //productGrid.Items.Add(item1);
            //productGrid.Items.Add(item1);
            //productGrid.Items.Add(item1);
            //productGrid.Items.Add(item1);
            //productGrid.Items.Add(item1);
            //productGrid.Items.Add(item1);
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
            uc_SellerUI.Visibility = Visibility.Collapsed;
            uc_PurchasingUI.Visibility = Visibility.Visible;
        }

        private void mnuitStock_Click(object sender, RoutedEventArgs e)
        {
            uc_PurchasingUI.Visibility = Visibility.Collapsed;
            uc_SellerUI.Visibility = Visibility.Visible;
        }
    }
}

