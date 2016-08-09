$(document).ready(function (){

    $('#data_1 .input-group.date').datepicker({
        todayBtn: "linked",
        keyboardNavigation: false,
        forceParse: false,
        calendarWeeks: true,
        autoclose: true
    });


    /* Movilidad al Modal*/
    $("#md_detalle").draggable({
        handle: ".modal-header"
    });

});