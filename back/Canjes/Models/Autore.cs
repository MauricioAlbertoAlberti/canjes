using System;
using System.Collections.Generic;

namespace Canjes.Models;

public partial class Autore
{
    public int IdAutor { get; set; }

    public string? Nombre { get; set; }

    public string? Apellido { get; set; }

    public string? Biografia { get; set; }

    public string? Libros { get; set; }

    public string? Generos { get; set; }

    public string? Nacionalidad { get; set; }

    public string? RutaImagen { get; set; }
}
