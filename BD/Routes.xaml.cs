using BD.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Логика взаимодействия для Routes.xaml
    /// </summary>
    public partial class Routes : Window
    {
        BAZANOWEntities model;

        public Routes()
        {
            InitializeComponent();
        }

        private void ButtonAdd(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonEdit(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonDelete(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonClose(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
