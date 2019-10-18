using BD.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;

namespace BD
{
    /// <summary>
    /// Логика взаимодействия для AD.xaml
    /// </summary>
    public partial class AddRoute : Window, INotifyPropertyChanged
    {
        BAZANOWEntities model;
        Транспортные_маршруты route;
        ObservableCollection<Остановки> _stops;

        public AddRoute(BAZANOWEntities model, Транспортные_маршруты route)
        {
            InitializeComponent();
            this.route = route;
            DataContext = route;
            this.model = model;
            Stopss = new ObservableCollection<Остановки>(model.Остановки.Include("Улицы").ToArray());
            DataGrid.ItemsSource = Stopss;
            DataGrid2.ItemsSource = route.Остановки.ToArray();
            ComboBoxTransportMod.ItemsSource = model.Виды_Транспорта.ToArray();

            if (model.Entry(route).State == System.Data.Entity.EntityState.Detached)
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

        public ObservableCollection<Остановки> Stopss
        {
            get => _stops;
            set
            {
                _stops = value;
                OnPropertyChanged();
            }
        }

        private void AddEditClick(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Подтверждение", "Вы уверены, что хотите внести изменения в базу данных?", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                try
                {
                    if (model.Entry(route).State == System.Data.Entity.EntityState.Detached)
                    {
                        model.Транспортные_маршруты.Add(route);
                    }
                    model.SaveChanges();
                    OnPropertyChanged();
                }
                catch (System.Data.Entity.Infrastructure.DbUpdateException)
                {
                    MessageBox.Show("Ошибка", "Проверьте правильность вводимых данных", MessageBoxButton.OK);
                }
            }
        }

        void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
        public event PropertyChangedEventHandler PropertyChanged;

        private void ButtonAddRoute(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Подтверждение", "Вы уверены, что хотите внести изменения в базу данных?", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                try
                {
                    route.Остановки.Add(DataGrid.SelectedItem as Остановки);
                    model.SaveChanges();
                    //OnPropertyChanged();
                }
                catch (System.Data.Entity.Infrastructure.DbUpdateException)
                {
                    MessageBox.Show("Ошибка", "Проверьте правильность вводимых данных", MessageBoxButton.OK);
                }
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Подтверждение", "Вы уверены, что хотите внести изменения в базу данных?", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                try
                {
                    route.Остановки.Remove(DataGrid2.SelectedItem as Остановки);
                    model.SaveChanges();
                    OnPropertyChanged();
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
            }
        }

        private void DataGrid2_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (DataGrid2.SelectedItem != null)
            {
                Delete.IsEnabled = true;
            }
        }
    }
}
