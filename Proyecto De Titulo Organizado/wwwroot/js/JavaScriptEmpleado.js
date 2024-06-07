
    $(document).ready(function () {
        $("#botoneliminar").click(function (event) {
            var confirmacion = confirm('¿Estás seguro de que deseas eliminar este empleado? Esta accion borrara todos los registros y las observaciones que pertenecen a ese empleado');
            if (!confirmacion) {
                event.preventDefault(); // Prevenir el envío del formulario si el usuario cancela
            }
        })
    });