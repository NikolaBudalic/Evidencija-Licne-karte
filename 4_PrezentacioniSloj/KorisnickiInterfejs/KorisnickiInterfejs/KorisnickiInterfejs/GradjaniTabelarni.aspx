<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="GradjaniTabelarni.aspx.cs" Inherits="KorisnickiInterfejs.GradjaniTabelarni" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .naslov {
            font-size: large;
            font-weight: bold;
            text-align: center;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <table style="width:95%; margin:auto;">
        <tr>
            <td class="naslov">
                TABELARNI PRIKAZ GRAĐANA
            </td>
        </tr>

        <tr>
            <td style="text-align:center; padding:10px;">
                Filter JMBG / ime / prezime:
                <asp:TextBox ID="FilterTextBox" runat="server"></asp:TextBox>

                <asp:Button ID="FiltrirajButton" runat="server"
                    Text="FILTRIRAJ"
                    OnClick="FiltrirajButton_Click" />

                <asp:Button ID="SviButton" runat="server"
                    Text="SVI"
                    OnClick="SviButton_Click" />
            </td>
        </tr>

        <tr>
            <td>
                <asp:GridView ID="GradjaniGridView" runat="server"
                    AutoGenerateColumns="False"
                    Width="100%"
                    GridLines="Both"
                    CellPadding="5"
                    HeaderStyle-BackColor="#50739C"
                    HeaderStyle-ForeColor="White"
                    RowStyle-BackColor="#F5F5F5"
                    AlternatingRowStyle-BackColor="#E8E8E8">

                    <Columns>
                        <asp:HyperLinkField
                            HeaderText="Detalji"
                            Text="DETALJI"
                            DataNavigateUrlFields="JMBG"
                            DataNavigateUrlFormatString="GradjaniDetaljiEdit.aspx?JMBG={0}" />

                        <asp:BoundField DataField="JMBG" HeaderText="JMBG" />
                        <asp:BoundField DataField="Ime" HeaderText="Ime" />
                        <asp:BoundField DataField="Prezime" HeaderText="Prezime" />
                        <asp:BoundField DataField="DatumRodjenja" HeaderText="Datum rođenja" DataFormatString="{0:dd.MM.yyyy}" />
                        <asp:BoundField DataField="Pol" HeaderText="Pol" />
                        <asp:BoundField DataField="Drzavljanstvo" HeaderText="Državljanstvo" />
                        <asp:BoundField DataField="AdresaPrebivalista" HeaderText="Adresa" />
                    </Columns>

                </asp:GridView>
            </td>
        </tr>
    </table>

</asp:Content>