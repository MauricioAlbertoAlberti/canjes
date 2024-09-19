using System;
using System.Collections.Generic;

namespace Canjes.Models;

public partial class Genero
{
    public int IdGenero { get; set; }

    public string? DescripcionGenero { get; set; }

    public int? Orden { get; set; }

    public virtual ICollection<Libro> Libros { get; set; } = new List<Libro>();
}
