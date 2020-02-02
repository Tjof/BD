using BD.Class;
using System.Windows;
using System.Windows.Forms;

namespace BD
{
    public partial class Main : Window
    {
        public Main()
        {
            InitializeComponent();
            Users users = new Users();
            users.Read(Handbooks, GenerateReports);
        }
        
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void FindDrugsClick(object sender, RoutedEventArgs e)
        {
            SearchMenu searchMenu = new SearchMenu
            {
                Owner = this
            };
            searchMenu.Show();
        }

        private void PasswordChangeClick(object sender, RoutedEventArgs e)
        {
            Repass repass= new Repass
            {
                Owner = this
            };
            repass.Show();
        }

        private void DrugstoresClick(object sender, RoutedEventArgs e)
        {
            Drugstore apteki = new Drugstore
            {
                Owner = this
            };
            apteki.Show();
        }

        private void StreetsClick(object sender, RoutedEventArgs e)
        {
            Streets streets = new Streets()
            {
                Owner = this
            };
            streets.Show();
        }

        private void AssortmentClick(object sender, RoutedEventArgs e)
        {
            Assortment Assortiment = new Assortment()
            {
                Owner = this
            };
            Assortiment.Show();
        }

        private void StopsClick(object sender, RoutedEventArgs e)
        {
            Stops ostanovki = new Stops()
            {
                Owner = this
            };
            ostanovki.Show();
        }

        private void RoutesClick(object sender, RoutedEventArgs e)
        {
            Routes marshruti = new Routes()
            {
                Owner = this
            };
            marshruti.Show();
        }

        private void DrugsClick(object sender, RoutedEventArgs e)
        {
            Drugs lekarstva = new Drugs()
            {
                Owner = this
            };
            lekarstva.Show();
        }

        private void PackingFormsClick(object sender, RoutedEventArgs e)
        {
            PackingForm formi_Upakovki = new PackingForm()
            {
                Owner = this
            };
            formi_Upakovki.Show();
        }

        private void TransportModeClick(object sender, RoutedEventArgs e)
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
            Users users = new Users();
            users.Help();
        }

        private void AboutTheProgram(object sender, RoutedEventArgs e)
        {
            AboutTheProgram aboutTheProgram = new AboutTheProgram()
            {
                Owner = this
            };
            aboutTheProgram.Show();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void Generate_Click(object sender, RoutedEventArgs e)
        {
            GenerateReports generateReports = new GenerateReports()
            {
                Owner = this
            };
            generateReports.Show();
        }
    }
}
