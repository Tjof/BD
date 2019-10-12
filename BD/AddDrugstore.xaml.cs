using BD.Model;
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
            try
            {
                if (model.Entry(drugstore).State == System.Data.Entity.EntityState.Detached)
                {
                    model.Аптеки.Local.Add(drugstore);
                }
                model.SaveChanges();
                OnPropertyChanged();
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException)
            {
                System.Windows.MessageBox.Show("Такая аптека уже существует");
            }
            DialogResult = true;
        }

        void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
