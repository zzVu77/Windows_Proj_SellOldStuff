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

            var test =new Item_DAO();
            test.Load();
            foreach  (Item a in test.items)
            {
                productGrid.Items.Add(a);
            }    
            
        }

        private void productGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            new WinNewItem().ShowDialog();
            productGrid.Items.Clear();
            var test = new Item_DAO();
            test.Load();
            foreach (Item a in test.items)
            {
                productGrid.Items.Add(a);
            }
        }
    }
}
