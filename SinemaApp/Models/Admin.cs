using System;
using System.Collections.Generic;

namespace SinemaApp.Models;

public partial class Admin
{
    public int Id { get; set; }

    public string? Rol { get; set; }

    public virtual Kullanici IdNavigation { get; set; } = null!;
}
