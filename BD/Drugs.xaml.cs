using BD.Class;
using BD.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    public partial class Drugs : Window
    {
        BAZANOWEntities model;
        public ICollectionView Drugss { get; set; }

        public Drugs()
        {
            InitializeComponent();
            DataContext = this;
            model = new BAZANOWEntities();
            model.Лекарство.Load();
            Drugss = CollectionViewSource.GetDefaultView(model.Лекарство.Local);
            Users users = new Users();
            users.Write(Add);

            DataGrid.Items.SortDescriptions.Clear();
            DataGrid.Items.SortDescriptions.Add(new SortDescription("Название_лекарства", ListSortDirection.Ascending));
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
            DataGrid.SelectedItem = a;
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Подтверждение", "Вы уверены, что хотите внести изменения в базу данных?", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                var a = DataGrid.SelectedItem as Лекарство;
                try
                {
                    if(a.Ассортимент_товара.Count != 0)
                    {
                        throw new DbUpdateException("Лекарство связано!");
                    }
                    model.Лекарство.Local.Remove(a);
                    model.SaveChanges();
                }
                catch (DbUpdateException)
                {
                    MessageBox.Show("Ошибка", "Удаляемые данные связаны!", MessageBoxButton.OK);
                }
                Edit.IsEnabled = false;
                Delete.IsEnabled = false;
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
                Users users = new Users();
                users.EditDelete(Edit, Delete);
            }
        }
    }
}
