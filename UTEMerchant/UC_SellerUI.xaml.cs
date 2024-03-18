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
    /// Interaction logic for UC_SellerUI.xaml
    /// </summary>
    public partial class UC_SellerUI : UserControl
    {
        public UC_SellerUI()
        {
            InitializeComponent();
            Item item1 = new Item(01, "Iphone 14 Pro Maxsdsdasdas", "RAM 8GB ROM 256GB", 1000, 600, "/Img/iPhone-14-Pro-Max-9907.jpg", new DateTime(2023, 11, 20), "95%", "Smart Phone");
            productGrid.Items.Add(item1);
            productGrid.Items.Add(item1);
            productGrid.Items.Add(item1);
            productGrid.Items.Add(item1);
            productGrid.Items.Add(item1);
            productGrid.Items.Add(item1);
            productGrid.Items.Add(item1);
            productGrid.Items.Add(item1);
            productGrid.Items.Add(item1);
            productGrid.Items.Add(item1);
            productGrid.Items.Add(item1);
            productGrid.Items.Add(item1);
            productGrid.Items.Add(item1);
            productGrid.Items.Add(item1);
            productGrid.Items.Add(item1);
            productGrid.Items.Add(item1);
            productGrid.Items.Add(item1);
            productGrid.Items.Add(item1);
            productGrid.Items.Add(item1);
            productGrid.Items.Add(item1);
            productGrid.Items.Add(item1);
        }

        private void productGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
