using Canjes.Comandos;
using Canjes.Datos;
using Canjes.Dto;
using Canjes.Models;
using Canjes.Repository;
using Canjes.Resultados;
using Canjes.Servicios;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Data;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Diagnostics.Eventing.Reader;
using Microsoft.EntityFrameworkCore;
using static System.Net.WebRequestMethods;

namespace Canjes.Controllers
{

    [Route("[controller]")]
    [EnableCors("misCors")]
    public class UsuariosController : ControllerBase
    {

        public readonly CanjeDB dbAdo;

        CanjeContext _contexto = new CanjeContext();


        public CanjeContext contexto = new CanjeContext();

        public UsuariosController(CanjeContext contexto)
        {
            this._contexto = contexto;

        }

        [HttpGet]
        [Route("ListarTodosUsuarios")]
        public IActionResult ListarTodosUsuarios()
        {
            List<Usuario> lista = new List<Usuario>();

            try
            {
                var listaUsuarios = _contexto.Usuarios.ToList();
                List<UsuarioResultado> listaUsuariosResultado = new List<UsuarioResultado>();
                foreach (var usu in listaUsuarios)
                {
                    UsuarioResultado usuarioR = new UsuarioResultado();
                    usuarioR.IdUsuario = usu.IdUsuario;
                    usuarioR.Apellido = usu.Apellido;
                    usuarioR.Nombre = usu.Nombre;
                    usuarioR.Mail = usu.Mail;

                    listaUsuariosResultado.Add(usuarioR);
                }

                return Ok(listaUsuariosResultado);

            }
            catch (Exception ex)
            {
                string mensaje = ex.Message;
                return Ok("llego a la api, pero con error dentro de la api");
            }

        }

        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult<ResultadoApi>> Login([FromBody] ComandoLoginValidar comando)
        {
            ResultadoApi resultado = new ResultadoApi();

            if (comando == null)
            {
                resultado.Error = "No se ingresaron datos";
                resultado.StatusCode = 400;// Código de error HTTP para Bad Request por parte del cliente, algo paso del lado del front que el comando llego nulo
                return BadRequest(resultado);
            }

            try
            {
                comando.Mail = comando.Mail.Trim();
                string sha = Utilidad.Encriptar(comando.Pass);
                // Hacemos la consulta asíncrona
                var usuario = await _contexto.Usuarios.FirstOrDefaultAsync(x => (x.Mail == comando.Mail) && (x.Pass == sha));

                if (usuario != null)
                {
                    if (usuario.CuentaConfirmada == false)
                    {
                        resultado.Error = $"Falta confirmar la cuenta del usuario, revisar la casilla de mail ingresada: {usuario.Mail}";
                        return Ok(resultado); // HTTP 200 pero con un mensaje de advertencia
                    }
                    else if (usuario.RestablecerPass == true)
                    {
                        resultado.Error = $"Esta en proceso de restablecimiento de la contraseña de la cuenta del usuario, revisar la casilla de mail ingresada: {usuario.Mail}";
                        return Ok(resultado); // HTTP 200 pero con un mensaje de advertencia
                    }
                    else
                    {
                        resultado.objeto = usuario;
                        resultado.StatusCode = 200;
                        resultado.Error = "";
                        return Ok(resultado);
                    }
                }
                else
                {
                    resultado.Error = "No se encontró un usuario con esos datos de login en la base de datos";
                    resultado.StatusCode = 404; // HTTP 404 para Not Found
                    return NotFound(resultado); // Es más adecuado que Ok() en este caso
                }

            }
            catch (Exception ex)
            {
                string mensaje = ex.Message;
                return Ok(mensaje);
            }
        }


        [HttpGet]
        [Route("ObtenerBarrios")]
        public ActionResult<List<Barrio>> ObtenerBarrios()
        {

            try
            {
                var lista = _contexto.Barrios.ToList();
                return Ok(lista);
            }
            catch (Exception ex)
            {
                // Aquí puedes loguear el error y devolver un mensaje más genérico
                // Log.Error("Error en el login", ex);
                return StatusCode(500, "Ocurrió un error interno en el servidor. Inténtalo nuevamente más tarde.");
            }
        }



