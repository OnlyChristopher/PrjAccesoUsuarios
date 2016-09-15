<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Comisiones_movimiento.aspx.vb" Inherits="Main_Comision_movimiento" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server" autocomplete="off">

            
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
                                          <div class="form-group" id="data_fechadoc">
                                        <div class="input-group date">
                                            <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                            <input type="text" id="txtfechareferencia" class="form-control" placeholder="Fecha Doc."/>
                                        </div>
                                    </div>
                                    </div>
                                </div>
                                   
                                <div class="form-group" style="height:40px;">
                                    <label class="col-lg-2 control-label">Tipo Registro:</label>

                                    <div class="col-lg-5">
                                       <input type="text" id="txtdocreferencia" placeholder="Documento" class="form-control" />
                                       </div>
                                </div>
                                      <div class="form-group" style="height:40px;">
                                    <label class="col-lg-2 control-label">Tipo Control:</label>

                                    <<div class="col-lg-5">
                                       <input type="text" id="txtdocreferencia" placeholder="Documento" class="form-control" />
                                       </div>
                                </div>
                                          <div class="form-group" style="height:40px;">
                                    <label class="col-lg-2 control-label">Obervación:</label>

                                    <div class="col-lg-5">
                                        <textarea id="txtobservacionmovimiento"  placeholder="Obervacion" class="form-control"></textarea>
                                       </div>
                                </div>
                               
                               
                                                            
                                </div>
     
                    </div>
                  
                   
                              
                                  </div>
                <div class="modal-footer">
                    <button id="btnguardar_referencia" type="button" class="btn btn-primary">Guardar</button>
                    <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
                </div>
            <asp:TextBox ID="txtrcontrol_transa" runat="server" Visible="False"></asp:TextBox>
    </form>
</body>
</html>
