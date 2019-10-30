using BD.Model;
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
    /// Логика взаимодействия для FindDrugs.xaml
    /// </summary>
    public partial class FindDrugs : Window
    {
        BAZANOWEntities model;
        public FindDrugs(BAZANOWEntities model, Лекарство drug, Остановки stop)
        {
            InitializeComponent();
            DrugName.Text = drug.Название_лекарства;
            var search = model.Лекарство.Include("Ассортимент_товара").Include("Аптеки").Select(x=> x.Ассортимент_товара == drug.Ассортимент_товара);
            DataContext = search;
        }
    }
}
