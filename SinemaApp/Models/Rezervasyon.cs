using System;
using System.Collections.Generic;

namespace SinemaApp.Models;

public partial class Rezervasyon
{
    public int Id { get; set; }

    public int? KoltukSayisi { get; set; }

    public DateTime? OlusturulmaTarihi { get; set; }

    public int? KullaniciId { get; set; }

    public int? GosterimId { get; set; }

    public virtual Gosterim? Gosterim { get; set; }

    public virtual ICollection<GösterimKoltuk> GösterimKoltuks { get; set; } = new List<GösterimKoltuk>();

    public virtual Kullanici? Kullanici { get; set; }
}
