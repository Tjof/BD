using BD.Class;
using BD.Model;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace BD
{
    /// <summary>
    /// Логика взаимодействия для AD.xaml
    /// </summary>
    public partial class AddAssortment : Window
    {
        BAZANOWEntities model;
        Ассортимент_товара assortment;

        public AddAssortment(BAZANOWEntities model, Ассортимент_товара assortment)
        {
            InitializeComponent();
            this.assortment = assortment;
            DataContext = assortment;
            this.model = model;
            DrugName.ItemsSource = model.Лекарство.ToArray();
            DrugstoreName.ItemsSource = model.Аптеки.ToArray();
            PackingFormName.ItemsSource = model.Формы_упаковки.ToArray();

            if (assortment.id_лекарство == 0)
            {
                Title = "Добавление нового ассортимента";
                AddEdit.Content = "Добавить";
            }
            else
            {
                Title = "Редактирование ассортимента";
                AddEdit.Content = "Изменить";
                DrugName.IsEnabled = false;
                DrugstoreName.IsEnabled = false;
                PackingFormName.IsEnabled = false;
            }

        }

        private void AddEditClick(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Подтверждение", "Вы уверены, что хотите внести изменения в базу данных?", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                if (RegexClass.RegexAssortment(Count.Text, Price.Text))
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
                        if (assortment.id_лекарство == 0) //new record
                        {
                            model.Ассортимент_товара.Add(assortment);
                        }
                        else
                        {
                            model.Entry(assortment).State = System.Data.Entity.EntityState.Modified;
                        }
                        model.SaveChanges();
                        this.Close();
                    }
                    catch (System.Data.Entity.Infrastructure.DbUpdateException)
                    {
                        MessageBox.Show("Ошибка", "Проверьте правильность вводимых данных", MessageBoxButton.OK);
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
