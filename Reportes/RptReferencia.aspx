<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RptReferencia.aspx.vb" Inherits="Reportes_RptReferencia" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
        <script type="text/javascript" src="js/plugins/datapicker/bootstrap-datepicker.js"></script>
        <script type="text/javascript" src="../js/rptreferencia.js"></script>
</head>
<body>
    <form id="form1" runat="server" autocomplete="off">

            
    <div class="modal-header referencia">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" id="myModalLabel">Referencia</h4>
                </div>
                <div class="modal-body">                 
     <div class="ibox float-e-margins">
                        <div class="ibox" style="border-bottom:4px solid #e7eaec">
                            <h4>Documento</h4>
                            
                        </div>
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
                                            <input type="text" class="form-control" placeholder="Fecha Doc."/>
                                        </div>
                                    </div>
                                    </div>
                                </div>
                                   
                                <div class="form-group">
                                    <label class="col-lg-2 control-label">Documento:</label>

                                    <div class="col-lg-10">
                                       <input type="text" placeholder="Documento" class="form-control" />
                                       </div>
                                </div>
                               
                                 <div class="col-md-9">
                                  
                                </div>    
                                                            
                                </div>
     
                    </div>
                  
                   
                              
                                  </div>
                <div class="modal-footer">
                    <button id="btnguardar_comireg" type="button" class="btn btn-primary">Guardar</button>
                    <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
                </div>
   
    </form>
</body>
</html>
