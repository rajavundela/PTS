<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="PTS.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 48%;
            background-color: #99FF99;
        }
        .auto-style6 {
            width: 166px;
            height: 38px;
            background-color: #006600;
        }
        .auto-style7 {
            table-layout: auto;
            height: 61px;
            width: 967px;
        }
        .auto-style8 {
            table-layout: auto;
            height: 22px;
            width: 967px;
        }
        .auto-style9 {
            background-color: #0099FF;
        }
        .auto-style10 {
            height: 328px;
        }
        .auto-style11 {
            table-layout: auto;
            height: 48px;
            width: 967px;
        }
        .auto-style14 {
            width: 166px;
            height: 36px;
            background-color: #006600;
        }
        .auto-style15 {
            width: 219px;
            height: 38px;
            background-color: #006600;
        }
        .auto-style16 {
            width: 219px;
            height: 36px;
            background-color: #006600;
        }
    </style>
</head>
<body style="height: 112px">
    <form id="form1" runat="server" class="auto-style10">
        <p class="auto-style7" style="font-family: Arial; background-image: url('Content/Media/ProfileImages/logo.png'); background-repeat: no-repeat;">
            &nbsp;</p>
        <p class="auto-style8" style="font-family: 'Bell MT'; color: #008000;">
            Generate OR Code&nbsp;&nbsp;&nbsp;&nbsp;
        </p>
        <table border="3" class="auto-style1">
            <tr>
                <td class="auto-style15" style="color: #FFFFFF"><strong>Feed the URL in QR</strong></td>
                <td class="auto-style6">
                    <asp:TextBox ID="TextBox1" runat="server" Width="160px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style16"></td>
                <td class="auto-style14">
                    <asp:Button ID="Button1" runat="server" CssClass="auto-style9" OnClick="Button1_Click" Text="Generate QR Code" Width="140px" />
                </td>
            </tr>
        </table>
        <p class="auto-style11" style="font-family: 'Bell MT'; color: #008000;" visible="False">
            Generated QR Code&nbsp; <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
        </p>
    </form>
</body>
</html>
