$(document).ready(function () {

    var mov;
   

    
    $(".opciones_dgvcomisiones").hide();

       
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

        /* Limpia la variable */
        $('#hdcip').val("");

    });

    
    function OnSuccess(response) {       

        $('.dgvcomisiones').find('tbody:not(:has(thead))').html('');
      
        limpiar();
            
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
                        { "name": "COD_UNIDAD", "title": "UNIDAD", "breakpoints": "xs sm md" },
                        { "name": "MOVIMIENTO", "title": "MOV.", "breakpoints": "xs sm md", "style": { "width": 50, "maxWidth": 50 } }
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
        $(".dgvcomisiones").find("tbody tr:not(:has(th))").click(function () {
            var td = $(this).children("td");
            for (var i = 0; i < 8; ++i) {
                $("#hdcip").val(td[0].innerText);

                mov = td[7].innerText
                cell = td[0].innerText + " - " + td[1].innerText + " - " + td[3].innerText + " " + td[4].innerText + " " + td[5].innerText

                //console.log(i + ': ' + td[i].innerText);
            }

            $(".background-tr").removeClass("background-tr");

            $(this).children("td").addClass("background-tr");
            
        }).dblclick(function () {
            $('#btndetalle').click();;
        });

    };
 
    /*Limpiamos los textbox*/
    function limpiar() {
        $('input[type=text]').val("");
    }


    /************************* 
        Detalle de Movimientos 
    **************************/

  


    $('#btndetalle').click(function () {
        if ($('#hdcip').val() != "" && $('#hdcip').val() != "No hay resultados") {
            if (mov != 0) {
                $("#md_detalle .modal-content").load("Main/Comisiones_detalle.aspx?maspe_carne=" + $("#hdcip").val());
                $('#md_detalle').modal('show');
            } else {
                 toastr.options = {
                    "closeButton": true,
                    "positionClass": "toast-bottom-full-width"
                }
                 toastr.error('No tiene movimientos. <button id="btnagregart" type="button" class="btn btn-default btn-sm">Generar</button>', 'Comisiones');

                 $('#btnagregart').click(function () {
                     $("#md_registro .modal-content").load('Main/Comisiones_registro.aspx');
                     $('#md_registro').modal('show');
                 });
            }            
        } else {
            toastr.success('Seleccione un registro ...', 'Comisiones');
        }
    });

  

});


window.onresize = function(event) {
    data_debug.data.self.breakpoints.check();
    };