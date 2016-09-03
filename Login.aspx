<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Login.aspx.vb" Inherits="Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <meta charset="UTF-8">
    <title>Acceso a Control</title>   
    <link rel='stylesheet prefetch' href='css/font-awesome.min.css'>
    <link rel="stylesheet" href="css/login.css">    
 <script type="text/javascript" src="js/jquery-1.8.3.min.js"></script>
 
</head>
<body>
  
    <h1>POLICÍA NACIONAL DEL PERÚ</h1>
    <h2>MODULO DE CONTROL DE PERSONAL - DIREJPER</h2>

    <div class="login-form">
         <div id="logo"></div>
     <div class="form-group ">
       <input type="text" class="form-control" placeholder="Usuario" id="txtusername" name="txtusername"/>
         <span id="ErrorUsuario" class="alert">Ingrese Ususario</span>
         <i class="fa fa-user"></i>
     </div>
     <div class="form-group log-status">
       <input type="password" class="form-control" placeholder="Contraseña" id="txtpassword" name="txtpassword"/>
       <span id="ErrorClave" class="alert">Ingrese Clave</span>
       <i class="fa fa-lock"></i>
     </div>
      <span id="ErrorDatos" class="alert">Datos Incorretos</span>
      <button type="button" class="log-btn" >Ingreso</button>

</div>
        
 
<script src="js/login.js"></script>

</body>
     
</html>
