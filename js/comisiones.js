$(document).ready(function () {

    //$('.dgvcomisiones').hide();
    $('.opciones_dgvcomisiones').hide();

       
    $("#txtobservacion").attr('disabled', 'disabled');
    $("#txtobservacion").css('resize', 'none');
    
    $('input[type=text]').keypress(function (e) {
        var key = e.which;
        if (key == 13) {
            $('#btnbuscar').click();
        }

    }); 

    $('#btnbuscar').click(function () {

        if ($("#txtcip").val() != "" || $("#txtapepat").val() != "" || $("#txtapemat").val() != "" || $("#txtnombres").val() != "") {

            $.ajax({
                type: "POST",
                url: "Main/Comisiones.aspx/GetOficiales",
                data: '{maspe_carne: "' + $("#txtcip").val() + '", ape_pat: "' + $("#txtapepat").val() + '", ape_mat: "' + $("#txtapemat").val() + '", nombres: "' + $("#txtnombres").val() + '" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnSuccess
            });         
        }
      
    });

    
    function OnSuccess(response) {       

        $('.dgvcomisiones').find('tbody:not(:has(thead))').html('');
      
        limpiar();
            
        //$('.dgvcomisiones').show();
        $('.opciones_dgvcomisiones').show();
       
        var obj = jQuery.parseJSON(response.d);
        drawTable(obj);

        var columns =[
                        { "name": "CIP", "title": "CIP", "breakpoints": "xs sm", "style": { "width": 80, "maxWidth": 80 } },
                        { "name": "GRADO", "title": "GRADO", "style": { "width": 100, "maxWidth": 100 } },
                        { "name": "SITUACION", "title": "SITUACION", "style": { "width": 90, "maxWidth": 90} },
                        { "name": "APELLIDO_PATERNO", "title": "APELLIDO PATERNO", "style": { "width": 120, "maxWidth": 120 } },
                        { "name": "APELLIDO_MATERNO", "title": "APELLIDO MATERNO", "breakpoints": "xs sm", "style": { "width": 120, "maxWidth": 120, "overflow": "hidden", "textOverflow": "ellipsis", "wordBreak": "keep-all", "whiteSpace": "nowrap" } },
                        { "name": "NOMBRES", "title": "NOMBRES", "type": "date", "breakpoints": "xs sm md", "style": { "width": 150, "maxWidth": 150 } },
                        { "name": "COD_UNIDAD", "title": "UNIDAD", "breakpoints": "xs sm md" }
                    ]
  
        $('.dgvcomisiones').footable({
            "columns": columns,
            "rows": obj
        });


        selectionrow();

        $('.dgvcomisiones tfoot .footable-paging').click(function () {
            selectionrow();
        })

    }
         
    function drawTable(data) {
       
        var ncount = "";
            if (data.length > 1)
            {
                ncount = " Registros"
            }else{
                ncount = " Registro"
            }

            $('#countreg span').text(data.length + ncount)
            $('#countreg span').fadeIn(500);            
        }
      
  
 
    function selectionrow() {
        $('.dgvcomisiones').find('tbody tr:not(:has(th))').click(function () {
            var td = $(this).children('td');
            for (var i = 0; i < 1; ++i) {
                $('#hdcip').val(td[i].innerText);
                //console.log(i + ': ' + td[i].innerText);
            }

            $(".background-tr").removeClass("background-tr");

            $(this).children("td").addClass("background-tr");
        });
    }

 
    /*Limpiamos los textbox*/
    function limpiar() {
        $('input[type=text]').val("");
    }


    /************************* 
        Detalle de Movimientos 
    **************************/

    $('#btndetalle').click(function () {
        if ($('#hdcip').val() != "") {
            $('#md_detalle').modal('show');
          

            selectionrow_print();

            var valor = $('#hdcip').val();
            var img = $('.img-profile');
            img.attr("src", "images/FOTOS_PRUEBAS/" + valor + ".jpg");


            $.ajax({
                type: "POST",
                url: "Main/Comisiones.aspx/GetMovimientos",
                data: '{maspe_carne: "' + $("#hdcip").val() + '" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: jMovimiento
            });
            
        } else {
            toastr.success('Seleccione un registro ...', 'Comisiones');
        }

    });


    function jMovimiento(response) {
        $('.dgvdetallecomision').find('tr:not(:has(thead))').remove();      

        var obj = jQuery.parseJSON(response.d);

        var columns = [
                   { "name": "RCONTROL_TRANSA", "title": "RCONTROL_TRANSA", "style": { "width": 80, "maxWidth": 80 } ,"visible" : false},
                   { "name": "Documento", "title": "Documento", "style": { "width": 250, "maxWidth": 250 } },
                   { "name": "Inicio", "title": "Inicio","type" : "date", "style": { "width": 90, "maxWidth": 90 } },
                   { "name": "Termino", "title": "Termino", "type": "date", "style": { "width": 90, "maxWidth": 90 } },
                   { "name": "Motivo", "title": "Motivo", "style": { "width": 90, "maxWidth": 90, "overflow": "hidden", "textOverflow": "ellipsis", "wordBreak": "keep-all", "whiteSpace": "nowrap" } },
                   { "name": "Tip_Control", "title": "Tipo Control", "style": { "width": 100, "maxWidth": 100 } },
                   { "name": "Destino", "title": "Destino" }
        ]

        $('.dgvdetallecomision').footable({
            "columns": columns,
            "rows": obj
        });
       

        $('.dgvdetallecomision_hijo').find('tbody tr:not(:has(thead))').remove();

  
        selectionrow_detalle();

        $('.dgvdetallecomision tfoot .footable-paging').click(function () {
            selectionrow_detalle();
        })


    }

    function selectionrow_detalle() {
        $('.dgvdetallecomision').find('tbody tr:not(:has(th))').click(function () {
            var td = $(this).children('td');
            for (var i = 0; i < 1; ++i) {
                $('#nro_transa').val(td[i].innerText);
                //console.log(i + ': ' + td[i].innerText);
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
                //console.log(i + ': ' + td[i].innerText);
                $('#txtobservacion').val(td[i].innerText);
            }
        });
    }  



    /** Limpiar variable  .img-profile **/
    $('[data-dismiss]').click(function () {
        $('.img-profile').attr("src", "");
        $('#txtobservacion').val("");
    });


 
    
});


window.onresize = function(event) {
    data_debug.data.self.breakpoints.check();
    };