using Delivery.Models;
using System.Windows;
using System.Windows.Controls;

namespace Delivery
{
    public partial class ManipulationPage : Page
    {
        public ManipulationPage()
        {
            InitializeComponent();
            SetUp();
            DeliveryContext deliveryContext = new DeliveryContext();

            if (deliveryContext.UserOrders.Count() == 0)
                UserOrderId.Text = "1";
            else
                UserOrderId.Text = (deliveryContext.UserOrders.Max(o => o.UserOrderId) + 1).ToString();
        }

        public ManipulationPage(UserOrder order)
        {
            InitializeComponent();
            SetUp();
            UserOrderId.Text = order.UserOrderId.ToString();
            DeliveryDate.Text = order.OrderDate.ToString();
            Location.Text = order.Location;
            ClientCompleteName.Text = order.ClientCompleteName;
            ClientPhone.Text = order.ClientPhone;
            ItemTitle.Text = order.ItemTitle;
            StatusComboBox.SelectedIndex = (order.StatusId ?? 0) - 1;
            ExecutorComboBox.SelectedItem = order.ExecutorId;
            if (User.ActiveUser.RoleId == 2)
                ExecutorComboBox.IsEnabled = DeliveryDate.IsEnabled = ItemTitle.IsEnabled = ClientCompleteName.IsEnabled = ClientPhone.IsEnabled = Location.IsEnabled = false;
        }

        private void SetUp()
        {
            DeliveryContext deliveryContext = new DeliveryContext();
            StatusComboBox.ItemsSource = deliveryContext.Statuses.Select(s => s.Title).ToList();
            ExecutorComboBox.ItemsSource = deliveryContext.Users.Where(u => u.RoleId == 2).Select(u => u.UserId).ToList();
        }

        private void ApplyButton_Click(object sender, RoutedEventArgs e)
        {
            if (StatusComboBox.SelectedIndex != -1 && (DateTime.TryParse(DeliveryDate.Text, out _)) && Location.Text.Length < 100
                && ItemTitle.Text.Length < 100 && ClientPhone.Text.Length <= 11 && ClientCompleteName.Text.Length < 150)
            {
                DeliveryContext deliveryContext = new DeliveryContext();

                UserOrder? order = deliveryContext.UserOrders.FirstOrDefault(o => o.UserOrderId == Convert.ToInt32(UserOrderId.Text));
                if (deliveryContext.UserOrders.FirstOrDefault(o => o.UserOrderId == Convert.ToInt32(UserOrderId.Text)) != null)
                {
                    deliveryContext.UserOrders.Remove(order);
                }

                order = new UserOrder
                    (
                    Convert.ToInt32(UserOrderId.Text),
                    StatusComboBox.SelectedIndex + 1,
                    DateTime.Parse(DeliveryDate.Text),
                    ClientCompleteName.Text,
                    ClientPhone.Text,
                    ItemTitle.Text,
                    Location.Text,
                    ExecutorComboBox.SelectedItem != null ? Convert.ToInt32(ExecutorComboBox.SelectedItem.ToString()) : null
                    );
                deliveryContext.UserOrders.Add(order);
                deliveryContext.SaveChanges();

                MainWindow.Frame.Content = new ViewPage();
            }
            else
            {
                MessageBox.Show("Данные указаны неверно!");
            }
        }
    }
}