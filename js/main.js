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

