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
        private ObservableCollection<Районы_города> _districts;

        public Districts()
        {
            InitializeComponent();
            model = new BAZANOWEntities();
            Districtss = new ObservableCollection<Районы_города>(model.Районы_города.ToArray());
            DataGrid.ItemsSource = Districtss;
        }

        public ObservableCollection<Районы_города> Districtss
        {
            get => _districts;
            set
            {
                _districts = value;
                OnPropertyChanged();
            }
        }

        private void ButtonAdd(object sender, RoutedEventArgs e)
        {
            //AddDistricts addDistricts = new AddDistricts(model, (ICollection<Районы_города>)DataGrid.ItemsSource)
            //{
            //    Owner = this
            //};
            //addDistricts.Show();

            DistrictNameEdit.Text = "";
            DistrictNameEdit.IsReadOnly = false;
            AddDistrict.IsEnabled = true;
        }

        private void ButtonAddDistrict(object sender, RoutedEventArgs e)
        {
            
            Районы_города district = new Районы_города
            {
                Название_района = DistrictNameEdit.Text
            };

            // Добавить в DbSet
            // Сохранить изменения в базе данных
            var res = model.Районы_города.FirstOrDefault(a => a.Название_района == DistrictNameEdit.Text);
            if (res != null)
            {
                MessageBox.Show("Ошибка");
            }
            else
            {
                model.Районы_города.Local.Add(district);
                model.Районы_города.Add(district);
                model.SaveChanges();
                DataGrid.ItemsSource = new ObservableCollection<Районы_города>(model.Районы_города.ToArray());
                DistrictNameEdit.Text = "";
                DistrictNameEdit.IsReadOnly = true;
                AddDistrict.IsEnabled = false;
            }
        }

        private void ButtonDelete(object sender, RoutedEventArgs e)
        {
            try
            {
                model.Районы_города.Local.Remove(DataGrid.SelectedItem as Районы_города);
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
            SaveEdit.IsEnabled = true;
            DistrictNameEdit.IsReadOnly = false;
        }

        private void ButtonClose(object sender, RoutedEventArgs e)
        {
            Close();
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
                // ключ по которому будем менять данные 
                string rayon = (DataGrid.SelectedItem as Районы_города).Название_района;
                int key = model.Районы_города.FirstOrDefault(a => a.Название_района == rayon).id_района;
                var item = model.Районы_города.Find(key);
                if (item != null)
                {
                    item.Название_района = DistrictNameEdit.Text.ToString();
                    model.SaveChanges();
                    //OnPropertyChanged();
                    DataGrid.ItemsSource = new ObservableCollection<Районы_города>(model.Районы_города.ToArray());
                    DistrictNameEdit.Text = "";
                    DistrictNameEdit.IsReadOnly = true;
                    SaveEdit.IsEnabled = false;
                }
            }
        }        

        void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
        public event PropertyChangedEventHandler PropertyChanged;

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(DataGrid.SelectedItem != null)
            {
                Edit.IsEnabled = true;
                Delete.IsEnabled = true;
            }

            if (DataGrid.SelectedItem  == null)
            {
                DistrictNameEdit.Text = "";
            }
            else
            {
                DistrictNameEdit.Text = (DataGrid.SelectedItem as Районы_города).Название_района;
            }
        }

        
    }
}
