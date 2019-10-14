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
    public partial class AddDrugstore : Window, INotifyPropertyChanged
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
            if (model.Entry(drugstore).State == System.Data.Entity.EntityState.Detached)
            {
                Title = "Добавление аптеки";
                AddEdit.Content = "Добавить";
            }
            else
            {
                Title = "Редактирование аптеки";
                AddEdit.Content = "Изменить";
            }
            
        }

        private void AddEditClick(object sender, RoutedEventArgs e)
        {
            if(MessageBox.Show("Подтверждение", "Вы уверены, что хотите внести изменения в базу данных?", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                try
                {
                    if (model.Entry(drugstore).State == System.Data.Entity.EntityState.Detached && RegexClass.RegexDrugstore(DrugstoreName.Text, WorkStartTime.Text, WorkEndingTime.Text))
                    {
                        model.Аптеки.Local.Add(drugstore);
                    }else if(RegexClass.RegexDrugstore(DrugstoreName.Text, WorkStartTime.Text, WorkEndingTime.Text) == false)
                    {
                        MessageBox.Show("Ошибка","Проверьте правильность вводимых данных", MessageBoxButton.OK);
                    }
                    model.SaveChanges();
                    OnPropertyChanged();
                }
                catch (System.Data.Entity.Infrastructure.DbUpdateException)
                {
                    System.Windows.MessageBox.Show("Такая аптека уже существует");
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
