$(document).ready(function () {
    
    
    $(".registro").find(".modal-title").append(cell)

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
            $("#ddldepartamento").empty();
            $("#ddldepartamento").append('<option value="">DEPARTAMENTO</option>');

            $("#ddlprovincia").attr('disabled', 'disabled');
            $("#ddlprovincia").empty();
            $("#ddlprovincia").append('<option value="">PROVINCIA</option>');

            $("#ddlciudad").attr('disabled', 'disabled');
            $("#ddlciudad").empty();
            $("#ddlciudad").append('<option value="">CIUDAD</option>');
        } else {
            $("#ddldepartamento").empty();
            $("#ddldepartamento").removeAttr("disabled");
            $.ajax({
                type: "POST",
                url: "Main/Comisiones_registro.aspx/GetDepartamento",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    var obj = jQuery.parseJSON(response.d);
                    for (var i = 0; i < obj.length; i++) {
                        var ddl = obj[i];
                        $("#ddldepartamento").append('<option value="' + ddl.D + '">' + ddl.VALOR + '</option>');
                    }
                }

            })

            $("#ddlprovincia").removeAttr("disabled");
            $("#ddlciudad").removeAttr("disabled");
        }

    })


    
    $('#ddldepartamento').change(function () {

        $("#ddlprovincia").empty();

        $("#ddlciudad").empty();
        $("#ddlciudad").append('<option value="">CIUDAD</option>');

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