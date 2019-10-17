using BD.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
    /// Логика взаимодействия для Drugstore.xaml
    /// </summary>
    public partial class Stops : Window, INotifyPropertyChanged
    {
        BAZANOWEntities model;
        ObservableCollection<Остановки> _stops;

        public Stops()
        {
            InitializeComponent();
            model = new BAZANOWEntities();
            Stopss = new ObservableCollection<Остановки>(model.Остановки.Include("Улицы").ToArray());
            DataGrid.ItemsSource = Stopss;
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

        void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
        public event PropertyChangedEventHandler PropertyChanged;

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Подтверждение", "Вы уверены, что хотите внести изменения в базу данных?", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                try
                {
                    model.Остановки.Remove(DataGrid.SelectedItem as Остановки);
                    model.SaveChanges();
                    OnPropertyChanged();
                }
                catch (System.Data.Entity.Infrastructure.DbUpdateException)
                {
                    MessageBox.Show("Ошибка", "Удаляемые данные связаны!", MessageBoxButton.OK);
                }
            }
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            AddEditStop editStop = new AddEditStop(model, DataGrid.SelectedItem as Остановки)
            {
                Owner = this
            };
            editStop.ShowDialog();
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
