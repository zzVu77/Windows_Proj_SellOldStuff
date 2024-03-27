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
    /// Interaction logic for UC_ShoppingCartItemView.xaml
    /// </summary>
    public partial class UC_ShoppingCartItemView : UserControl
    {
        private Item shoppingCartItem;
        user_DAO user_DAO = new user_DAO();
        List<User> users = new List<User>();
        public UC_ShoppingCartItemView()
        {
            InitializeComponent();
        }

        public UC_ShoppingCartItemView(Item item) : this() 
        {
            users = user_DAO.Load();

            shoppingCartItem = item;
            tbShopName.Text = users.Where(u => u.Id_user == item.user_id).FirstOrDefault().Name_shop;
            tbItemName.Text = item.Name;
            tbItemPrice.Text = $"{item.Price.ToString()}$";
            var resourceUri = new Uri(shoppingCartItem.ImagePath, UriKind.RelativeOrAbsolute);
            imgItem.Source = new BitmapImage(resourceUri);

        }

        private void tbShopName_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
