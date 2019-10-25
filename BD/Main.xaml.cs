using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
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
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Poisk_Lekarstva(object sender, RoutedEventArgs e)
        {
            FindCure findCure = new FindCure
            {
                Owner = this
            };
            findCure.Show();
        }

        private void Smena_Pass(object sender, RoutedEventArgs e)
        {
            Repass repass= new Repass
            {
                Owner = this
            };
            repass.Show();
        }

        private void Apteki(object sender, RoutedEventArgs e)
        {
            Drugstore apteki = new Drugstore
            {
                Owner = this
            };
            apteki.Show();
        }

        private void Street(object sender, RoutedEventArgs e)
        {
            Streets streets = new Streets()
            {
                Owner = this
            };
            streets.Show();
        }

        private void Assortiment(object sender, RoutedEventArgs e)
        {
            Assortment Assortiment = new Assortment()
            {
                Owner = this
            };
            Assortiment.Show();
        }

        private void Ostanovki(object sender, RoutedEventArgs e)
        {
            Stops ostanovki = new Stops()
            {
                Owner = this
            };
            ostanovki.Show();
        }

        private void Marshruti(object sender, RoutedEventArgs e)
        {
            Routes marshruti = new Routes()
            {
                Owner = this
            };
            marshruti.Show();
        }

        private void Lekarstvo(object sender, RoutedEventArgs e)
        {
            Drugs lekarstva = new Drugs()
            {
                Owner = this
            };
            lekarstva.Show();
        }

        private void Formiupakovki(object sender, RoutedEventArgs e)
        {
            PackingForm formi_Upakovki = new PackingForm()
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

        private void WorkingWithHelp(object sender, RoutedEventArgs e)
        {

        }

        private void ClickContent(object sender, RoutedEventArgs e)
        {
            Help.ShowHelp(null, "Help.chm");
        }

        private void AboutTheProgram(object sender, RoutedEventArgs e)
        {
            AboutTheProgram aboutTheProgram = new AboutTheProgram()
            {
                Owner = this
            };
            aboutTheProgram.Show();
        }
    }
}
