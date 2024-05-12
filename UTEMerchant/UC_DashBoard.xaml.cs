using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
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
using LiveCharts;
using LiveCharts.Configurations;
using LiveCharts.Defaults;
using LiveCharts.Wpf;

namespace UTEMerchant
{
    /// <summary>
    /// Interaction logic for DashBoard.xaml
    /// </summary>
    public partial class UC_DashBoard : UserControl
    {
        UTEMerchantContext db = new UTEMerchantContext();
        public ChartValues<ObservableValue> DataValues { get; set; }
        public List<string> Labels { get; set; }
        PurchasedItem_DAO purchasedItemDAO = new PurchasedItem_DAO();
        Item_DAO itemDAO = new Item_DAO();
        private CustomerReviewDAO feedbackDAO = new CustomerReviewDAO();
        private List<CustomerReview> feedbacks = new List<CustomerReview>();
        private User_DAO userDAO = new User_DAO();
        public UC_DashBoard()
        {
            InitializeComponent();
            SetDefault();
        }
        public void LoadRevenueChart(int sellerID)
        {
            // Initialize DataValues and Labels
            DataValues = new ChartValues<ObservableValue>();
            Labels = new List<string>();

            using (var context = new UTEMerchantContext()) // Instantiate your DbContext
            {
                var query = from pp in context.purchasedProducts
                            join i in context.Items on pp.Item_Id equals i.Item_Id
                            where i.SellerID == sellerID
                            group new { pp, i } by new { pp.PurchaseDate.Value.Year, pp.PurchaseDate.Value.Month } into g
                            orderby g.Key.Year, g.Key.Month
                            select new
                            {
                                Year = g.Key.Year,
                                Month = g.Key.Month,
                                TotalRevenue = g.Sum(x => x.i.price)
                            };

                foreach (var result in query)
                {
                    string monthYear = $"{result.Month}/{result.Year}";
                    Labels.Add(monthYear);
                    DataValues.Add(new ObservableValue(result.TotalRevenue.Value));
                }
            }

            // Bind data to LiveChart
            DataContext = this;
        }

        public void SetDefault()
        {
            if (StaticValue.SELLER != null)
            {
                LoadRevenueChart(StaticValue.SELLER.SellerID);
                txbTotalValue.Text = "$" + purchasedItemDAO.CalculateTotalPrice(StaticValue.SELLER.SellerID).ToString();
                txbSoldValue.Text = purchasedItemDAO.CalculateTotalSold(StaticValue.SELLER.SellerID).ToString();
                txbProductsValue.Text = itemDAO.CalculateTotalProducts(StaticValue.SELLER.SellerID).ToString();
                txbAverageRatingnValue.Text = feedbackDAO.CalculateAverage(StaticValue.SELLER.SellerID).ToString();

                feedbacks = feedbackDAO.GetFeedback(StaticValue.SELLER.SellerID);
                if (feedbacks.Count() >0)
                {

                    spListFeedBacks.Children.Clear();
                    foreach (CustomerReview feedback in feedbacks)
                    {
                        Item item = itemDAO.GetItemByItemID(feedback.Item_Id);
                        User user = userDAO.GetUserByItemID(feedback.Id_user);
                        UC_FeedbackForDashBoard uC_FeedBackUI = new UC_FeedbackForDashBoard(item, user, feedback);
                        spListFeedBacks.Children.Add(uC_FeedBackUI);
                    }

                }
                else imgNotFound.Visibility = Visibility.Visible;
            }    
         
        }

        public void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            SetDefault();
        }
    }
}
    
