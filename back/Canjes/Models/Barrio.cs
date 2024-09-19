using System;
using System.Collections.Generic;

namespace Canjes.Models;

public partial class Barrio
{
    public int IdBarrio { get; set; }

    public string? NombreBarrio { get; set; }

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
