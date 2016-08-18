$(document).ready(function (){

    $('#data_1 .input-group.date').datepicker({
        todayBtn: "linked",
        keyboardNavigation: false,
        forceParse: false,
        calendarWeeks: true,
        autoclose: true
    });

    $('#data_2 .input-group.date').datepicker({
        todayBtn: "linked",
        keyboardNavigation: false,
        forceParse: false,
        calendarWeeks: true,
        autoclose: true
    });

    $('#data_3 .input-group.date').datepicker({
        todayBtn: "linked",
        keyboardNavigation: false,
        forceParse: false,
        calendarWeeks: true,
        autoclose: true
    });

    $(".ibox").css('margin-bottom', '10px');

    $('#ddlpais').change(function () {
        if ($(this).val() != '0001') {
            $("#ddldepartamento").attr('disabled', 'disabled');
            $("#ddlprovincia").attr('disabled', 'disabled');
            $("#ddlciudad").attr('disabled', 'disabled');
        } else {
            $("#ddldepartamento").removeAttr("disabled")
            $("#ddlprovincia").removeAttr("disabled");
            $("#ddlciudad").removeAttr("disabled")
        }

    })


    
    $('#ddldepartamento').change(function () {

        $("#ddlprovincia").empty();

        var valor = $(this).val();

        $.ajax({
            type: "POST",
            url: "Main/Comisiones_registro.aspx/GetProvincia",
            data: '{d: "' + valor + '" }',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                var obj = jQuery.parseJSON(response.d);
                for (var i = 0; i < obj.length; i++) {
                    var ddl = obj[i];
                   // console.log(ddl);
                    $("#ddlprovincia").append('<option value="' + ddl.P + '">' + ddl.VALOR + '</option>');
                }
            }

        })
    });



    $('#ddlprovincia').change(function () {
        $("#ddlciudad").empty();
        var d = $('#ddldepartamento').val();
        var p = $('#ddlprovincia').val();
        $.ajax({
            type: "POST",
            url: "Main/Comisiones_registro.aspx/GetCiudad",
            data: '{d: "' + d + '" ,p: "' + p + '"}',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                var obj = jQuery.parseJSON(response.d);
                for (var i = 0; i < obj.length; i++) {
                    var ddl = obj[i];
                    // console.log(ddl);
                    $("#ddlciudad").append('<option value="' + ddl.C + '">' + ddl.VALOR + '</option>');
                }
            }

        })
    });





    /* Movilidad al Modal*/
    $("#md_detalle").draggable({
        handle: ".modal-header"
    });

});