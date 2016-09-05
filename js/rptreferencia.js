
$(document).ready(function () {

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

            $(".input-group.date").datepicker("remove");
            $("#txtdocreferencia").attr("disabled", "disabled");

            $("#btnimprimir_reporte").show();
            $("#btnguardar_referencia").hide();

        }
    }


    $("#md_referencia")
        .on("hidden.bs.modal",
            function() {
                if ($(".modal:visible").length) {
                    $("body").addClass("modal-open");
                }
            });
})
