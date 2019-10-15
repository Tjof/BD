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
        private ObservableCollection<Транспортные_маршруты> _routes;

        public Routes()
        {
            InitializeComponent();
            model = new BAZANOWEntities();
            Route = new ObservableCollection<Транспортные_маршруты>(model.Транспортные_маршруты.Include("Виды_Транспорта").Include("Остановки").ToArray());
            DataGrid.ItemsSource = Route;
        }

        public ObservableCollection<Транспортные_маршруты> Route
        {
            get => _routes;
            set
            {
                _routes = value;
            }
        }

        private void ButtonAdd(object sender, RoutedEventArgs e)
        {
            RouteNumber.Text = "";
            TransportMode.Text = "";
            RouteNumber.IsReadOnly = false;
            TransportMode.IsReadOnly = false;
            AddRoute.IsEnabled = true;
            RouteNumber.IsEnabled = true;
            TransportMode.IsEnabled = true;
            RouteNumber.Focus();
        }

        private void ButtonDelete(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonEdit(object sender, RoutedEventArgs e)
        {
            SaveEdit.IsEnabled = true;
            RouteNumber.IsEnabled = true;
            TransportMode.IsEnabled = true;
            RouteNumber.IsReadOnly = false;
            TransportMode.IsReadOnly = false;
        }

        private void ButtonAddRoute(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonSaveEdit(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataGrid.SelectedItem != null)
            {
                Edit.IsEnabled = true;
                Delete.IsEnabled = true;
                RouteNumber.IsEnabled = false;
                TransportMode.IsEnabled = false;
                AddRoute.IsEnabled = false;
                SaveEdit.IsEnabled = false;
            }

            if (DataGrid.SelectedItem == null)
            {
                RouteNumber.Text = "";
                TransportMode.Text = "";
            }
            else
            {
                RouteNumber.Text = (DataGrid.SelectedItem as Транспортные_маршруты).Номер_маршрута;
                TransportMode.Text = (DataGrid.SelectedItem as Транспортные_маршруты).Виды_Транспорта.Вид_транспорта;
            }
        }
    }
}
