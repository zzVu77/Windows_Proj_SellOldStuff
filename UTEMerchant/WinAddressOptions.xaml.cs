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
using System.Windows.Threading;

namespace UTEMerchant
{
    /// <summary>
    /// Interaction logic for WinAddressOptions.xaml
    /// </summary>
    public partial class WinAddressOptions : Window
    {
        private DeliveryAddress _selectedDeliveryAddress;
        private int _selectedDeliveryPort;
        private int _userID;
        private List<DeliveryAddress> _deliveryAddresses;

        public WinAddressOptions()
        {
            InitializeComponent();
        }

        public WinAddressOptions(int userID) : this()
        {
            _userID = userID;
        }

        public WinAddressOptions(int userID, int ID) : this()
        {
            _userID = userID;
            _selectedDeliveryPort = ID;
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            // Get the delivery address
            var deliveryAddress = GetAddressBox(sender as DependencyObject).DeliveryAddress;
            // Open edit address window
            WinAddressCustomization winAddressCustomization = new WinAddressCustomization(deliveryAddress);
            winAddressCustomization.ShowDialog();

            // Update address
            if (winAddressCustomization.DialogResult == true)
            {
                // Reload the window
                Window_Loaded(this, new RoutedEventArgs());

            }
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            _selectedDeliveryAddress = GetAddressBox(sender as DependencyObject).DeliveryAddress;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (_userID == 0)
            {
                return;
            }

            // Load delivery addresses
            _deliveryAddresses = new DeliveryAddress_DAO().GetDeliveryAddressByUserID(_userID);
            spAddressOptions.Children.Clear();

            // Add delivery addresses to the stack panel
            foreach (var deliveryAddress in _deliveryAddresses)
            {
                // Create a group item from Window Resource
                GroupItem groupItem = (GroupItem)FindResource("AddressOption");
                // Replace the UC_AddressBox in the group item with the new UC_AddressBox
                var addressBox = GetAddressBox(groupItem);
                if (addressBox != null)
                {
                    if (_selectedDeliveryPort != 0)
                    {
                        if (deliveryAddress.ID == _selectedDeliveryPort)
                        {
                            // Set the radio button to checked
                            var radioButton = GetRadioButton(groupItem);
                            if (radioButton != null)
                            {
                                radioButton.IsChecked = true;
                            }
                        }
                    }
                    addressBox.SetData(deliveryAddress);
                }
                // Add the group item to the stack panel
                spAddressOptions.Children.Add(groupItem);
            }

        }

        private void btnAddAddress_Click(object sender, RoutedEventArgs e)
        {
            // Open add address window
            WinAddressCustomization winAddressCustomization = new WinAddressCustomization(false);
            winAddressCustomization.ShowDialog();

            // Update address
            if (winAddressCustomization.DialogResult == true)
            {
                // Reload the window
                Window_Loaded(this, new RoutedEventArgs());
            }
        }

        private UC_AddressBox GetAddressBox(DependencyObject control)
        {
            // Find GroupItem the radio button belongs to
            DependencyObject dependencyObject = control;
            while (dependencyObject != null && dependencyObject.GetType() != typeof(Grid) && dependencyObject.GetType() != typeof(GroupItem))
            {
                dependencyObject = VisualTreeHelper.GetParent(dependencyObject);
            }

            // Get UC_AddressBox
            if (dependencyObject.GetType() == typeof(GroupItem))
            {
                GroupItem groupItem = (GroupItem)dependencyObject;
                foreach (var child in LogicalTreeHelper.GetChildren(groupItem))
                {
                    if (child is Grid)
                    {
                        foreach (var grandChild in LogicalTreeHelper.GetChildren(child as DependencyObject))
                        {
                            if (grandChild is UC_AddressBox)
                            {
                                return (UC_AddressBox)grandChild;
                            }
                        }
                    }
                }
            }
            else
            {
                Grid grid = (Grid)dependencyObject;
                foreach (var child in LogicalTreeHelper.GetChildren(grid))
                {
                    if (child is UC_AddressBox)
                    {
                        return (UC_AddressBox)child;
                    }
                }
            }
            return null;
        }

        private RadioButton GetRadioButton(DependencyObject control)
        {
            // Find GroupItem the radio button belongs to
            DependencyObject dependencyObject = control;
            while (dependencyObject != null && dependencyObject.GetType() != typeof(GroupItem))
            {
                dependencyObject = VisualTreeHelper.GetParent(dependencyObject);
            }

            // Get radio button
            GroupItem groupItem = (GroupItem)dependencyObject;
            foreach (var child in LogicalTreeHelper.GetChildren(groupItem))
            {
                if (child is Grid)
                {
                    foreach (var grandChild in LogicalTreeHelper.GetChildren(child as DependencyObject))
                    {
                        if (grandChild is RadioButton)
                        {
                            return (RadioButton)grandChild;
                        }
                    }
                }
            }
            return null;
        }

        private void GroupItem_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Get the radio button
            var radioButton = GetRadioButton(sender as DependencyObject);
            if (radioButton != null)
            {
                radioButton.IsChecked = false;
                radioButton.IsChecked = true;
            }
            
            DialogResult = true;
            this.Close();
            
        }

        public DeliveryAddress SelectedDeliveryAddress => _selectedDeliveryAddress;
    }
}
