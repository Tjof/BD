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
    /// Логика взаимодействия для Main.xaml
    /// </summary>
    public partial class Main : Window
    {
        public Main()
        {
            InitializeComponent();
           // grid.ActualWidth
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Poisk_Lekarstva(object sender, RoutedEventArgs e)
        {
            
        }

        private void Smena_Pass(object sender, RoutedEventArgs e)
        {

        }

        private void Apteki(object sender, RoutedEventArgs e)
        {
            Aptekas Apteki = new Aptekas
            {
                Owner = this
            };
            Apteki.Show();
        }

        private void Street(object sender, RoutedEventArgs e)
        {
            Streets Streets = new Streets()
            {
                Owner = this
            };
            Streets.Show();
        }

        private void Assortiment(object sender, RoutedEventArgs e)
        {
            Assortimentxaml Assortiment = new Assortimentxaml()
            {
                Owner = this
            };
            Assortiment.Show();
        }

        private void Ostanovki(object sender, RoutedEventArgs e)
        {
            Ostanovki ostanovki = new Ostanovki()
            {
                Owner = this
            };
            ostanovki.Show();
        }

        private void Marshruti(object sender, RoutedEventArgs e)
        {
            Marshruti marshruti = new Marshruti()
            {
                Owner = this
            };
            marshruti.Show();
        }

        private void Lekarstvo(object sender, RoutedEventArgs e)
        {
            Lekarstva lekarstva = new Lekarstva()
            {
                Owner = this
            };
            lekarstva.Show();
        }

        private void Districts(object sender, RoutedEventArgs e)
        {
            Districts districts = new Districts()
            {
                Owner = this
            };
            districts.Show();
        }

        private void Formiupakovki(object sender, RoutedEventArgs e)
        {
            Formi_upakovki formi_Upakovki = new Formi_upakovki()
            {
                Owner = this
            };
            formi_Upakovki.Show();
        }

        private void TransportMode(object sender, RoutedEventArgs e)
        {
            TransportMode transportMode = new TransportMode()
            {
                Owner = this
            };
            transportMode.Show();
        }

        private void Rabota(object sender, RoutedEventArgs e)
        {

        }

        private void Soderzhanie(object sender, RoutedEventArgs e)
        {

        }

        private void O_programme(object sender, RoutedEventArgs e)
        {

        }
    }
}
