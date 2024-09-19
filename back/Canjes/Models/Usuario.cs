using System;
using System.Collections.Generic;

namespace Canjes.Models;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public int? IdBarrio { get; set; }

    public string? Nombre { get; set; }

    public string? Apellido { get; set; }

    public string? NombreUsuario { get; set; }

    public string? Pass { get; set; }

    public string? Mail { get; set; }

    public string? Celular { get; set; }

    public string? Direccion { get; set; }

    public bool? Activo { get; set; }

    public DateTime? FechaAlta { get; set; }

    public bool? Baja { get; set; }

    public int? Calificacion { get; set; }

    public int? IdRol { get; set; }

    public string? Contacto { get; set; }

    public string? Contacto2 { get; set; }

    public string? Comentario { get; set; }

    public string? Contacto3 { get; set; }

    public bool? RestablecerPass { get; set; }

    public bool? CuentaConfirmada { get; set; }

    public string? Token { get; set; }

    public virtual Barrio? IdBarrioNavigation { get; set; }

    public virtual Role? IdRolNavigation { get; set; }

    public virtual ICollection<Libro> Libros { get; set; } = new List<Libro>();

    public virtual ICollection<Notificacione> Notificaciones { get; set; } = new List<Notificacione>();

    public virtual ICollection<PedidosCanje> PedidosCanjes { get; set; } = new List<PedidosCanje>();
}
