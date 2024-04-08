using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
    /// Interaction logic for WinBuyingInterface.xaml
    /// </summary>
    public partial class WinBuyingInterface : Window
    {
        public event EventHandler ItemClicked;
        public Item info;
        List<User> users = new List<User>();
        user_DAO user_dao = new user_DAO();
        private int Id_User;
        public WinBuyingInterface()
        {
            InitializeComponent();
        }
        public WinBuyingInterface(Item item, int id_user)
        {
            info = item;
            Id_User = id_user;
            var user_dao = new user_DAO();
            users = user_dao.Load();
            InitializeComponent();
            SetDefault();
        }
        private void SetDefault()
        {
            txblOrderItemName.Text = info.Name;
            txblOrderOriginalPrice.Text = info.Original_Price.ToString()+"$";
            txblOrderPrice.Text = info.Price.ToString() + "$";
            txblConditon.Text = info.Condition.ToString()+"%";
            txblTotalValue.Text = info.Price.ToString() + "$";
            var resourceUri = new Uri(info.Image_Path, UriKind.RelativeOrAbsolute);
            imgOrderItem.Source = new BitmapImage(resourceUri);

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            purchasedItem item = new purchasedItem();
            item.Id_user = Id_User;
            item.Item_Id = info.Item_Id;
            new PurchasedItem_DAO().AddItem(item);
            this.Close();
        }
    }
}
