<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="Employee.WebForm2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
       <div style="font-family:Arial">
<table style="border: 1px solid black">
    <tr>
        <td colspan="2">
            <b>Login</b>
        </td>
    </tr>
    <tr>
        <td>
            User Name
        </td>    
        <td>
            :<asp:TextBox ID="txtUserName" runat="server">
            </asp:TextBox>
        </td>    
    </tr>
    <tr>
        <td>
            Password
        </td>    
        <td>
            :<asp:TextBox ID="txtPassword" TextMode="Password" runat="server">
            </asp:TextBox>
        </td>    
    </tr>
    <tr>
        <td>
                    
            <asp:CheckBox ID="chkBoxRememberMe" runat="server" Text="Remember me" />
                    
        </td>    
        <td>
            <asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click" />
            <asp:Button ID="reset" runat="server" OnClick="Button1_Click" Text="reset password" />
        </td>
        
    </tr>
         
            
        
</table>
<asp:Label ID="menssage" runat="server" ForeColor="Red"></asp:Label>
<br />
<a href="Register.aspx">Click here to register</a> 
if you do not have a user name and password.
</div>
    </div>
    </form>
</body>
</html>
