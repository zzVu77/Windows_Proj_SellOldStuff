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
    /// Interaction logic for UC_ItemDetail.xaml
    /// </summary>
    public partial class UC_ItemDetail : UserControl
    {
        public UC_ItemDetail()
        {
            InitializeComponent();

            SetDefaultText();
        }

        private void SetDefaultText()
        {
            // Tạo một FlowDocument
            FlowDocument flowDoc = new FlowDocument();

            // Thêm một Paragraph chứa văn bản mặc định vào FlowDocument
            Paragraph paragraph = new Paragraph(new Run("The iPhone 14 Pro Max comes with 6.7-inch OLED display with 120Hz refresh rate and Apple's improved Bionic A16 processor. On the back there is a Triple camera setup with 48MP main camera"));
            flowDoc.Blocks.Add(paragraph);

            // Gán FlowDocument cho RichTextBox
            rtbDescription.Document = flowDoc;
        }
    }
}
