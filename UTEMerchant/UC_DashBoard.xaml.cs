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
        DB_Connection db = new DB_Connection();
        public ChartValues<ObservableValue> DataValues { get; set; }
        public List<string> Labels { get; set; }
        PurchasedItem_DAO purchasedItemDAO = new PurchasedItem_DAO();
        Item_DAO itemDAO = new Item_DAO();
        private CustomerReviewDAO feedbackDAO = new CustomerReviewDAO();
        private List<CustomerReview> feedbacks = new List<CustomerReview>();
        private user_DAO userDAO = new user_DAO();
        public UC_DashBoard()
        {
            InitializeComponent();
            SetDefault();
        }
        public void LoadRevenueChart(int sellerID)
        {
            // Khởi tạo DataValues và Labels
            DataValues = new ChartValues<ObservableValue>();
            Labels = new List<string>();

            // Kết nối cơ sở dữ liệu và truy vấn dữ liệu
            string connectionString = db.connectionString;
            string query = @"
        SELECT 
            MONTH(pp.PurchaseDate) AS Month,
            YEAR(pp.PurchaseDate) AS Year,
            SUM(i.price) AS TotalRevenue
        FROM 
            PurchasedProducts pp
        INNER JOIN 
            Item i ON pp.Item_Id = i.Item_Id
        WHERE
            i.SellerID = @sellerID
        GROUP BY 
            YEAR(pp.PurchaseDate), MONTH(pp.PurchaseDate)
        ORDER BY 
            Year, Month";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@sellerID", sellerID); // Thêm tham số sellerID
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int month = Convert.ToInt32(reader["Month"]);
                    int year = Convert.ToInt32(reader["Year"]);
                    double revenue = Convert.ToDouble(reader["TotalRevenue"]);

                    string monthYear = $"{month}/{year}";
                    Labels.Add(monthYear);

                    // Thêm dữ liệu vào DataValues
                    DataValues.Add(new ObservableValue(revenue));
                }

                reader.Close();
            }

            // Binding dữ liệu vào LiveChart
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

                feedbacks = feedbackDAO.GetFeedBack(StaticValue.SELLER.SellerID);
                if (feedbacks.Count() >0)
                {

                    spListFeedBacks.Children.Clear();
                    foreach (CustomerReview feedback in feedbacks)
                    {
                        Item item = itemDAO.GetItemByItemID(feedback.Item_ID);
                        User user = userDAO.GetUserByItemID(feedback.ID_User);
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
    
