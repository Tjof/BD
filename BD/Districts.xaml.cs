using BD.Class;
using BD.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
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
        private Районы_города _dist;

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
            }
        }

        public Районы_города SelectedDistrict
        {
            get => _dist;
            set
            {
                _dist = value;
                OnPropertyChanged("SelectedDistrict");
            }
        }

        private void ButtonAdd(object sender, RoutedEventArgs e)
        {
            DistrictNameEdit.Text = "";
            DistrictNameEdit.IsReadOnly = false;
            SaveEdit.IsEnabled = true;
            DistrictNameEdit.IsEnabled = true;
            DistrictNameEdit.Focus();
        }

        private void ButtonDelete(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Подтверждение", "Вы уверены, что хотите внести изменения в базу данных?", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                try
                {
                    model.Районы_города.Remove(DataGrid.SelectedItem as Районы_города);
                    model.SaveChanges();
                }
                catch (System.Data.Entity.Infrastructure.DbUpdateException)
                {
                    MessageBox.Show("Ошибка", "Удаляемые данные связаны!", MessageBoxButton.OK);
                }
            }
        }

        private void ButtonEdit(object sender, RoutedEventArgs e)
        {
            SaveEdit.IsEnabled = true;
            DistrictNameEdit.IsEnabled = true;
            DistrictNameEdit.IsReadOnly = false;
            DistrictNameEdit.Focus();
            DistrictNameEdit.SelectionStart = DistrictNameEdit.Text.Length;
        }

        private void ButtonClose(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ButtonSaveEdit(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Подтверждение", "Вы уверены, что хотите внести изменения в базу данных?", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                if (SelectedDistrict != null)
                {
                    if (DistrictNameEdit != null)
                    {
                        try
                        {
                            SelectedDistrict.Название_района = DistrictNameEdit.Text;
                            model.SaveChanges();
                            DistrictNameEdit.Text = "";
                            DistrictNameEdit.IsReadOnly = true;
                            SaveEdit.IsEnabled = false;
                        }
                        catch (System.Data.Entity.Infrastructure.DbUpdateException)
                        {
                            MessageBox.Show("Ошибка", "Проверьте правильность вводимых данных", MessageBoxButton.OK);
                        }
                    }
                }
                else
                {
                    if(DistrictNameEdit != null)
                    {
                        Районы_города district = new Районы_города();
                        try
                        {
                            district.Название_района = DistrictNameEdit.Text;
                            model.Районы_города.Add(district);
                            model.SaveChanges();
                            DistrictNameEdit.Text = "";
                            DistrictNameEdit.IsReadOnly = true;
                            SaveEdit.IsEnabled = false;
                        }
                        catch (System.Data.Entity.Infrastructure.DbUpdateException)
                        {
                            MessageBox.Show("Ошибка", "Проверьте правильность вводимых данных", MessageBoxButton.OK);
                        }
                    }
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataGrid.SelectedItem != null)
            {
                Edit.IsEnabled = true;
                Delete.IsEnabled = true;
                DistrictNameEdit.IsEnabled = false;
                SaveEdit.IsEnabled = false;
            }

            if (DataGrid.SelectedItem == null)
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
