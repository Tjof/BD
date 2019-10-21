using BD.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace BD
{
    /// <summary>
    /// Логика взаимодействия для AD.xaml
    /// </summary>
    public partial class AddDrug : Window
    {
        BAZANOWEntities model;
        Лекарство drugs;
        ObservableCollection<Лекарство> _drugs;

        public AddDrug(BAZANOWEntities model, Лекарство drugs)
        {
            InitializeComponent();
            this.drugs = drugs;
            DataContext = drugs;
            var a = model.Лекарство.ToArray();
            this.model = model;

            DataGrid.ItemsSource = a;
            DataGrid2.ItemsSource = drugs.Лекарство2.ToArray();

            if (drugs.id_лекарство == 0)
            {
                Title = "Добавление лекарства";
                AddEdit.Content = "Добавить";
            }
            else
            {
                Title = "Изменение лекарства";
                AddEdit.Content = "Изменить";
                DrugName.IsEnabled = true;
            }

        }

        public ObservableCollection<Лекарство> Drugss
        {
            get => _drugs;
            set
            {
                _drugs = value;
            }
        }

        private void AddEditClick(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Подтверждение", "Вы уверены, что хотите внести изменения в базу данных?", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
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
                    if (drugs.id_лекарство == 0) //new record
                    {
                        model.Лекарство.Add(drugs);
                    }
                    else
                    {
                        model.Entry(drugs).State = System.Data.Entity.EntityState.Modified;
                    }
                    model.SaveChanges();
                    this.Close();
                }
                catch (System.Data.Entity.Infrastructure.DbUpdateException)
                {
                    model.Лекарство.Local.Remove(drugs);
                    MessageBox.Show("Ошибка", "Проверьте правильность вводимых данных", MessageBoxButton.OK);
                    this.Close();
                }
            }
        }

        private void AddStop(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Подтверждение", "Вы уверены, что хотите внести изменения в базу данных?", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                try
                {
                    drugs.Лекарство2.Add(DataGrid.SelectedItem as Лекарство);
                    model.SaveChanges();
                }
                catch (System.Data.Entity.Infrastructure.DbUpdateException)
                {
                    MessageBox.Show("Ошибка", "Проверьте правильность вводимых данных", MessageBoxButton.OK);
                }
            }
        }

        private void DeleteStop(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Подтверждение", "Вы уверены, что хотите внести изменения в базу данных?", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                try
                {
                    drugs.Лекарство2.Remove(DataGrid2.SelectedItem as Лекарство);
                    model.SaveChanges();
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
