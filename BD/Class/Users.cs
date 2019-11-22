using BD.Model;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace BD.Class 
{
    public class Users : Window
    {
        BAZANOWEntities model;
        public static string Login { get; set; }
        public void Read(MenuItem Handbooks)
        {
            model = new BAZANOWEntities();
            var user = model.Пользователи.FirstOrDefault(x => x.login == Login);
            var ActiveUser = model.ПользователиОбъекты.FirstOrDefault(x => x.id_пользователя == user.id_пользователя);
            if (ActiveUser.R == false)
            {
                Handbooks.IsEnabled = false;
            }
        }
        public void Write(Button Add)
        {
            model = new BAZANOWEntities();
            var user = model.Пользователи.FirstOrDefault(x => x.login == Login);
            var ActiveUser = model.ПользователиОбъекты.FirstOrDefault(x => x.id_пользователя == user.id_пользователя);
            if (ActiveUser.W == false)
            {
                Add.IsEnabled = false;
            }
        }
        public void EditDelete(Button Edit, Button Delete)
        {
            model = new BAZANOWEntities();
            var user = model.Пользователи.FirstOrDefault(x => x.login == Login);
            var ActiveUser = model.ПользователиОбъекты.FirstOrDefault(x => x.id_пользователя == user.id_пользователя);
            if (ActiveUser.E == false)
            {
                Edit.IsEnabled = false;
            }
            else
            {

                Edit.IsEnabled = true;
            }
            if (ActiveUser.D == false)
            {
                Delete.IsEnabled = false;
            }
            else
            {
                Delete.IsEnabled = true;
            }
        }
        
    }
}
