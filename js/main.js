$(document).ready(function () {   
    $('#lbntcomi').click(function () {      
        $(".row .content").load('Main/Comisiones.aspx');
    })

    $('#lbntvaca').click(function () {      
        $(".row .content").load('Main/Vacaciones.aspx');
    })

    $('#lbntperm').click(function () {       
        $(".row .content").load('Main/Permisos.aspx');
    })

    $('#lbntlice').click(function () {       
        $(".row .content").load('Main/Licencias.aspx');
    })  
});

