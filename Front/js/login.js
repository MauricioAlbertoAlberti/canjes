

      function miFuncion() {
        var form = document.getElementById("formLogin");
        form.addEventListener("submit", function (e) {
          e.preventDefault();
          var email = document.getElementById("txtEmail").value;
          var pass = document.getElementById("txtPass").value;

          miValidacion();
          
          fetch("http://localhost:5143/Usuarios/Login", {
            method: "POST",
            body: JSON.stringify({
              Mail: email,
              Pass: pass,
            }),
            headers: {
              "Content-type": "application/json; charset=UTF-8",
            },
          })
            .then(function (response) {
              // if(!response.ok){alert("No se pudo loguear , erro 400 ?? Nunca llego a la api, algo paso en el camino");} //quiero informa al usuario que mierda paso luego de su login en caso de que no se pueda loguear
              // if(response.status == 200){alert("llego a la api: status code 200");}
              return response.json();
            })
            .then(function (data) {
              /* if((data == "" || data == null || data == undefined) && (data.status == 200 || data.ok==true)){
				alert("No se encontro el usuario, fue a la api, pero fallo algo o es que no estan registrados en la bas??");
			} */
              //COMO hago para saber que paso en la API???? cuando devuelve un ActionResult o un ActionResult<MiObjeto>
              console.log(data); //el data que trae es un ResultadoApi personalizado
            /*  if(data.statusCode == 200){
								alert("llego a la api: status code 200");
			  } */
			 if(data.error !== ""){
				alert(data.error);//todos los objetos y propiedades que llegan de la Api, se convierten en Json y la primera letra de las variables pasa de mayusculas pasan a minusculas
			 }
			if (data.error === "") {
                var exito = "Te logueaste bien: " + data.objeto.nombreUsuario +  " - Mail: " + data.objeto.mail;
                alert("Logueado exitoso " + exito);

                var idUsuario = data.objeto.idUsuario;
                //localStorage.removeItem("idUsuarioSesion");
                window.localStorage.clear();
                window.localStorage.setItem("idUsuarioSesion", idUsuario);
                //var usu = localStorage.getItem("idUsuarioSesion")
                //alert("id de usuario de sesion almacenado en el localStorage: " + usu );

                if (data.objeto.idRol == 2) {
                  //id 1= admin
                  // window.location.assign("../vistas/listarLibrosMenosLosPropios.html?id=" + idUsuario ); //ir a algun catalogo de inicio: ordenado por fecha de registro de los libros de los demas usuarios
                   window.location.replace("./catalogo.html?id=" + idUsuario); //paso el id por parametro con stringquery
                }
              }
			 
              /*  if (data.idRol == 1 && localStorage.getItem("idUsuarioSesion")) {
				window.location.assign('../vistas/listarLibrosMenosLosPropios.html');}
			} */
              //if (data.idRol == 2) window.location.assign por ejemplo listar todos : ('../vistas/listarUsuarios.html');
              //if (data.idRol == 2) window.location.assign ir a un catalogo : ('../vistas/listarLibros.html');
            })
            .catch((error) => console.error("Error:", error)); //como lo recupero despues = donde veo los errores?? como interpreto estas cosas
        });
      } //fin de miFuncion


      //document.addEventListener("DOMContentLoaded", miFuncion);
      window.addEventListener("load", miFuncion); //cuando suceda el evento automatico de que se carge toda la pagina entoces se desencadena automaticamente la funcion
      //notese que la funcion sin parametros se llama sin ()
      //esto es similar al document.ready de jQuery
	   
	   
	 function miValidacion() {
			var email = document.getElementById("txtEmail").value;
			var pass = document.getElementById("txtPass").value;
			
			if (email === "" || email == undefined) {
			  alert("llene el mail");
			  document.getElementById("txtEmail").focus();
			  return false;
			}else if (pass === "" || pass == undefined) {
			  alert("llene el pass");
			  document.getElementById("txtPass").focus();
			  return false;
			}
		 alert("validaciones js superadas");
		 return true;
		}
