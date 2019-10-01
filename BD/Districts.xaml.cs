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

namespace BD
{
    /// <summary>
    /// Логика взаимодействия для Districts.xaml
    /// </summary>
    public partial class Districts : Window, INotifyPropertyChanged
    {
        BAZANOWEntities model;

        public event PropertyChangedEventHandler PropertyChanged;

        public Districts()
        {
            InitializeComponent();
            model = new BAZANOWEntities();
            DataGrid.ItemsSource = new ObservableCollection<Районы_города>(model.Районы_города.ToArray());
        }

        private void ButtonAdd(object sender, RoutedEventArgs e)
        {
            AddDistricts addDistricts = new AddDistricts(model, (ICollection<Районы_города>)DataGrid.ItemsSource)
            {
                Owner = this
            };
            addDistricts.Show();
        }

        private void ButtonDelete(object sender, RoutedEventArgs e)
        {
            try
            {
                model.Районы_города.Local.Remove(DataGrid.SelectedItem as Районы_города);
                //районы_Городаss.Remove(DataGrid.SelectedItem as Районы_города);
                model.SaveChanges();
                DataGrid.ItemsSource = new ObservableCollection<Районы_города>(model.Районы_города.ToArray());
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException)
            {
                MessageBox.Show("Ашибка! Запись связана!!!");
            }

        }

        private void ButtonEdit(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonClose(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void DistrictNameEdit_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void ButtonSaveEdit(object sender, RoutedEventArgs e)
        {
            var res = model.Районы_города.FirstOrDefault(a => a.Название_района == DistrictNameEdit.Text);
            if (res != null)
            {
                MessageBox.Show("Такой район уже существует");
            }
            else
            {
                //model.Районы_города.Local.Add(district);
                //районы_Городаs.Add(district);
                // ключ по которому будем менять данные 
                string rayon = (DataGrid.SelectedItem as Районы_города).Название_района;
                int key = model.Районы_города.FirstOrDefault(a => a.Название_района == rayon).id_района;
                var item = model.Районы_города.Find(key);
                if (item != null)
                {
                    item.Название_района = DistrictNameEdit.Text.ToString();
                    model.SaveChanges();
                    DataGrid.ItemsSource = new ObservableCollection<Районы_города>(model.Районы_города.ToArray());
                    //OnPropertyChanged();
                    //ProcRefresh();
                }
            }
        }

        //private object[] _processesList;

        //public object[] ProcessesList
        //{
        //    get => _processesList;
        //    set
        //    {
        //        _processesList = value;
        //        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ProcessesList"));
        //    }
        //}

        //void ProcRefresh()
        //{
        //    ProcessesList = model.Районы_города.Select()
        //        .Select(pi => new {  })
        //        .ToArray();
        //}

        //void OnPropertyChanged([CallerMemberName] string prop = "")
        //{
        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        //}

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DistrictNameEdit.Text = (DataGrid.SelectedItem as Районы_города).Название_района;
        }
    }
}
