<%@ Page Title="" Language="C#" MasterPageFile="~/Login.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="KorisnickiInterfejs.Login1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .style1
        {
            width: 360px;
            text-align: right;
            margin-left: 160px;
        }
        .style2
        {
            font-size: medium;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table style="width:100%;">
        <tr>
            <td class="style1">
                &nbsp;</td>
            <td class="style2">
                <strong>PRIJAVA KORISNIKA</strong></td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style1">
                <asp:Label ID="Label1" runat="server" Text="Korisnicko ime"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="KorisnickoImeTextBox" runat="server" Width="165px"></asp:TextBox>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style1">
                <asp:Label ID="Label2" runat="server" Text="Sifra"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="SifraTextBox" runat="server" Width="164px" TextMode="Password"></asp:TextBox>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style1">
                &nbsp;</td>
            <td>
                <asp:Label ID="lblStatus" runat="server" Text="status"></asp:Label>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style1">
                &nbsp;</td>
            <td style="margin-left: 40px">
                <asp:Button ID="PrijavaButton" runat="server" onclick="PrijavaButton_Click" 
                    Text="PRIJAVA" />
                <asp:Button ID="OdustaniButton" runat="server" onclick="OdustaniButton_Click" 
                    Text="Odustani" />
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style1">
                &nbsp;</td>
            <td style="margin-left: 40px">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
</asp:Content>
