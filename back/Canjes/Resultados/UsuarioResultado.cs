namespace Canjes.Resultados
{
    public class UsuarioResultado
    {
            public int IdUsuario { get; set; }

            public string? Nombre { get; set; }

            public string? Apellido { get; set; }

            public string? Pass { get; set; }

            public string? NombreUsuario { get; set; }

            public string? Mail { get; set; }

            public string? Celular { get; set; }

            public string? Direccion { get; set; }

            public DateTime? FechaAlta { get; set; }

            public int? Calificacion { get; set; }

            public int? IdRol { get; set; }
            public bool? RestablecerPass { get; set; }

            public bool? CuentaConfirmada { get; set; }

            public string? Token { get; set; }
    }
    }
