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
    /// Логика взаимодействия для EditDrugstore.xaml
    /// </summary>
    public partial class EditDrugstore : Window
    {
        BAZANOWEntities model;
        public EditDrugstore(BAZANOWEntities model, ICollection<Аптеки> drugstore)
        {
            InitializeComponent();
            this.model = model;
            model = new BAZANOWEntities();
            ComboBox_street.ItemsSource = model.Улицы.ToArray();

        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            var res = model.Аптеки.FirstOrDefault(a => a.Название == DrugstoreName.Text);
            if (res != null)
            {
                System.Windows.MessageBox.Show("Такой район уже существует");
            }
            else
            {
                // ключ по которому будем менять данные 
                //string drugstore = (DataGrid.SelectedItem as Аптеки).Название; //???????????????????
                int key = model.Аптеки.FirstOrDefault(a => a.Название == DrugstoreName.Text).id_аптеки;
                var item = model.Аптеки.Find(key);
                if (item != null)
                {
                    item.Название = DrugstoreName.Text.ToString();
                    item.id_улицы = Convert.ToInt32(ComboBox_street.SelectedValue);
                    item.Номер_дома = Convert.ToInt32(HouseNumber.Text);
                    item.Время_начала_работы = Convert.ToDateTime(WorkStartTime.Text);
                    item.Время_окончания_работы = Convert.ToDateTime(WorkEndingTime.Text);
                    model.SaveChanges();
                    //OnPropertyChanged();
                    //DataGrid.ItemsSource = new ObservableCollection<Районы_города>(model.Районы_города.ToArray());
                }
            }
        }
    }
}
