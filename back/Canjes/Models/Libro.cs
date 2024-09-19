using System;
using System.Collections.Generic;

namespace Canjes.Models;

public partial class Libro
{
    public int IdLibro { get; set; }

    public string? Titulo { get; set; }

    public string? Autor { get; set; }

    public string? Editorial { get; set; }

    public int? IdUsuario { get; set; }

    public int? IdIdioma { get; set; }

    public string? Edicion { get; set; }

    public int? IdEstadoLibro { get; set; }

    public int? AnioPublicacion { get; set; }

    public int? IdGenero { get; set; }

    public int? IdConservacion { get; set; }

    public DateTime? FechaResgistro { get; set; }

    public string? Resenia { get; set; }

    public bool? Baja { get; set; }

    public string? ImagenRuta { get; set; }

    public string? ImagenNombre { get; set; }

    public bool? TapaDura { get; set; }

    public bool? Bolsillo { get; set; }

    public int? NumeroPaginas { get; set; }

    public bool? Ilustrado { get; set; }

    public string? Traduccion { get; set; }

    public int? Isbn { get; set; }

    public string? Pais { get; set; }

    public string? Observaciones { get; set; }

    public bool? Deseado { get; set; }

    public virtual Conservacione? IdConservacionNavigation { get; set; }

    public virtual EstadosLibro? IdEstadoLibroNavigation { get; set; }

    public virtual Genero? IdGeneroNavigation { get; set; }

    public virtual Idioma? IdIdiomaNavigation { get; set; }

    public virtual Usuario? IdUsuarioNavigation { get; set; }

    public virtual ICollection<PedidosCanje> PedidosCanjes { get; set; } = new List<PedidosCanje>();
}
