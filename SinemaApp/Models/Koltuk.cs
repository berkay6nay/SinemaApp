using System;
using System.Collections.Generic;

namespace SinemaApp.Models;

public partial class Koltuk
{
    public int Id { get; set; }

    public bool? DoluMu { get; set; }

    public int? Fiyat { get; set; }

    public string? Sira { get; set; }

    public int? SiraNo { get; set; }

    public int? SalonId { get; set; }

    public int? GosterimId { get; set; }

    public virtual Gosterim? Gosterim { get; set; }

    public virtual Salon? Salon { get; set; }
}
