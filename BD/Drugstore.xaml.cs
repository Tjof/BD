using BD.Model;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace BD
{
    /// <summary>
    /// Логика взаимодействия для Drugstore.xaml
    /// </summary>
    public partial class Drugstore : Window
    {
        BAZANOWEntities model;
        ObservableCollection<Аптеки> _drugstore;

        public Drugstore()
        {
            InitializeComponent();
            DataContext = this;
            model = new BAZANOWEntities();
            model.Аптеки.Load();
            Drugstores = model.Аптеки.Local;
        }

        public ObservableCollection<Аптеки> Drugstores
        {
            get => _drugstore;
            set
            {
                _drugstore = value;
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

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Подтверждение", "Вы уверены, что хотите внести изменения в базу данных?", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                var a = DataGrid.SelectedItem as Аптеки;
                try
                {
                    if(a.Ассортимент_товара.Count != 0)
                    {
                        throw new DbUpdateException("Аптека связана!");
                    }
                    model.Аптеки.Local.Remove(a);
                    model.SaveChanges();
                }
                catch (DbUpdateException)
                {
                    System.Windows.MessageBox.Show("Ашибка! Запись связана!!!");
                }
            }
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            Аптеки a = DataGrid.SelectedItem as Аптеки;
            using (CollectionViewSource.GetDefaultView(Drugstores).DeferRefresh())
            {
                AddDrugstore editDrugstore = new AddDrugstore(model, a)
                {
                    Owner = this
                };
                //анализируем результат
                editDrugstore.ShowDialog();
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
