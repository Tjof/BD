using BD.Model;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace BD
{
    /// <summary>
    /// Логика взаимодействия для Streets.xaml
    /// </summary>
    public partial class Streets : Window, INotifyPropertyChanged
    {
        BAZANOWEntities model;
        private ObservableCollection<Улицы> _streetss;
        private bool _editAllowed;

        public Streets()
        {
            InitializeComponent();
            model = new BAZANOWEntities();
            model.Улицы.Load();
            Streetss = model.Улицы.Local;
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
                var a = DataGrid.SelectedItem as Улицы;
                try
                {
                    if (a.Аптеки.Count != 0 || a.Остановки.Count != 0)
                    {
                        throw new DbUpdateException("Улица связана!");
                    }
                    model.Улицы.Local.Remove(a);
                    model.SaveChanges();
                    CollectionViewSource.GetDefaultView(model.Улицы.Local).Refresh();
                }
                catch (DbUpdateException)
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
                            (DataGrid.SelectedItem as Улицы).Название_улицы = StreetNameEdit.Text;
                            model.SaveChanges();
                            CollectionViewSource.GetDefaultView(model.Улицы.Local).Refresh();
                            StreetNameEdit.Text = String.Empty;
                            StreetNameEdit.IsReadOnly = true;
                            AddEditStreet.IsEnabled = false;
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
                            street.Название_улицы = StreetNameEdit.Text;
                            model.Улицы.Local.Add(street);
                            model.SaveChanges();
                            EditAllowed = false;
                            StreetNameEdit.Text = String.Empty;
                            StreetNameEdit.IsReadOnly = true;
                            AddEditStreet.IsEnabled = false;
                        }
                        catch (System.Data.Entity.Infrastructure.DbUpdateException)
                        {
                            model.Улицы.Local.Remove(street);
                            MessageBox.Show("Ошибка", "Проверьте правильность вводимых данных", MessageBoxButton.OK);
                        }
                    }
                }
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
                StreetNameEdit.Text = (DataGrid.SelectedItem as Улицы).Название_улицы;
            }
            else
            {
                StreetNameEdit.Text = "";
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