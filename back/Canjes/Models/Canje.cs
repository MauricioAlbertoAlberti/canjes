using System;
using System.Collections.Generic;

namespace Canjes.Models;

public partial class Canje
{
    public int IdCanje { get; set; }

    public DateTime? FechaCanje { get; set; }

    public int? IdPedidoCanje { get; set; }

    public virtual ICollection<DetallesCanje> DetallesCanjes { get; set; } = new List<DetallesCanje>();

    public virtual PedidosCanje? IdPedidoCanjeNavigation { get; set; }
}
