using System;
using System.Collections.Generic;
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

namespace UTEMerchant
{
    /// <summary>
    /// Interaction logic for WinShopView.xaml
    /// </summary>
    public partial class WinShopView : Window
    {
        // Flag to check if the categories button is clicked
        private bool IsCategoriesButtonClicked = false;

        // Properties
        private User user;
        private List<Item> items;

        public WinShopView()
        {
            InitializeComponent();
            Loaded += delegate
            {
                txtAboutUsContent.Text = "UTEMerchant is a platform for UTE students to buy and sell items.\nIt is a project for the subject of Software Engineering.\nThe project is developed by a group of students from the University of Technology and Education.";
            };

        }

        public WinShopView(User user, List<Item> items) : this()
        {
            this.user = user;
            this.items = items;
        }

        private void wpShopItems_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void XIcon_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtSearchBox.Text = "";
        }

        private void btnShop_Checked(object sender, RoutedEventArgs e)
        {
            dpAboutOption.Visibility = Visibility.Collapsed;
            //dpFeedbackOption.Visibility = Visibility.Collapsed;
            dpShopOption.Visibility = Visibility.Visible;
        }

        private void btnAbout_Checked(object sender, RoutedEventArgs e)
        {
            dpShopOption.Visibility = Visibility.Collapsed;
            //dpFeedbackOption.Visibility = Visibility.Collapsed;
            dpAboutOption.Visibility = Visibility.Visible;
        }

        private void btnFeedback_Checked(object sender, RoutedEventArgs e)
        {
            dpShopOption.Visibility = Visibility.Collapsed;
            dpAboutOption.Visibility = Visibility.Collapsed;
            //dpFeedbackOption.Visibility = Visibility.Visible;
        }

        private void btnCategories_Click(object sender, RoutedEventArgs e)
        {
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
    }
}
