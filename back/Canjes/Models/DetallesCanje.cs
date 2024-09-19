using System;
using System.Collections.Generic;

namespace Canjes.Models;

public partial class DetallesCanje
{
    public int IdDetalleCanje { get; set; }

    public int IdLibroEntregado { get; set; }

    public int? IdLibroRecibido { get; set; }

    public string? ComentarioCanje { get; set; }

    public int? ValoracionCanje { get; set; }

    public int? IdCanje { get; set; }

    public int? IdUsuario { get; set; }

    public virtual Canje? IdCanjeNavigation { get; set; }
}
