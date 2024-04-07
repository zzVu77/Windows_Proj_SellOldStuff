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
    /// Interaction logic for UC_DeliveryUI.xaml
    /// </summary>
    public partial class UC_DeliveryUI : UserControl
    {
        public UC_DeliveryUI()
        {
            InitializeComponent();
        }

        private void rbPending_Checked(object sender, RoutedEventArgs e)
        {
            svDeliveredStatusChecking.Visibility = Visibility.Collapsed;
            svDeliveringStatusChecking.Visibility = Visibility.Collapsed;
            //svCancelledStatusChecking.Visibility = Visibility.Collapsed;

            //svPendingStatusChecking.Visibility = Visibility.Visible;
        }

        private void rbDelivering_Checked(object sender, RoutedEventArgs e)
        {
            //svPendingStatusChecking.Visibility = Visibility.Collapsed;
            svDeliveredStatusChecking.Visibility = Visibility.Collapsed;
            //svCancelledStatusChecking.Visibility = Visibility.Collapsed;

            svDeliveringStatusChecking.Visibility = Visibility.Visible;
        }

        private void rbDelivered_Checked(object sender, RoutedEventArgs e)
        {
            //svPendingStatusChecking.Visibility = Visibility.Collapsed;
            svDeliveringStatusChecking.Visibility = Visibility.Collapsed;
            //svCancelledStatusChecking.Visibility = Visibility.Collapsed;

            svDeliveredStatusChecking.Visibility = Visibility.Visible;
        }

        private void rbCancelled_Checked(object sender, RoutedEventArgs e)
        {
            //svPendingStatusChecking.Visibility = Visibility.Collapsed;
            svDeliveringStatusChecking.Visibility = Visibility.Collapsed;
            svDeliveredStatusChecking.Visibility = Visibility.Collapsed;

            //svCancelledStatusChecking.Visibility = Visibility.Visible;
        }
    }
}
