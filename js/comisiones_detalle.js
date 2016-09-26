/**
 * DETALLE DE MOVIMIENTOS
 * @description [description]
 * @author [author] <>
 * @copyright [description]
 * @version [version]
 */

/** @type {String} [Valor del CIP seleccionado o buscado] */
var valor = $("#hdcip").val();
var defaultUrl = "images/sin_foto.png";
var imgUrl;

/**
 * Funciones Principales
 * @type {Object}
 */
var comDet = {
    jMovimientoDetalle: function (response) {
        $(".dgvdetallecomision_hijo").find("tbody tr:not(:has(thead))").remove();
        var obj = jQuery.parseJSON(response.d);
        $(".dgvdetallecomision_hijo").footable({
            "columns": $.Deferred(function (d) {
                setTimeout(function () {
                    $.get("js/dgvdetallecomisionhijo_columns.json").then(d.resolve, d.reject);
                }, 900);
                setTimeout(function () {
                    comDet.selectionrowPrint();
                }, 950);
            }),
            "rows": obj
        });
        comDet.updateBackground();
    },
    getdgvdetallecomisionHijo: function (idPadre) {
        $.ajax({
            type: "POST",
            url: "Main/Comisiones.aspx/GetDetallesMovimientos",
            data: '{rcontrol_transa: "' + idPadre + '" }',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: comDet.jMovimientoDetalle
        });
    },
    /**
     * Para obtener el valor de la fila seleccionada
     */
    selectionrowDetalle: function () {
        $(".dgvdetallecomision").find("tbody tr:not(:has(th))").off('click');
        $(".dgvdetallecomision").find("tbody tr:not(:has(th))").click(function () {
            var td = $(this).children("td");
            var nroTransa = "";
            for (var i = 0; i < 1; ++i) {
                nroTransa = td[i].innerText;
                $("#nro_transa").val(nroTransa);
            }
            $(".background-detalle-tr").removeClass("background-detalle-tr");
            $(this).children("td").addClass("background-detalle-tr");
            $("#txtobservacion").val("");
            comDet.getdgvdetallecomisionHijo(nroTransa);
        });
    },
    /**
     * Recuperar Datos de Oficial Seleccionado
     */
    getFunciones: function () {
        tabla = $(".dgvdetallecomision").find("tbody tr:not(:has(th))");
        tablaTd = tabla.children("td");
        $(".dgvdetallecomision_hijo").find("tr:not(:has(thead))").remove();
        comDet.selectionrowDetalle();
        comDet.getdgvdetallecomisionHijo(tablaTd[0].innerText);
    },
    jMovimiento: function (response) {
        $(".dgvdetallecomision").find("tr:not(:has(thead))").remove();
        var obj = jQuery.parseJSON(response.d);
        $(".dgvdetallecomision").footable({
            "columns": $.Deferred(function (d) {
                setTimeout(function () {
                    $.get("js/dgvdetallecomision_columns.json").then(d.resolve, d.reject);
                }, 1000);
                setTimeout(function () {
                    comDet.getFunciones();
                }, 1500);
            }),
            "rows": obj
        });
    },
    getMovimientos: function () {
        $.ajax({
            type: "POST",
            url: "Main/Comisiones.aspx/GetMovimientos",
            data: '{maspe_carne: "' + valor + '" }',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: comDet.jMovimiento
        });
    },
    updateBackground: function () {
        $(".dgvdetallecomision tfoot .footable-paging").click(function () {
            $(".background-detalle-tr").removeClass("background-detalle-tr");
            comDet.selectionrowDetalle();
        });
    },
    selectionrowPrint: function () {
        $('.dgvdetallecomision_hijo').find('tbody tr:not(:has(th))').click(function () {
            var td = $(this).children('td');
            for (var i = 3; i < 4; ++i) {
                $('#txtobservacion').val(td[i].innerText);
            }
        });
    }
}

$(document).ready(function (funcion) {

    $("#txtobservacion").attr("disabled", "disabled");
    $("#txtobservacion").css("resize", "none");
    $(".dgvdetallecomision_hijo").find("tbody tr:not(:has(thead))").remove();

    comDet.getMovimientos();

    var valor = $("#hdcip").val();
    var img = $(".img-profile");
    var rutaImg = valor.length;

    /** @type {String} [Obtengo valor del cip y extraigo el ultimo caracter para obtener la ruta de la foto] */
    rutaImg = valor.charAt(rutaImg - 1);

    toastr.options = {
        "closeButton": true,
        "positionClass": "toast-bottom-full-width"
    };
    $("#btnreportedetallado").click(function () {
        if ($("#nro_transa").val() === "") {
            toastr.error("Seleccione un registro", "Comisiones");
        } else {
            $("#md_referencia .modal-content")
            .load("Reportes/RptReferencia.aspx?rcontrol_trans=" + $("#nro_transa").val());
            $("#md_referencia").modal("show");
        }
    });
    $("#btnaddmov").click(function () {
        if ($("#nro_transa").val() === "") {
            toastr.error("Seleccione un registro", "Comisiones");
        } else {
            $("#md_referencia .modal-content")
            .load("Main/Comisiones_movimiento.aspx?rcontrol_trans=" + $("#nro_transa").val());
            $("#md_referencia").modal("show");
        }
    });

    if (valor.substr(0, 2) === "00") {
        var nvalor = valor.replace("00", "");
        rutaImg = nvalor.length;
        rutaImg = nvalor.charAt(rutaImg - 1);
        imgUrl = "http://172.16.1.13/fotostitular/" + rutaImg + "/" + nvalor + ".jpg";
    } else {
        imgUrl = "http://172.16.1.13/fotostitular/" + rutaImg + "/" + valor + ".jpg";
    }

    img.error(function () {
        $(this).attr("src", defaultUrl);
    });
    img.attr("src", imgUrl);

    $("#btnadicionar").click(function () {
        $("#md_registro .modal-content").load("Main/Comisiones_registro.aspx?xtipreg=xReg");
        $("#md_registro").modal("show");
    });

    /** [Limpiar variable  .img-profile] */
    $("[data-dismiss]").click(function () {
        $(".img-profile").attr("src", "");
        $("#txtobservacion").val("");
        $("#hdcip").val("");
        $(".background-tr").removeClass("background-tr");
        $(".dgvdetallecomision").find("tr:not(:has(thead))").remove();
        $(".dgvdetallecomision_hijo").find("tr:not(:has(thead))").remove();
    });

    /** @type {String} [Movilidad al Modal] */
    $("#md_detalle").draggable({
        handle: ".modal-header"
    });

});