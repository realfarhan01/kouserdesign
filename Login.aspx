<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Login.aspx.vb" Inherits="Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<meta name="viewport" content="width=device-width, initial-scale=1.0" />
<title>Kouser Design – Handcrafted 925 Silver Jewellery | Munich, Germany</title>
<link rel="stylesheet" href="login/css/style.default.css" type="text/css" />
<script type="text/javascript" src="login/js/jquery-1.9.1.min.js"></script>
<script type="text/javascript" src="login/js/jquery-migrate-1.1.1.min.js"></script>
<link rel="shortcut icon" href="favicon.png" type="image/x-icon">
<link rel="icon" href="favicon.png" type="image/x-icon">
</head>
<body class='loginbody'>
    <form id="form1" runat="server">
 <div class="loginwrapper">
	<div class="loginwrap zindex100 animate2 bounceInDown">
	<h1 class="logintitle"><span class="iconfa-lock"></span> Aspire CMS Panel <span class="subtitle">Administrator Login </span></h1>
        <div class="loginwrapperinner">
           
              
								   
      <div runat="server" id="msg" style="display: none;" class="alert alert-error hide">
                                    </div>

         <asp:Literal ID="litmsg" runat="server"></asp:Literal>
        
       <p class="animate4 bounceIn">
         <asp:TextBox runat="server" MaxLength="20" placeholder="Username" ID="txtlogin" class="form-control" ValidationGroup="Login"></asp:TextBox>
            <asp:RequiredFieldValidator ControlToValidate="txtlogin" ValidationGroup="Login" ID="RequiredFieldValidator1" runat="server" ErrorMessage="Enter User Name." SetFocusOnError="true" Display="None"></asp:RequiredFieldValidator>
           </p>
        
    
   <p class="animate5 bounceIn">
           <asp:TextBox runat="server" MaxLength="20" ID="txtPassword" placeholder="Password" TextMode="Password" class="form-control" ValidationGroup="Login"></asp:TextBox>
            <asp:RequiredFieldValidator ControlToValidate="txtPassword" ValidationGroup="Login" 
                    ID="RequiredFieldValidator3" runat="server" ErrorMessage="Enter Password." Display="None"></asp:RequiredFieldValidator></p>
             
           <p class="animate6 bounceIn">
             <asp:Button runat="server" ID="btnlogin" ValidationGroup="Login"  ForeColor="White"  Text="Login" class="btn btn-default btn-block"  /></p>
             <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Login" CssClass="alert alert-error hide">    </asp:ValidationSummary>
      
     </div><!--loginwrapperinner-->
    </div>
    <div class="loginshadow animate3 fadeInUp"></div>
</div><!--loginwrapper-->
                        <div align="center">
                        <p>Copyright &copy; 2025.</p><p> Design & Developed by <a href="http://aspiretechnosys.com/" target="_blank" >Aspire Technosys Pvt Ltd</a></p>	</div>	        	
			   
           
       <script type="text/javascript">
           jQuery.noConflict();

           jQuery(document).ready(function () {

               var anievent = (jQuery.browser.webkit) ? 'webkitAnimationEnd' : 'animationend';
               jQuery('.loginwrap').bind(anievent, function () {
                   jQuery(this).removeClass('animate2 bounceInDown');
               });

               jQuery('#username,#password').focus(function () {
                   if (jQuery(this).hasClass('error')) jQuery(this).removeClass('error');
               });

               jQuery('#loginform button').click(function () {
                   if (!jQuery.browser.msie) {
                       if (jQuery('#username').val() == '' || jQuery('#password').val() == '') {
                           if (jQuery('#username').val() == '') jQuery('#username').addClass('error'); else jQuery('#username').removeClass('error');
                           if (jQuery('#password').val() == '') jQuery('#password').addClass('error'); else jQuery('#password').removeClass('error');
                           jQuery('.loginwrap').addClass('animate0 wobble').bind(anievent, function () {
                               jQuery(this).removeClass('animate0 wobble');
                           });
                       } else {
                           jQuery('.loginwrapper').addClass('animate0 fadeOutUp').bind(anievent, function () {
                               jQuery('#loginform').submit();
                           });
                       }
                       return false;
                   }
               });
           });
</script>



      
        
        
       
    
					
  
    </form>
</body>
</html>