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
            OldPassword.Focus();
        }

        private void ChangePassword_Click(object sender, RoutedEventArgs e)
        {
            var user = model.Пользователи.FirstOrDefault(x => x.login == Users.Login);


            if (MessageBox.Show("Подтверждение", "Вы уверены, что хотите сменить пароль?", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                if (RegexClass.RegexPass(NewPassword.Password))
                {
                    try
                    {
                        if (OldPassword.Password != null && NewPassword.Password != null && RepeatNewPassword.Password != null)
                        {

                            if ((user as Пользователи).pass == OldPassword.Password && OldPassword.Password != NewPassword.Password && NewPassword.Password == RepeatNewPassword.Password)
                            {
                                (user as Пользователи).pass = NewPassword.Password;
                                model.SaveChanges();
                                MessageBox.Show("Подтверждение", "Пароль успешно изменен", MessageBoxButton.OK);
                                this.Close();
                            }
                            else
                            {
                                MessageBox.Show("Ошибка", "Проверьте правильность вводимых данных", MessageBoxButton.OK);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Ошибка", "Не все поля заполненны", MessageBoxButton.OK);
                        }
                    }
                    catch (System.Data.Entity.Infrastructure.DbUpdateException)
                    {
                        MessageBox.Show("Ошибка", "Проверьте правильность вводимых данных", MessageBoxButton.OK);
                    }
                    
                }
                else
                {
                    MessageBox.Show("Ошибка", "Проверьте правильность вводимых данных", MessageBoxButton.OK);
                }

            }

        }
    }
}
