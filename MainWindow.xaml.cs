using System.Collections.ObjectModel;
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
using System.Xml.Serialization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Users
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<User> users = new List<User>();
        User? selectedUserForEdit;
        public MainWindow()
        {
            InitializeComponent();
            InitializeRoles();
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
            users.Remove(selectedUserForEdit);
            Refresh();
            AddButton.IsEnabled = true;
            DeleteButton.IsEnabled = false;
            EditButton.IsEnabled = false;
        }
        private void Edit(object sender, RoutedEventArgs e)
        {
            LeftPanel.IsEnabled = true;
            AddButton.IsEnabled = false;
            DeleteButton.IsEnabled = false;
            EditButton.IsEnabled = false;
            LeftButton.Content = "Изменить";

        }
        private void Append(object sender, RoutedEventArgs e)
        {
            try
            {
                Valid();
                string name = NameInput.Text;
                string mail = MailInput.Text;
                string phone = PhoneInput.Text;
                string date = DateInput.SelectedDate?.ToShortDateString() ?? "";
                string password = PasswordInput.Text;
                Role role = (Role)RoleInput.SelectedItem;
                if ((string)LeftButton.Content == "Добавить")
                {
                    int id = 1;
                    while (users.Any(u => u.Id == id)) id++;
                    User newuser = new User(id, name, mail, password, phone, date, role);
                    users.Add(newuser);
                    Refresh();
                    Clear();
                    LeftPanel.IsEnabled = false;
                }
                else
                {
                    selectedUserForEdit.Name = name;
                    selectedUserForEdit.Email = mail;
                    selectedUserForEdit.Phone = phone;
                    selectedUserForEdit.Date = date;
                    selectedUserForEdit.Password = password;
                    selectedUserForEdit.Role = role;
                    Refresh();
                    Clear();
                    selectedUserForEdit = null;
                    LeftButton.Content = "Добавить";
                    LeftPanel.IsEnabled = false;
                    AddButton.IsEnabled = true;
                    DeleteButton.IsEnabled = false;
                    EditButton.IsEnabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            
        }
        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedUserForEdit = ((ListBox)sender).SelectedItem as User;
            DeleteButton.IsEnabled = true;
            EditButton.IsEnabled = true;
            if (selectedUserForEdit != null)
            {
                NameInput.Text = selectedUserForEdit.Name;
                MailInput.Text = selectedUserForEdit.Email;
                PhoneInput.Text = selectedUserForEdit.Phone;
                PasswordInput.Text = selectedUserForEdit.Password;
                RoleInput.SelectedItem = selectedUserForEdit.Role;
                DateInput.Text = selectedUserForEdit.Date;
            }
        }
        private void Clear()
        {
            NameInput.Clear();
            MailInput.Clear();
            PasswordInput.Clear();
            PhoneInput.Clear();
            RoleInput.SelectedIndex = 0;
            DateInput.SelectedDate = null;
        }
        private void Refresh()
        {
            AllUsersListBox.Items.Clear();
            UsersListBox.Items.Clear();
            ManagersListBox.Items.Clear();
            AdminsListBox.Items.Clear();
            foreach (var u in users)
            {
                AllUsersListBox.Items.Add(u);
                if (u.Role?.Id == "1") UsersListBox.Items.Add(u);
                if (u.Role?.Id == "2") ManagersListBox.Items.Add(u);
                if (u.Role?.Id == "3") AdminsListBox.Items.Add(u);
            }
        }
        private void Valid()
        {
            string name = NameInput.Text;
            string mail = MailInput.Text;
            string phone = PhoneInput.Text;
            string password = PasswordInput.Text;
            if (string.IsNullOrWhiteSpace(name))
                throw new Exception("Введите имя");
            if (string.IsNullOrWhiteSpace(mail))
                throw new Exception("Введите email");
            if (string.IsNullOrWhiteSpace(phone))
                throw new Exception("Введите телефон");
            if (string.IsNullOrWhiteSpace(password))
                throw new Exception("Введите пароль");
            if (!name.All(c => char.IsLetter(c)))
                throw new Exception("Имя может содержать только буквы, пробел и дефис");
            if (!mail.Contains('@'))
                throw new Exception("Email должен содержать @");
            if (!phone.All(char.IsDigit))
                throw new Exception("Телефон может содержать только цифры");
            if (phone.Length != 11)
                throw new Exception("Телефон должен содержать ровно 11 цифр");
            if (!DateInput.SelectedDate.HasValue)
                throw new Exception("Выберите дату рождения");
            if (DateInput.SelectedDate.Value.Date > DateTime.Now.Date)
                throw new Exception("Дата рождения не может быть в будущем");
        } 
    }
}