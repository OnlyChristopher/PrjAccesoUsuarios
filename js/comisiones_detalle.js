var valor = $("#hdcip").val();//Valor del CIP seleccionado o buscado

$(document).ready(function (funcion) {

  

    /************************* 
        Detalle de Movimientos 
    **************************/
       
    $("#txtobservacion").attr("disabled", "disabled");
    $("#txtobservacion").css("resize", "none");
    $(".dgvdetallecomision_hijo").find("tbody tr:not(:has(thead))").remove();

    getMovimientos(); //Recibe los Movimientos

    var valor = $("#hdcip").val();
    var img = $(".img-profile");
    var rutaImg = valor.length;
    var imgUrl;


    toastr.options = {
        "closeButton": true,
        "positionClass": "toast-bottom-full-width"
    };
    

   
   $("#btnreportedetallado")
        .click(function() {
            if ($("#nro_transa").val() === "") {
                toastr.error("Seleccione un registro", "Comisiones");

            } else {
                $("#md_referencia .modal-content")
                    .load("Reportes/RptReferencia.aspx?rcontrol_trans=" + $("#nro_transa").val());
                $("#md_referencia").modal("show");
            }


        });

    $("#btnaddmov")
        .click(function() {
            if ($("#nro_transa").val() === "") {
                toastr.error("Seleccione un registro", "Comisiones");

            } else {
                $("#md_referencia .modal-content")
                    .load("Main/Comisiones_movimiento.aspx?rcontrol_trans=" + $("#nro_transa").val());
                $("#md_referencia").modal("show");
            }
        });


    /* Obtengo valor del cip y extraigo el ultimo caracter para obtener la ruta de la foto*/
    rutaImg = valor.charAt(rutaImg - 1);
   
    if (valor.substr(0, 2) === "00") {
        var nvalor = valor.replace("00", "");
        rutaImg = nvalor.length;
        rutaImg = nvalor.charAt(rutaImg - 1);
         imgUrl = "http://172.16.1.13/fotostitular/" + rutaImg + "/" + nvalor + ".jpg";
    } else {
         imgUrl = "http://172.16.1.13/fotostitular/" + rutaImg + "/" + valor + ".jpg";
    }


    var defaultUrl = "images/sin_foto.png";

    img.error(function () {
        $(this).attr("src", defaultUrl);
    });
    img.attr("src", imgUrl);


    $("#btnadicionar")
        .click(function() {
            $("#md_registro .modal-content").load("Main/Comisiones_registro.aspx?xtipreg=xReg");
            $("#md_registro").modal("show");
        });


    /** Limpiar variable  .img-profile **/
    $("[data-dismiss]")
        .click(function() {
            $(".img-profile").attr("src", "");
            $("#txtobservacion").val("");
            $("#hdcip").val("");
            $(".background-tr").removeClass("background-tr");
            $(".dgvdetallecomision").find("tr:not(:has(thead))").remove();
            $(".dgvdetallecomision_hijo").find("tr:not(:has(thead))").remove();
        });

    /* Movilidad al Modal*/
    $("#md_detalle").draggable({
        handle: ".modal-header"
    });
    
});

function getMovimientos() {
    $.ajax({
        type: "POST",
        url: "Main/Comisiones.aspx/GetMovimientos",
        data: '{maspe_carne: "' + valor + '" }',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: jMovimiento
    });
}




function jMovimiento(response) {
    $(".dgvdetallecomision").find("tr:not(:has(thead))").remove();
    var obj = jQuery.parseJSON(response.d);
    $(".dgvdetallecomision")
        .footable({
            "columns": $.Deferred(function (d) {
                setTimeout(function () {
                    $.get("js/dgvdetallecomision_columns.json").then(d.resolve, d.reject);
                }, 1000);
                setTimeout(function () {
                    getFunciones();
                }, 1500);
            }),
            "rows": obj

        });
}

function getFunciones() {
    /* Recuperar Datos de Oficial Seleccionado */
    var tabla = $(".dgvdetallecomision").find("tbody tr:not(:has(th))");
    var tablaTd = tabla.children("td");

    $(".dgvdetallecomision_hijo").find("tr:not(:has(thead))").remove();

    selectionrowDetalle();//Para obtener el valor de la fila seleccionada
 

    getdgvdetallecomisionHijo(tablaTd[0].innerText);

   

}

function selectionrowDetalle() {
    var j = 0;
    $(".dgvdetallecomision").find("tbody tr:not(:has(th))").click(function () {
        
        j += 1;
        console.log(j);
        var td = $(this).children("td");
        var nroTransa = "";

        for (var i = 0; i < 1; ++i) {
            nroTransa = td[i].innerText;
            $("#nro_transa").val(nroTransa);
        }

        $(".background-detalle-tr").removeClass("background-detalle-tr");
        $(this).children("td").addClass("background-detalle-tr");
        $("#txtobservacion").val("");
     
        getdgvdetallecomisionHijo(nroTransa);
       

    });


}


function updateBackground() {
    $(".dgvdetallecomision tfoot .footable-paging").click(null);
    $(".dgvdetallecomision tfoot .footable-paging").click(function () {
        $(".background-detalle-tr").removeClass("background-detalle-tr");
        //console.log()
        selectionrowDetalle();
    });
}

function getdgvdetallecomisionHijo(idPadre) {
    $.ajax({
        type: "POST",
        url: "Main/Comisiones.aspx/GetDetallesMovimientos",
        data: '{rcontrol_transa: "' + idPadre + '" }',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: jMovimientoDetalle
    });
}

function jMovimientoDetalle(response) {
    $(".dgvdetallecomision_hijo").find("tbody tr:not(:has(thead))").remove();
    var obj = jQuery.parseJSON(response.d);
    $(".dgvdetallecomision_hijo").footable({
        "columns": $.Deferred(function (d) {
            setTimeout(function () {
                $.get("js/dgvdetallecomisionhijo_columns.json").then(d.resolve, d.reject);
            }, 900);
            setTimeout(function () {
                selectionrowPrint();
            }, 950);
        }),
        "rows": obj
    });
    updateBackground();
}

function selectionrowPrint() {
    $('.dgvdetallecomision_hijo').find('tbody tr:not(:has(th))').click(function () {
        var td = $(this).children('td');
        for (var i = 3; i < 4; ++i) {
            $('#txtobservacion').val(td[i].innerText);
        }
    });
}
