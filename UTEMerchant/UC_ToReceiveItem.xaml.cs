using System;
using System.Collections.Generic;
using System.Globalization;
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
    /// Interaction logic for UC_ToReceiveItem.xaml
    /// </summary>
    public partial class UC_ToReceiveItem : UserControl
    {

        private readonly Item Item;
        private readonly User User;

        public UC_ToReceiveItem()
        {
            InitializeComponent();
        }

        public UC_ToReceiveItem(Item item, User user) : this()
        {
            this.Item = item;
            this.User = user;
            SetData(item, user);
        }

        //Binding data to the UI
        private void SetData(Item item, User user)
        {
            imgToReceiveItem.Source = item.Image;
            txblShopName.Text = user.Name;
            txblToReceiveOriginalPrice.Text = $"{item.Original_Price.ToString(CultureInfo.InvariantCulture)}$";
            txblToReceivePrice.Text = $"{item.Price.ToString(CultureInfo.InvariantCulture)}$";
            txblToReceiveConditon.Text = $"{item.Condition.ToString(CultureInfo.InvariantCulture)}%";
            txblToReceiveItemName.Text = item.Name;
        }
    }
}
