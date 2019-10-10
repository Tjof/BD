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
using Xceed.Wpf.Toolkit;

namespace BD
{
    /// <summary>
    /// Логика взаимодействия для AD.xaml
    /// </summary>
    public partial class AddDrugstore : Window
    {
        BAZANOWEntities model;

        public AddDrugstore(BAZANOWEntities model, ICollection<Аптеки> drugstore)
        {
            InitializeComponent();
            this.model = model;
            model = new BAZANOWEntities();
            ComboBox_street.ItemsSource = model.Улицы.ToArray();
        }



        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Аптеки drugstore = new Аптеки
            {
                Название = DrugstoreName.Text,
                id_улицы = Convert.ToInt32(ComboBox_street.SelectedValue),
                Номер_дома = Convert.ToInt32(HouseNumber.Text),
                Время_начала_работы = Convert.ToDateTime(WorkStartTime.Text),
                Время_окончания_работы = Convert.ToDateTime(WorkEndingTime.Text)
            };

            // Добавить в DbSet
            // Сохранить изменения в базе данных
            var res = model.Аптеки.FirstOrDefault(a => a.Название == DrugstoreName.Text);
            //СДЕЛАТЬ ЕЩЁ ПРОВЕРКУ НА СООТВЕТСТВИЕ УЛИЦЫ!!!!!!!!!!!!!!!!!! ДОБАВИТЬ && !!!!!!!!!!!!
            //и ещё вот это  && a.Номер_дома == Convert.ToInt32(HouseNumber.Text)
            if (res != null)
            {
                System.Windows.MessageBox.Show("Ашибка!! Такая аптека есть уже! Подумай ещё раз, друг!");
            }
            else
            {
                model.Аптеки.Local.Add(drugstore);
                model.Аптеки.Add(drugstore);
                model.SaveChanges();
            }
        }


    }
}
