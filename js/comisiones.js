$(document).ready(function () {

    $('.dgvcomisiones').hide();
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

        $('.dgvcomisiones').find('tr:not(:has(th))').remove();

        selectionrow();

        limpiar();
            
        $('.dgvcomisiones').show();
        $('.opciones_dgvcomisiones').show();

        var obj = jQuery.parseJSON(response.d);
        drawTable(obj);

        var columns =[
                        { "name": "CIP", "title": "CIP", "breakpoints": "xs sm", "style": { "width": 80, "maxWidth": 80 } },
                        { "name": "GRADO", "title": "GRADO" },
                        { "name": "SITUACION", "title": "SITUACION" },
                        { "name": "APELLIDO_PATERNO", "title": "APELLIDO PATERNO" },
                        { "name": "APELLIDO_MATERNO", "title": "APELLIDO MATERNO", "breakpoints": "xs sm", "style": { "maxWidth": 200, "overflow": "hidden", "textOverflow": "ellipsis", "wordBreak": "keep-all", "whiteSpace": "nowrap" } },
                        { "name": "NOMBRES", "title": "NOMBRES", "type": "date", "breakpoints": "xs sm md"},
                        { "name": "COD_UNIDAD", "title": "UNIDAD", "breakpoints": "xs sm md" }
                    ]
      
        $('.dgvcomisiones').footable({
            "columns": columns,
            "rows": obj
        });
    }

        
    function drawTable(data) {
        for (var i = 0; i < data.length; i++) {
                drawRow(data[i]);
        }
        var ncount = "";
            if (data.length > 1)
            {
                ncount = " Registros"
            }else{
                ncount = " Registro"
            }

            $('#countreg span').text(data.length + ncount)
            $('#countreg span').fadeIn(500);

            selectionrow();
        }

    function drawRow(rowData) {           
            var row = $("<tr />")
            $("#dgvcomisiones").append(row); //this will append tr element to table... keep its reference for a while since we will add cels into it
            row.append($("<td>" + rowData.CIP + "</td>"));
            row.append($("<td>" + rowData.GRADO + "</td>"));
            row.append($("<td>" + rowData.SITUACION + "</td>"));
            row.append($("<td>" + rowData.APELLIDO_PATERNO + "</td>"));
            row.append($("<td>" + rowData.APELLIDO_MATERNO + "</td>"));
            row.append($("<td>" + rowData.NOMBRES + "</td>"));
            row.append($("<td>" + rowData.COD_UNIDAD + "</td>"));
    }

 
    function selectionrow() {
        $('.dgvcomisiones').find('tr:not(:has(th))').click(function () {
            var td = $(this).children('td');
            for (var i = 0; i < 1; ++i) {
                $('#hdcip').val(td[i].innerText);
                //console.log(i + ': ' + td[i].innerText);
            }

            $(".background-tr").removeClass("background-tr");

            $(this).children("td").addClass("background-tr");
            $(this).children("tr").addClass("background-tr");
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
        $('.dgvdetallecomision').find('tr:not(:has(th))').remove();      

        var obj = jQuery.parseJSON(response.d);       

        for (var i = 0; i < obj.length; i++) {

            var rowData = obj[i];    

        var row = $("<tr />")
            $(".dgvdetallecomision").append(row); //this will append tr element to table... keep its reference for a while since we will add cels into it
            row.append($("<td>" + rowData.RCONTROL_TRANSA + "</td>"));
            row.append($("<td>" + rowData.Documento + "</td>"));
            row.append($("<td>" + rowData.Inicio + "</td>"));
            row.append($("<td>" + rowData.Termino + "</td>"));
            row.append($("<td>" + rowData.Motivo + "</td>"));
            row.append($("<td>" + rowData.Tip_Control + "</td>"));
            row.append($("<td>" + rowData.Destino + "</td>"));
        }

        $('.dgvdetallecomision_hijo').find('tr:not(:has(th))').remove();

        ocultarcolumna('dgvdetallecomision', 1);
        ocultarcolumna('dgvdetallecomision_hijo', 4);

        selectionrow_detalle();
    }

    function selectionrow_detalle() {
        $('.dgvdetallecomision').find('tr:not(:has(th))').click(function () {
            var td = $(this).children('td');
            for (var i = 0; i < 1; ++i) {
                $('#nro_transa').val(td[i].innerText);
                //console.log(i + ': ' + td[i].innerText);
            }

            $(".background-detalle-tr").removeClass("background-detalle-tr");

            $(this).children("td").addClass("background-detalle-tr");
            $(this).children("tr").addClass("background-detalle-tr");

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
        $('.dgvdetallecomision_hijo').find('tr:not(:has(th))').remove();

        var obj = jQuery.parseJSON(response.d);

        for (var i = 0; i < obj.length; i++) {

            var rowData = obj[i];

            var row = $("<tr />")
            $(".dgvdetallecomision_hijo").append(row); //this will append tr element to table... keep its reference for a while since we will add cels into it
            row.append($("<td>" + rowData.Fecha_Hora + "</td>"));
            row.append($("<td>" + rowData.Tipo_Registro + "</td>"));
            row.append($("<td>" + rowData.Tipo_Control + "</td>"));
            row.append($("<td>" + rowData.Observacion + "</td>"));
        }

        selectionrow_print()
        ocultarcolumna('dgvdetallecomision_hijo', 4);

    }




    function selectionrow_print() {
        $('#dgvdetallecomision_hijo').find('tr:not(:has(th))').click(function () {
            var td = $(this).children('td');
            for (var i = 3; i < 4; ++i) {
                //console.log(i + ': ' + td[i].innerText);
                $('#txtobservacion').val(td[i].innerText);
            }
        });
    }

  
    function ocultarcolumna(grilla, columna) {
        var i = columna;
        $('#' + grilla + '').find('td:nth-child(' + [i] + '),th:nth-child(' + [i] + ')').hide();
        //console.log($('#' + grilla + '').find('td:nth-child(' + [i] + '),th:nth-child(' + [i] + ')'));
    }


    /** Limpiar variable  .img-profile **/
    $('[data-dismiss]').click(function () {
        $('.img-profile').attr("src", "");
    });


  
    
});