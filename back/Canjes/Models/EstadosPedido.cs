using System;
using System.Collections.Generic;

namespace Canjes.Models;

public partial class EstadosPedido
{
    public int IdEstadoPedido { get; set; }

    public string? Descripcion { get; set; }

    public virtual ICollection<PedidosCanje> PedidosCanjes { get; set; } = new List<PedidosCanje>();
}
