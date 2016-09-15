<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Comisiones_registro.aspx.vb" Inherits="Main_Comisiones_registro" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
        <script type="text/javascript" src="js/plugins/datapicker/bootstrap-datepicker.js"></script>
        <script type="text/javascript" src="js/comisiones_registro.js"></script>
    <title></title>
    <style>
        .error{
   border: 1px solid rgba(215, 0, 0, 0.75);
   box-shadow:inset 0px 0px 2px 0px rgba(255, 0, 0, 0.75); 
}
    </style>
</head>
<body>
    <form id="form1" runat="server" autocomplete="off">
    <div>
            
    <div class="modal-header registro">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" id="myModalLabel">Registro - </h4>
                </div>
                <div class="modal-body">                 
     <div class="ibox float-e-margins">
                        <div class="ibox" style="border-bottom:4px solid #e7eaec">
                            <h4>Documento</h4>
                            
                        </div>
                        <div class="col-sm-16">
                                <div class="row">
                                                                                
                                    <div class="col-md-2" title="Tipo de Documento">
                                        <asp:DropDownList ID="ddltipodoc" CssClass="form-control m-b" runat="server"></asp:DropDownList>
                                    </div>            
                                    <div class="col-md-2">
                                        <input type="number" id="txtnrodoc" name="txtnrodoc" class="form-control" placeholder="Numero" required="" />
                                    </div> 
                                    <div class="col-md-2">
                                        <input type="text" id="txtsiglasdoc" name="txtsiglasdoc" class="form-control" placeholder="Siglas" required="" />
                                    </div>
                                     <div class="col-md-3">
                                    <div class="form-group" id="data_1">
                                        <div class="input-group date">
                                            <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                            <input type="text" class="form-control" id="txtfechadoc" placeholder="Fecha Doc." required=""/>
                                        </div>
                                    </div>
                                    </div>                                                          
                                </div>
                            </div>
                    </div>
                    <div class="ibox float-e-margins">
                        <div class="ibox" style="border-bottom:4px solid #e7eaec">
                            <h4>Detalle</h4>
                            
                        </div>
                        <div class="col-sm-16">
                                <div class="row">
                                    <div class="col-md-2">
                                        <asp:DropDownList ID="ddltipomov" CssClass="form-control m-b" runat="server"></asp:DropDownList>
                                    </div>                                              
                                    <div class="col-md-2">
                                        <asp:DropDownList ID="ddltipcon" CssClass="form-control m-b" runat="server"></asp:DropDownList>
                                    </div>
                                     <div id="cdocumento" class="col-md-2">
                                        <input type="text" id="txtcdocumento" name="txtcdocumento" class="form-control" placeholder="Detalle Documento" />
                                    </div>
                                     <div class="col-md-3">
                                   <div class="form-group" id="data_2">
                                        <div class="input-group date">
                                            <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                            <input type="text" class="form-control" id="txtfechaini" placeholder="Fecha Ini." required=""/>
                                        </div>
                                    </div>
                                         </div>
                                    <div class="col-md-3">
                                        <div class="form-group" id="data_3">
                                            <div class="input-group date">
                                                <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                <input type="text" class="form-control" id="txtfechafin" placeholder="Fecha Fin."/>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="checkbox i-checks">
                                        <label> <input type="checkbox" id="chksintermino" value=""/> <i></i> Hasta el termino</label>
                                    </div>                                 
                                </div>
                            </div>
                    </div>
                    <div class="ibox float-e-margins">
                        <div class="ibox" style="border-bottom:4px solid #e7eaec">
                            <h4>Destino</h4>
                            
                        </div>
                        <div class="col-sm-16">
                                <div class="row">                                            
                                    <div class="col-md-2">
                                        <asp:DropDownList ID="ddlpais" runat="server" CssClass="form-control m-b">
                                            <asp:ListItem Text = "PAIS" Value = ""></asp:ListItem>
                                        </asp:DropDownList>

                                    </div>            
                                    <div class="col-md-2">
                                        <asp:DropDownList ID="ddldepartamento" runat="server" CssClass="form-control m-b">
                                            <asp:ListItem Text = "DEPARTAMENTO" Value = ""></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>            
                                   
                                     <div class="col-md-2">
                                        <asp:DropDownList ID="ddlprovincia" runat="server" CssClass="form-control m-b">
                                            <asp:ListItem Text = "PROVINCIA" Value = ""></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>            
                                   
                                     <div class="col-md-2">
                                        <asp:DropDownList ID="ddlciudad" runat="server" CssClass="form-control m-b">
                                           <asp:ListItem Text = "CIUDAD" Value = ""></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>            
                                                                 
                                </div>
                            </div>
                                   <div class="col-sm-16">
                                <div class="row">                                            
                                    <div class="col-md-6">
                                      <input type="text" placeholder="Detalle" id="txtdetalleregistro" class="form-control" required="" />
                                        
                                    </div>            
                                
                                                                            
                                </div>
                            </div>
                    </div>
                    <div class="ibox float-e-margins">
                        <div class="ibox" style="border-bottom:4px solid #e7eaec">
                            <h4>Observación</h4>
                            
                        </div>
                      
                                   <div class="col-sm-16">
                                <div class="row">                                            
                                    <div class="col-md-9">
                                      <input type="text" placeholder="Observación" id="txtobservacionregistro" class="form-control" />
                                        
                                    </div>            
                                
                                                                            
                                </div>
                            </div>
                    </div>
        
                              
                                  </div>
                <div class="modal-footer">
                    <button id="btnguardar_comireg" type="button" class="btn btn-primary">Guardar</button>
                    <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
                </div>
    </div>
    
    </form>
     <!-- iCheck -->
    <script src="js/plugins/iCheck/icheck.min.js"></script>
     <script>
            $(document).ready(function () {
                $('.i-checks').iCheck({
                    checkboxClass: 'icheckbox_square-green',
                    radioClass: 'iradio_square-green',
                });

                $(".iCheck-helper")
                    .click(function() {
                        var cbAns = ($("#chksintermino").is(":checked")) ? 1 : 0;
                        console.log(cbAns);
                        if (cbAns === 1) {
                            $("#data_3 .input-group.date").datepicker("option", "disabled", true);
                            $("#txtfechafin").attr("disabled", "disabled");
                            $("#txtfechafin").val("");
                        } else {
                            $("#data_3 .input-group.date").datepicker("option", "disabled", false);
                            $("#txtfechafin").removeAttr("disabled");
                        }
                    });
            });

        </script>
</body>
</html>
