using BD.Model;
using BD.Class;
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
using Xceed.Wpf.Toolkit;

namespace BD
{
    /// <summary>
    /// Логика взаимодействия для AD.xaml
    /// </summary>
    public partial class AddAssortment : Window, INotifyPropertyChanged
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
            if (model.Entry(assortment).State == System.Data.Entity.EntityState.Detached)
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
                try
                {
                    if (model.Entry(assortment).State == System.Data.Entity.EntityState.Detached)
                    {
                        model.Ассортимент_товара.Add(assortment);
                    }
                    //else
                    //{
                    //    MessageBox.Show("Ошибка", "Проверьте правильность вводимых данных", MessageBoxButton.OK);
                    //}
                    model.SaveChanges();
                    OnPropertyChanged();
                }
                catch (System.Data.Entity.Infrastructure.DbUpdateException)
                {
                    MessageBox.Show("Ошибка", "Проверьте правильность вводимых данных", MessageBoxButton.OK);
                }
            }


        }

        void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
