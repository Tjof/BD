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
    /// Логика взаимодействия для Ostanovki.xaml
    /// </summary>
    public partial class Ostanovki : Window
    {
        public Ostanovki()
        {
            InitializeComponent();

            using (BAZANOWEntities model = new BAZANOWEntities())
            {
                var a = model.Остановки.Include("Улицы").ToArray();
                DataGrid.ItemsSource = a;
            }
        }
    }
}
