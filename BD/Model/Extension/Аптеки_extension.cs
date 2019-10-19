namespace BD.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public partial class Аптеки : INotifyPropertyChanged
    {
        public string Name
        {
            get { return Название; }
            set
            {
                if (Название != value)
                {
                    Название = value;
                    OnPropertyChanged("Название");
                }

            }
        }

        public string StreetName
        {
            get { return Улицы.Название_улицы; }
            set
            {
                if (Улицы.Название_улицы != value)
                {
                    Улицы.Название_улицы = value;
                    OnPropertyChanged("Название_улицы");
                }

            }
        }

        public string HouseNumber
        {
            get { return Номер_дома; }
            set
            {
                if (Номер_дома != value)
                {
                    Номер_дома = value;
                    OnPropertyChanged("Номер_дома");
                }

            }
        }

        public string WorkStartingTime
        {
            get { return Время_начала_работы; }
            set
            {
                if (Время_начала_работы != value)
                {
                    Время_начала_работы = value;
                    OnPropertyChanged("Время_начала_работы");
                }

            }
        }

        public string WorkEndingTime
        {
            get { return Время_окончания_работы; }
            set
            {
                if (Время_окончания_работы != value)
                {
                    Время_окончания_работы = value;
                    OnPropertyChanged("Время_окончания_работы");
                }

            }
        }

        public string Stop
        {
            get { return Остановки.Название_остановки; }
            set
            {
                if (Остановки.Название_остановки != value)
                {
                    Остановки.Название_остановки = value;
                    OnPropertyChanged("Название_остановки");
                }

            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
