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
    public partial class Drugs : Window
    {
        BAZANOWEntities model;
        ObservableCollection<Лекарство> _drugs;

        public Drugs()
        {
            InitializeComponent();
            DataContext = this;
            model = new BAZANOWEntities();
            model.Лекарство.Load();
            Drugss = model.Лекарство.Local;
        }

        public ObservableCollection<Лекарство> Drugss
        {
            get => _drugs;
            set
            {
                _drugs = value;
            }
        }

        private void ButtonClose(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ButtonAddStop(object sender, RoutedEventArgs e)
        {
            Лекарство a = new Лекарство();
            AddDrug addDrug = new AddDrug(model, a);
            addDrug.ShowDialog();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Подтверждение", "Вы уверены, что хотите внести изменения в базу данных?", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                try
                {
                    model.Лекарство.Local.Remove(DataGrid.SelectedItem as Лекарство);
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
            Лекарство a = DataGrid.SelectedItem as Лекарство;
            using (CollectionViewSource.GetDefaultView(Drugss).DeferRefresh())
            {
                AddDrug addDrug = new AddDrug(model, a)
                {
                    Owner = this
                };
                addDrug.ShowDialog();
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
