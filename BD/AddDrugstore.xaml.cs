using BD.Model;
using BD.Class;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace BD
{
    /// <summary>
    /// Логика взаимодействия для AD.xaml
    /// </summary>
    public partial class AddDrugstore : Window
    {
        BAZANOWEntities model;
        Аптеки drugstore;

        public AddDrugstore(BAZANOWEntities model, Аптеки drugstore)
        {
            InitializeComponent();
            this.drugstore = drugstore;
            DataContext = drugstore;
            this.model = model;
            ComboBox_street.ItemsSource = model.Улицы.ToArray();
            ComboBox_stop.ItemsSource = model.Остановки.ToArray();

            if (drugstore.id_аптеки == 0)
            {
                Title = "Служба 067 - Добавление аптеки";
                AddEdit.Content = "Добавить";
                DrugstoreName.Focus();
            }
            else
            {
                Title = "Служба 067 - Редактирование аптеки";
                AddEdit.Content = "Изменить";
            }

        }

        private void AddEditClick(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Подтверждение", "Вы уверены, что хотите внести изменения в базу данных?", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                if (RegexClass.RegexDrugstore(DrugstoreName.Text, WorkStartingTime.Text, WorkEndingTime.Text))
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
                        if (drugstore.id_аптеки == 0)
                        {
                            model.Аптеки.Add(drugstore);
                        }
                        else
                        {
                            model.Entry(drugstore).State = System.Data.Entity.EntityState.Modified;
                        }
                        model.SaveChanges();
                        this.Close();
                    }
                    catch (System.Data.Entity.Infrastructure.DbUpdateException)
                    {
                        model.Аптеки.Local.Remove(drugstore);
                        MessageBox.Show("Ошибка", "Проверьте правильность вводимых данных", MessageBoxButton.OK);
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Ошибка", "Проверьте правильность вводимых данных", MessageBoxButton.OK);
                }
            }
        }
    }
}
