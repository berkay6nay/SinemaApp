using System;
using System.Collections.Generic;

namespace SinemaApp.Models;

public partial class Sinema
{
    public int Id { get; set; }

    public string Isim { get; set; } = null!;

    public int? SalonSayisi { get; set; }

    public string? Adres { get; set; }

    public virtual ICollection<Salon> Salons { get; set; } = new List<Salon>();
}
