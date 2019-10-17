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
    public partial class AddEditStop : Window, INotifyPropertyChanged
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
            if (model.Entry(stop).State == System.Data.Entity.EntityState.Detached)
            {
                Title = "Добавление остановки";
                AddEdit.Content = "Добавить";
                StopName.Focus();
            }
            else
            {
                Title = "Изменение данных об остановке";
                AddEdit.Content = "Изменить";
            }
        }

        private void AddEditClick(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Подтверждение", "Вы уверены, что хотите внести изменения в базу данных?", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                try
                {
                    if (model.Entry(stop).State == System.Data.Entity.EntityState.Detached)
                    {
                        model.Остановки.Add(stop);
                    }
                    
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
