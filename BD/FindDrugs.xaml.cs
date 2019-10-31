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
            this.model = model;
            DrugName.Text = drug.Название_лекарства;
            var search = model.Ассортимент_товара
                .Where(x => x.id_лекарство == drug.id_лекарство)
                .ToList();
            //var search = model.Ассортимент_товара.Where(at => at.id_лекарство == drug.id_лекарство);
            DataContext = search;

        }
    }
}
