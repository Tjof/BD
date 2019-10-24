using BD.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

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

            Drugss = new ObservableCollection<Лекарство>(drugs.Лекарство2.ToList());
            DataGrid.ItemsSource = a;
            DataGrid2.ItemsSource = Drugss;

            if (drugs.id_лекарство == 0)
            {
                Title = "Добавление лекарства";
                AddEdit.Content = "Добавить";
                DrugName.Focus();
            }
            else
            {
                Title = "Изменение лекарства";
                AddEdit.Content = "Изменить";
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

        private void AddDrugs(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Подтверждение", "Вы уверены, что хотите внести изменения в базу данных?", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                var drug = DataGrid.SelectedItem as Лекарство;
                var findDrug = Drugss.FirstOrDefault(p => p.id_лекарство == drug.id_лекарство);
                if (findDrug == null)
                {
                    Drugss.Add(drug);
                    drugs.Лекарство2.Add(drug);
                    model.SaveChanges();
                }
                else
                {
                    MessageBox.Show("Ошибка", "Проверьте правильность вводимых данных", MessageBoxButton.OK);
                }
            }
        }

        private void DeleteDrug(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Подтверждение", "Вы уверены, что хотите внести изменения в базу данных?", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                var drug = DataGrid2.SelectedItem as Лекарство;
                try
                {
                    drugs.Лекарство2.Remove(drug);
                    Drugss.Remove(drug);
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
                Delete.IsEnabled = false;
                DataGrid2.SelectedItem = null;
            }
        }

        private void DataGrid2_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (DataGrid2.SelectedItem != null)
            {
                Delete.IsEnabled = true;
                Add.IsEnabled = false;
                DataGrid.SelectedItem = null;
            }
        }
    }
}
