<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Comisiones_registro.aspx.vb" Inherits="Main_Comisiones_registro" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
        <script type="text/javascript" src="js/plugins/datapicker/bootstrap-datepicker.js"></script>
        <script type="text/javascript" src="../js/comisiones_registro.js"></script>
    <title></title>
</head>
<body>
    <form id="form1" runat="server" autocomplete="off">
    <div>
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
                                                                                
                                    <div class="col-md-2">
                                        <asp:DropDownList ID="ddltipodoc" CssClass="form-control m-b" runat="server"></asp:DropDownList>
                                    </div>            
                                    <div class="col-md-2">
                                        <input type="text" id="txtapepat" name="txtapepat" class="form-control" placeholder="Numero" />
                                    </div> 
                                    <div class="col-md-2">
                                        <input type="text" id="txtapemat" name="txtapemat" class="form-control" placeholder="Siglas" />
                                    </div>
                                     <div class="col-md-3">
                                    <div class="form-group" id="data_1">
                                        <div class="input-group date">
                                            <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                            <input type="text" class="form-control" placeholder="Fecha Doc."/>
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
                                     <div class="col-md-3">
                                   <div class="form-group" id="data_2">
                                        <div class="input-group date">
                                            <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                            <input type="text" class="form-control" placeholder="Fecha Ini."/>
                                        </div>
                                    </div>
                                         </div>
                                     <div class="col-md-3">
                                   <div class="form-group" id="data_3">
                                        <div class="input-group date">
                                            <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                            <input type="text" class="form-control" placeholder="Fecha Fin."/>
                                        </div>
                                    </div>
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
                                
                                    <%--<div class="col-md-1">
                                        <button id="btnbuscar" type="button" class="btn btn-primary">Buscar</button>
                                    </div>    --%>                                           
                                </div>
                            </div>
                                   <div class="col-sm-16">
                                <div class="row">                                            
                                    <div class="col-md-6">
                                      <input type="text" placeholder="Detalle" class="form-control" />
                                        
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
                                      <input type="text" placeholder="Observación" class="form-control" />
                                        
                                    </div>            
                                
                                                                            
                                </div>
                            </div>
                    </div>
        
                              
                                  </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-primary">Guardar</button>
                    <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
                </div>
    </div>
    </div>
    </form>
    
</body>
</html>
