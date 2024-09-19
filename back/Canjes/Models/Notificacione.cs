using System;
using System.Collections.Generic;

namespace Canjes.Models;

public partial class Notificacione
{
    public int IdNotificacion { get; set; }

    public int IdUsuario { get; set; }

    public string? Notificacion { get; set; }

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
