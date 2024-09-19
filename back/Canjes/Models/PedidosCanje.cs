using System;
using System.Collections.Generic;

namespace Canjes.Models;

public partial class PedidosCanje
{
    public int IdPedidoCanje { get; set; }

    public DateTime? FechaPedido { get; set; }

    public int? IdUsuarioEmisor { get; set; }

    public int? IdEstadoPedido { get; set; }

    public int? IdLibroReceptor { get; set; }

    public virtual ICollection<Canje> Canjes { get; set; } = new List<Canje>();

    public virtual EstadosPedido? IdEstadoPedidoNavigation { get; set; }

    public virtual Libro? IdLibroReceptorNavigation { get; set; }

    public virtual Usuario? IdUsuarioEmisorNavigation { get; set; }
}
