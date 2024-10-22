﻿using System;
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
    /// Interaction logic for UC_DeliveringItemsBox.xaml
    /// </summary>
    public partial class UC_DeliveringItemsBox : UserControl
    {
        public event EventHandler ReceivedButtonClicked;
        private readonly List<purchasedItem> _orders;
        private readonly Seller _seller;
        private readonly int _userId;        
        public UC_DeliveringItemsBox()
        {
            InitializeComponent();
            this.Width = 1300;
        }

        public UC_DeliveringItemsBox(List<purchasedItem> orders, Seller seller, int userId) : this()
        {
            this._orders = orders;
            this._userId = userId; 
            this._seller = seller;
        }

        private void btnReceived_Click(object sender, RoutedEventArgs e)
        {
            //string deliveryStatus = purchasedItemDAO.GetPurchasedProductStatus(Item.Item_Id, this.userID);
            foreach (var item in _orders)
            {
                new PurchasedItem_DAO().UpdateDeliveryStatus(item.PurchaseID, "delivered");
            }
            ReceivedButtonClicked?.Invoke(this, EventArgs.Empty);
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (_seller != null)
            {
                tbShopName.Text = _seller.ShopName;
            }

            if (_orders != null)
            {
                foreach (var item in _orders)
                {
                    AddItem(item);
                }
            }
        }

        private void AddItem(purchasedItem item)
        {
            foreach (UC_DeliveringItem ucDeliveringItem in spItems.Children)
            {
                if (ucDeliveringItem.Item.Item_Id == item.Item_Id)
                {
                    return;
                }
            }
            UC_DeliveringItem uc_item = new UC_DeliveringItem(item);
            spItems.Children.Add(uc_item);
        }

        private void tbTotalValue_Loaded(object sender, RoutedEventArgs e)
        {
            double totalValue = 0;
            foreach (var item in _orders)
            {
                totalValue += new PurchasedItem_DAO().GetItem(item.PurchaseID).Price;
            }
            tbTotalValue.Text = $"${totalValue.ToString("F", CultureInfo.CurrentCulture)}";
        }

        private void tbNumberOfItems_Loaded(object sender, RoutedEventArgs e)
        {
            tbNumberOfItems.Text = $"{_orders.Count} items";
        }
    }
}
