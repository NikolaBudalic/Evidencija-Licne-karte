<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="ZahteviTabelarni.aspx.cs" Inherits="KorisnickiInterfejs.ZahteviTabelarni" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

    <style type="text/css">

        .style1 {
            width: 220px;
            text-align: right;
        }

        .naslov {
            font-size: large;
            font-weight: bold;
        }

    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <table style="width:95%; margin:auto;">

        <tr>
            <td class="style1">&nbsp;</td>

            <td class="naslov">
                TABELARNI PRIKAZ ZAHTEVA
            </td>

            <td>&nbsp;</td>
        </tr>

        <tr>
            <td>&nbsp;</td>
        </tr>

        <tr>

            <td class="style1">
                Filter statusa:
            </td>

            <td>

                <asp:TextBox ID="FilterTextBox" runat="server"></asp:TextBox>

                <asp:Button ID="FiltrirajButton"
                    runat="server"
                    OnClick="FiltrirajButton_Click"
                    Text="FILTRIRAJ" />

                <asp:Button ID="SviButton"
                    runat="server"
                    OnClick="SviButton_Click"
                    Text="SVI"
                    Width="68px" />

            </td>

            <td>&nbsp;</td>

        </tr>

        <tr>
            <td>&nbsp;</td>
        </tr>

        <tr>

            <td colspan="3">          

                <asp:GridView ID="SpisakZahtevaGridView"
                    runat="server"
                    AutoGenerateColumns="False"
                    Width="100%"
                    GridLines="Both"
                    CellPadding="5"
                    Font-Size="Small"
                    BorderStyle="Solid"
                    BorderWidth="1px"
                    HeaderStyle-BackColor="#50739C"
                    HeaderStyle-ForeColor="White"
                    RowStyle-BackColor="#F5F5F5"
                    AlternatingRowStyle-BackColor="#E8E8E8">

                    <Columns>
                        <asp:HyperLinkField
                         HeaderText="Detalji"
                         Text="DETALJI"
                         DataNavigateUrlFields="IDZahteva"
                         DataNavigateUrlFormatString="ZahteviDetaljiEdit.aspx?IDZahteva={0}" />

                        <asp:HyperLinkField
                         HeaderText="Štampa"
                         Text="ŠTAMPA"
                         DataNavigateUrlFields="IDZahteva"
                         DataNavigateUrlFormatString="ZahteviStampa.aspx?IDZahteva={0}" />

                        <asp:BoundField DataField="JMBG" HeaderText="JMBG">
                            <ItemStyle Width="140px" />
                        </asp:BoundField>

                        <asp:BoundField DataField="Ime" HeaderText="Ime">
                            <ItemStyle Width="80px" />
                        </asp:BoundField>

                        <asp:BoundField DataField="Prezime" HeaderText="Prezime">
                            <ItemStyle Width="100px" />
                        </asp:BoundField>

                        <asp:BoundField DataField="DatumPodnosenja"
                            HeaderText="Datum podnošenja"
                            DataFormatString="{0:dd.MM.yyyy}">
                            <ItemStyle Width="140px" />
                        </asp:BoundField>

                        <asp:BoundField DataField="RazlogIzdavanja"
                            HeaderText="Razlog">
                            <ItemStyle Width="140px" />
                        </asp:BoundField>

                        <asp:BoundField DataField="TipZahteva"
                            HeaderText="Tip">
                            <ItemStyle Width="80px" />
                        </asp:BoundField>

                        <asp:BoundField DataField="MestoPodnosenja"
                            HeaderText="Mesto">
                            <ItemStyle Width="120px" />
                        </asp:BoundField>

                        <asp:BoundField DataField="StatusZahteva"
                            HeaderText="Status">
                            <ItemStyle Width="100px" />
                        </asp:BoundField>

                    </Columns>

                </asp:GridView>

            </td>

            <td>&nbsp;</td>

        </tr>

    </table>

</asp:Content>