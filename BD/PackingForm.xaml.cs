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
    /// Логика взаимодействия для PackingForm.xaml
    /// </summary>
    public partial class PackingForm : Window
    {
        public PackingForm()
        {
            InitializeComponent();

            using (BAZANOWEntities model = new BAZANOWEntities())
            {
                DataGrid.ItemsSource = model.Формы_упаковки.ToArray();
            }
        }
    }
}
