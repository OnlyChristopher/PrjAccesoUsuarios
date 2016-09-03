
$(document).ready(function () {
    $('#data_fechadoc .input-group.date').datepicker({
        todayBtn: "linked",
        keyboardNavigation: false,
        forceParse: false,
        calendarWeeks: true,
        autoclose: true
    });

    $('#md_referencia').on('hidden.bs.modal', function () {
        if ($('.modal:visible').length) {
            $('body').addClass('modal-open');
        }
    });
})
