using System;
using System.Collections.Generic;

namespace SinemaApp.Models;

public partial class SinemaSalonuKoltuk
{
    public int Id { get; set; }

    public string Sira { get; set; } = null!;

    public int SiraNo { get; set; }

    public int? SalonId { get; set; }

    public virtual ICollection<GösterimKoltuk> GösterimKoltuks { get; set; } = new List<GösterimKoltuk>();

    public virtual Salon? Salon { get; set; }
}
