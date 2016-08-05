<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Comisiones.aspx.vb" Inherits="Main_Comisiones" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
<%--    <script src="http://code.jquery.com/jquery-1.8.3.js"></script> 
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.11.0/jquery.min.js"></script>--%>
    <link rel="stylesheet" href="../css/footable.bootstrap.min.css" />

        <script src="../js/comisiones.js"></script> 



</head>
<body>
    <form id="form1" runat="server" class="form-horizontal" style="margin-left:20px;" autocomplete="off">      
            <div class="row wrapper page-heading" style="border-bottom:4px solid #e7eaec">
                <div class="col-lg-10">
                    <h1>Comisiones</h1>                   
                </div>
             </div>
       
         <div class="wrapper wrapper-content animated fadeIn">               
                       <div class="form-group">
                            <div class="col-sm-10">
                                <div class="row">                                            
                                    <div class="col-md-2">
                                        <label>CIP</label>
                                        <input maxlength="8" type="text" id="txtcip" name="txtcip" class="form-control" placeholder="CIP" />
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
                    <table id="dgvcomisiones" class="table table-hover dgvcomisiones" data-paging="true"></table>
                    </div>
              
                <div id="countreg">
                    <span class="badge badge-warning" style="padding:8px;display:none;"></span>
                </div>   
                                           
            </div>
        </div>                      
           <div class="row opciones_dgvcomisiones">              
                    <div class="ibox-title">                                  
                        <table>
                            <tr>
                                <td>
                                    <button type="button" class="btn btn-primary" id="btndetalle" <%--data-toggle="modal" data-target="#myModal"--%>>Detalle</button>&nbsp;&nbsp;
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

    

    <input id="hdcip" type="hidden" />

    
    </form>
     <script type="text/javascript" src="../js/footable.js"></script>

</body>
</html>
