﻿<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Main.aspx.vb" Inherits="Main" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">



<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
    <title>.:: Modulo de Control de Personal - DIREJPER</title>

    <link href="css/bootstrap.min.css" rel="stylesheet"/>
    <link href="css/plugins/datapicker/datepicker3.css" rel="stylesheet"/>

    <link href="css/animate.css" rel="stylesheet"/>
    <link href="css/style.css" rel="stylesheet"/>
    <link href="font-awesome/css/font-awesome.css" rel="stylesheet"/>   
    <link href="css/plugins/toastr/toastr.min.css" rel="stylesheet"/>
    <link href="css/plugins/iCheck/custom.css" rel="stylesheet"/>

    
    <script type="text/javascript" src="js/jquery-2.1.1.js"></script>
    <script type="text/javascript" src="js/main.js"></script>
    <script type="text/javascript" src="js/plugins/toastr/toastr.min.js"></script>
    
</head>


<body >
<form id="form1" runat="server">

<div id="wrapper">
    <nav class="navbar-default navbar-static-side" role="navigation">
            <div class="sidebar-collapse">
                <ul class="nav" id="side-menu">
                    <li class="nav-header">
                        <div class="dropdown profile-element"> <span>
                            <img alt="image"  src="css/img/logo.png" />
                             </span>                          
                            <ul class="dropdown-menu animated fadeInRight m-t-xs">
                                <li><a href="#">Profile</a></li>
                                <li><a href="contacts.html">Contacts</a></li>
                                <li><a href="#">Mailbox</a></li>
                                <li class="divider"></li>
                                <li><a href="login.html">Logout</a></li>
                            </ul>
                        </div> 
                        
                        <div class="logo-element">
                            P.N.P <i class="fa fa-database"></i>
                        </div>
                    </li>
                    <li class="active">
                        <a href="#"><i class="fa fa-th-large"></i> <span class="nav-label">Control</span> <span class="fa arrow"></span></a>
                        <ul class="nav nav-second-level">
                            <li ><a id="lbtncomi" href="#">Comisiones</a></li>
                            <li ><a id="lbtnvaca" href="#">Vacaciones</a></li>
                            <li ><a id="lbtnperm" href="#">Permisos</a></li>
                            <li ><a id="lbtnlice" href="#">Licencias</a></li>
                        </ul>
                    </li>
                   
                    <li>
                        <a href="#"><i class="fa fa-bar-chart-o"></i> <span class="nav-label">Reportes</span><span class="fa arrow"></span></a>
                        <ul class="nav nav-second-level">
                            <li><a id="lbtnmovi" href="#">Movimientos</a></li>
                            <li><a id="lbtnotro"href="#">Otros</a></li>
                        </ul>
                    </li>
                
                  
                    <li>
                        <a href="#"><i class="fa fa-edit"></i> <span class="nav-label">Sistema</span><span class="fa arrow"></span></a>
                        <ul class="nav nav-second-level">
                            <li><a id="lbtnacce" href="#">Accesos</a></li>
                            <li><a id="lbtnperf" href="#">Perfiles</a></li>
                            <li><a id="lbtnusua" href="#">Usuarios</a></li>
                        </ul>
                    </li>      
                </ul>

            </div>
        </nav>
   <div id="page-wrapper" class="gray-bg dashbard-1">
        <div class="row border-bottom">
        <nav class="navbar navbar-static-top" role="navigation" style="margin-bottom: 0">
    <div class="navbar-header">
            <a class="navbar-minimalize minimalize-styl-2 btn btn-primary " href="#"><i class="fa fa-bars"></i> </a>
                <%--<form role="search" class="navbar-form-custom" method="post" action="search_results.html">
                    <div class="form-group">
                        <input type="text" placeholder="Search for something..." class="form-control" name="top-search" id="top-search">
                    </div>
                </form>--%>
        </div>
            <ul class="nav navbar-top-links navbar-right">
                <li style="padding-top:10px;">
                    <span class="m-r-sm text-muted welcome-message">                     
                    </span>
                </li>
                <li class="dropdown">
                    <a class="dropdown-toggle count-info" data-toggle="dropdown" href="#">
                        <i class="fa fa-envelope"></i>  <span class="label label-warning">16</span>
                    </a>
                    <ul class="dropdown-menu dropdown-messages">
                        <li>
                            <div class="dropdown-messages-box">
                                <a href="#" class="pull-left">
                                    <img alt="image" class="img-circle" src="img/a7.jpg">
                                </a>
                                <div class="media-body">
                                    <small class="pull-right">46h ago</small>
                                    <strong>Mike Loreipsum</strong> started following <strong>Monica Smith</strong>. <br>
                                    <small class="text-muted">3 days ago at 7:58 pm - 10.06.2014</small>
                                </div>
                            </div>
                        </li>
                        <li class="divider"></li>
                        <li>
                            <div class="dropdown-messages-box">
                                <a href="#" class="pull-left">
                                    <img alt="image" class="img-circle" src="img/a4.jpg"/>
                                </a>
                                <div class="media-body ">
                                    <small class="pull-right text-navy">5h ago</small>
                                    <strong>Chris Johnatan Overtunk</strong> started following <strong>Monica Smith</strong>. <br>
                                    <small class="text-muted">Yesterday 1:21 pm - 11.06.2014</small>
                                </div>
                            </div>
                        </li>
                        <li class="divider"></li>
                        <li>
                            <div class="dropdown-messages-box">
                                <a href="#" class="pull-left">
                                    <img alt="image" class="img-circle" src="img/profile.jpg" />
                                </a>
                                <div class="media-body ">
                                    <small class="pull-right">23h ago</small>
                                    <strong>Monica Smith</strong> love <strong>Kim Smith</strong>. <br>
                                    <small class="text-muted">2 days ago at 2:30 am - 11.06.2014</small>
                                </div>
                            </div>
                        </li>
                        <li class="divider"></li>
                        <li>
                            <div class="text-center link-block">
                                <a href="#">
                                    <i class="fa fa-envelope"></i> <strong>Read All Messages</strong>
                                </a>
                            </div>
                        </li>
                    </ul>
                </li>
                <li class="dropdown">
                    <a class="dropdown-toggle count-info" data-toggle="dropdown" href="#">
                        <i class="fa fa-bell"></i>  <span class="label label-primary">8</span>
                    </a>
                    <ul class="dropdown-menu dropdown-alerts">
                        <li>
                            <a href="#">
                                <div>
                                    <i class="fa fa-envelope fa-fw"></i> You have 16 messages
                                    <span class="pull-right text-muted small">4 minutes ago</span>
                                </div>
                            </a>
                        </li>
                        <li class="divider"></li>
                        <li>
                            <a href="#">
                                <div>
                                    <i class="fa fa-twitter fa-fw"></i> 3 New Followers
                                    <span class="pull-right text-muted small">12 minutes ago</span>
                                </div>
                            </a>
                        </li>
                        <li class="divider"></li>
                        <li>
                            <a href="#">
                                <div>
                                    <i class="fa fa-upload fa-fw"></i> Server Rebooted
                                    <span class="pull-right text-muted small">4 minutes ago</span>
                                </div>
                            </a>
                        </li>
                        <li class="divider"></li>
                        <li>
                            <div class="text-center link-block">
                                <a href="notifications.html">
                                    <strong>See All Alerts</strong>
                                    <i class="fa fa-angle-right"></i>
                                </a>
                            </div>
                        </li>
                    </ul>
                </li>


                <li>
                  
                        <asp:LinkButton ID="lbtncerrar" runat="server"><i class="fa fa-sign-out"></i> Cerrar Sessión</asp:LinkButton>
                   
                </li>
            </ul>

        </nav>
         
    </div>
        <div class="row  border-bottom white-bg dashboard-header">
            <div class="wrapper wrapper-content animated fadeInRight">

                    <div class="row content">
                        <h2>Sistema Integral de Recursos Humanos</h2>
                        <small>Menu Principal</small>
                        <div class="tooltip-demo">
                        <ul class="list-group clear-list m-t">
                            <li class="list-group-item fist-item">
                                <span class="pull-right">
                                    09:00 pm
                                </span>

                                <span class="label label-success" data-toggle="tooltip" data-original-title="Usuario" data-placement="bottom"><i class="fa fa-user"></i></span> <asp:Label ID="lblgrado" runat="server" Text=""></asp:Label> <asp:Label ID="lblusuario" runat="server" Text=""></asp:Label>
                            </li>
                            <li class="list-group-item">
                                <span class="pull-right">
                                    10:16 am
                                </span>
                                <span class="label label-success" data-toggle="tooltip" data-original-title="Unidad" data-placement="bottom"><i class="fa fa-briefcase"></i></span> <asp:Label ID="lblunidad" runat="server" Text=""></asp:Label>
                            </li>
                            <li class="list-group-item">
                                <span class="pull-right">
                                    08:22 pm
                                </span>
                                 <span class="label label-success" data-toggle="tooltip" data-original-title="Equipo" data-placement="bottom"><i class="fa fa-desktop"></i></span> <asp:Label ID="lblequipo" runat="server" Text=""></asp:Label>
                            </li> 
                            <li class="list-group-item">
                                <span class="pull-right">
                                    08:22 pm
                                </span>
                                 <span class="label label-success" data-toggle="tooltip" data-original-title="Dominio" data-placement="bottom"><i class="fa fa-windows"></i></span> <asp:Label ID="lbldomain_user" runat="server" Text=""></asp:Label>
                            </li>                          
                        </ul>
                            </div>
                    </div>


            </div>
          <div class="footer">
            <%--<div class="pull-right">
                10GB of <strong>250GB</strong> Free.
            </div>--%>
            <div class="text-center">
                <strong>Copyright</strong> DIREJPER - OFITCE © 2016
            </div>
        </div>
    </div>
      </div>
 
      </div>       
    <!-- MODAL DETALLE-->
        <div class="modal fade" id="md_detalle" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" data-backdrop="static" data-keyboard="false">
            <div class="modal-dialog modal-lg" role="document">
                <div class="modal-content"></div>                
            </div>       
        </div>
    <!-- MODAL REGISTRO-->
        <div class="modal fade" id="md_registro" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" data-backdrop="static" data-keyboard="false">
            <div class="modal-dialog modal-lg" role="document">
                <div class="modal-content"></div>                
            </div>       
        </div>  
    
       <!-- MODAL REFERENCIA --> 
     <div class="modal fade" id="md_referencia" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" data-backdrop="static" data-keyboard="false">
            <div class="modal-dialog" role="document">
                <div class="modal-content"></div>                
            </div>       
        </div> 
         
    <!-- MODAL FULLSCREEN --> 
     <div class="modal modal-fullscreen fade" id="mdrptcontent" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" data-backdrop="static" data-keyboard="false">
            <div class="modal-dialog modal-lg" role="document">
                <div class="modal-content"></div>                
            </div>       
        </div> 
      
        

    </form>

<script type="text/javascript" src="js/bootstrap.min.js"></script>
<script type="text/javascript" src="js/plugins/metisMenu/jquery.metisMenu.js"></script>
<script type="text/javascript" src="js/plugins/slimscroll/jquery.slimscroll.min.js"></script>
<!-- Custom and plugin javascript -->
<script type="text/javascript" src="js/inspinia.js"></script>
<script type="text/javascript" src="js/plugins/pace/pace.min.js"></script>

<!-- jQuery UI -->
<script type="text/javascript" src="js/plugins/jquery-ui/jquery-ui.min.js"></script>


  
  
    

</body>
</html>
