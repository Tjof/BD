namespace BD.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public partial class Улицы : INotifyPropertyChanged
    {
        public string Name
        {
            set
            {
                if (Название_улицы != value)
                {
                    Название_улицы = value;
                    OnPropertyChanged();
                }

            }
            get { return Название_улицы; }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

    }

}
