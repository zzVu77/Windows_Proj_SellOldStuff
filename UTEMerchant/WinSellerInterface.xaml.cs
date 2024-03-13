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
            Item item1 = new Item(01, "Iphone", "Iphone 14 Pro Max 512GB", 100000, "E:\\HCM_UTE\\Semester_4\\Windows Programming\\FinalProj\\Window_Proj_UTEMerchant\\UTEMerchant\\Img\\iPhone-14-Pro-Max-9907.jpg",10);
            productGrid.Items.Add(item1);
            Item item2 = new Item(02, "Iphone", "Iphone 11 Pro Max 512GB", 500, "E:\\HCM_UTE\\Semester_4\\Windows Programming\\FinalProj\\Window_Proj_UTEMerchant\\UTEMerchant\\Img\\iPhone-14-Pro-Max-9907.jpg",2);
            productGrid.Items.Add(item2);
             Item item3 = new Item(03, "Iphone", "Iphone 11 Pro Max 512GB", 500, "E:\\HCM_UTE\\Semester_4\\Windows Programming\\FinalProj\\Window_Proj_UTEMerchant\\UTEMerchant\\Img\\iPhone-14-Pro-Max-9907.jpg",2);
            productGrid.Items.Add(item3);
             Item item4 = new Item(04, "Iphone", "Iphone 11 Pro Max 512GB", 500, "E:\\HCM_UTE\\Semester_4\\Windows Programming\\FinalProj\\Window_Proj_UTEMerchant\\UTEMerchant\\Img\\iPhone-14-Pro-Max-9907.jpg",2);
            productGrid.Items.Add(item4);
             Item item5 = new Item(05, "Iphone", "Iphone 11 Pro Max 512GB", 500, "E:\\HCM_UTE\\Semester_4\\Windows Programming\\FinalProj\\Window_Proj_UTEMerchant\\UTEMerchant\\Img\\iPhone-14-Pro-Max-9907.jpg",2);
            productGrid.Items.Add(item5);
             Item item6 = new Item(06, "Iphone", "Iphone 11 Pro Max 512GB  dsadasdas asdasd adasdasd asdadasd adasdasdasdasdadaas adas asasdasdas asd ádasdsd", 500, "E:\\HCM_UTE\\Semester_4\\Windows Programming\\FinalProj\\Window_Proj_UTEMerchant\\UTEMerchant\\Img\\iPhone-14-Pro-Max-9907.jpg",2);
            productGrid.Items.Add(item6);
             Item item7 = new Item(07, "Iphone", "Iphone 11 Pro Max 512GB", 500, "E:\\HCM_UTE\\Semester_4\\Windows Programming\\FinalProj\\Window_Proj_UTEMerchant\\UTEMerchant\\Img\\iPhone-14-Pro-Max-9907.jpg",2);
            productGrid.Items.Add(item7);

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
    }
}

