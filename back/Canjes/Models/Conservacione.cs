using System;
using System.Collections.Generic;

namespace Canjes.Models;

public partial class Conservacione
{
    public int IdConservacion { get; set; }

    public string? DescripcionConservacion { get; set; }

    public int? Orden { get; set; }

    public virtual ICollection<Libro> Libros { get; set; } = new List<Libro>();
}
