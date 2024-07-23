using System;
using System.Collections.Generic;

namespace SinemaApp.Models;

public partial class Kullanici
{
    public int Id { get; set; }

    public string Sifre { get; set; } = null!;

    public string Isim { get; set; } = null!;

    public string? TelNo { get; set; }

    public string? Rol { get; set; }

    public virtual Admin? Admin { get; set; }

    public virtual ICollection<Rezervasyon> Rezervasyons { get; set; } = new List<Rezervasyon>();
}
