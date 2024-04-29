using System;
using System.Collections;
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
    /// Interaction logic for UC_PendingOrderBox.xaml
    /// </summary>
    public partial class UC_PendingOrderBox : UserControl
    {
        private readonly IGrouping<DateTime, Item> _items;
        private User _user;

        public UC_PendingOrderBox()
        {
            InitializeComponent();
        }

        public UC_PendingOrderBox(IGrouping<DateTime, Item> items, User user) : this()
        {
            _items = items;
            _user = user;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (_items != null && _user != null)
            {
                spItems.Children.Clear();
                IOrderedEnumerable<Item> order = _items.OrderBy(x => x.SellerID);
                int k = 0;
                for (int i = 0; i < order.Count(); i++)
                {
                    if (i == order.Count() - 1) ;
                    else if (order.ElementAt(i).SellerID == order.ElementAt(i + 1).SellerID) continue;
                    UC_PendingItemsBox ucPendingItemsBox = new UC_PendingItemsBox(order.Skip(k).Take(i - k + 1).ToList(), new Seller_DAO().GetSeller(order.ElementAt(i).SellerID), _user.Id_user);
                    spItems.Children.Add(ucPendingItemsBox);
                    k = i + 1;
                }

                tbTotalValue.Text = CalculateTotalPrice().ToString("F", System.Globalization.CultureInfo.CurrentCulture);
            }
        }

        private double CalculateTotalPrice()
        {
            double totalPrice = 0;
            foreach (UC_PendingItemsBox ucPendingItemsBox in spItems.Children)
            {
                foreach (UC_PendingItem ucPendingItem in ucPendingItemsBox.spItems.Children)
                {
                    totalPrice += ucPendingItem.Item.Price;
                }
            }
            return totalPrice;
        }
    }
}
