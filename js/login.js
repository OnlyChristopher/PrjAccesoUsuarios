$(document).ready(function () {              
    limpiar();

    window.history.forward();

    $('.log-btn').click(function () {
        if ($("#txtusername").val() != "" && $("#txtpassword").val() != "") {
                $.ajax({
                type: "POST",
                url: "../Login.aspx/GetUsuario",
                data: '{user: "' + $("#txtusername").val() + '", pass: "' + $("#txtpassword").val() + '" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    if (response.d === "Error") 
                        {
                            validar();
                        }
                    else if (response.d === "Ingreso") {
                        //valor = response.d;
                        $(location).attr("href", "Main.aspx");
                    };
                },

            });
        } else {
                
            if ($("#txtusername").val() == "")
            {
                $("#ErrorUsuario").fadeIn(500);
                setTimeout("$('#ErrorUsuario').fadeOut(1500);", 3000);
            }

            if ($("#txtpassword").val() == "") {
                $("#ErrorClave").fadeIn(500);
                setTimeout("$('#ErrorClave').fadeOut(1500);", 3000);
            }
        }       
        
    });

    function limpiar() {
        /*Limpiamos los textbox*/
        $('#txtusername').val("");
        $('#txtpassword').val("");

        /*Focus en el textbox*/
        $('#txtusername').focus();
    }

    function validar() {
        limpiar();
        $('.log-status').addClass('wrong-entry');
        $('#ErrorDatos').fadeIn(500);
        setTimeout("$('#ErrorDatos').fadeOut(1500);", 3000);

        $('.form-control').keypress(function () {
            $('.log-status').removeClass('wrong-entry');
        });
    }

    
 
});
        
 
     
        
