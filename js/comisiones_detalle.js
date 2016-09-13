$(document).ready(function (funcion) {

  

    /************************* 
        Detalle de Movimientos 
    **************************/
       
    $("#txtobservacion").attr("disabled", "disabled");
    $("#txtobservacion").css("resize", "none");
    
    selectionrow_print();

    var valor = $("#hdcip").val();
    var img = $(".img-profile");
    var ruta_img = valor.length;

    $.ajax({
        type: "POST",
        url: "Main/Comisiones.aspx/GetMovimientos",
        data: '{maspe_carne: "'   + valor + '" }',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: jMovimiento
    });

      
   
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
        var tabla = $('.dgvdetallecomision').find('tbody tr:not(:has(th))');
        var tabla_td = tabla.children('td');
      
        $(".dgvdetallecomision_hijo").find("tbody tr:not(:has(thead))").remove();

        selectionrow_detalle();

        $(".dgvdetallecomision tfoot .footable-paging")
            .click(function () {
                selectionrow_detalle();
            });


        Getdgvdetallecomision_hijo(tabla_td[0].innerText);

    }

    function selectionrow_detalle() {
        $('.dgvdetallecomision').find('tbody tr:not(:has(th))').click(function () {
            var td = $(this).children('td');
            for (var i = 0; i < 1; ++i) {
                $('#nro_transa').val(td[i].innerText);
            }
            $(".background-detalle-tr").removeClass("background-detalle-tr");

            $(this).children("td").addClass("background-detalle-tr");
            
            $('#txtobservacion').val("");
            Getdgvdetallecomision_hijo($("#nro_transa").val());
        });

        
    }

    function Getdgvdetallecomision_hijo(idPadre) {        

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
        $('.dgvdetallecomision_hijo').find('tbody tr:not(:has(thead))').remove();
        var obj = jQuery.parseJSON(response.d);
        $(".dgvdetallecomision_hijo").footable({
            "columns": $.Deferred(function (d) {
                setTimeout(function () {
                    $.get("js/dgvdetallecomisionhijo_columns.json").then(d.resolve, d.reject);
                }, 1000);
                setTimeout(function () {
                    selectionrow_print();
                }, 1500);
            }),
            "rows": obj
        });
    }


    function selectionrow_print() {
        $('.dgvdetallecomision_hijo').find('tbody tr:not(:has(th))').click(function () {
            var td = $(this).children('td');
            for (var i = 3; i < 4; ++i) {
                $('#txtobservacion').val(td[i].innerText);
            }
        });
    }  


    $('#btnreportedetallado').click(function () {
        $("#md_referencia .modal-content").load("Reportes/RptReferencia.aspx?rcontrol_trans=" + $("#nro_transa").val());
        $('#md_referencia').modal("show");
        
    });


    /* Obtengo valor del cip y extraigo el ultimo caracter para obtener la ruta de la foto*/
    ruta_img = valor.charAt(ruta_img - 1);

    if (valor.substr(0, 2) === "00") {
        var nvalor = valor.replace("00", "");
        ruta_img = nvalor.length;
        ruta_img = nvalor.charAt(ruta_img - 1);
        var img_url = "http://172.16.1.13/fotostitular/" + ruta_img + "/" + nvalor + ".jpg";
    } else {
        var img_url = "http://172.16.1.13/fotostitular/" + ruta_img + "/" + valor + ".jpg";
    }


    var default_url = "images/sin_foto.png";

    img.error(function () {
        $(this).attr("src", default_url);
    });
    img.attr("src", img_url);





    $('#btnadicionar').click(function () {
        $("#md_registro .modal-content").load('Main/Comisiones_registro.aspx?xtipreg=xReg');
        $('#md_registro').modal('show');
    });


    /** Limpiar variable  .img-profile **/
    $('[data-dismiss]').click(function () {
        $(".img-profile").attr("src", "");
        $("#txtobservacion").val("");
        $("#hdcip").val("");
    });

    /* Movilidad al Modal*/
    $("#md_detalle").draggable({
        handle: ".modal-header"
    });

  

});


