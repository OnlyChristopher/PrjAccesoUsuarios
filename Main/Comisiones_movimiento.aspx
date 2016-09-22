<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Comisiones_movimiento.aspx.vb" Inherits="Main_Comision_movimiento" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" href="css/bootstrap-datetimepicker.css" />
    <script type="text/javascript" src="js/moment-with-locales.js"></script>
    <script type="text/javascript" src="js/bootstrap-datetimepicker.js"></script>
    <script type="text/javascript" src="js/comisiones_movimiento.js"></script>

     
    <title></title>
</head>
<body>
    <form id="frmmovimientos" runat="server" autocomplete="off">

            
    <div class="modal-header registro-movimiento">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" id="myModalLabel">Registro de Movimiento</h4>
                </div>
                <div class="modal-body">                 
     <div class="ibox float-e-margins">
                   
                                <div class="row">
                                <div class="col-md-9">
                                        
                                    </div>    
                                                                                      
                                </div>
         
                                <div class="row" style="margin-top:10px;">                                            
                           
                                       
                                <div class="form-group" style="height:40px;">
                                    <label class="col-lg-2 control-label">Fecha:</label>
                                    <div class="col-lg-5">
                                          <div class="form-group" id="data_fechacontrolmov" >
                                        <div class="input-group date">
                                            <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                            <input type="text" id="txtfechamovimiento" class="form-control" placeholder="Fecha Doc."/>
                                        </div>
                                    </div>
         
                                    </div>
                                </div>
                                   
                                <div class="form-group" style="height:40px;">
                                    <label class="col-lg-2 control-label">Tipo Registro:</label>

                                    <div class="col-lg-5">
                                        <asp:DropDownList ID="ddltiporeg" CssClass="form-control m-b" runat="server"></asp:DropDownList>
                                       </div>
                                </div>
                                      <div class="form-group" style="height:40px;">
                                    <label class="col-lg-2 control-label">Tipo Control:</label>

                                    <div class="col-lg-5">
                                        <asp:DropDownList ID="ddltipocon" CssClass="form-control m-b" runat="server"></asp:DropDownList>
                                       </div>
                                </div>
                                          <div class="form-group" style="height:40px;">
                                    <label class="col-lg-2 control-label">Obervación:</label>

                                    <div class="col-lg-5">
                                        <textarea id="txtobservacionmovimiento"  placeholder="Obervacion" class="form-control"></textarea>
                                       </div>
                                                          <asp:TextBox ID="txtcomision_id" runat="server" Visible="False"></asp:TextBox>

                                </div>
                               
                               
                                                            
                                </div>
     
                    </div>
                  
                   
                              
                                  </div>
                <div class="modal-footer">
                    <button id="btnguardar_comisiondetalle" type="button" class="btn btn-primary">Guardar</button>
                    <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
                </div>
        
        
    </form>
</body>
</html>
