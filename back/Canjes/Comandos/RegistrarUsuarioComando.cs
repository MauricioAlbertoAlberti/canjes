namespace Canjes.Comandos
{
    public class RegistrarUsuarioComando
    {
        
        public int? IdBarrio { get; set; }
        public string? NombreUsuario { get; set; }
        public string? Password { get; set; }
        public string? ConfirmarPass { get; set; }
        public string? Mail { get; set; }        
        public string? Token { get; set; }

        public bool? RestablecerPass { get; set; }
        public bool? CuentaConfirmada { get; set; }

    }
}
