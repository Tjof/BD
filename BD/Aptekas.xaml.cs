using BD.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
    /// Логика взаимодействия для Aptekas.xaml
    /// </summary>
    public partial class Aptekas : Window
    {
        BAZANOWEntities model;
        private ObservableCollection<Аптеки> _drugs;

        public Aptekas()
        {
            InitializeComponent();
            model = new BAZANOWEntities();
            Drugs = new ObservableCollection<Аптеки>(model.Аптеки.Include("Улицы").ToArray());
            DataGrid.ItemsSource = Drugs;
        }

        public ObservableCollection<Аптеки> Drugs
        {
            get => _drugs;
            set
            {
                _drugs = value;
                OnPropertyChanged();
            }
        }

        private void ButtonClose(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ButtonAddDrugstore(object sender, RoutedEventArgs e)
        {
            AddDrugstore addDrugstore= new AddDrugstore(model, (ICollection<Аптеки>)DataGrid.ItemsSource)
            {
                Owner = this
            };
            addDrugstore.Show();
        }

        void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
