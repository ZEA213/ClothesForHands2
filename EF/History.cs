
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------


namespace ClothesForHands.EF
{

using System;
    using System.Collections.Generic;
    
public partial class History
{

    public int IdHistory { get; set; }

    public int IdMaterial { get; set; }

    public string Action { get; set; }

    public Nullable<int> OldValue { get; set; }

    public int NewValue { get; set; }

    public System.DateTime DateUpdate { get; set; }

}

}