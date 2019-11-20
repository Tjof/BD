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

            var search = model.GetRoutes1(drug.id_лекарство, stop.id_остановки)
                .GroupBy(r => new { r.Название, r.Название_улицы, r.Номер_дома, r.Время_начала_работы, r.Время_окончания_работы, r.Название_остановки, r.Разница })
                .Select( a => 
                    new {
                        a.Key.Название, a.Key.Название_улицы, a.Key.Номер_дома, a.Key.Время_начала_работы, a.Key.Время_окончания_работы, a.Key.Название_остановки, a.Key.Разница,
                        Маршруты = a.Select( mo => new { mo.Номер_маршрута, mo.Вид_транспорта }).Distinct().ToArray(),
                        Лекарства = a.Select( lek => new { lek.Название_формы, lek.Цена }).Distinct().ToArray()
                })
                .ToArray();

            DataContext = search;

        }
    }
}
