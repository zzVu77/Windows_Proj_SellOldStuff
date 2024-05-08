using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.NetworkInformation;
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
using Wpf.Ui.Controls;
using Image = System.Windows.Controls.Image;

namespace UTEMerchant
{
    /// <summary>
    /// Interaction logic for UC_SellerUI.xaml
    /// </summary>
    public partial class UC_SellerUI : UserControl
    {
        List<Item> items;
        Item_DAO dao = new Item_DAO();
        //private Seller _seller;
        public UC_SellerUI()
        {
            InitializeComponent();       
            

        }
        //public UC_SellerUI(Seller seller) :this()
        //{
        //    this._seller = seller;
        //    items = dao.GetItemsBySellerID(seller.SellerID);
        //}
        private void productGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            items = dao.GetItemsBySellerID(StaticValue.SELLER.SellerID);
            DataGridRow clickedRow = (DataGridRow)productGrid.ItemContainerGenerator.ContainerFromItem(productGrid.SelectedItem);
            int rowIndex = productGrid.ItemContainerGenerator.IndexFromContainer(clickedRow);
            WinUpdateItem winUpdateItem = new WinUpdateItem(items[rowIndex]);
            winUpdateItem.ShowDialog();

            productGrid.Items.Clear();
            UserControl_Loaded(this, new RoutedEventArgs());



        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            
            items= dao.GetItemsBySellerID(StaticValue.SELLER.SellerID);
            DataGridRow clickedRow = (DataGridRow)productGrid.ItemContainerGenerator.ContainerFromItem(productGrid.SelectedItem);
            int rowIndex = productGrid.ItemContainerGenerator.IndexFromContainer(clickedRow);
            if (items[rowIndex].Sale_Status == false)
            {
                dao.RemoveItem(items[rowIndex]);

                productGrid.Items.RemoveAt(rowIndex);

                productGrid.Items.Refresh();
            }
            else
            {
                System.Windows.MessageBox.Show("11111");
            }
        }

        

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new WinNewItem(StaticValue.SELLER.SellerID).ShowDialog();
            productGrid.Items.Clear();
            
            foreach (Item a in dao.Load())
            {
                if (StaticValue.SELLER.SellerID== a.SellerID)
                    productGrid.Items.Add(a);
            }
        }

        private void textBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            /*if (!string.IsNullOrWhiteSpace(textBoxSearch.Text))
            {
                List<Item> itemsSearch = dao.Search(textBoxSearch.Text);
                if (itemsSearch.Count > 0)
                {
                    productGrid.Items.Clear();
                    foreach (Item item in itemsSearch)
                    {
                        if (IdSeller == item.SellerID)
                            productGrid.Items.Add(item);
                    }
                }
            }
            else
            {
                productGrid.Items.Clear();
                foreach (Item a in items)
                {
                    productGrid.Items.Add(a);
                }
            }*/
        }

        private void textBoxFilter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (!string.IsNullOrWhiteSpace(textBoxFilter.Text))
                {
                    List<Item> itemsSearch = dao.Search(textBoxFilter.Text);
                    if (itemsSearch.Count > 0)
                    {
                        productGrid.Items.Clear();
                        foreach (Item item in itemsSearch)
                        {
                            if (StaticValue.SELLER.SellerID == item.SellerID)
                                productGrid.Items.Add(item);
                        }
                    }
                }
                else
                {
                    productGrid.Items.Clear();
                    foreach (Item a in items)
                    {
                        productGrid.Items.Add(a);
                    }
                }
            }
        }

        private void imgItem_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Image viewer
            WinImageZoom winImageZoom = new WinImageZoom(sender as Image);
            winImageZoom.ShowDialog();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (items != null)
            {
                productGrid.Items.Clear();
                foreach (Item a in items)
                {
                    productGrid.Items.Add(a);
                }
            }

                
        }

        public void SetSeller()
        {
            
            items = dao.GetItemsBySellerID(StaticValue.SELLER.SellerID);
            UserControl_Loaded(this, new RoutedEventArgs());
        }
    }

}
