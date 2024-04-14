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
    /// Interaction logic for UC_StarRating.xaml
    /// </summary>
    public partial class UC_StarRating : UserControl
    {
        private float Rating;

        public UC_StarRating()
        {
            InitializeComponent();

        }

        private void DisableMouseOver()
        {
            btn1stStarFirstHalf.IsEnabled = false;
            btn1stStarLatterHalf.IsEnabled = false;
            btn2ndStarFirstHalf.IsEnabled = false;
            btn2ndStarLatterHalf.IsEnabled = false;
            btn3rdStarFirstHalf.IsEnabled = false;
            btn3rdStarLatterHalf.IsEnabled = false;
            btn4thStarFirstHalf.IsEnabled = false;
            btn4thStarLatterHalf.IsEnabled = false;
            btn5thStarFirstHalf.IsEnabled = false;
            btn5thStarLatterHalf.IsEnabled = false;
        }

        private void EnableMouseOver()
        {
            btn1stStarFirstHalf.IsEnabled = true;
            btn1stStarLatterHalf.IsEnabled = true;
            btn2ndStarFirstHalf.IsEnabled = true;
            btn2ndStarLatterHalf.IsEnabled = true;
            btn3rdStarFirstHalf.IsEnabled = true;
            btn3rdStarLatterHalf.IsEnabled = true;
            btn4thStarFirstHalf.IsEnabled = true;
            btn4thStarLatterHalf.IsEnabled = true;
            btn5thStarFirstHalf.IsEnabled = true;
            btn5thStarLatterHalf.IsEnabled = true;
        }

        private void btn1stStarFirstHalf_Click(object sender, RoutedEventArgs e)
        {
            Rating += 0.5f;
            DisableMouseOver();
            grdInteract.Visibility = Visibility.Collapsed;
            grdView.Visibility = Visibility.Visible;
        }

        private void btn1stStarLatterHalf_Click(object sender, RoutedEventArgs e)
        {
            Rating += 0.5f;
            btn1stStarFirstHalf_Click(sender, e);
        }

        private void btn2ndStarFirstHalf_Click(object sender, RoutedEventArgs e)
        {
            Rating += 0.5f;
            btn1stStarLatterHalf_Click(sender, e);
        }

        private void btn2ndStarLatterHalf_Click(object sender, RoutedEventArgs e)
        {
            Rating += 0.5f;
            btn2ndStarFirstHalf_Click(sender, e);
        }

        private void btn3rdStarFirstHalf_Click(object sender, RoutedEventArgs e)
        {
            Rating += 0.5f;
            btn2ndStarLatterHalf_Click(sender, e);
        }

        private void btn3rdStarLatterHalf_Click(object sender, RoutedEventArgs e)
        {
            Rating += 0.5f;
            btn3rdStarFirstHalf_Click(sender, e);
        }

        private void btn4thStarFirstHalf_Click(object sender, RoutedEventArgs e)
        {
            Rating += 0.5f;
            btn3rdStarLatterHalf_Click(sender, e);
        }

        private void btn4thStarLatterHalf_Click(object sender, RoutedEventArgs e)
        {
            Rating += 0.5f;
            btn4thStarFirstHalf_Click(sender, e);
        }

        private void btn5thStarFirstHalf_Click(object sender, RoutedEventArgs e)
        {
            Rating += 0.5f;
            btn4thStarLatterHalf_Click(sender, e);
        }

        private void btn5thStarLatterHalf_Click(object sender, RoutedEventArgs e)
        {
            Rating += 0.5f;
            btn5thStarFirstHalf_Click(sender, e);
        }

        public float GetRating()
        {
            return Rating;
        }

        private void grdView_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Rating = 0;
            grdView.Visibility = Visibility.Collapsed;
            grdInteract.Visibility = Visibility.Visible;
            EnableMouseOver();
        }

        public void SetStar(float point)
        {
            switch (point)
            {
                case 0.5f:
                    btn1stStarFirstHalf_Click(this, null);
                    break;
                case 1.0f:
                    btn1stStarLatterHalf_Click(this, null);
                    break;
                case 1.5f:
                    btn2ndStarFirstHalf_Click(this, null);
                    break;
                case 2.0f:
                    btn2ndStarLatterHalf_Click(this, null);
                    break;
                case 2.5f:
                    btn3rdStarFirstHalf_Click(this, null);
                    break;
                case 3.0f:
                    btn3rdStarLatterHalf_Click(this, null);
                    break;
                case 3.5f:
                    btn4thStarFirstHalf_Click(this, null);
                    break;
                case 4.0f:
                    btn4thStarLatterHalf_Click(this, null);
                    break;
                case 4.5f:
                    btn5thStarFirstHalf_Click(this, null);
                    break;
                case 5.0f:
                    btn5thStarLatterHalf_Click(this, null);
                    break;
                default:
                    // Xử lý trường hợp mặc định (nếu cần)
                    break;
            }
        }

    }
}
