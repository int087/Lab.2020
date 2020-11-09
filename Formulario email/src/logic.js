$(document).ready(function(){


    $(':input[type="submit"]').click(function(event) {
        event.preventDefault();

        // Validar campos obligatorios.
        var email = $('#email').val();
        var pass  = $('#password').val();
        var msj = "";

        (!email || !pass) ? msj = "Debe ingresar email y password." : msj = "Datos ingresados correctamente.";

        alert(msj);
    });
});