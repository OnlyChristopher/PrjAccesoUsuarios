$(document).ready(function () {


    $("#txtfechamovimiento").datetimepicker({
        locale: "es",
        format: "DD/MM/YYYY hh:mm:ss",
        defaultDate: new Date()
    });

    $("#txtfechamovimiento").attr("disabled", "disabled");

    $("select").prop("required", true);

    $("#btnguardar_comisiondetalle")
        .click(function () {
            toastr.options = {
                "closeButton": true,
                "positionClass": "toast-bottom-full-width"
            }
            if (comprobarCamposRequired("frmmovimientos")) {
                $.ajax({
                    type: "POST",
                    url: "Main/Comisiones_movimiento.aspx/SetComisionDetalle",
                    data: '{fecha_origen: "' +
                        $("#txtfechamovimiento").val() +
                        '", ttip1control: "' +
                        $("#ddltiporeg").val() +
                        '", tipo_control: "' +
                        $("#ddltipocon").val() +
                        '", observacion: "' +
                        $("#txtobservacionmovimiento").val() +
                        '" }',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function(response) {
                        toastr.success(response.d, "Comisiones");
                        $("#btnguardar_comisiondetalle").hide();
                        getDgvdetallecomision();
                    }
                });
            } else {
                toastr.error("Rellene todos los campos ", "Comisiones");
            }

        });

    function getDgvdetallecomision() {
        getMovimientos();
        console.log("Recarga");
    };

    $("#md_referencia")
        .on("hidden.bs.modal",
            function() {
                if ($(".modal:visible").length) {
                    $("body").addClass("modal-open");
                }
            });
});