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
    /// Interaction logic for UC_ShoppingCartItemsView.xaml
    /// </summary>
    public partial class UC_ShoppingCartItemsView : UserControl
    {
        // Flag to determine whether all items should be unchecked when the checkbox for all items is unchecked from code-behind
        private bool _shouldUnmarkAll = true;

        // Flag to determine whether all items are already checked
        private bool _alreadyCheckedEveryItem = false;

        private readonly Seller _seller;
        private readonly List<Item> _items = new List<Item>();
        private readonly List<Item> _selectedItems = new List<Item>();

        public UC_ShoppingCartItemsView()
        {
            InitializeComponent();
        }

        public UC_ShoppingCartItemsView(Seller seller) : this()
        {
            this._seller = seller;
        }

        private void tbShopName_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        public bool AddItem(Item item)
        {
            _items.Add(item);
            UC_ItemInShoppingCartItemsView ucItem = new UC_ItemInShoppingCartItemsView(item);
            ucItem.TogItemChecked += togItem_Checked;
            ucItem.TogItemUnchecked += togItem_Unchecked;
            ucItem.RmItemClicked += btnRemoveItem_Click;
            spItems.Children.Add(ucItem);
            return true;
        }

        private void togAll_Checked(object sender, RoutedEventArgs e)
        {
            if (_alreadyCheckedEveryItem)
            {
                return;
            }

            // Add all items to the list of selected items
            foreach (UC_ItemInShoppingCartItemsView item in spItems.Children)
            {
                if (item.togItem.IsChecked == false)
                    item.togItem.IsChecked = true;
            }

            _shouldUnmarkAll = true;
        }

        private void togAll_Unchecked(object sender, RoutedEventArgs e)
        {
            if (_shouldUnmarkAll)
            {
                _alreadyCheckedEveryItem = false;

                // Clear the list of selected items
                foreach (UC_ItemInShoppingCartItemsView item in spItems.Children)
                {
                    item.togItem.IsChecked = false;
                }
            }
        }

        private void togItem_Unchecked(object sender, RoutedEventArgs e)
        {

            if (sender is UC_ItemInShoppingCartItemsView item)
            {
                _selectedItems.Remove(item.GetItem());
                UncheckCheckboxAll();
            }
        }

        private void togItem_Checked(object sender, RoutedEventArgs e)
        {
            if (sender is UC_ItemInShoppingCartItemsView item)
            {
                _selectedItems.Add(item.GetItem());
                CheckCheckboxAll();
            }
        }

        // If all items are selected, but then one item is unchecked, mark the checkbox for all items as unchecked
        public void UncheckCheckboxAll()
        {
            if (_selectedItems.Count < _items.Count)
            {
                // Set the flag to false in order to prevent the checkbox for all items from being unchecked
                _shouldUnmarkAll = false;

                // Mark that not all items are checked
                _alreadyCheckedEveryItem = false;

                // Uncheck the checkbox for all items
                togAll.IsChecked = false;
            }
        }


        // Method to check whether all items are selected, if so, mark the checkbox for all items as checked
        public void CheckCheckboxAll()
        {
            var allChecked = _selectedItems.Count == _items.Count;
            if (allChecked)
            {
                // Mark that all items can be unchecked at once
                _shouldUnmarkAll = true;

                // Set the flag to true in order to prevent the checkbox for all items from being checked again
                _alreadyCheckedEveryItem = true;

                // Check the checkbox for all items
                togAll.IsChecked = true;
            }
        }

        public List<Item> GetSelectedItems()
        {
            return _selectedItems;
        }

        public Seller GetSeller()
        {
            return _seller;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (_seller != null)
            {
                tbShopName.Text = _seller.ShopName;
                tbShopName.MouseDown += tbShopName_MouseDown;
            }

        }

        private void lblEdit_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (lblEdit.Text == "Edit")
            {
                lblEdit.Text = "Done";
                foreach (UC_ItemInShoppingCartItemsView item in spItems.Children)
                {
                    item.SetRemoveButtonVisibility(true);
                }
            }
            else
            {
                lblEdit.Text = "Edit";
                foreach (UC_ItemInShoppingCartItemsView item in spItems.Children)
                {
                    item.SetRemoveButtonVisibility(false);
                }
            }
        }

        private void btnRemoveItem_Click(object sender, RoutedEventArgs e)
        {
            if (sender is UC_ItemInShoppingCartItemsView item)
            {
                // Remove the item
                item.SetRemoveButtonVisibility(false);
                item.togItem.IsChecked = false;
                _items.Remove(item.GetItem());
                spItems.Children.Remove(item);

                // Check if there are no items left of a shop
                if (_items.Count == 0)
                {
                    // Get the parent object of the current object
                    DependencyObject parentObject = this;
                    while (parentObject != null && !(parentObject is UC_ShoppingCart))
                    {
                        parentObject = VisualTreeHelper.GetParent(parentObject);
                    }

                    UC_ShoppingCart parent = parentObject as UC_ShoppingCart;

                    parent.spItems.Children.Remove(this);
                    parent.CheckCart();
                }
            }
        }
    }
}
