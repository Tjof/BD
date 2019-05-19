﻿using BD.Model;
using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для FindCure.xaml
    /// </summary>
    public partial class FindCure : Window
    {
        public FindCure()
        {
            InitializeComponent();

            using (TVOYABAZAEntities model = new TVOYABAZAEntities())
            {
                comboBox_drugs.ItemsSource = model.Лекарство.ToArray();
                comboBox_street.ItemsSource = model.Улицы.ToArray();
                comboBox_stops.ItemsSource = model.Остановки.ToArray();
            }
        }


    }
}
