using Delivery.Models;
using System.Windows;
using System.Windows.Controls;

namespace Delivery
{
    /// <summary>
    /// Interaction logic for AuthorizationPage.xaml
    /// </summary>
    public partial class AuthorizationPage : Page
    {
        public AuthorizationPage()
        {
            InitializeComponent();
        }

        private void EnterButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DeliveryContext deliveryContext = new DeliveryContext();
                User? user = deliveryContext.Users.FirstOrDefault(u => u.Login == LoginTextBox.Text);
                if (user == null)
                {
                    MessageBox.Show("Пользователь не найден!");
                }
                else if (user.Password != PasswordTextBox.Text)
                {
                    MessageBox.Show("Пароль не подходит!");
                }
                else
                {
                    MessageBox.Show("Авторизация прошла успешно!");
                    User.ActiveUser = user;
                    MainWindow.Frame.Content = new ViewPage();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}