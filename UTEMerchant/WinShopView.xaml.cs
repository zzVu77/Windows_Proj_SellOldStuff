using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Documents.DocumentStructures;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;

namespace UTEMerchant
{
    /// <summary>
    /// Interaction logic for WinShopView.xaml
    /// </summary>
    ///
    
    public partial class WinShopView : Window
    {
        // Flag to check if the categories button is clicked
        private bool IsCategoriesButtonClicked = false;

        // Properties
        private Seller seller;
        private List<Item> items;
        private CustomerReviewDAO feedbackDAO = new CustomerReviewDAO();
        private Item_DAO itemDAO = new Item_DAO();
        private User_DAO userDAO = new User_DAO();
        private List<CustomerReview> feedbacks = new List<CustomerReview>();

        private const string ImgRelativePath = "../../Img/";

        private static readonly string ExecutablePath = AppDomain.CurrentDomain.BaseDirectory;

        private static readonly string ImgFilePath = System.IO.Path.GetFullPath(System.IO.Path.Combine(ExecutablePath, ImgRelativePath));

        public WinShopView()
        {
            InitializeComponent();
            
            Loaded += delegate
            {
                txtAboutUsContent.Text = "UTEMerchant is a platform for UTE students to buy and sell items.\nIt is a project for the subject of Software Engineering.\nThe project is developed by a group of students from the University of Technology and Education.";
                if (items != null)
                {
                    FillShopItems(items);
                    LoadCategories(items);
                }
            };
        }
        public WinShopView(Seller seller, List<Item> items) : this()
        {
            this.seller = seller;
            this.items = items;
            txtShopName.Text = this.seller.ShopName;
            User user = userDAO.GetUserByItemID(this.seller.Id_user);

            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(System.IO.Path.Combine(ImgFilePath, user.Image_path), UriKind.RelativeOrAbsolute); ;
            bitmap.EndInit();
            imgbrAvatar.ImageSource = bitmap;

        }
        private void wpShopItems_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is UC_ItemView itemView)
            {
                WinDeltailItem winDeltailItem = new WinDeltailItem(itemView.info, seller, seller.SellerID);
                winDeltailItem.ShowDialog();
            }
        }
        private void XIcon_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtSearchBox.Text = "";
        }
        private void btnShop_Checked(object sender, RoutedEventArgs e)
        {
            dpAboutOption.Visibility = Visibility.Collapsed;
            grdFeedBack.Visibility = Visibility.Collapsed;
            dpShopOption.Visibility = Visibility.Visible;
        }
        private void btnAbout_Checked(object sender, RoutedEventArgs e)
        {
            dpShopOption.Visibility = Visibility.Collapsed;
            grdFeedBack.Visibility = Visibility.Collapsed;
            dpAboutOption.Visibility = Visibility.Visible;
        }
        private void btnFeedback_Checked(object sender, RoutedEventArgs e)
        {
          
            dpShopOption.Visibility = Visibility.Collapsed;
            dpAboutOption.Visibility = Visibility.Collapsed;
            grdFeedBack.Visibility = Visibility.Visible;
            spDeliveredStatus.Children.Clear();
            LoadFeedBack();
        }
        private void btnCategories_Click(object sender, RoutedEventArgs e)
        {
            btnShop.IsChecked = true;

            if (!IsCategoriesButtonClicked)
            {
                DoubleAnimation widthAnimation = new DoubleAnimation
                {
                    From = 0,
                    To = svCategories.ActualWidth,
                    Duration = TimeSpan.FromSeconds(0.5),
                    EasingFunction = new CircleEase()
                };

                // Creating the Storyboard and adding the animation to it
                Storyboard storyboard = new Storyboard();
                storyboard.Children.Add(widthAnimation);

                // Setting the target of the animation
                Storyboard.SetTarget(widthAnimation, grdCategories);
                Storyboard.SetTargetProperty(widthAnimation, new PropertyPath("Width"));
                storyboard.Begin();

                // Set the margin of the grid
                grdCategories.Margin = new Thickness(0, 0, 20, 0);

                // Set the flag to true
                IsCategoriesButtonClicked = true;
            }
            else
            {
                //Reverse the animation
                DoubleAnimation widthAnimation = new DoubleAnimation
                {
                    From = svCategories.ActualWidth,
                    To = 0,
                    Duration = TimeSpan.FromSeconds(0.5),
                    EasingFunction = new CircleEase()
                };

                // Creating the Storyboard and adding the animation to it
                Storyboard storyboard = new Storyboard();
                storyboard.Children.Add(widthAnimation);

                // Setting the target of the animation
                Storyboard.SetTarget(widthAnimation, grdCategories);
                Storyboard.SetTargetProperty(widthAnimation, new PropertyPath("Width"));
                storyboard.Begin();

                // Set the flag to false
                IsCategoriesButtonClicked = false;

                // Set the margin of the grid
                grdCategories.Margin = new Thickness(0, 0, 0, 0);
            }

        }
        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is TextBlock textBlock)
            {
                string Category = textBlock.Text;
            }
        }
        private void FillShopItems(List<Item> items)
        {
            foreach (var item in items)
            {
                UC_ItemView itemView = new UC_ItemView(item);
                wpItems.Children.Add(itemView);
                itemView.MouseLeftButtonDown += wpShopItems_MouseLeftButtonDown;
            }
        }

        private void CategoryClicked(object sender, RoutedEventArgs e)
        {
            if (sender is RadioButton radioButton)
            {
                // Clear the items
                wpItems.Children.Clear();

                // Get the category
                string Category = radioButton.Content.ToString();

                // Fill the items
                if (Category == "All")
                {
                    FillShopItems(items);
                }
                else
                {
                    List<Item> itemsByCategory = items.Where(i => i.type == Category).ToList();
                    FillShopItems(itemsByCategory);
                }
            }
        }

        private void LoadCategories(List<Item> items)
        {
            txtPercentagePosotiveFeedback.Text = feedbackDAO.CalculateAverage(this.seller.SellerID).ToString()+" ";
            txtPostsNumber.Text = itemDAO.CalculateTotalProducts(this.seller.SellerID).ToString() + " ";
            // All category
            RadioButton radioButtonAll = new RadioButton
            {
                Content = "All",
                GroupName = "Categories",
            };

            radioButtonAll.Click += CategoryClicked;
            spCategories.Children.Add(radioButtonAll);

            // Other categories
            // Make sure the category is not duplicated
            List<string> categories = items.Select(i => i.type).Distinct().ToList();

            foreach (var itemCategory in categories)
            {
                RadioButton radioButton = new RadioButton
                {
                    Content = itemCategory,
                    GroupName = "Categories",
                };

                radioButton.Click += CategoryClicked;
                spCategories.Children.Add(radioButton);
            }
        }
        private void iconClose_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void LoadFeedBack()
        {
            
            feedbacks = feedbackDAO.GetFeedback(this.seller.SellerID);
            foreach (CustomerReview feedback in feedbacks)
            {
                Item item = itemDAO.GetItemByItemID(feedback.Item_Id);
                User user =userDAO.GetUserByItemID(feedback.Id_user);
                UC_FeedBackUI uC_FeedBackUI = new UC_FeedBackUI(item, user, feedback);
                uC_FeedBackUI.Width = 1440;
                spDeliveredStatus.Children.Add(uC_FeedBackUI);
            }
        }
    }
}
