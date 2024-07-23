using System;
using System.Collections.Generic;

namespace SinemaApp.Models;

public partial class GösterimKoltuk
{
    public int Id { get; set; }

    public int Fiyat { get; set; }

    public bool Durum { get; set; }

    public int? SinemaSalonuKoltukId { get; set; }

    public int? GosterimId { get; set; }

    public virtual Gosterim? Gosterim { get; set; }

    public virtual SinemaSalonuKoltuk? SinemaSalonuKoltuk { get; set; }
}
