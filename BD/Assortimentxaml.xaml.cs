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

            using (TVOYABAZAEntities model = new TVOYABAZAEntities())
            {
                DataGrid.ItemsSource = model.Ассортимент_товара.ToArray();
            }
        }
    }
}
