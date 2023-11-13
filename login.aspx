<%@ Page Language="VB" AutoEventWireup="false" CodeFile="login.aspx.vb" Inherits="login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
        <meta charset="utf-8" />
        <title>Login</title>
        <link rel="stylesheet" href="css/login.css" type="text/css" />
        <base target="_self" />
</head>
<body topmargin=0 leftmargin=0 >
        <center>
                <form id="form1" runat="server">
                <div id="login" align="center">
                        <table width="339" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                        <td height="155" align="center" valign="bottom">
                                                <img src="images/login/logo.jpg" width="126" height="133">
                                        </td>
                                </tr>
                                <tr height="30">
                                        <td align="left">
                                                <div style="position: relative; left: 50px">
                                                        ชื่อผู้ใช้
                                                </div>
                                        </td>
                                </tr>
                                <tr>
                                        <td height="54" align="center">
                                                <%-- <input  type="text" class="loginbox" id="txtUsername" runat =server >--%>
                                                <asp:TextBox class="loginbox" ID="txtUsername" runat="server" />
                                        </td>
                                </tr>
                                <tr height="30">
                                        <td align="left">
                                                <div style="position: relative; left: 50px">
                                                        รหัสผ่าน
                                                </div>
                                        </td>
                                </tr>
                                <tr>
                                        <td height="54" align="center">
                                                <%--<input  type="password" class="loginbox" id="Password1"   runat =server >--%>
                                                <asp:TextBox class="loginbox" ID="txtPassword"   TextMode=Password   runat="server" />
                                        </td>
                                </tr>
                                <tr>
                                        <td height="51" align="center">
                                                <asp:ImageButton ID="btnLogin" runat="server" ImageUrl="images/login/login-bt.png"
                                                        Style="cursor: pointer; outline: 0;" />
                                                &nbsp;<asp:ImageButton ID="btnLogin0" runat="server" ImageUrl="images/login/logout-bt.png"
                                                        Style="cursor: pointer; outline: 0;" />
                                        </td>
                                </tr>
                        </table>
                </div>
                </form>
        </center>
</body>
</html>
