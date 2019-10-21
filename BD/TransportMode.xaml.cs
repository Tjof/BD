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
    public partial class TransportMode : Window, INotifyPropertyChanged
    {
        BAZANOWEntities model;
        private ObservableCollection<Виды_Транспорта> _transportMode;
        private bool _editAllowed;

        public TransportMode()
        {
            InitializeComponent();
            model = new BAZANOWEntities();
            model.Виды_Транспорта.Load();
            TransportModes = model.Виды_Транспорта.Local;
            DataGrid.ItemsSource = TransportModes;
        }

        public ObservableCollection<Виды_Транспорта> TransportModes
        {
            get => _transportMode;
            set
            {
                _transportMode = value;
            }
        }

        private void ButtonAdd(object sender, RoutedEventArgs e)
        {
            TransportModeNameEdit.Text = "";
            TransportModeNameEdit.IsReadOnly = false;
            AddEditTransportMode.IsEnabled = true;
            TransportModeNameEdit.IsEnabled = true;
            TransportModeNameEdit.Focus();
            DataGrid.SelectedItem = null;
        }

        private void ButtonDelete(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Подтверждение", "Вы уверены, что хотите внести изменения в базу данных?", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                var a = DataGrid.SelectedItem as Виды_Транспорта;
                try
                {
                    if(a.Транспортные_маршруты.Count != 0)
                    {
                        throw new DbUpdateException("Вид транспорта связан!");
                    }
                    model.Виды_Транспорта.Local.Remove(a);
                    model.SaveChanges();
                    CollectionViewSource.GetDefaultView(model.Виды_Транспорта.Local).Refresh();
                }
                catch (DbUpdateException)
                {
                    MessageBox.Show("Ошибка", "Удаляемые данные связаны!", MessageBoxButton.OK);
                }
            }
        }

        private void ButtonEdit(object sender, RoutedEventArgs e)
        {
            AddEditTransportMode.IsEnabled = true;
            TransportModeNameEdit.IsEnabled = true;
            TransportModeNameEdit.IsReadOnly = false;
            TransportModeNameEdit.Focus();
            TransportModeNameEdit.SelectionStart = TransportModeNameEdit.Text.Length;
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
                    if (TransportModeNameEdit != null)
                    {
                        try
                        {
                            (DataGrid.SelectedItem as Виды_Транспорта).Вид_транспорта = TransportModeNameEdit.Text;
                            model.SaveChanges();
                            CollectionViewSource.GetDefaultView(model.Виды_Транспорта.Local).Refresh();
                            TransportModeNameEdit.Text = String.Empty;
                            TransportModeNameEdit.IsReadOnly = true;
                            AddEditTransportMode.IsEnabled = false;
                        }
                        catch (System.Data.Entity.Infrastructure.DbUpdateException)
                        {
                            MessageBox.Show("Ошибка", "Проверьте правильность вводимых данных", MessageBoxButton.OK);
                        }
                    }
                }
                else
                {
                    if (TransportModeNameEdit != null)
                    {
                        Виды_Транспорта transportMode = new Виды_Транспорта();
                        try
                        {
                            transportMode.Вид_транспорта = TransportModeNameEdit.Text;
                            model.Виды_Транспорта.Add(transportMode);
                            model.SaveChanges();
                            EditAllowed = false;
                            TransportModeNameEdit.Text = String.Empty;
                            TransportModeNameEdit.IsReadOnly = true;
                            AddEditTransportMode.IsEnabled = false;
                        }
                        catch (System.Data.Entity.Infrastructure.DbUpdateException)
                        {
                            model.Виды_Транспорта.Local.Remove(transportMode);
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
                TransportModeNameEdit.IsEnabled = false;
                AddEditTransportMode.IsEnabled = false;
                TransportModeNameEdit.Text = (DataGrid.SelectedItem as Виды_Транспорта).Вид_транспорта;
            }
            else
            {
                TransportModeNameEdit.Text = "";
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