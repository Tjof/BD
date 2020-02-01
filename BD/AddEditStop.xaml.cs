using BD.Model;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace BD
{
    /// <summary>
    /// Логика взаимодействия для AD.xaml
    /// </summary>
    public partial class AddEditStop : Window
    {
        BAZANOWEntities model;
        Остановки stop;

        public AddEditStop(BAZANOWEntities model, Остановки stop)
        {
            InitializeComponent();
            this.stop = stop;
            DataContext = stop;
            this.model = model;
            ComboBoxStreet.ItemsSource = model.Улицы.ToArray();

            if (stop.id_остановки == 0)
            {
                Title = "Служба 067 - Добавление остановки";
                AddEdit.Content = "Добавить";
                StopName.Focus();
            }
            else
            {
                Title = "Служба 067 - Изменение данных об остановке";
                AddEdit.Content = "Изменить";
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
                    if (stop.id_остановки == 0)
                        model.Остановки.Add(stop);
                    else
                        model.Entry(stop).State = System.Data.Entity.EntityState.Modified;
                    model.SaveChanges();
                    this.Close();
                }
                catch (System.Data.Entity.Infrastructure.DbUpdateException)
                {
                    model.Остановки.Local.Remove(stop);
                    MessageBox.Show("Ошибка", "Проверьте правильность вводимых данных", MessageBoxButton.OK);
                    this.Close();
                }
            }
        }
    }
}
