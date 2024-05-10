using Delivery.Models;
using Microsoft.EntityFrameworkCore;
using System.Windows;
using System.Windows.Controls;

namespace Delivery
{
    /// <summary>
    /// Interaction logic for ViewPage.xaml
    /// </summary>
    public partial class ViewPage : Page
    {
        private List<UserOrder> orders;
        private List<UserOrder> viewOrders = new List<UserOrder>();

        public ViewPage()
        {
            InitializeComponent();
            DeliveryContext deliveryContext = new DeliveryContext();
            if (User.ActiveUser.RoleId == 1)
            {
                orders = deliveryContext.UserOrders
                    .Include(o => o.Status)
                    .ToList();
            }
            else
            {
                orders = deliveryContext.UserOrders
                    .Where(o => o.ExecutorId == User.ActiveUser.UserId)
                    .Include(o => o.Status)
                    .ToList();
                CreateButton.IsEnabled = false;
            }
            UpdateView();
        }

        private void UpdateView()
        {
            viewOrders.Clear();
            foreach (UserOrder order in orders)
            {
                if (order.ItemTitle.ToLower().Contains(SearchTextBox.Text.ToLower()))
                {
                    viewOrders.Add(order);
                }
            }
            MainDataGrid.ItemsSource = null;
            MainDataGrid.ItemsSource = viewOrders;
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.Frame.Content = new ManipulationPage((sender as FrameworkElement).DataContext as UserOrder);
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.Frame.Content = new ManipulationPage();
        }
    }
}