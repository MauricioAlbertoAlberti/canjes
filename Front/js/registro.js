//cargar el modal de los terminos
// cargarModal();//si meto la ejecucion de esta funcion al cargar pag, se hace un bucle

window.addEventListener("load", (e) => {
    e.preventDefault();
    llenarComboBarrios();

  document.getElementById("form").addEventListener("submit", (e) => {
    e.preventDefault();
    //document.getElementById("modal").showModal(); //cuando se presenta el modal se interrumpe aca?
    alert("Complete");

    if (miValidacion() == true) {
      const usuario = document.getElementById("txtUsuario").value;
      const mail = document.getElementById("txtEmail").value;
      const password = document.getElementById("txtPassword").value;
      const idBarrio = document.getElementById("cbBarrio").value;
      const confirmarPass = document.getElementById("txtRePassword").value;

      var body = {
        idBarrio:idBarrio ,
        nombreUsuario: usuario,
        password: password,
        confirmarPass: confirmarPass,
        mail: mail
      };

      fetch("http://localhost:5143/Usuarios/RegistrarUsuario", {
        method: "PUT",
        body: JSON.stringify(body),
        headers: {
          "Content-type": "application/json; charset=UTF-8",
        },
      })
        .then(function (response) {
          return response.json();
        })
        .then(function (data) {
          console.log(data);
          if (data.StatusCode == 200) {
            //??eso puede se asi
            if (data) alert("Se registro con exito"); //que trae data?
          }
          let id = data; //me devuelve el id el controlador??
          window.localStorage.setItem("idUsuarioSesion", id);
          alert("id del nuevo usuario: " + id);
          window.location.replace("./catalogo.html"); //el ide del usuario de la sesion se podria pasar por un queryString ?key=valor
        })
        .catch((error) => console.error("Error:", error));
    } //fin del bloque if
  }); // fin del  form.addEventListener para registrar usuario
});


function miValidacion() {
  let email = document.getElementById("txtEmail").value;//obteber el valor ,value , el tipo de dato de email se convierte a string
  let txtPassword = document.getElementById("txtPassword").value;
  let usuario = document.getElementById("txtUsuario").value;
  let barrio = document.getElementById("cbBarrio").value;
  let confirmarPass = document.getElementById("txtRePassword").value;

  //validar formato de mail, tambien se puede hacer desde los tributos del Tag input correspondiente en HTML
  //hay varias capas en serie de validaciones: Desde el input en html, desde el js , y luego desde el controlador en la api
  const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
  if (!email || email === "") {
    alert("El campo de correo es obligatorio.");
    document.getElementById("txtEmail").focus();//se aplica la funcion al elemento no al valor.value, ya que la funcion no se puede aplicar a un string
    return false;
  } /* else if (!emailRegex.test(email)) {
    alert("Por favor, introduce un correo válido.");
    return false;
  }
 */
  if (!usuario || usuario === "") {
    alert("El campo de nombre de usuario es obligatorio.");
    document.getElementById("txtUsuario").focus();
    return false;
  }

  if (!barrio || barrio.value === 0) {
    alert("Por favor, selecciona un barrio.");
    return false;
  }
 
  // Validar que las contraseñas no estén vacías y coincidan
  if (!txtPassword || txtPassword==="") {
    alert("El campo de contraseña es obligatorio.");
    document.getElementById("txtPassword").focus();
    return false;
  } else if (!confirmarPass || confirmarPass ==="") {
    alert("El campo de repetir contraseña es obligatorio.");
    document.getElementById("txtRePassword").focus();
    return false;
  } else if (txtPassword !== confirmarPass) {
    document.getElementById("txtRePassword").focus();
    alert("Las contraseñas no coinciden.");
    return false;
  }
  // Expresión regular para validar la contraseña
  const passwordRegex = /^(?=.*[A-Z])(?=.*\d)(?=.*[\W_])[A-Za-z\d\W_]{8,20}$/;

  // Validar que cumpla con la expresión regular
  if (!passwordRegex.test(txtPassword)) {
    alert("Formato de contraseña no valido: Debe tener al menos 8 caracteres, 20 como maximo. Una letra Mayuscula al menos. Al menos un numero y un caracter especial");
    document.getElementById("txtPassword").focus();
	return false; // Contraseña no válida
  }

  alert("Validaciones de js superadas");
  return true; // Contraseña válida
}

function llenarComboBarrios() {
  fetch("http://localhost:5143/Usuarios/ObtenerBarrios")
    .then((response) => response.json())
    .then((data) => {
      let barrio = document.getElementById("cbBarrio");

      for (let i = 0; i < data.length; i++) {
        let option = document.createElement("option");
        option.value = data[i].idBarrio;
        option.text = data[i].nombreBarrio;
        barrio.add(option);
      }
    })
    .catch((error) => console.log(error));
}

/* function cargarModal() {
        let modal = document.getElementById("modal");
        modal.showModal();

        document
          .getElementById("btnCerrarModal")
          .addEventListener("click", () => {
            modal.close();
          });

        let btnAbrirModal = document.getElementById("btnAbrirModal");
        btnAbrirModal.addEventListener("click", () => {
          modal.showModal();
        });
      } */

//script para el modal que dara los mensajes de las validaciones de campos
// Obtener el modal y el botón de cierre
/* const modal = document.getElementById("validationModal");
const span = document.getElementsByClassName("close")[0];
const modalMessage = document.getElementById("modalMessage");

// Función para mostrar el modal con el mensaje de error
function showModal(message) {
  modalMessage.innerText = message;
  modal.style.display = "block";
}

// Cuando el usuario haga clic en el botón de cierre (x)
span.onclick = function () {
  modal.style.display = "none";
};

// Cuando el usuario haga clic fuera del modal, se cierra
window.onclick = function (event) {
  if (event.target == modal) {
    modal.style.display = "none";
  }
}; */
