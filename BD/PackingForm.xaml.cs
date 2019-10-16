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
    public partial class PackingForm : Window, INotifyPropertyChanged
    {
        BAZANOWEntities model;
        private ObservableCollection<Формы_упаковки> _packingformss;

        public PackingForm()
        {
            InitializeComponent();
            model = new BAZANOWEntities();
            PackingForms = new ObservableCollection<Формы_упаковки>(model.Формы_упаковки.ToArray());
            DataGrid.ItemsSource = PackingForms;

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
                try
                {
                    model.Формы_упаковки.Remove(DataGrid.SelectedItem as Формы_упаковки);
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
                            PackingFormNameEdit.Text = "";
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
                            model.Формы_упаковки.Add(packingform);
                            model.SaveChanges();
                            PackingFormNameEdit.Text = "";
                            PackingFormNameEdit.IsReadOnly = true;
                            AddEditPackingform.IsEnabled = false;
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
                PackingFormNameEdit.IsEnabled = false;
                AddEditPackingform.IsEnabled = false;
            }

            if (DataGrid.SelectedItem == null)
            {
                PackingFormNameEdit.Text = "";
            }
            else
            {
                PackingFormNameEdit.Text = (DataGrid.SelectedItem as Формы_упаковки).Название_формы;
            }
        }
    }
}