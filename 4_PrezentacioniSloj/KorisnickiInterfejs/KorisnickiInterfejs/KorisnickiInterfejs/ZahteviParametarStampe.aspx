<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="ZahteviParametarStampe.aspx.cs" Inherits="KorisnickiInterfejs.ZahteviParametarStampe" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .style1 {
            text-align: right;
            width: 250px;
        }

        .naslov {
            color: #0000CC;
            font-weight: bold;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table style="width:95%; margin:auto;">
        <tr>
            <td class="style1">&nbsp;</td>
            <td>&nbsp;</td>
        </tr>

        <tr>
            <td class="style1">&nbsp;</td>
            <td class="naslov">
                IZBOR PARAMETRA ŠTAMPE SPISKA ZAHTEVA
            </td>
        </tr>

        <tr>
            <td class="style1">&nbsp;</td>
            <td>&nbsp;</td>
        </tr>

        <tr>
            <td class="style1">
                Status zahteva:
            </td>

            <td>
                <asp:DropDownList ID="StatusDropDownList" runat="server">
                    <asp:ListItem>U obradi</asp:ListItem>
                    <asp:ListItem>Na proveri</asp:ListItem>
                    <asp:ListItem>Odobren</asp:ListItem>
                    <asp:ListItem>Odbijen</asp:ListItem>
                    <asp:ListItem>Neispravan zahtev</asp:ListItem>
                </asp:DropDownList>

                <asp:Button ID="FilterStampaButton"
                    runat="server"
                    OnClick="FilterStampaButton_Click"
                    Text="PRIKAŽI FILTRIRANI SPISAK ZA ŠTAMPU"
                    Width="330px" />
            </td>
        </tr>
    </table>
</asp:Content>