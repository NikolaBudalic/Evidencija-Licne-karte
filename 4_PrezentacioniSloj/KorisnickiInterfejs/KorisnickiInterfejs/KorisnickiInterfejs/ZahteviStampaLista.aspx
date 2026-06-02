<%@ Page Title="" Language="C#" MasterPageFile="~/StampaZaglavlje.Master" AutoEventWireup="true" CodeBehind="ZahteviStampaLista.aspx.cs" Inherits="KorisnickiInterfejs.ZahteviStampaLista" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

    <style type="text/css">

        .naslov {
            font-size: x-large;
            font-weight: bold;
            color: #003366;
        }

    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <table style="width:95%; margin:auto;">

        <tr>
            <td style="text-align:center;">
                <span class="naslov">
                    FILTRIRANI SPISAK ZAHTEVA
                </span>
            </td>
        </tr>

        <tr>
            <td>&nbsp;</td>
        </tr>

        <tr>
            <td>

                <asp:GridView ID="SpisakZahtevaGridView"
                    runat="server"
                    Width="100%"
                    AutoGenerateColumns="False"
                    GridLines="Both"
                    CellPadding="5">

                    <Columns>

                        <asp:BoundField DataField="JMBG" HeaderText="JMBG" />

                        <asp:BoundField DataField="Ime" HeaderText="Ime" />

                        <asp:BoundField DataField="Prezime" HeaderText="Prezime" />

                        <asp:BoundField
                            DataField="DatumPodnosenja"
                            HeaderText="Datum"
                            DataFormatString="{0:dd.MM.yyyy}" />

                        <asp:BoundField
                            DataField="RazlogIzdavanja"
                            HeaderText="Razlog" />

                        <asp:BoundField
                            DataField="TipZahteva"
                            HeaderText="Tip" />

                        <asp:BoundField
                            DataField="MestoPodnosenja"
                            HeaderText="Mesto" />

                        <asp:BoundField
                            DataField="StatusZahteva"
                            HeaderText="Status" />

                    </Columns>

                </asp:GridView>

            </td>
        </tr>

    </table>

</asp:Content>