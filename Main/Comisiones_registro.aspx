<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Comisiones_registro.aspx.vb" Inherits="Main_Comisiones_registro" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
             <div class="wrapper wrapper-content animated fadeIn">               
                       <div class="form-group">
                            <div class="col-sm-10">
                                <div class="row">                                            
                                    <div class="col-md-2">
                                        <label>CIP</label>
                                        <input type="text" id="txtcip" name="txtcip" class="form-control" placeholder="CIP" />
                                    </div>
                                    <div class="col-md-2">
                                        <label>Apellido Paterno</label>
                                        <input type="text" id="txtapepat" name="txtapepat" class="form-control" placeholder="Apellido Paterno" />
                                    </div> 
                                    <div class="col-md-2">
                                        <label>Apellido Materno</label>
                                        <input type="text" id="txtapemat" name="txtapemat" class="form-control" placeholder="Apellido Materno" />
                                    </div>
                                    <div class="col-md-2">
                                        <label>Nombres</label>
                                        <input type="text" id="txtnombres" name="txtnombres" class="form-control" placeholder="Nombres" />
                                    </div> 
                                    <div class="col-md-1">
                                        <label>&nbsp;</label>
                                        <button id="btnbuscar" type="button" class="btn btn-primary">Buscar</button>
                                    </div>                                               
                                </div>
                            </div>
                         </div>    
                      
        </div>
    </div>
    </form>
</body>
</html>
