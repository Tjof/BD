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
        BAZANOWEntities model;
        public Districts()
        {
            InitializeComponent();
            model = new BAZANOWEntities();
            DataGrid.ItemsSource = new ObservableCollection<Районы_города>( model.Районы_города.ToArray());
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddDistricts addDistricts = new AddDistricts(model, (ICollection<Районы_города>) DataGrid.ItemsSource)
            {
                Owner = this
                
            };
            addDistricts.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
