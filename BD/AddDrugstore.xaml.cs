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
using Xceed.Wpf.Toolkit;

namespace BD
{
    /// <summary>
    /// Логика взаимодействия для AD.xaml
    /// </summary>
    public partial class AddDrugstore : Window
    {
        public AddDrugstore(BAZANOWEntities model, ICollection<Аптеки> drugstore)
        {
            InitializeComponent();
        }

        
    }
}
