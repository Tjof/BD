using BD.Model;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Windows;

namespace BD
{
    /// <summary>
    /// Логика взаимодействия для SearchMenu.xaml
    /// </summary>
    public partial class SearchMenu : Window
    {
        BAZANOWEntities model;
        ObservableCollection<Лекарство> _drug;
        ObservableCollection<Остановки> _stop;
        public SearchMenu()
        {
            InitializeComponent();
            DataContext = this;
            model = new BAZANOWEntities();

            model.Лекарство.Load();
            model.Остановки.Load();
            Drug = model.Лекарство.Local;
            Stop = model.Остановки.Local;
            comboBox_drugs.ItemsSource = Drug;
            comboBox_stops.ItemsSource = Stop;
        }

        public ObservableCollection<Лекарство> Drug
        {
            get => _drug;
            set
            {
                _drug = value;
            }
        }

        public ObservableCollection<Остановки> Stop
        {
            get => _stop;
            set
            {
                _stop = value;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var drug = comboBox_drugs.SelectedItem as Лекарство;
            var stop = comboBox_stops.SelectedItem as Остановки;
            FindDrugs findDrugs = new FindDrugs(model, drug, stop);
            findDrugs.Show();
        }
    }
}
