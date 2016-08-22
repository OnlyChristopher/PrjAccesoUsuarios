$(document).ready(function () {
    //var rpt_cip = $('#hdcip').val();
    //var rpt_transa = $('#nro_transa').val();

    //$('#maspe_carne_h').val(rpt_cip);

    //$('#rcontrol_transa_h').val(rpt_transa);


    var pdf_link = "Reportes/Reporte.pdf";
        //var iframe = '<div class="iframe-container"><iframe src="'+pdf_link+'"></iframe></div>'
        //var iframe = '<object data="'+pdf_link+'" type="application/pdf"><embed src="'+pdf_link+'" type="application/pdf" /></object>'        
    $('#mdrptcontent .modal-body').append('<object type="application/pdf" data="' + pdf_link + '" width="100%" height="700">No Support</object>');
    
           
  


});