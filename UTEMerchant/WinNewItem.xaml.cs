using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Wpf.Ui.Controls;

namespace UTEMerchant
{
    /// <summary>
    /// Interaction logic for WinNewItem.xaml
    /// </summary>
    public partial class WinNewItem : Window
    {
        public WinNewItem()
        {
            InitializeComponent();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Filter = "Tất cả các tệp (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                string selectedFilePath = openFileDialog.FileName;
                // Xử lý đường dẫn đã chọn ở đây
            }
        }

        private void rtbDescription_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
      
        //private void ToggleButton_Click(object sender, RoutedEventArgs e)
        //{
        //    var toggleButton = sender as ToggleButton;
        //    if (toggleButton != null)
        //    {
        //        var comboBox = toggleButton.TemplatedParent as ComboBox;
        //        if (comboBox != null)
        //        {
        //            comboBox.IsDropDownOpen = !comboBox.IsDropDownOpen;
        //        }
        //    }
        //}
    }
}
