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
    /// Логика взаимодействия для Streets.xaml
    /// </summary>
    public partial class Streets : Window, INotifyPropertyChanged
    {
        BAZANOWEntities model;
        private ObservableCollection<Улицы> _streetss;

        public Streets()
        {
            InitializeComponent();
            model = new BAZANOWEntities();
            Streetss = new ObservableCollection<Улицы>(model.Улицы.ToArray());
            DataGrid.ItemsSource = Streetss;

        }

        public ObservableCollection<Улицы> Streetss
        {
            get => _streetss;
            set
            {
                _streetss = value;
            }
        }

        private void ButtonAdd(object sender, RoutedEventArgs e)
        {
            StreetNameEdit.Text = "";
            StreetNameEdit.IsReadOnly = false;
            AddEditStreet.IsEnabled = true;
            StreetNameEdit.IsEnabled = true;
            StreetNameEdit.Focus();
            DataGrid.SelectedItem = null;
        }

        private void ButtonDelete(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Подтверждение", "Вы уверены, что хотите внести изменения в базу данных?", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                try
                {
                    _streetss.Remove(DataGrid.SelectedItem as Улицы);
                    model.Улицы.Remove(DataGrid.SelectedItem as Улицы);
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
            AddEditStreet.IsEnabled = true;
            StreetNameEdit.IsEnabled = true;
            StreetNameEdit.IsReadOnly = false;
            StreetNameEdit.Focus();
            StreetNameEdit.SelectionStart = StreetNameEdit.Text.Length;
        }

        private void ButtonClose(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ButtonSaveEdit(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Подтверждение", "Вы уверены, что хотите внести изменения в базу данных?", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                if (DataGrid.SelectedItem != null)
                {
                    if (StreetNameEdit != null)
                    {
                        try
                        {
                            (DataGrid.SelectedItem as Улицы).Name = StreetNameEdit.Text;
                            model.SaveChanges();
                            StreetNameEdit.Text = "";
                            StreetNameEdit.IsReadOnly = true;
                            AddEditStreet.IsEnabled = false;
                            //CollectionViewSource.GetDefaultView(_streetss).Refresh();
                            //Streetss = new ObservableCollection<Улицы>(model.Улицы.ToArray());
                        }
                        catch (System.Data.Entity.Infrastructure.DbUpdateException)
                        {
                            MessageBox.Show("Ошибка", "Проверьте правильность вводимых данных", MessageBoxButton.OK);
                        }
                    }
                }
                else
                {
                    if (StreetNameEdit != null)
                    {
                        Улицы street = new Улицы();
                        try
                        {
                            street.Name = StreetNameEdit.Text;
                            _streetss.Add(street);
                            model.Улицы.Add(street);
                            model.SaveChanges();
                            StreetNameEdit.Text = "";
                            StreetNameEdit.IsReadOnly = true;
                            AddEditStreet.IsEnabled = false;
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
                StreetNameEdit.IsEnabled = false;
                AddEditStreet.IsEnabled = false;
            }

            if (DataGrid.SelectedItem == null)
            {
                StreetNameEdit.Text = "";
            }
            else
            {
                StreetNameEdit.Text = (DataGrid.SelectedItem as Улицы).Название_улицы;
            }
        }
    }
}