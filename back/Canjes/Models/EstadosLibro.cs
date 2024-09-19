using System;
using System.Collections.Generic;

namespace Canjes.Models;

public partial class EstadosLibro
{
    public int IdEstadoLibro { get; set; }

    public string? DescripcionEstadoLibro { get; set; }

    public int? Orden { get; set; }

    public virtual ICollection<Libro> Libros { get; set; } = new List<Libro>();
}
