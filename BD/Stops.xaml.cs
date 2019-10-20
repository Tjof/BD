using BD.Model;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace BD
{
    /// <summary>
    /// Логика взаимодействия для Drugstore.xaml
    /// </summary>
    public partial class Stops : Window
    {
        BAZANOWEntities model;
        ObservableCollection<Остановки> _stops;

        public Stops()
        {
            InitializeComponent();
            DataContext = this;
            model = new BAZANOWEntities();
            model.Остановки.Load();
            Stopss = model.Остановки.Local;
        }

        public ObservableCollection<Остановки> Stopss
        {
            get => _stops;
            set
            {
                _stops = value;
            }
        }

        private void ButtonClose(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ButtonAddStop(object sender, RoutedEventArgs e)
        {
            Остановки a = new Остановки();
            AddEditStop addStop = new AddEditStop(model, a);
            addStop.ShowDialog();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Подтверждение", "Вы уверены, что хотите внести изменения в базу данных?", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                try
                {
                    model.Остановки.Local.Remove(DataGrid.SelectedItem as Остановки);
                    model.SaveChanges();
                }
                catch (System.Data.Entity.Infrastructure.DbUpdateException)
                {
                    MessageBox.Show("Ошибка", "Удаляемые данные связаны!", MessageBoxButton.OK);
                }
            }
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            Остановки a = DataGrid.SelectedItem as Остановки;
            using (CollectionViewSource.GetDefaultView(Stopss).DeferRefresh())
            {
                AddEditStop editStop = new AddEditStop(model, a)
                {
                    Owner = this
                };
                editStop.ShowDialog();
            }
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataGrid.SelectedItem != null)
            {
                Edit.IsEnabled = true;
                Delete.IsEnabled = true;
            }
        }
    }
}
