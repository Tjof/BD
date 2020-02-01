using BD.Model;
using System;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using Excel = Microsoft.Office.Interop.Excel;

namespace BD
{
    public partial class GenerateReports : Window
    {
        BAZANOWEntities model;
        ObservableCollection<Лекарство> _drug;
        public GenerateReports()
        {
            InitializeComponent();
            DataContext = this;
            model = new BAZANOWEntities();

            model.Лекарство.Load();
            Drug = model.Лекарство.Local;
            comboBox_drugs.ItemsSource = Drug;
        }

        public ObservableCollection<Лекарство> Drug
        {
            get => _drug;
            set
            {
                _drug = value;
            }
        }

        private void GenerateClick(object sender, RoutedEventArgs e)
        {
            var drug = comboBox_drugs.SelectedItem as Лекарство;

            // Создаём экземпляр нашего приложения
            Excel.Application excelApp = new Excel.Application();
            // Создаём экземпляр рабочий книги Excel
            Excel.Workbook workBook;
            // Создаём экземпляр листа Excel
            Excel.Worksheet workSheet;

            workBook = excelApp.Workbooks.Add();
            workSheet = (Excel.Worksheet)workBook.Worksheets.get_Item(1);

            //ширина колонок
            workSheet.Columns[1].ColumnWidth = 14;
            workSheet.Columns[2].ColumnWidth = 20;
            //размер шрифта для ячеек А1 и А2
            workSheet.get_Range("A1", "A2").Font.Size = 16;
            //шрифт текста
            workSheet.get_Range("A1", "A2").Font.Name = "Time New Roman";
            workSheet.Range["A1"].Value = "Служба 067";
            workSheet.Range["A2"].Value = "Отчет по лекарству " + drug.Название_лекарства + " за год";
            workSheet.Range["A4"].Value = "Месяц";
            workSheet.Range["B4"].Value = "Количество запросов";

            workSheet.Range["A5"].Value = "Январь";
            workSheet.Range["A6"].Value = "Февраль";
            workSheet.Range["A7"].Value = "Март";
            workSheet.Range["A8"].Value = "Апрель";
            workSheet.Range["A9"].Value = "Май";
            workSheet.Range["A10"].Value = "Июнь";
            workSheet.Range["A11"].Value = "Июль";
            workSheet.Range["A12"].Value = "Август";
            workSheet.Range["A13"].Value = "Сентябрь";
            workSheet.Range["A14"].Value = "Октябрь";
            workSheet.Range["A15"].Value = "Ноябрь";
            workSheet.Range["A16"].Value = "Декабрь";

            var january = model.Статистика_поиска.FirstOrDefault(x => x.id_лекарство == drug.id_лекарство && x.Месяц == 1);
            if (january == null)
                workSheet.Range["B5"].Value = 0;
            else
                workSheet.Range["B5"].Value = january.Запросов;

            var february = model.Статистика_поиска.FirstOrDefault(x => x.id_лекарство == drug.id_лекарство && x.Месяц == 2);
            if(february == null)
                workSheet.Range["B6"].Value = 0;
            else
                workSheet.Range["B6"].Value = february.Запросов;

            var march = model.Статистика_поиска.FirstOrDefault(x => x.id_лекарство == drug.id_лекарство && x.Месяц == 3);
            if(march==null)
                workSheet.Range["B7"].Value = 0;
            else
                workSheet.Range["B7"].Value = march.Запросов;

            var april = model.Статистика_поиска.FirstOrDefault(x => x.id_лекарство == drug.id_лекарство && x.Месяц == 4);
            if (april == null)
                workSheet.Range["B8"].Value = 0;
            else
                workSheet.Range["B8"].Value = april.Запросов;

            var may = model.Статистика_поиска.FirstOrDefault(x => x.id_лекарство == drug.id_лекарство && x.Месяц == 5);
            if (may == null)
                workSheet.Range["B9"].Value = 0;
            else
                workSheet.Range["B9"].Value = may.Запросов;

            var june = model.Статистика_поиска.FirstOrDefault(x => x.id_лекарство == drug.id_лекарство && x.Месяц == 6);
            if (june == null)
                workSheet.Range["B10"].Value = 0;
            else
                workSheet.Range["B10"].Value = june.Запросов;

            var july = model.Статистика_поиска.FirstOrDefault(x => x.id_лекарство == drug.id_лекарство && x.Месяц == 7);
            if (july == null)
                workSheet.Range["B11"].Value = 0;
            else
                workSheet.Range["B11"].Value = july.Запросов;

            var august = model.Статистика_поиска.FirstOrDefault(x => x.id_лекарство == drug.id_лекарство && x.Месяц == 8);
            if (august == null)
                workSheet.Range["B12"].Value = 0;
            else
                workSheet.Range["B12"].Value = august.Запросов;

            var september = model.Статистика_поиска.FirstOrDefault(x => x.id_лекарство == drug.id_лекарство && x.Месяц == 9);
            if (september == null)
                workSheet.Range["B13"].Value = 0;
            else
                workSheet.Range["B13"].Value = september.Запросов;

            var october = model.Статистика_поиска.FirstOrDefault(x => x.id_лекарство == drug.id_лекарство && x.Месяц == 10);
            if (october == null)
                workSheet.Range["B14"].Value = 0;
            else
                workSheet.Range["B14"].Value = october.Запросов;

            var november = model.Статистика_поиска.FirstOrDefault(x => x.id_лекарство == drug.id_лекарство && x.Месяц == 11);
            if (november == null)
                workSheet.Range["B15"].Value = 0;
            else
                workSheet.Range["B15"].Value = november.Запросов;

            var december = model.Статистика_поиска.FirstOrDefault(x => x.id_лекарство == drug.id_лекарство && x.Месяц == 12);
            if (december == null)
                workSheet.Range["B16"].Value = 0;
            else
                workSheet.Range["B16"].Value = december.Запросов;

            Excel.Range crange;
            Excel.ChartObjects cb = (Excel.ChartObjects)workSheet.ChartObjects(Type.Missing);
            Excel.ChartObject cbc = (Excel.ChartObject)cb.Add(300,60,400,250);
            Excel.Chart cp = cbc.Chart;

            crange = workSheet.get_Range("A4", "B16");
            cp.SetSourceData(crange, System.Reflection.Missing.Value);
            cp.ChartType = Excel.XlChartType.xlColumnClustered;
            cp.HasLegend = false;
            cp.HasTitle = true;
            cp.ChartTitle.Characters.Text = "Отчет по лекарству " + drug.Название_лекарства + " за год";
            cp.Axes(Excel.XlAxisType.xlCategory).HasTitle = true;
            cp.Axes(Excel.XlAxisType.xlCategory).AxisTitle.Characters.Text = "Месяцы";

            Excel.Borders borders = crange.Borders;
            borders.LineStyle = Excel.XlLineStyle.xlContinuous;

            // Открываем созданный excel-файл
            excelApp.Visible = true;
            excelApp.UserControl = true;

            this.Close();
        }
    }
}
