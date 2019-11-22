using BD.Class;
using BD.Model;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace BD
{
    /// <summary>
    /// Логика взаимодействия для Assortment.xaml
    /// </summary>
    public partial class Assortment : Window
    {
        BAZANOWEntities model;
        ObservableCollection<Ассортимент_товара> _assortment;

        public Assortment()
        {
            InitializeComponent();
            DataContext = this;
            model = new BAZANOWEntities();
            model.Ассортимент_товара.Load();
            Assortments = model.Ассортимент_товара.Local;
            Users users = new Users();
            users.Write(Add);
        }

        public ObservableCollection<Ассортимент_товара> Assortments
        {
            get => _assortment;
            set
            {
                _assortment = value;
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

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Подтверждение", "Вы уверены, что хотите внести изменения в базу данных?", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                try
                {
                    model.Ассортимент_товара.Local.Remove(DataGrid.SelectedItem as Ассортимент_товара);
                    model.SaveChanges();
                }
                catch (System.Data.Entity.Infrastructure.DbUpdateException)
                {
                    System.Windows.MessageBox.Show("Ашибка! Запись связана!!!");
                }
            }
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            Ассортимент_товара a = DataGrid.SelectedItem as Ассортимент_товара;
            using (CollectionViewSource.GetDefaultView(Assortments).DeferRefresh())
            {
                AddAssortment editAssortment = new AddAssortment(model, a)
                {
                    Owner = this
                };
                editAssortment.ShowDialog();
            }
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataGrid.SelectedItem != null)
            {
                Users users = new Users();
                users.EditDelete(Edit, Delete);
            }
        }
    }
}
