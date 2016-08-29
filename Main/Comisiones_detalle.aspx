<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Comisiones_detalle.aspx.vb" Inherits="Main_Comisiones_detalle" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script src="../js/comisiones_detalle.js"></script> 
    
   
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <div class="modal-header detalle">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" id="myModalLabel">Detalles</h4>
                </div>
                <div class="modal-body">
                 <h2>Datos Personales</h2>
                     <div class="ibox-content">
                        <div class="col-lg-8">
                           
                            
                            
                                <div class="well" style="font-size:11px;">
                                
                                    <p> <i class="fa fa-user"></i> CIP : <asp:Label ID="lbldatos1" runat="server" Text=""></asp:Label> </p>
                                    <p><i class="fa fa-briefcase"></i> Unidad : <asp:Label ID="lbldatos2" runat="server" Text=""></asp:Label></p>
                                    <p><i class="fa fa-map-marker"></i> Dirección : <asp:Label ID="lbldatos3" runat="server" Text=""></asp:Label> </p>                     
                                     <p><i class="fa fa-map-marker"></i> Distrito : <asp:Label ID="lbldatos4" runat="server" Text=""></asp:Label> </p>                     
                                     <p><i class="fa fa-map-marker"></i> Provincia : <asp:Label ID="lbldatos5" runat="server" Text=""></asp:Label> </p>
                                    <p><i class="fa fa-map-marker"></i> Departamento : <asp:Label ID="lbldatos6" runat="server" Text=""></asp:Label> </p>
                                    </div>
                        </div>
                    <div class="col-lg-3" style="padding-right:0px;">
                    
                        <div class="well">
                            <img alt="image" class="img-circle circle-border img-profile"  width="148" height="148" src="images/sin_foto.png" />
                         </div>
                        
                         </div>
        </div>

        <div class="row">  
            <div class="ibox-content">   
                <div class="table-responsive"> 
                  <table id="dgvdetallecomision" class="table table-hover dgvdetallecomision" data-paging="true"></table>
                </div>
                                                   
            </div>
            <input type="hidden" id="nro_transa" runat="server" />            
        </div>  
                         <div class="row">
                <div class="col-lg-7">
                    <div class="ibox float-e-margins">
                      
                        <div class="ibox-content">
                            <table id="dgvdetallecomision_hijo" class="table table-hover dgvdetallecomision_hijo" data-paging="true"></table>
                        </div>
                    </div>
                </div>
                <div class="col-lg-5">
                    <div class="ibox float-e-margins">
      
                        <div class="ibox-content">
                            <h3>Observacion</h3>
                            <textarea id="txtobservacion" cols="40" rows="7" runat="server"></textarea>                      
                        </div>
                    </div>
                </div>
            </div>          
                                  </div>
                <div class="modal-footer">                    
                    <button id="btnadicionar" type="button" class="btn btn-primary">Adicionar</button>
                    <button type="button" class="btn btn-primary">Modificar</button>
                    <button type="button" class="btn btn-primary">Eliminar</button>
                    <button id="btnreportedetallado" type="button" class="btn btn-primary">Reporte</button>
                    <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
                </div>
    </div>
    </form>
</body>
</html>
