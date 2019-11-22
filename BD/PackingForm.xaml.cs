using BD.Class;
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
    public partial class PackingForm : Window, INotifyPropertyChanged
    {
        BAZANOWEntities model;
        private ObservableCollection<Формы_упаковки> _packingformss;
        private bool _editAllowed;

        public PackingForm()
        {
            InitializeComponent();
            model = new BAZANOWEntities();
            model.Формы_упаковки.Load();
            PackingForms = model.Формы_упаковки.Local;
            DataGrid.ItemsSource = PackingForms;
            Users users = new Users();
            users.Write(Add);
        }

        public ObservableCollection<Формы_упаковки> PackingForms
        {
            get => _packingformss;
            set
            {
                _packingformss = value;
            }
        }

        private void ButtonAdd(object sender, RoutedEventArgs e)
        {
            PackingFormNameEdit.Text = "";
            PackingFormNameEdit.IsReadOnly = false;
            AddEditPackingform.IsEnabled = true;
            PackingFormNameEdit.IsEnabled = true;
            PackingFormNameEdit.Focus();
            DataGrid.SelectedItem = null;
        }

        private void ButtonDelete(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Подтверждение", "Вы уверены, что хотите внести изменения в базу данных?", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                var a = DataGrid.SelectedItem as Формы_упаковки;
                try
                {
                    if (a.Ассортимент_товара.Count != 0)
                    {
                        throw new DbUpdateException("Форма упаковки связана!");
                    }
                    model.Формы_упаковки.Local.Remove(a);
                    model.SaveChanges();
                    CollectionViewSource.GetDefaultView(model.Формы_упаковки.Local).Refresh();
                }
                catch (DbUpdateException)
                {
                    MessageBox.Show("Ошибка", "Удаляемые данные связаны!", MessageBoxButton.OK);
                }
            }
        }

        private void ButtonEdit(object sender, RoutedEventArgs e)
        {
            AddEditPackingform.IsEnabled = true;
            PackingFormNameEdit.IsEnabled = true;
            PackingFormNameEdit.IsReadOnly = false;
            PackingFormNameEdit.Focus();
            PackingFormNameEdit.SelectionStart = PackingFormNameEdit.Text.Length;
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
                    if (PackingFormNameEdit != null)
                    {
                        try
                        {
                            (DataGrid.SelectedItem as Формы_упаковки).Название_формы = PackingFormNameEdit.Text;
                            model.SaveChanges();
                            CollectionViewSource.GetDefaultView(model.Формы_упаковки.Local).Refresh();
                            PackingFormNameEdit.Text = String.Empty;
                            PackingFormNameEdit.IsReadOnly = true;
                            AddEditPackingform.IsEnabled = false;
                        }
                        catch (System.Data.Entity.Infrastructure.DbUpdateException)
                        {
                            MessageBox.Show("Ошибка", "Проверьте правильность вводимых данных", MessageBoxButton.OK);
                        }
                    }
                }
                else
                {
                    if (PackingFormNameEdit != null)
                    {
                        Формы_упаковки packingform = new Формы_упаковки();
                        try
                        {
                            packingform.Название_формы = PackingFormNameEdit.Text;
                            model.Формы_упаковки.Local.Add(packingform);
                            EditAllowed = false;
                            model.SaveChanges();
                            PackingFormNameEdit.Text = String.Empty;
                            PackingFormNameEdit.IsReadOnly = true;
                            AddEditPackingform.IsEnabled = false;
                        }
                        catch (System.Data.Entity.Infrastructure.DbUpdateException)
                        {
                            model.Формы_упаковки.Local.Remove(packingform);
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
                Users users = new Users();
                users.EditDelete(Edit, Delete);
                PackingFormNameEdit.IsEnabled = false;
                AddEditPackingform.IsEnabled = false;
                PackingFormNameEdit.Text = (DataGrid.SelectedItem as Формы_упаковки).Название_формы;
            }
            else
            {
                PackingFormNameEdit.Text = "";
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