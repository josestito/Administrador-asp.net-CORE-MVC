
    document.addEventListener("DOMContentLoaded", function () {
        fetch('/controladorapi/ObtenerEspaciosJson')
            .then(response => {
                return response.json();
            })
            .then(data => {
                var Espacios = data;

                var selectIdEspacio = document.querySelector("#selectidespacio");
                var selectNombreEspacio = document.querySelector("#selectnombreespacio");

                function updateNombreEspacio() {
                    Espacios.forEach(function (espacio) {
                        if (selectIdEspacio.value == espacio.id_esp) {
                            selectNombreEspacio.value = espacio.nom_esp;
                        }
                    });
                }

                updateNombreEspacio();
                selectIdEspacio.addEventListener("change", updateNombreEspacio);
            })
    });