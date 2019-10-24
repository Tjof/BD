using BD.Model;
using BD.Class;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Collections.Generic;

namespace BD
{
    /// <summary>
    /// Логика взаимодействия для AD.xaml
    /// </summary>
    public partial class AddRoute : Window
    {
        BAZANOWEntities model;
        Транспортные_маршруты route;
        ObservableCollection<МаршрутыОстановки> stops;

        public AddRoute(BAZANOWEntities model, Транспортные_маршруты route)
        {
            InitializeComponent();
            this.route = route;
            DataContext = route;
            var a = model.Остановки.ToArray();
            this.model = model;

            Stopss = new ObservableCollection<МаршрутыОстановки>(route.МаршрутыОстановки.ToList());

            DataGrid.ItemsSource = a;
            DataGrid2.ItemsSource = Stopss;
            ComboBoxTransportMod.ItemsSource = model.Виды_Транспорта.ToArray();

            if (route.id_маршрута == 0)
            {
                Title = "Добавление маршрута";
                AddEdit.Content = "Добавить";
                RouteNumber.Focus();
            }
            else
            {
                Title = "Изменение маршрута";
                AddEdit.Content = "Изменить";
            }

        }

        public ObservableCollection<МаршрутыОстановки> Stopss
        {
            get => stops;
            set
            {
                stops = value;
            }
        }

        private void AddEditClick(object sender, RoutedEventArgs e)
        {

            if (MessageBox.Show("Подтверждение", "Вы уверены, что хотите внести изменения в базу данных?", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                if (RegexClass.RegexRoute(RouteNumber.Text))
                {
                    try
                    {
                        foreach (FrameworkElement element in elementsGrid.Children)
                        {
                            if (element is TextBox)
                                element.GetBindingExpression(TextBox.TextProperty)?.UpdateSource();
                            else if (element is ComboBox)
                                element.GetBindingExpression(ComboBox.SelectedItemProperty)?.UpdateSource();
                        }
                        if (route.id_маршрута == 0) //new record
                        {
                            model.Транспортные_маршруты.Add(route);
                        }
                        else
                        {
                            model.Entry(route).State = System.Data.Entity.EntityState.Modified;
                        }
                        model.SaveChanges();
                        this.Close();
                    }
                    catch (System.Data.Entity.Infrastructure.DbUpdateException)
                    {
                        model.Транспортные_маршруты.Local.Remove(route);
                        MessageBox.Show("Ошибка", "Проверьте правильность вводимых данных", MessageBoxButton.OK);
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Ошибка", "Проверьте правильность вводимых данных", MessageBoxButton.OK);
                }
            }
        }

        private void AddStop(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Подтверждение", "Вы уверены, что хотите внести изменения в базу данных?", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                МаршрутыОстановки a = new МаршрутыОстановки
                {
                    Остановки = DataGrid.SelectedItem as Остановки,
                    Транспортные_маршруты = route,
                    Порядок = 11
                };
                try
                {
                    Stopss.Add(a);
                    model.МаршрутыОстановки.Add(a);
                    model.SaveChanges();
                }
                catch (System.Data.Entity.Infrastructure.DbUpdateException)
                {
                    Stopss.Remove(a);
                    MessageBox.Show("Ошибка", "Проверьте правильность вводимых данных", MessageBoxButton.OK);
                }
            }
        }

        private void DeleteStop(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Подтверждение", "Вы уверены, что хотите внести изменения в базу данных?", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                try
                {
                    model.МаршрутыОстановки.Remove(DataGrid2.SelectedItem as МаршрутыОстановки);
                    Stopss.Remove(DataGrid2.SelectedItem as МаршрутыОстановки);
                    model.SaveChanges();
                }
                catch (System.Data.Entity.Infrastructure.DbUpdateException)
                {
                    MessageBox.Show("Ошибка", "Произошла ошибка. Шок Кавоо?! КАК?!", MessageBoxButton.OK);
                }
            }
        }

        private void DataGrid_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (DataGrid.SelectedItem != null)
            {
                Add.IsEnabled = true;
                Delete.IsEnabled = false;
                DataGrid2.SelectedItem = null;
            }
        }

        private void DataGrid2_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (DataGrid2.SelectedItem != null)
            {
                Delete.IsEnabled = true;
                Add.IsEnabled = false;
                DataGrid.SelectedItem = null;
            }
        }
    }
}
