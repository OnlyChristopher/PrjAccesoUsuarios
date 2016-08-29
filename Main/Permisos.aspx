<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Permisos.aspx.vb" Inherits="Main_Permisos" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script src="../js/permisos.js"></script>
</head>
<body>
    <form id="form1" runat="server" class="form-horizontal" style="margin-left:20px;">
       
          <div class="row wrapper page-heading" style="border-bottom:4px solid #e7eaec">
                <div class="col-lg-10">
                    <h1>Permisos</h1>                 
                </div>
             </div>
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
            <div class="row">  
                <div class="ibox-content">      
                <div class="table-responsive"> 
                    <table id="dgvpermisos" class="table table-hover dgvpermisos" data-paging="true"></table>
                </div>
              
                <div id="countreg">
                    <span class="badge badge-warning" style="padding:8px;display:none;"></span>
                </div>   
               </div>
               </div>
                      
       
             <div class="row opciones_dgvpermisos">              
                    <div class="ibox-title">                                  
                        <table>
                            <tr>
                                <td>
                                    <button type="button" class="btn btn-primary" id="btndetalle">Detalle</button>&nbsp;&nbsp;
                                </td>
                                <td>                                        
                                    <button type="button" class="btn btn-primary" id="btnreporte">Reporte</button>&nbsp;&nbsp;
                                </td>                                       
                                <td>
                                    <button type="button" class="btn btn-primary" id="btnexportar">Exportar</button>
                                 </td>
                            </tr>
                        </table>                         
                    </div>      
        </div>

    </form>
</body>
</html>
