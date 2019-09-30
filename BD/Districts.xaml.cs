using BD.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
    public partial class Districts : Window
    {
        private readonly ICollection<Районы_города> районы_Городаss;
        BAZANOWEntities model;
        public Districts()
        {
            InitializeComponent();
            model = new BAZANOWEntities();
            DataGrid.ItemsSource = new ObservableCollection<Районы_города>(model.Районы_города.ToArray());
        }

        private void ButtonAdd(object sender, RoutedEventArgs e)
        {
            AddDistricts addDistricts = new AddDistricts(model, (ICollection<Районы_города>)DataGrid.ItemsSource)
            {
                Owner = this

            };
            addDistricts.Show();
        }

        private void ButtonDelete(object sender, RoutedEventArgs e)
        {
            try
            {
                model.Районы_города.Local.Remove(DataGrid.SelectedItem as Районы_города);
                //районы_Городаss.Remove(DataGrid.SelectedItem as Районы_города);
                model.SaveChanges();
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException)
            {
                MessageBox.Show("Ашибка! Запись связана!!!");
            }

        }

        private void ButtonClose(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
