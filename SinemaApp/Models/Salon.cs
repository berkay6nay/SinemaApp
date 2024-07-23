using System;
using System.Collections.Generic;

namespace SinemaApp.Models;

public partial class Salon
{
    public int Id { get; set; }

    public string Isim { get; set; } = null!;

    public int? ToplamKoltuk { get; set; }

    public int? SinemaId { get; set; }

    public virtual ICollection<Gosterim> Gosterims { get; set; } = new List<Gosterim>();

    public virtual Sinema? Sinema { get; set; }

    public virtual ICollection<SinemaSalonuKoltuk> SinemaSalonuKoltuks { get; set; } = new List<SinemaSalonuKoltuk>();
}
