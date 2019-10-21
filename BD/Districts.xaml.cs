using BD.Model;
using BD.Class;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace BD
{
    /// <summary>
    /// Логика взаимодействия для Districts.xaml
    /// </summary>
    public partial class Districts : Window, INotifyPropertyChanged
    {
        BAZANOWEntities model;
        private ObservableCollection<Районы_города> _districts;
        private bool _editAllowed;

        public Districts()
        {
            InitializeComponent();
            model = new BAZANOWEntities();
            model.Районы_города.Load();
            Districtss = model.Районы_города.Local;
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

        private void ButtonAdd(object sender, RoutedEventArgs e)
        {
            DistrictNameEdit.Text = "";
            DistrictNameEdit.IsReadOnly = false;
            AddEditDistrict.IsEnabled = true;
            DistrictNameEdit.IsEnabled = true;
            DistrictNameEdit.Focus();
            DataGrid.SelectedItem = null;
        }

        private void ButtonDelete(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Подтверждение", "Вы уверены, что хотите внести изменения в базу данных?", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                try
                {
                    model.Районы_города.Local.Remove(DataGrid.SelectedItem as Районы_города);
                    model.SaveChanges();
                    CollectionViewSource.GetDefaultView(model.Районы_города.Local).Refresh();
                }
                catch (System.Data.Entity.Infrastructure.DbUpdateException)
                {
                    MessageBox.Show("Ошибка", "Удаляемые данные связаны!", MessageBoxButton.OK);
                }
            }
        }

        private void ButtonEdit(object sender, RoutedEventArgs e)
        {
            AddEditDistrict.IsEnabled = true;
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
                if (RegexClass.RegexDistrict(DistrictNameEdit.Text))
                {
                    if (DataGrid.SelectedItem != null)
                    {
                        if (DistrictNameEdit != null)
                        {
                            try
                            {
                                (DataGrid.SelectedItem as Районы_города).Название_района = DistrictNameEdit.Text;
                                model.SaveChanges();
                                CollectionViewSource.GetDefaultView(model.Районы_города.Local).Refresh();
                                DistrictNameEdit.Text = "";
                                DistrictNameEdit.IsReadOnly = true;
                                AddEditDistrict.IsEnabled = false;
                            }
                            catch (System.Data.Entity.Infrastructure.DbUpdateException)
                            {
                                MessageBox.Show("Ошибка", "Проверьте правильность вводимых данных", MessageBoxButton.OK);
                            }
                        }
                    }
                    else
                    {
                        if (DistrictNameEdit != null)
                        {
                            Районы_города district = new Районы_города();
                            try
                            {
                                district.Название_района = DistrictNameEdit.Text;
                                model.Районы_города.Local.Add(district);
                                model.SaveChanges();
                                EditAllowed = false;
                                DistrictNameEdit.Text = "";
                                DistrictNameEdit.IsReadOnly = true;
                                AddEditDistrict.IsEnabled = false;
                            }
                            catch (System.Data.Entity.Infrastructure.DbUpdateException)
                            {
                                model.Районы_города.Local.Remove(district);
                                MessageBox.Show("Ошибка", "Проверьте правильность вводимых данных", MessageBoxButton.OK);
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Ошибка", "Проверьте правильность вводимых данных", MessageBoxButton.OK);
                }
            }
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataGrid.SelectedItem != null)
            {
                Edit.IsEnabled = true;
                Delete.IsEnabled = true;
                DistrictNameEdit.IsEnabled = false;
                AddEditDistrict.IsEnabled = false;
                DistrictNameEdit.Text = (DataGrid.SelectedItem as Районы_города).Название_района;
            }
            else
            {
                DistrictNameEdit.Text = "";
            }
        }

        public bool EditAllowed
        {
            get { return _editAllowed; }
            set
            {
                _editAllowed = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

    }
}
