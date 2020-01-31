using BD.Model;
using System;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
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

        private void SearchClick(object sender, RoutedEventArgs e)
        {
            var drug = comboBox_drugs.SelectedItem as Лекарство;
            var stop = comboBox_stops.SelectedItem as Остановки;

            Статистика_поиска searchStatistics = new Статистика_поиска();
            int month = Int32.Parse(DateTime.Today.Month.ToString());
            var searchElement = model.Статистика_поиска.FirstOrDefault(x => x.id_лекарство == drug.id_лекарство && x.Месяц == month);
            if (searchElement == null)
            {
                Статистика_поиска search = new Статистика_поиска
                {
                    id_лекарство = drug.id_лекарство,
                    Месяц = month,
                    Запросов = 1
                };
                model.Статистика_поиска.Add(search);
            }
            else
            {
                searchElement.Запросов += 1;
            }
            model.SaveChanges();

            FindDrugs findDrugs = new FindDrugs(model, drug, stop);
            findDrugs.Show();
        }
    }
}
