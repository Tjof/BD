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
    /// Логика взаимодействия для Assortimentxaml.xaml
    /// </summary>
    public partial class Assortimentxaml : Window
    {
        public Assortimentxaml()
        {
            InitializeComponent();

            using (BAZANOWEntities model = new BAZANOWEntities())
            {
                var a = model.Ассортимент_товара.Include("Лекарство").Include("Аптеки").Include("Формы_упаковки").ToArray();
                DataGrid.ItemsSource = a;
            }
        }
    }
}