        [HttpPut]
        [Route("RegistrarUsuario")]
        public ActionResult RegistrarUsuario([FromBody] RegistrarUsuarioComando comando)
        {

            if (comando == null)
            {
                return BadRequest("No se ingresaron datos");
            }


            try
            {
                var usu = ObtenerUsuarioPorMail(comando.Mail);
                var usu2 = ObtenerUsuarioPorNombreUsuario(comando.NombreUsuario);
                //hay que validar que no haya un usuario con el mismo mail o nombre de usuario ya registrado
                if (comando.Password != comando.ConfirmarPass) { return Ok("las contraseñas no coinciden"); }

                else if (usu.Mail == comando.Mail)
                {
                    return Ok("el mail ingresado ya se encuentra registrado en el sistema");
                }
                else if (usu2.NombreUsuario == comando.NombreUsuario)
                {
                    return Ok("ya existe ese nombre de usuario registrado en el sistema, intente con otro");
                }
                else
                {

                    Usuario u = new Usuario();
                    u.NombreUsuario = comando.NombreUsuario;
                    u.Mail = comando.Mail;

                    u.Pass = Utilidad.Encriptar(comando.Password);
                    u.Token = Utilidad.GenerarToken();

                    u.IdRol = 2;
                    u.IdBarrio = comando.IdBarrio;

                    u.CuentaConfirmada = false;
                    u.RestablecerPass = false;
                    u.Activo = false;

                    _contexto.Add(u);
                    _contexto.SaveChanges();

                    int idUsuario = 0;
                    idUsuario = u.IdUsuario;//si lo creo en la base debe devolver mayor a 0

                    if (idUsuario > 0)
                    {
                        //enviar correo de confirmacion
                        //arma un string con la url a la que se va a redireccionar, a la vista de Confirmacion de validacion o confirmacion de la cuenta creada
                        // string url = "http://localhost:5143/";//deberia ir a la vista de confirmacion y pasar el Token del usuario u.Token por parametro de queryString ?token=u.Token

                        //string url = string.Format("{0}://{1}{2}", Request.Scheme, Request.Headers["host"], "/ConfirmarResgistro?token=" + u.Token); //le pasa el token generado por ?queryString variable=valor
                        // en el visual studio Code con el plugin de Live Server monta el front en 
                        //http://127.0.0.1:5500/
                        //la redireccion desde el correo deberia ser a la direccion donde estara montado el front a la pag de la vista.html de confirmarRegistro
                        // por ejemplo http://127.0.0.1:5500/ ese ip es el ip local, diferente al ip publico sumistrado por el proveedor de intenet fibertel, etc
                        string url = "http://127.0.0.1:5500/ConfirmarRegistro.html?token=" + u.Token; //http:/localhost:5500/
                        string htmlBody = @"<!DOCTYPE html>
                                            <html lang='es'>
                                             <body>
                                              <div style='width:600px;padding:20px;border:1px solid #DBDBDB;border-radius:12px;font-family:Sans-serif'>
                                                 <h1 style='color:#C76F61'>Confirmar correo electrónico</h1>
                                                 <p style='margin-bottom:25px'>Estimado/a&nbsp;<b>" + u.NombreUsuario + @"</b>:</p>
                                                 <p style='margin-bottom:25px'>Gracias por abrir una cuenta con nosotros. Para utilizar su cuenta, primero deberá confirmar su correo electrónico haciendo clic en el boton a continuación.
                            Copie el Token en la vista de confirmacion: " + u.Token + @"</p> 
                                                    <a style='padding:12px;border-radius:12px;background-color:#6181C7;color:#fff;text-decoration:none' href='" + url + @"' target='_blank'>Confirme su correo electronico</a>
                                                   <p style='margin-top:25px'>Gracias.</p>
                                                </div>
                                              </body>
                                             </html>";

                        //var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
                        //string path = Path.Combine((string)AppDomain.CurrentDomain.GetData("ContentRootPath"), "~/Plantillas/Confirmar.html");
                        //string path = "C:\\Ps2024\\_PRESENTAR\\Presentar_13Sept\\back\\Canjes\\Plantillas\\ConfirmarRegistro.html\"";
                        //string content = System.IO.File.ReadAllText(path);//el contenido del mail es el html la plantilla 
                          // string htmlBody = string.Format(content, u.Nombre, url);

                        CorreoDto correoDto = new CorreoDto()
                        {
                            Destinatario = u.Mail,
                            Asunto = "Correo confirmacion",
                            Contenido = htmlBody
                        };


                        bool enviado = Correo.EnviarCorreo(correoDto);

                        string mensaje = $"Su cuenta ha sido creada. Hemos enviado un mensaje al correo {u.Mail} para confirmar su cuenta";
                        //return Ok(idUsuario);
                        return Ok(mensaje);

                    }
                    else { return Ok("No se pudo registrar"); }

                }
            }
            catch (Exception ex) { return Ok(ex.Message); }


        }


        [HttpPost]
        [Route("ValidarConfirmacionRegistro")]
        public ActionResult ValidarConfirmacionRegistro([FromBody] ValidarConfirmacionRegistroComando comando) {
            if (comando == null) {
                return BadRequest("Datos del comando erroneos o vacios");
            }

            string token = comando.Token;
            string mail = comando.Mail;
            string pass = comando.Password;

            try
            {
                var usuario = _contexto.Usuarios.FirstOrDefault(x => (x.Mail == mail) && (x.Pass == Utilidad.Encriptar(pass)) && (x.Token == token));

                if (usuario != null) { 
                    bool confirmada = (bool)usuario.CuentaConfirmada;
                    bool restablecer = (bool)usuario.RestablecerPass;

                    CanjeDB dbAdo = new CanjeDB();
                    UsuarioRepositoryAdo ado = new UsuarioRepositoryAdo(dbAdo);
                    ado.RestablecerActualizar(token, mail, confirmada, restablecer); //esto actualiza el registro de la base Usuarios, para que el usuario quede con la cuenta activada, y confirmada y con restablecer en false 
                
                    return Ok("Se valido, Confirmo y activo la cuenta nueva");
                }

                return NotFound("No se encontro el usuario en la base");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocurrió un error interno en el servidor. Inténtalo nuevamente más tarde.");
            }

            
        }



        //estos metodos deberian parsar a Repository y no ser EndPoints, sin http,  sino solo de conulta interna 
        [HttpGet]
        [Route("ObtenerUsuarioPorMail")]
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

        [HttpGet]
        [Route("ObtenerUsuarioPorNombreUsuario")]
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

