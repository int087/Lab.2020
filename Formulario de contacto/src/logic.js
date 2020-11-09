$(document).ready(function(){
 
    // Validaciones formulario.
    $(':input[type="submit"]').click(function(event) {
        event.preventDefault();

        // Valida Nombre y Apellido
        var firstName = $("#firstName").val();
        var lastName = $("#lastName").val();

        if(!firstName || !lastName)
        {
            alert("Los campos Nombre y Apellido son obligatorios.");
            return;
        }

        // Valida Edad.
        var age = $('#age').val();

        if (age <= 0)
        {
            alert("La edad debe ser mayor a cero.");
            return
        }
    });

    // Limpiar campos.
    $('button').click(function(event) {
        $(':input[type="text"]').val('');
        $(':input[type="number"]').val(0);
    });
})