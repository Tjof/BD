using BD.Model;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace BD
{
    /// <summary>
    /// Логика взаимодействия для Streets.xaml
    /// </summary>
    public partial class TransportMode : Window
    {
        BAZANOWEntities model;
        private ObservableCollection<Виды_Транспорта> _transportMode;

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
                try
                {
                    model.Виды_Транспорта.Local.Remove(DataGrid.SelectedItem as Виды_Транспорта);
                    model.SaveChanges();
                    CollectionViewSource.GetDefaultView(model.Виды_Транспорта.Local).Refresh();
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
                            CollectionViewSource.GetDefaultView(model.Виды_Транспорта.Local).Refresh();
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
                            CollectionViewSource.GetDefaultView(model.Виды_Транспорта.Local).Refresh();
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
    }
}