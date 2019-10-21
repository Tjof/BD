namespace BD.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public partial class Остановки : INotifyPropertyChanged
    {
        public string Stop
        {
            get
            {
                return Название_остановки;
            }
            set
            {
                Название_остановки = value;
                OnPropertyChanged("Название_остановки");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
