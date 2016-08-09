$(document).ready(function () {


    /************************* 
        Detalle de Movimientos 
    **************************/
       
    $("#txtobservacion").attr('disabled', 'disabled');
    $("#txtobservacion").css('resize', 'none');
    
     selectionrow_print();

            var valor = $('#hdcip').val();
            var img = $('.img-profile');      
           
       
            var default_url = "images/sin_foto.png";
            var img_url = "images/FOTOS_PRUEBAS/" + valor + ".jpg";

            img.error(function () {
                $(this).attr('src', default_url)
            });
            img.attr('src', img_url);

       
            $.ajax({
                type: "POST",
                url: "Main/Comisiones.aspx/GetMovimientos",
                data: '{maspe_carne: "' + valor + '" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: jMovimiento
            });
            
   
    function jMovimiento(response) {
        $('.dgvdetallecomision').find('tr:not(:has(thead))').remove();      

        var obj = jQuery.parseJSON(response.d);

        var columns = [
                   { "name": "RCONTROL_TRANSA", "title": "RCONTROL_TRANSA", "style": { "width": 80, "maxWidth": 80 } ,"visible" : false},
                   { "name": "Documento", "title": "Documento", "style": { "width": 250, "maxWidth": 250 } },
                   { "name": "Inicio", "title": "Inicio","type" : "date", "style": { "width": 90, "maxWidth": 90 } },
                   { "name": "Termino", "title": "Termino", "type": "date", "style": { "width": 90, "maxWidth": 90 } },
                   { "name": "Motivo", "title": "Motivo", "style": { "width": 100, "maxWidth": 100, "overflow": "hidden", "textOverflow": "ellipsis", "wordBreak": "keep-all", "whiteSpace": "nowrap" } },
                   { "name": "Tip_Control", "title": "Tipo Control", "style": { "width": 100, "maxWidth": 100 } },
                   { "name": "Destino", "title": "Destino" },
                   { "name": "Datos", "title": "Datos","visible" : false }
        ]

        $('.dgvdetallecomision').footable({
            "columns": columns,
            "rows": obj
        });
       
        /* Recuperar Datos de Oficial Seleccionado */
        var tabla = $('.dgvdetallecomision').find('tbody tr:not(:has(th))')
        var tabla_td = tabla.children('td');
        var data = tabla_td[7].innerText;
        var data_array = data.split(',');
            $('#lbldatos1').text(data_array[0]);
            $('#lbldatos2').text(data_array[1]);
            $('#lbldatos3').text(data_array[2]);
            $('#lbldatos4').text(data_array[3]);
            $('#lbldatos5').text(data_array[4]);
            $('#lbldatos6').text(data_array[5]);


        $('.dgvdetallecomision_hijo').find('tbody tr:not(:has(thead))').remove();
  
        selectionrow_detalle();

        $('.dgvdetallecomision tfoot .footable-paging').click(function () {
            selectionrow_detalle();
        })


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
            Getdgvdetallecomision_hijo($('#nro_transa').val());
        });

        
    }

    function Getdgvdetallecomision_hijo(valor) {        

        $.ajax({
            type: "POST",
            url: "Main/Comisiones.aspx/GetDetallesMovimientos",
            data: '{rcontrol_transa: "' + valor + '" }',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: jMovimientoDetalle
        });
    }




    function jMovimientoDetalle(response) {
        $('.dgvdetallecomision_hijo').find('tbody tr:not(:has(thead))').remove();

        var obj = jQuery.parseJSON(response.d);
        
        var columns = [
                 { "name": "Fecha_Hora", "title": "Fecha y Hora", "style": { "width": 100, "maxWidth": 100 } },
                 { "name": "Tipo_Registro", "title": "Tipo Registro", "style": { "width": 100, "maxWidth": 100 } },
                 { "name": "Tipo_Control", "title": "Tipo Control", "type": "date", "style": { "width": 100, "maxWidth": 100 } },
                 { "name": "Observacion", "title": "Observacion", "type": "date", "style": { "width": 90, "maxWidth": 90 }, "visible": false },
                 { "name": "Operador", "title": "Operador", "type": "date", "style": { "width": 100, "maxWidth": 100 } }

        ]

        $('.dgvdetallecomision_hijo').footable({
            "columns": columns,
            "rows": obj
        });
        selectionrow_print()
    }


    function selectionrow_print() {
        $('.dgvdetallecomision_hijo').find('tbody tr:not(:has(th))').click(function () {
            var td = $(this).children('td');
            for (var i = 3; i < 4; ++i) {
                $('#txtobservacion').val(td[i].innerText);
            }
        });
    }  



    /** Limpiar variable  .img-profile **/
    $('[data-dismiss]').click(function () {
        $('.img-profile').attr("src", "");
        $('#txtobservacion').val("");
    });

    /* Movilidad al Modal*/
    $("#md_detalle").draggable({
        handle: ".modal-header"
    });

});


