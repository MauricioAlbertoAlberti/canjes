using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Canjes.Comandos
{
    public class ValidarConfirmacionRegistroComando
    {
        public string? Token { set; get; }
        public string? Mail { set; get; } 
        public string? Password { set; get; } 
    }
}
