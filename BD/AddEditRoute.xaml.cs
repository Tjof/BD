using BD.Model;
using BD.Class;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using BD.Services;
using System;

namespace BD
{
    /// <summary>
    /// Логика взаимодействия для AD.xaml
    /// </summary>
    public partial class AddEditRoute : Window
    {
        BAZANOWEntities model;
        Транспортные_маршруты route;
        ObservableCollection<МаршрутыОстановки> stops;
        ObservableCollection<Остановки> stopsss;

        public AddEditRoute(BAZANOWEntities model, Транспортные_маршруты route)
        {
            InitializeComponent();
            this.route = route;
            DataContext = route;
            this.model = model;

            StopsRoute = new ObservableCollection<МаршрутыОстановки>(route.МаршрутыОстановки.ToList());

            var a = StopsRoute.Select(s => s.Остановки);
            AllStops = new ObservableCollection<Остановки>(model.Остановки.ToArray().Except(a, new ОстановкиComaprer()).ToArray());

            DataGrid.ItemsSource = AllStops;
            DataGrid2.ItemsSource = StopsRoute;
            ComboBoxTransportMod.ItemsSource = model.Виды_Транспорта.ToArray();

            if (route.id_маршрута == 0)
            {
                Title = "Служба 067 - Добавление маршрута";
                AddEdit.Content = "Добавить";
                RouteNumber.Focus();
            }
            else
            {
                Title = "Служба 067 - Изменение маршрута";
                AddEdit.Content = "Изменить";
            }

            // Сортируем остановки в таблице по полю Порядок
            DataGrid2.Items.SortDescriptions.Clear();
            DataGrid2.Items.SortDescriptions.Add(new SortDescription("Порядок", ListSortDirection.Ascending));

            foreach (DataGridColumn sort in DataGrid2.Columns)
            {
                sort.CanUserSort = false;
            }
        }

        public ObservableCollection<Остановки> AllStops
        {
            get => stopsss;
            set
            {
                stopsss = value;
            }
        }

        public ObservableCollection<МаршрутыОстановки> StopsRoute
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
                        if (route.id_маршрута == 0)
                            model.Транспортные_маршруты.Add(route);
                        else
                            model.Entry(route).State = System.Data.Entity.EntityState.Modified;
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
                int maxp;
                if (StopsRoute.Count == 0)
                    maxp = 0;
                else
                    maxp = StopsRoute.Max(p => p.Порядок);
                МаршрутыОстановки stop = new МаршрутыОстановки
                {
                    Остановки = DataGrid.SelectedItem as Остановки,
                    Транспортные_маршруты = route,
                    Порядок = maxp + 1
                };
                try
                {
                    AllStops.Remove(DataGrid.SelectedItem as Остановки);
                    StopsRoute.Add(stop);
                    route.МаршрутыОстановки.Add(stop);
                    model.SaveChanges();
                    Add.IsEnabled = false;
                }
                catch (System.Data.Entity.Infrastructure.DbUpdateException ex)
                {
                    MessageBox.Show("Ошибка", "Проверьте правильность вводимых данных" + Environment.NewLine+ ex.Message, MessageBoxButton.OK);
                }
            }
        }

        private void DeleteStop(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Подтверждение", "Вы уверены, что хотите внести изменения в базу данных?", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Остановки stop = (DataGrid2.SelectedItem as МаршрутыОстановки).Остановки;
                try
                {
                    route.МаршрутыОстановки.Remove(DataGrid2.SelectedItem as МаршрутыОстановки);
                    StopsRoute.Remove(DataGrid2.SelectedItem as МаршрутыОстановки);
                    AllStops.Add(stop);
                    model.SaveChanges();
                    Delete.IsEnabled = false;
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

        private void UpStop(object sender, RoutedEventArgs e)
        {
            var p = DataGrid2.SelectedItem as МаршрутыОстановки;
            if (p.Порядок != 1)
            {
                DataGridRow row = (DataGridRow)DataGrid2.ItemContainerGenerator.ContainerFromIndex(DataGrid2.SelectedIndex - 1);
                var p2 = row.Item as МаршрутыОстановки;
                var tmp = p.Порядок;
                p.Порядок = p2.Порядок;
                p2.Порядок = tmp;
            }
            model.SaveChanges();
            CollectionViewSource.GetDefaultView(StopsRoute).Refresh();
        }

        private void DownStop(object sender, RoutedEventArgs e)
        {
            var p = DataGrid2.SelectedItem as МаршрутыОстановки;
            int maxp = StopsRoute.Max(s => s.Порядок);
            if (p.Порядок != maxp)
            {
                DataGridRow row = (DataGridRow)DataGrid2.ItemContainerGenerator.ContainerFromIndex(DataGrid2.SelectedIndex + 1);
                var p2 = row.Item as МаршрутыОстановки;
                var tmp = p.Порядок;
                p.Порядок = p2.Порядок;
                p2.Порядок = tmp;
            }
            model.SaveChanges();
            CollectionViewSource.GetDefaultView(StopsRoute).Refresh();
        }

    }
}
