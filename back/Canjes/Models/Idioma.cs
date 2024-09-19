﻿using System;
using System.Collections.Generic;

namespace Canjes.Models;

public partial class Idioma
{
    public int IdIdioma { get; set; }

    public string? Descripcion { get; set; }

    public virtual ICollection<Libro> Libros { get; set; } = new List<Libro>();
}
