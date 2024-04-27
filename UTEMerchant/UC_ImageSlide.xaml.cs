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
    /// Interaction logic for UC_ImageSlide.xaml
    /// </summary>
    public partial class UC_ImageSlide : UserControl
    {
        public event EventHandler<RoutedEventArgs> ImageClicked;
        public string imgPath;
        public UC_ImageSlide()
        {
            InitializeComponent();
            this.MouseUp += UC_Image_MouseUp;
        }
        public UC_ImageSlide(string imgPath):this()
        {
            this.imgPath = imgPath;
            SetDefault();

        }
        private void UC_Image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ImageClicked?.Invoke(this, EventArgs.Empty as RoutedEventArgs);
        }

        public void SetDefault()
        {
            var resourceUri = new Uri(this.imgPath, UriKind.RelativeOrAbsolute);
            img.Source = new BitmapImage(resourceUri);
        }
    }
}
