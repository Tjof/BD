using BD.Model;
using BD.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BD
{
    /// <summary>
    /// Логика взаимодействия для Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
            tbLogin.Focus();
        }

        private void Authorization_Click(object sender, RoutedEventArgs e)
        {
            using (BAZANOWEntities entities = new BAZANOWEntities())
            {
                var res =  entities.Пользователи.FirstOrDefault(a => a.login == tbLogin.Text && a.pass == passwordBox.Password);
                if(res == null)
                {
                    MessageBox.Show("Неверный логин или пароль", "Ошибочка ;(", MessageBoxButton.OK);
                }
                else
                {
                    Users.Login = tbLogin.Text;
                    Main main = new Main();
                    main.Show();
                    this.Close();
                }

            }
        }
    }
}
