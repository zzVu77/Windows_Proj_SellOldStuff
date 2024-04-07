using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Wpf.Ui.Controls;

namespace UTEMerchant
{
    /// <summary>
    /// Interaction logic for UC_SellerUI.xaml
    /// </summary>
    public partial class UC_SellerUI : UserControl
    {
        List<Item> items;
        Item_DAO dao = new Item_DAO();
        public UC_SellerUI()
        {
            InitializeComponent();       
            

        }
        public UC_SellerUI(int idSeller) :this()
        {
            items = dao.Load();
            
            foreach (Item a in items)
            {
                if(idSeller == a.SellerID)
                    productGrid.Items.Add(a);
            }

        }
        private void productGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        //private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        //{
        //    new WinNewItem().ShowDialog();
        //    productGrid.Items.Clear();
        //    var test = new Item_DAO();
        //    test.Load();
        //    foreach (Item a in test.items)
        //    {
        //        productGrid.Items.Add(a);
        //    }
        //}


        /*private void btnDelete_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (productGrid.SelectedItem != null)
            {
                DataGridRow clickedRow = (DataGridRow)productGrid.ItemContainerGenerator.ContainerFromItem(productGrid.SelectedItem);
                if (clickedRow != null)
                {
                    int rowIndex = productGrid.ItemContainerGenerator.IndexFromContainer(clickedRow);
                    Item_DAO dAO = new Item_DAO();
                    dAO.remove(items[rowIndex]);

                    if (productGrid.ItemsSource is IList data)
                    {
                        data.RemoveAt(rowIndex);
                    }
                }
            }

        }

        private void btnUpdate_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
        }
*/
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            
            items= dao.Load();
            DataGridRow clickedRow = (DataGridRow)productGrid.ItemContainerGenerator.ContainerFromItem(productGrid.SelectedItem);
            int rowIndex = productGrid.ItemContainerGenerator.IndexFromContainer(clickedRow);
            dao.RemoveItem(items[rowIndex]);
        
            
            productGrid.Items.RemoveAt(rowIndex);
            productGrid.Items.Refresh();    
        }

        

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new WinNewItem().ShowDialog();
            productGrid.Items.Clear();
            
            foreach (Item a in dao.Load())
            {
                productGrid.Items.Add(a);
            }
        }
    }
}
