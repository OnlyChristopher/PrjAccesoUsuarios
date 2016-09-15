$(document).ready(function () {

  

    $(".registro").find(".modal-title").append(cell);
    $("#cdocumento").hide();

    $("select").prop("required", true);

    $("#data_1 .input-group.date").datepicker({
        todayBtn: "linked",
        keyboardNavigation: false,
        forceParse: false,
        calendarWeeks: true,
        autoclose: true
    });

    $("#data_2 .input-group.date").datepicker({
        todayBtn: "linked",
        keyboardNavigation: false,
        forceParse: false,
        calendarWeeks: true,
        autoclose: true
    });

    $("#data_3 .input-group.date").datepicker({
        todayBtn: "linked",
        keyboardNavigation: false,
        forceParse: false,
        calendarWeeks: true, 
        autoclose: true
    });

    $(".ibox").css("margin-bottom", "10px");


    $("#btnguardar_comireg")
        .click(function() {
            toastr.options = {
                "closeButton": true,
                "positionClass": "toast-bottom-full-width"
            }
            if (comprobarCamposRequired()) {
                $.ajax({
                    type: "POST",
                    url: "Main/Comisiones_registro.aspx/SetComision",
                    data: '{maspe_carne: "' +
                        $("#hdcip").val() +
                        '", ttidocu: "' +
                        $("#ddltipodoc").val() +
                        '", nro_doc: "' +
                        $("#txtnrodoc").val() +
                        '", siglas: "' +
                        $("#txtsiglasdoc").val() +
                        '", fecha_doc: "' +
                        $("#txtfechadoc").val() +
                        '", tipo_control: "' +
                        $("#ddltipcon").val() +
                        '", control_doc: "' +
                        $("#txtcdocumento").val() +
                        '", tipo_movimiento: "' +
                        $("#ddltipomov").val() +
                        '", observacion: "' +
                        $("#txtobservacionregistro").val() +
                        '", fecha_inicio: "' +
                        $("#txtfechaini").val() +
                        '", fecha_termino: "' +
                        $("#txtfechafin").val() +
                        '", ubigeo_d: "' +
                        $("#ddldepartamento").val() +
                        $("#ddlprovincia").val() +
                        $("#ddlciudad").val() +
                        '", tpais_cod: "' +
                        $("#ddlpais").val() +
                        '" }',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function(response) {
                        toastr.success(response.d, "Comisiones");
                        $("#btnguardar_comireg").hide();
                    }
                });
            } else {
                toastr.error("Rellene todos los campos ...", "Comisiones");
            }


        });

    function comprobarCamposRequired() {
        var correcto = true;
        var text   = $('input[type="text"]:required');
        var number = $('input[type="number"]:required');
        var select = $("select:required");
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

    $('input[type="text"]:required').click(function () {
        $(this).removeClass("error");
    });

    $('input[type="number"]:required').click(function () {
        $(this).removeClass("error");
    });

    $("select:required").click(function () {
        $(this).removeClass("error");
    });

    $("#ddltipcon")
        .change(function() {
            if ($(this).val() === "2") {
                $("#cdocumento").show();
            } else {
                $("#cdocumento").hide();
            }
        });


    $("#ddlpais")
        .change(function() {
            if ($(this).val() != "0001") {
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
                    success: function(response) {
                        var obj = jQuery.parseJSON(response.d);
                        for (var i = 0; i < obj.length; i++) {
                            var ddl = obj[i];
                            $("#ddldepartamento").append('<option value="' + ddl.D + '">' + ddl.VALOR + '</option>');
                        }
                    }

                });

                $("#ddlprovincia").removeAttr("disabled");
                $("#ddlciudad").removeAttr("disabled");
            }

        });


    
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
            success: function(response) {
                var obj = jQuery.parseJSON(response.d);
                for (var i = 0; i < obj.length; i++) {
                    var ddl = obj[i];
                    // console.log(ddl);
                    $("#ddlprovincia").append('<option value="' + ddl.P + '">' + ddl.VALOR + '</option>');
                }
            }

        });
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
            success: function(response) {
                var obj = jQuery.parseJSON(response.d);
                for (var i = 0; i < obj.length; i++) {
                    var ddl = obj[i];
                    // console.log(ddl);
                    $("#ddlciudad").append('<option value="' + ddl.C + '">' + ddl.VALOR + '</option>');
                }
            }

        });
    });

    /* Movilidad al Modal*/
    $("#md_registro").draggable({
        handle: ".modal-header"
    });


    $('#md_registro').on('hidden.bs.modal', function () {
        if ($('.modal:visible').length) {
            $('body').addClass('modal-open');
        }
    });
});