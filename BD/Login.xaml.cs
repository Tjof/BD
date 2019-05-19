using BD.Model;
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
        }

        private void Authorization_Click(object sender, RoutedEventArgs e)
        {

            /*using (SqlConnection connection = new SqlConnection("Data Source=tcp:ZLOY,49172;Initial Catalog=TVOYABAZA;Integrated Security=True"))
            {
                SqlCommand command = new SqlCommand("select * from [dbo].[Аптеки] where Название=@apt", connection);
                command.Parameters.Add(new SqlParameter("@apt", "dsfkgsfjk"));
                var reader = command.ExecuteReader();
                if( reader.Read())
                {
                    var result = Convert.ToString( reader["Название"]);
                }
                else
                {

                }
                //while(reader.Read())

            }*/
            using (TVOYABAZAEntities entities = new TVOYABAZAEntities())
            {
                var res =  entities.Пользователи.FirstOrDefault(a => a.login == tbLogin.Text && a.pass == passwordBox.Password);
                if(res == null)
                {
                    MessageBox.Show("Неверный логин или пароль", "Ошибочка ;(", MessageBoxButton.OK, MessageBoxImage.Hand);
                }
                else
                {
                    Main glavnaya = new Main();
                    glavnaya.Show();
                    this.Close();
                }

            }




                
        }
    }
}
