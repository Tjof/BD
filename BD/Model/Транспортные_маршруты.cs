//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BD.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Транспортные_маршруты
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Транспортные_маршруты()
        {
            this.Остановки = new HashSet<Остановки>();
        }
    
        public int id_маршрута { get; set; }
        public string Номер_маршрута { get; set; }
        public int id_ВидаТранспорта { get; set; }
    
        public virtual Виды_Транспорта Виды_Транспорта { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Остановки> Остановки { get; set; }
    }
}
