using Canjes.Datos;
using Canjes.Models;
using Canjes.Resultados;

namespace Canjes.Repository
{
    public class UsuarioRepository
    {
        public  CanjeContext _contexto = new CanjeContext();

        public UsuarioRepository(CanjeContext contexto)
        {
            this._contexto = contexto;

        }
        //public UsuarioRepository(CanjeContext contexto, CanjeDB dbAdo = null)
        //{
        //    _contexto = contexto ?? throw new ArgumentNullException(nameof(contexto));
        //    this.dbAdo = dbAdo; // Esto puede ser nulo si es opcional
        //}

        public UsuarioResultado ObtenerUsuarioPorMail(string mail)
        {
            UsuarioResultado usuario = new UsuarioResultado();

            try
            {
                var usu = _contexto.Usuarios.FirstOrDefault(x => x.Mail == mail);
                if (usu != null)
                {
                    usuario.NombreUsuario = usu.NombreUsuario;
                    usuario.IdUsuario = usu.IdUsuario;
                    usuario.Pass = usu.Pass;
                    usuario.Mail = usu.Mail;
                    usuario.RestablecerPass = usu.RestablecerPass;
                    usuario.CuentaConfirmada = usu.CuentaConfirmada;
                    usuario.Token = usu.Token;

                    return usuario;
                }
                else
                {
                    return usuario;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public UsuarioResultado ObtenerUsuarioPorNombreUsuario(string nombreUsuario)
        {
            UsuarioResultado usuario = new UsuarioResultado();

            try
            {
                var usu = _contexto.Usuarios.FirstOrDefault(x => x.NombreUsuario == nombreUsuario);
                if (usu != null)
                {
                    usuario.NombreUsuario = usu.NombreUsuario;
                    usuario.IdUsuario = usu.IdUsuario;
                    usuario.Pass = usu.Pass;
                    usuario.Mail = usu.Mail;
                    usuario.RestablecerPass = usu.RestablecerPass;
                    usuario.CuentaConfirmada = usu.CuentaConfirmada;
                    usuario.Token = usu.Token;

                    
                }

                return usuario;
                
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al obtener usuario por nombre de usuario", ex);
            }
        }

     
    }
}

