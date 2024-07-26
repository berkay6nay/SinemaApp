using System;
using System.Collections.Generic;

namespace SinemaApp.Models;

public partial class Rapor1
{
    public string Isim { get; set; } = null!;

    public int Id { get; set; }

    public int Expr1 { get; set; }

    public DateTime? Baslangic { get; set; }

    public DateTime? Bitis { get; set; }

    public int? FilmId { get; set; }

    public int? SalonId { get; set; }

    public int Expr2 { get; set; }

    public string Sira { get; set; } = null!;

    public int? Expr3 { get; set; }
}
