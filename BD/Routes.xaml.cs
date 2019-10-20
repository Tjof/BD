﻿using BD.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
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
    /// Логика взаимодействия для Routes.xaml
    /// </summary>
    public partial class Routes : Window
    {
        BAZANOWEntities model;
        ObservableCollection<Транспортные_маршруты> _routes;

        public Routes()
        {
            InitializeComponent();
            DataContext = this;
            model = new BAZANOWEntities();
            model.Транспортные_маршруты.Load();
            Routess = model.Транспортные_маршруты.Local;
        }

        public ObservableCollection<Транспортные_маршруты> Routess
        {
            get => _routes;
            set
            {
                _routes = value;
            }
        }

        private void ButtonClose(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ButtonAddRoute(object sender, RoutedEventArgs e)
        {
            Транспортные_маршруты a = new Транспортные_маршруты();
            AddRoute addRoute = new AddRoute(model, a);
            addRoute.ShowDialog();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Подтверждение", "Вы уверены, что хотите внести изменения в базу данных?", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                try
                {
                    model.Транспортные_маршруты.Local.Remove(DataGrid.SelectedItem as Транспортные_маршруты);
                    model.SaveChanges();
                }
                catch (System.Data.Entity.Infrastructure.DbUpdateException)
                {
                    MessageBox.Show("Ошибка", "Удаляемые данные связаны!", MessageBoxButton.OK);
                }
            }
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            Транспортные_маршруты a = DataGrid.SelectedItem as Транспортные_маршруты;
            using (CollectionViewSource.GetDefaultView(Routess).DeferRefresh())
            {
                AddRoute editStop = new AddRoute(model, a)
                {
                    Owner = this
                };
                editStop.ShowDialog();
            }
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataGrid.SelectedItem != null)
            {
                Edit.IsEnabled = true;
                Delete.IsEnabled = true;
            }
        }
    }
}
