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
    public partial class TransportMode : Window, INotifyPropertyChanged
    {
        BAZANOWEntities model;
        private ObservableCollection<Виды_Транспорта> _transportMode;

        public TransportMode()
        {
            InitializeComponent();
            model = new BAZANOWEntities();
            TransportModes = new ObservableCollection<Виды_Транспорта>(model.Виды_Транспорта.ToArray());
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
                try
                {
                    model.Виды_Транспорта.Remove(DataGrid.SelectedItem as Виды_Транспорта);
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
                            TransportModeNameEdit.Text = "";
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
                            TransportModeNameEdit.Text = "";
                            TransportModeNameEdit.IsReadOnly = true;
                            AddEditTransportMode.IsEnabled = false;
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
                TransportModeNameEdit.IsEnabled = false;
                AddEditTransportMode.IsEnabled = false;
            }

            if (DataGrid.SelectedItem == null)
            {
                TransportModeNameEdit.Text = "";
            }
            else
            {
                TransportModeNameEdit.Text = (DataGrid.SelectedItem as Виды_Транспорта).Вид_транспорта;
            }
        }
    }
}