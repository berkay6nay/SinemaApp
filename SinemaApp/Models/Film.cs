using System;
using System.Collections.Generic;

namespace SinemaApp.Models;

public partial class Film
{
    public int Id { get; set; }

    public string Isim { get; set; } = null!;

    public string? Aciklama { get; set; }

    public int? Sure { get; set; }

    public string? Dil { get; set; }

    public DateTime? CikisTarihi { get; set; }

    public string? Tur { get; set; }

    public string? FotoUrl { get; set; }

    public virtual ICollection<Gosterim> Gosterims { get; set; } = new List<Gosterim>();
}
