using Canjes.Datos;
using Canjes.Resultados;

namespace Canjes.Repository
{
    public class UsuarioRepositoryAdo
    {
        public CanjeDB dbAdo;
        public UsuarioRepositoryAdo(CanjeDB dbAdo)
        {
            this.dbAdo = dbAdo;
        }

        public bool RestablecerActualizar(string token, string mail, bool confirmada, bool restablecer)
        {

            // Si dbAdo es opcional, verifica si es nulo y créalo si es necesario
            var db = dbAdo ?? new CanjeDB(); //tiliza el operador de coalescencia nula (??), que significa "si el valor de la izquierda es null, usa el valor de la derecha"
            if (restablecer == false && confirmada == false && mail != null && token != null)
            {
                int filasAfectadas;
                try
                {
                    string query = @"update usuarios set cuentaconfirmada=1, activo=1, restablecerPass=0 where token=@token";
                    db.SetearQuery(query);
                    db.SetearParametro("@token", token);
                    filasAfectadas = db.EjecutarNonQuery();
                    if (filasAfectadas > 0)
                    {
                        return true;
                    }
                    else { return false; }
                    //var usu = _contexto.Usuarios.FirstOrDefault(x => x.Mail == mail);

                    //if (usu != null)
                    //{
                    //    usuario.NombreUsuario = usu.NombreUsuario;
                    //    usuario.IdUsuario = usu.IdUsuario;
                    //    usuario.Pass = usu.Pass;
                    //    usuario.Mail = usu.Mail;
                    //    usuario.RestablecerPass = usu.RestablecerPass;
                    //    usuario.CuentaConfirmada = usu.CuentaConfirmada;
                    //    usuario.Token = usu.Token;

                    //    return usuario;
                    //}
                    //else
                    //{
                    //    return false;
                    //}
                }
                catch (Exception ex)
                {
                    throw new ApplicationException("Error al actualizar el restablecimiento", ex);
                }
                finally
                {
                    db.CerrarConexion(); // finally se ejecuta si o si independiente del error
                                         // Si dbAdo fue inyectado, esto cerrará la conexión
                }
            }
            else {
                return false;
            }
        }



    }
}
