
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
    
public partial class Ассортимент_товара
{

    public int id_лекарство { get; set; }

    public int id_аптеки { get; set; }

    public Nullable<int> Количество { get; set; }

    public int id_ФормыУпаковки { get; set; }

    public Nullable<double> Цена { get; set; }



    public virtual Аптеки Аптеки { get; set; }

    public virtual Лекарство Лекарство { get; set; }

    public virtual Формы_упаковки Формы_упаковки { get; set; }

}

}
