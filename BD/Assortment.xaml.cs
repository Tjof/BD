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
    /// Логика взаимодействия для Assortment.xaml
    /// </summary>
    public partial class Assortment : Window, INotifyPropertyChanged
    {
        BAZANOWEntities model;
        ObservableCollection<Ассортимент_товара> _assortment;

        public Assortment()
        {
            InitializeComponent();
            model = new BAZANOWEntities();
            Assortments = new ObservableCollection<Ассортимент_товара>(model.Ассортимент_товара.Include("Лекарство").Include("Аптеки").Include("Формы_упаковки").ToArray());
            DataGrid.ItemsSource = Assortments;
        }

        public ObservableCollection<Ассортимент_товара> Assortments
        {
            get => _assortment;
            set
            {
                _assortment = value;
                OnPropertyChanged();
            }
        }

        private void ButtonClose(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ButtonAddAssortment(object sender, RoutedEventArgs e)
        {
            Ассортимент_товара a = new Ассортимент_товара();
            AddAssortment addAssortment = new AddAssortment(model, a);
            addAssortment.ShowDialog();
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
                    model.Ассортимент_товара.Remove(DataGrid.SelectedItem as Ассортимент_товара);
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
            AddAssortment editAssortment = new AddAssortment(model, DataGrid.SelectedItem as Ассортимент_товара)
            {
                Owner = this
            };
            editAssortment.ShowDialog();
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
