<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>.:: Control de Usuario</title>
<style type="text/css"> 
body{ 
background:url(images/bg_infosat.png) repeat-x;
font-family:Verdana, Geneva, sans-serif;
font-size:10px;
}
h1{ 

font-family:Bebas;
color:#09C;
font-size:30px;
width:810px;

text-align:center;
padding-bottom:25px;
}
.bg_td {
	background:url(images/bg_td_2.png) repeat-x;
	color:#FFF;
	font-size:13px;
	text-align:center;
	font-weight:bold;
	padding:5px;}
.bg_t {
	background:#003C77;
	color:#FFF;
	font-size:11px;
	text-align:center;
	font-weight:bold;
	padding:4px;}
	
#contenedor{
	border:0px;
	width:auto;
	height:auto;
	}
	
.Button{
	background-color:#666;
	border:2px solid #666;
	font-family:"Lucida Grande", Tahoma, Arial, Verdana, sans-serif;
	color:#FFF }


    .auto-style1 {
        background: #003C77;
        color: #FFF;
        font-size: 11px;
        text-align: center;
        font-weight: bold;
        padding: 4px;
        width: 85px;
        height: 22px;
    }
    .auto-style2 {
        background: #003C77;
        color: #FFF;
        font-size: 11px;
        text-align: center;
        font-weight: bold;
        padding: 4px;
        width: 200px;
        height: 22px;
    }
    .auto-style3 {
        background: #003C77;
        color: #FFF;
        font-size: 11px;
        text-align: center;
        font-weight: bold;
        padding: 4px;
        height: 22px;
    }


</style>

</head>


<body >
<div id="contenedor">
    <form id="form1" runat="server">
    <div align="center">
        <asp:Panel id="Panel1" runat="server" height="626px" width="878px">
            <asp:Panel id="Panel6" runat="server" Borderwidth="0px" width="818px">
            
            <h1>CONTROL DE ACCESO A LOS USUARIOS - INFOSAT</h1>
          
            <table style="width: 812px; margin: 1px;" border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 135px; height: 19px;">Usuario:</td>
                    <td style="width: 180px; height: 19px;">
                        <asp:DropDownList id="DdlUsuario" runat="server" width="159px" AutoPostBack="True" Font-Size="10pt"></asp:DropDownList>
                    </td>
                    <td style="width: 114px; height: 19px">Apellido Paterno:</td>
                    <td colspan="3" align="left">
                        <asp:Label id="LblPaterno" runat="server" Text="Label" width="378px" ForeColor="Navy" Font-Size="10pt"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 135px; height: 15px;">Estado de Usuario</td>
                    <td style="width: 180px; height: 15px;">
                        <asp:DropDownList id="DdlEstadoUsuario" runat="server" width="159px" Font-Size="10pt"></asp:DropDownList>
                    </td>
                    <td style="width: 114px; height: 15px">Apellido Materno:</td>
                    <td colspan="3" align="left">
                        <asp:Label id="LblMaterno" runat="server" Text="Label" width="378px" ForeColor="Navy" Font-Size="10pt"></asp:Label>
                     </td>
                </tr>
                <tr>
                    <td style="width: 135px; height: 18px;">Rol de usuario</td>
                    <td style="width: 180px; height: 18px;">
                        <asp:DropDownList id="DdlRolUsuario" runat="server" width="159px" Font-Size="10pt"></asp:DropDownList>
                    </td>
                    <td style="width: 114px; height: 18px">Nombres:</td>
                    <td colspan="3" align="left">
                        <asp:Label id="LblNombres" runat="server" Text="Label" width="377px" ForeColor="Navy" Font-Size="10pt"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 135px; height: 18px"></td>
                    <td style="width: 180px; height: 18px"></td>
                    <td style="width: 114px; height: 18px"></td>
                    <td colspan="3" style="height: 18px"></td>
                </tr>
            </table>
            <table style="width: 717px" border="0" cellpadding="1" cellspacing="1">
                <tr>
                    <td rowspan="1" class="bg_td" style="width: 341px">Acceso a modulos</td>
                    <td rowspan="1" style="vertical-align: top; width: 16px; height: 12px; text-align: left"></td>
                    <td colspan="3" class="bg_td">Accesos a tipos de operaciones</td>
                </tr>
              
                <tr>
                <td rowspan="2" style="width: 341px; vertical-align: top; text-align: left;">
               
                    <asp:GridView id="DgvModulo" runat="server" width="332px" AutoGenerateColumns="False" CellPadding="0" BackColor="White" BorderColor="LightSlateGray" 
                        BorderStyle="Solid" Borderwidth="1px" Font-Size="9pt" height="1px" EnableTheming="True">
                        <FooterStyle BackColor="White" ForeColor="#333333" />
                        <Columns>
                            <asp:BoundField DataField="modulo_id"  ReadOnly="True" ShowHeader="False" HeaderText="id">
                                <ItemStyle height="10px" HorizontalAlign="Center" />
                                <HeaderStyle width="80px" HorizontalAlign="Center" CssClass="bg_t" />
                            </asp:BoundField>
                            <asp:BoundField DataField="descripcion"  ReadOnly="True" ShowHeader="False" HeaderText="descripcion" >
                                <ItemStyle height="10px" />
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:TemplateField  ShowHeader="False" HeaderText="acceso">
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle width="60px" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:CheckBox id="ChkAcceso" Checked='<%# DataBinder.Eval(Container.DataItem, "acceso") %>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <RowStyle BackColor="White" ForeColor="#333333" />
                        <SelectedRowStyle BackColor="#003C77" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#003C77" ForeColor="White" HorizontalAlign="Center" />
                        <HeaderStyle BackColor="#003C77" Font-Bold="True" ForeColor="White" />
                     </asp:GridView>
               <br />
                        <asp:button id="BtnMarcaTodosMod" CssClass="Button" runat="server" Text="Marcar Todos" width="121px" height="22px" Font-Size="9pt" />
                        <asp:button id="BtnDesmarcaTodosMod" CssClass="Button" runat="server" Text="Desmarcar Todos" width="125px" height="22px" Font-Size="9pt" /></td>
                    <td rowspan="2" style="vertical-align: top; width: 16px; height: 12px; text-align: left"></td>
                    <td class="auto-style1">id</td>
                    <td class="auto-style2">descripcion</td>
                    <td class="auto-style3">acceso</td>
                </tr>
                <tr>
                    <td colspan="3" style="vertical-align: top; text-align: left; height: 150px;">
            <asp:Panel id="Panel3" runat="server" BorderColor="#336666" BorderStyle="None" Borderwidth="1px"
                height="122px" ScrollBars="Auto" width="374px" style="text-align: left">
                <asp:GridView id="DgvTipoOperacion" runat="server" width="351px" AutoGenerateColumns="False" CellPadding="0" BackColor="White" BorderColor="LightSlateGray"
                     BorderStyle="Solid" Borderwidth="1px" Font-Size="9pt" height="1px" ShowHeader="False">
                    <FooterStyle BackColor="White" ForeColor="#333333" />
                    <Columns>
                        <asp:BoundField DataField="tipo_operacion_id" ReadOnly="True" ShowHeader="False">
                            <ItemStyle width="80px" height="10px" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="descripcion" ReadOnly="True" ShowHeader="False" >
                            <ItemStyle height="10px" />
                        </asp:BoundField>
                        <asp:TemplateField ShowHeader="False">
                            <ItemStyle HorizontalAlign="Center" width="60px" />
                            <ItemTemplate>
                                <asp:CheckBox id="ChkAcceso" Checked='<%# DataBinder.Eval(Container.DataItem, "acceso") %>' runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <RowStyle BackColor="White" ForeColor="#333333" />
                    <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
                    <HeaderStyle BackColor="#336666" Font-Bold="True" ForeColor="White" />
                </asp:GridView>
                </asp:Panel>
              <br />
                        <asp:button id="BtnMarcarTodos" CssClass="Button" runat="server" Text="Marcar Todos" width="121px" height="22px" Font-Size="9pt" />
                        <asp:button id="BtnDesmarcarTodos" CssClass="Button" runat="server" Text="Desmarcar Todos" width="125px" height="22px" Font-Size="9pt" />
                    </td>
                  
                </tr>
                <tr>
                    <td rowspan="1" style="vertical-align: top; width: 341px; height: 13px; text-align: left">
                    </td>
                    <td rowspan="1" style="vertical-align: top; width: 3751px; height: 13px; text-align: left">
                    </td>
                    <td colspan="3" style="vertical-align: top; height: 13px; text-align: left">
                    </td>
                </tr>
                <tr>
                    <td rowspan="1" class="bg_td" style="width: 341px">
                        Fraccionamiento - Accesos</td>
                    <td rowspan="1" style="vertical-align: top; width: 3751px; height: 13px; text-align: left">
                    </td>
                    <td colspan="3" class="bg_td">
                        Multas Administrativas - Accesos</td>
                </tr>
                <tr>
                    <td rowspan="1" style="border-right: lightslategray 1px solid; border-top: lightslategray 1px solid;
                        vertical-align: top; border-left: lightslategray 1px solid; width: 341px; border-bottom: lightslategray 1px solid;
                        height: 13px; text-align: left">
                        <asp:CheckBox id="ChkAccesoSolicitudFR" runat="server" Text="Solicitud y Resolucion" /><br />
                        <asp:CheckBox id="ChkAccesoReportesFR" runat="server" Text="Reportes Fraccionamiento" /></td>
                    <td rowspan="1" style="vertical-align: top; width: 3751px; height: 13px; text-align: left">
                    </td>
                    <td colspan="3" style="border-right: lightslategray 1px solid; border-top: lightslategray 1px solid;
                        vertical-align: top; border-left: lightslategray 1px solid; border-bottom: lightslategray 1px solid;
                        height: 13px; text-align: left">
                        <asp:CheckBox id="ChkEditaMultasAdm" runat="server" Text="Editar Multas Administrativas" /></td>
                </tr>
            </table>
            </asp:Panel>
            <br />
                        <asp:CheckBox id="ChkAccesoCaja" runat="server" Text="Activar acceso a Caja" AutoPostBack="True" /><br />
            <asp:Panel id="Panel2" runat="server" Borderwidth="0px" width="818px" height="329px">
                <table style="width: 714px" border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 153px; height: 17px">
                        Tipo de Usuario de Caja</td>
                    <td style="width: 264px; height: 17px">
                        <asp:DropDownList id="DdlTipoUsuarioCaja" runat="server" width="223px">
                        </asp:DropDownList></td>
                    <td style="width: 104px; height: 17px">
                        IP - &nbsp;(*) Todos</td>
                    <td style="height: 17px; width: 139px;">
                        <asp:TextBox id="TxtIP" runat="server" width="149px"></asp:TextBox></td>
                </tr>
                    <tr>
                        <td style="width: 153px; height: 17px">
                            Condicion Usuario</td>
                        <td style="width: 264px; height: 17px">
                            <asp:DropDownList id="DdlCondicionUsuarioCaja" runat="server" width="223px">
                            </asp:DropDownList></td>
                        <td style="width: 104px; height: 17px">
                        </td>
                        <td style="width: 139px; height: 17px">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 153px; height: 17px">
                            Agencia</td>
                        <td style="width: 264px; height: 17px">
                            <asp:DropDownList id="DdlAgencia" runat="server" width="223px">
                            </asp:DropDownList></td>
                        <td style="width: 104px; height: 17px">
                        </td>
                        <td style="width: 139px; height: 17px">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 153px; height: 17px">
                            Estado de Caja</td>
                        <td style="width: 264px; height: 17px"><asp:DropDownList id="DdlEstadoCaja" runat="server" width="223px">
                        </asp:DropDownList></td>
                        <td style="width: 104px; height: 17px">
                        </td>
                        <td style="width: 139px; height: 17px">
                        </td>
                    </tr>
            </table>
                <br />
                <table style="width: 764px; height: 180px;" border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td colspan="3" class="bg_td">
                            Accesos del Menu Principal</td>
                        <td colspan="1" style="font-weight: bold; font-size: 12px; vertical-align: middle;
                            width: 16px; color: white; border-bottom: white 1px solid; height: 16px; text-align: center;
                            border-right-width: 1px; border-right-color: white">
                        </td>
                        <td colspan="3" class="bg_td">
                            Accesos al Menu Reportes</td>
                    </tr>
                    <tr>
                        <td class="bg_t">
                            Id</td>
                        <td class="bg_t">
                            Descripcion</td>
                        <td class="bg_t">
                            Acceso</td>
                        <td style="font-weight: bold; font-size: 11px; width: 16px; color: white; height: 12px;
                            text-align: center; border-bottom-style: none">
                        </td>
                        <td class="bg_t">
                            Id</td>
                        <td class="bg_t">
                            Descripcion</td>
                        <td class="bg_t">
                            Acceso</td>
                    </tr>
                    <tr>
                        <td colspan="3" style="vertical-align: top; text-align: left; height: 150px;"><asp:Panel id="Panel4" runat="server" BorderColor="#336666" BorderStyle="None" Borderwidth="0px"
                height="129px" ScrollBars="Auto" width="374px" style="text-align: left">
                <asp:GridView id="DgvCajaMenuPrincipal" runat="server" width="348px" AutoGenerateColumns="False" CellPadding="0" BackColor="White" BorderColor="LightSlateGray" BorderStyle="Solid" Borderwidth="1px" Font-Size="10pt" height="1px" ShowHeader="False">
                    <FooterStyle BackColor="White" ForeColor="#333333" />
                    <Columns>
                        <asp:BoundField DataField="id_opcion" ReadOnly="True">
                            <ItemStyle height="10px" HorizontalAlign="Center" VerticalAlign="Middle" width="80px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="descripcion" ReadOnly="True" >
                            <ItemStyle height="10px" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Acceso">
                            <ItemStyle HorizontalAlign="Center" height="10px" width="60px" />
                            <ItemTemplate>
                                <asp:CheckBox id="ChkAcceso0" Checked='<%# DataBinder.Eval(Container.DataItem, "acceso") %>' runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <RowStyle BackColor="White" ForeColor="#333333" />
                    <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
                    <HeaderStyle BackColor="#336666" Font-Bold="True" ForeColor="White"  />
                </asp:GridView>
                        </asp:Panel>
                        </td>
                        <td colspan="1" style="vertical-align: top; width: 16px; height: 150px; text-align: left">
                        </td>
                        <td colspan="3" style="vertical-align: top; height: 150px; text-align: left">
                            <asp:Panel id="Panel5" runat="server" BorderColor="#336666" BorderStyle="None" Borderwidth="1px"
                height="129px" ScrollBars="Auto" width="374px" style="text-align: left">
                <asp:GridView id="DgvCajaAccesoReporte" runat="server" width="348px" AutoGenerateColumns="False" CellPadding="0" BackColor="White" BorderColor="LightSlateGray" BorderStyle="Solid" Borderwidth="1px" Font-Size="10pt" height="1px" ShowHeader="False">
                    <FooterStyle BackColor="White" ForeColor="#333333" />
                    <Columns>
                        <asp:BoundField DataField="id_opcion" ReadOnly="True">
                            <ItemStyle height="10px" HorizontalAlign="Center" VerticalAlign="Middle" width="80px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="descripcion" HeaderText="Descripcion" ReadOnly="True" >
                            <ItemStyle height="10px" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Acceso">
                            <ItemStyle HorizontalAlign="Center" height="10px" width="60px" />
                            <ItemTemplate>
                                <asp:CheckBox id="ChkAcceso" Checked='<%# DataBinder.Eval(Container.DataItem, "acceso") %>' runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <RowStyle BackColor="White" ForeColor="#333333" />
                    <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
                    <HeaderStyle BackColor="#336666" Font-Bold="True" ForeColor="White" />
                </asp:GridView>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="7" style="vertical-align: top; text-align: center; height: 22px;">
                        <asp:button id="BtnMarcarTodosCaj" CssClass="Button" runat="server" Text="Marcar Todos" width="121px" height="22px" Font-Size="9pt" />&nbsp;
                        <asp:button id="BtnDescMarcarTodosCaj" CssClass="Button" runat="server" Text="Desmarcar Todos" width="125px" height="22px" Font-Size="9pt" /></td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel id="Panel7" runat="server" BorderStyle="None" Borderwidth="0px" height="62px"
                width="818px">
                <br />
                <table border="0" cellpadding="0" cellspacing="0" style="width: 810px">
                    <tr>
                        <td style="width: 100px">
                        </td>
                        <td style="width: 100px">
                            <asp:button id="BtnGrabar" CssClass="Button" runat="server" Text="Grabar" width="99px" /></td>
                        <td style="width: 100px">
                            <asp:button id="BtnCancelar" CssClass="Button" runat="server" Text="Cancelar" width="93px" /></td>
                        <td style="width: 100px">
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:Panel>
        &nbsp;</div>
    </form>
    </div>
</body>
</html>
