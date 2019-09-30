using BD.Model;
using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для AddDistricts.xaml
    /// </summary>
    public partial class AddDistricts : Window
    {
        BAZANOWEntities context;
        private readonly ICollection<Районы_города> районы_Городаs;

        public AddDistricts(BAZANOWEntities context, ICollection<Районы_города> районы_Городаs)
        {
            InitializeComponent();
            this.context = context;
            this.районы_Городаs = районы_Городаs;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Создать нового покупателя
            Районы_города district = new Районы_города
            {
                Название_района = DistrictsBox.Text
            };

            // Добавить в DbSet
            // Сохранить изменения в базе данных
            var res = context.Районы_города.FirstOrDefault(a => a.Название_района == DistrictsBox.Text);
            if (res != null)
            {
                MessageBox.Show("Ошибка");
            }
            else
            {
                context.Районы_города.Local.Add(district);
                районы_Городаs.Add(district);
                context.SaveChanges();
            }
        }
    }
}
