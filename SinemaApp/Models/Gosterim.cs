using System;
using System.Collections.Generic;

namespace SinemaApp.Models;

public partial class Gosterim
{
    public int Id { get; set; }

    public DateTime? Baslangic { get; set; }

    public DateTime? Bitis { get; set; }

    public int? FilmId { get; set; }

    public int? SalonId { get; set; }

    public virtual Film? Film { get; set; }

    public virtual ICollection<GösterimKoltuk> GösterimKoltuks { get; set; } = new List<GösterimKoltuk>();

    public virtual ICollection<Rezervasyon> Rezervasyons { get; set; } = new List<Rezervasyon>();

    public virtual Salon? Salon { get; set; }
}
