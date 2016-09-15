
$(document).ready(function () {

    var valor = $("#hdcip").val();
    var dataArray;

    $("#data_fechadoc .input-group.date")
        .datepicker({
            todayBtn: "linked",
            keyboardNavigation: false,
            forceParse: false,
            calendarWeeks: true,
            autoclose: true
        });

    $("#btnimprimir_reporte").hide();

    $("#txtfechareferencia").attr("disabled", "disabled");

    $("#btnguardar_referencia")
        .click(function() {
            validarRpt();

        });


    function validarRpt() {
        if ($("#txtfechareferencia").val() === "") {
            alert("Seleccione Fecha");
        }
        if ($("#txtdocreferencia").val() === "") {
            alert("Ingrese Documento");
            $("#txtdocreferencia").focus();
        } else {

            $.ajax({
                type: "POST",
                url: "Reportes/RptReferencia.aspx/SetDocumentoReferencia",
                data: '{comision_id: "' + $("#txtrcontrol_transa").text() + '",fecha_referencia: "' + $("#txtfechareferencia").val() + '",documento_referencia: "' + $("#txtdocreferencia").val() + '" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    $.each(response, function (index, el) {
                        dataArray = el.split(",");
                        alert(dataArray[0]); 
                    });

                    $(".input-group.date").datepicker("remove");
                    $("#txtdocreferencia").attr("disabled", "disabled");

                    $("#btnimprimir_reporte").show();
                    $("#btnguardar_referencia").hide();
                }

            });

         

        }
    }

    $("#btnimprimir_reporte")
        .click(function() {

            $.ajax({
                type: "POST",
                url: "Main/Comisiones_detalle.aspx/SetVariablesRpt",
                data: '{maspe_carne: "' +valor +'",nro_transa: "' +$("#nro_transa").val() +'",id_referencia: ' +dataArray[1] +" }",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function(response) {
                    $("#mdrptcontent .modal-content").load("Reportes/RptContent.aspx");
                    $("#mdrptcontent").modal("show");
                }
            });

        });



    $("#md_referencia")
        .on("hidden.bs.modal",
            function() {
                if ($(".modal:visible").length) {
                    $("body").addClass("modal-open");
                }
            });
})
