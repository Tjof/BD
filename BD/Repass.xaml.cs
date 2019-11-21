using BD.Class;
using BD.Model;
using System.Data.Entity;
using System.Linq;
using System.Windows;

namespace BD
{
    /// <summary>
    /// Логика взаимодействия для Repass.xaml
    /// </summary>
    public partial class Repass : Window
    {
        BAZANOWEntities model;
        public Repass()
        {
            InitializeComponent();
            model = new BAZANOWEntities();

        }

        private void ChangePassword_Click(object sender, RoutedEventArgs e)
        {
            string login = Users.Login;
            var user = model.Пользователи.Where(x => x.login == login);

            if (MessageBox.Show("Подтверждение", "Вы уверены, что хотите сменить пароль?", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                

            }

        }
    }
}
