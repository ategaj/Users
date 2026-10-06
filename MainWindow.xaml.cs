using System.Data;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Users
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<User> users = new List<User>();
        User selectedUserForEdit = null;
        public MainWindow()
        {
            InitializeComponent();
            InitializeRoles();
            UsersTabControl.ItemsSource = users;
        }
        private void InitializeRoles()
        {
            RoleInput.Items.Add(new Role("1", "Пользователь"));
            RoleInput.Items.Add(new Role("2", "Менеджер"));
            RoleInput.Items.Add(new Role("3", "Администратор"));
        }
        private void Add(object sender, RoutedEventArgs e)
        {
            LeftPanel.IsEnabled = true;
            LeftButton.Content = "Добавить";
        }
        private void Delete(object sender, RoutedEventArgs e)
        {
        }

        private void Edit(object sender, RoutedEventArgs e)
        {
            User selected = (User)UsersTabControl.SelectedItem;
            if (selected == null) return;
            users.Remove(selected);
            RefreshUsers();
            LeftPanel.IsEnabled = true;
            LeftButton.Content = "Изменить";
        }
        private void RefreshUsers()
        {
            UsersTabControl.ItemsSource = null;
            UsersTabControl.ItemsSource = users;
        }
        private void Append(object sender, RoutedEventArgs e)
        {
        }

    }

}
