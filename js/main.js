$(document).ready(function () {
    $("#lbtncomi")
        .click(function() {
            $(".row .content").load("Main/Comisiones.aspx");
        });

    $("#lbtnvaca")
        .click(function() {
            $(".row .content").load("Main/Vacaciones.aspx");
        });

    $("#lbtnperm")
        .click(function() {
            $(".row .content").load("Main/Permisos.aspx");
        });

    $("#lbtnlice")
        .click(function() {
            $(".row .content").load("Main/Licencias.aspx");
        });

    $("#lbtnmovi")
        .click(function () {
            $(".row .content").load("Main/Movimientos.aspx");
        });

    $("#lbtnotro")
        .click(function () {
            $(".row .content").load("Main/Otros.aspx");
        });
    $("#lbtnacce")
       .click(function () {
           $(".row .content").load("Main/Sistema/Accesos.aspx");
       });

    $("#lbtnperf")
      .click(function () {
          $(".row .content").load("Main/Sistema/Perfiles.aspx");
      });

    $("#lbtnusua")
     .click(function () {
         $(".row .content").load("Main/Sistema/Usuarios.aspx");
     });

   

});
function comprobarCamposRequired(form) {
    var correcto = true;
    var text =   $('#'+ form +' input[type="text"]:required');
    var number = $('#'+ form +' input[type="number"]:required');
    var select = $("#"+ form +" select:required");
    $(text).each(function () {
        if ($(this).val() === "") {
            correcto = false;
            $(this).addClass("error");
        }
    });
    $(number).each(function () {
        if ($(this).val() === "") {
            correcto = false;
            $(this).addClass("error");
        }
    });
    $(select).each(function () {
        if ($(this).val() === "0" || $(this).val() === "0000" || $(this).val() === "") {
            correcto = false;
            $(this).addClass("error");
        }
    });
    return correcto;
}

//Se utiliza para que el campo de texto solo acepte letras
function soloLetras(e) {
    var key = e.keyCode || e.which;
    var tecla = String.fromCharCode(key).toString();
    var letras = " áéíóúabcdefghijklmnñopqrstuvwxyzÁÉÍÓÚABCDEFGHIJKLMNÑOPQRSTUVWXYZ";//Se define todo el abecedario que se quiere que se muestre.
    var especiales = [8, 37, 39, 46, 6]; //Es la validación del KeyCodes, que teclas recibe el campo de texto.

    var teclaEspecial = false;
    for(var i in especiales) {
        if(key == especiales[i]) {
            teclaEspecial = true;
            break;
        }
    }

    if(letras.indexOf(tecla) === -1 && !teclaEspecial)
        return false;
}
