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
    
    public partial class Остановки
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Остановки()
        {
            this.Аптеки = new HashSet<Аптеки>();
            this.МаршрутыОстановки = new HashSet<МаршрутыОстановки>();
        }
    
        public int id_остановки { get; set; }
        public int id_улицы { get; set; }
        public string Название_остановки { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Аптеки> Аптеки { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<МаршрутыОстановки> МаршрутыОстановки { get; set; }
        public virtual Улицы Улицы { get; set; }
    }
}
