<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RptContent.aspx.vb" Inherits="Reportes_RptContent" %>

<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
                    <script type="text/javascript" src="../js/rptcontent.js"></script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:TextBox ID="maspe_carne_h" runat="server" Visible="False" />
        <asp:TextBox ID="rcontrol_transa_h" runat="server" Visible="False" />
            <div class="modal-header detalle">
  <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" id="myModalLabel">Reporte</h4>
                </div>
                <div class="modal-body">      
         </div>
        
        </div>
    </form>

</body>
</html>
