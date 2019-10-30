using BD.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
    /// Логика взаимодействия для SearchMenu.xaml
    /// </summary>
    public partial class SearchMenu : Window
    {
        BAZANOWEntities model;
        public SearchMenu()
        {
            InitializeComponent();
            DataContext = model;
            model = new BAZANOWEntities();
            comboBox_drugs.ItemsSource = model.Лекарство.ToList();
            comboBox_stops.ItemsSource = model.Остановки.ToList();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var drug = comboBox_drugs.SelectedItem as Лекарство;
            var stop = comboBox_stops.SelectedItem as Остановки;
            FindDrugs findDrugs = new FindDrugs(model, drug, stop);
            findDrugs.ShowDialog();
            this.Close();
        }
    }
}
