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
            Paragraph paragraph = new Paragraph(new Run("Size and Weight2\r\nWidth: 3.05 inches (77.6 mm)\r\nHeight: 6.33 inches (160.7 mm)\r\nDepth: 0.31 inch (7.85 mm)\r\nWeight: 8.47 ounces (240 grams)\r\nDisplay\r\nSuper Retina XDR display\r\n6.7‑inch (diagonal) all‑screen OLED display\r\n2796‑by‑1290-pixel resolution at 460 ppi\r\nDynamic Island\r\nAlways-On display\r\nProMotion technology with adaptive refresh rates up to 120Hz\r\nHDR display\r\nTrue Tone\r\nWide color (P3)\r\nHaptic Touch\r\n2,000,000:1 contrast ratio (typical)\r\n1000 nits max brightness (typical); 1600 nits peak brightness (HDR); 2000 nits peak brightness (outdoor)\r\nFingerprint-resistant oleophobic coating\r\nSupport for display of multiple languages and characters simultaneously"));
            flowDoc.Blocks.Add(paragraph);

            // Gán FlowDocument cho RichTextBox
            rtbDescription.Document = flowDoc;
        }
    }
}
