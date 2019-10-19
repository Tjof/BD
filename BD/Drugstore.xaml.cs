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
    public partial class Drugstore : Window, INotifyPropertyChanged
    {
        BAZANOWEntities model;
        ObservableCollection<Аптеки> _drugs;

        public Drugstore()
        {
            InitializeComponent();
            DataContext = this;
            model = new BAZANOWEntities();
            Drugs = new ObservableCollection<Аптеки>(model.Аптеки.Include("Улицы").Include("Остановки").ToArray());
        }

        public ObservableCollection<Аптеки> Drugs
        {
            get => _drugs;
            set
            {
                _drugs = value;
                OnPropertyChanged();
            }
        }

        private void ButtonClose(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ButtonAddDrugstore(object sender, RoutedEventArgs e)
        {
            Аптеки a = new Аптеки();
            AddDrugstore addDrugstore = new AddDrugstore(model, a);
            addDrugstore.ShowDialog();
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
                    model.Аптеки.Remove(DataGrid.SelectedItem as Аптеки);
                    model.SaveChanges();
                    OnPropertyChanged();
                }
                catch (System.Data.Entity.Infrastructure.DbUpdateException)
                {
                    System.Windows.MessageBox.Show("Ашибка! Запись связана!!!");
                }
            }
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            AddDrugstore editDrugstore = new AddDrugstore(model, DataGrid.SelectedItem as Аптеки)
            {
                Owner = this
            };
            editDrugstore.ShowDialog();
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
