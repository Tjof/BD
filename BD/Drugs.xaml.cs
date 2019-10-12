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
using System.Collections;

namespace BD
{
    /// <summary>
    /// Логика взаимодействия для Drugs.xaml
    /// </summary>
    public partial class Drugs : Window
    {
        BAZANOWEntities model;
        public Drugs()
        {
            InitializeComponent();
            model = new BAZANOWEntities();
            var a = model.Лекарство.ToArray();
            DataGrid.ItemsSource = a;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            model.Dispose();
        }
    }
}
