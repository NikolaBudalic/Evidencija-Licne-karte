<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="ZahteviDetaljiEdit.aspx.cs" Inherits="KorisnickiInterfejs.ZahteviDetaljiEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .style1 {
            width: 230px;
            text-align: right;
            padding-right: 10px;
        }

        .style2 {
            width: 420px;
        }

        .validator {
            color: red;
            margin-left: 8px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <asp:Button ID="NazadButton" runat="server" Text="NAZAD" PostBackUrl="~/ZahteviTabelarni.aspx" CausesValidation="False" />

    <table style="width:95%; margin:auto;">

        <tr>
            <td class="style1">&nbsp;</td>
            <td class="style2"><b>DETALJNI PRIKAZ ZAHTEVA</b></td>
            <td>&nbsp;</td>
        </tr>

        <tr>
            <td class="style1">ID zahteva:</td>
            <td class="style2">
                <asp:TextBox ID="IDZahtevaTextBox" runat="server" Enabled="False"></asp:TextBox>
            </td>
            <td>&nbsp;</td>
        </tr>

        <tr>
            <td class="style1">JMBG građanina:</td>
            <td class="style2">
                <asp:TextBox ID="JMBGGradjaninaTextBox" runat="server" Enabled="False"></asp:TextBox>
            </td>
            <td>&nbsp;</td>
        </tr>

        <tr>
            <td class="style1">Ime i prezime:</td>
            <td class="style2">
                <asp:TextBox ID="ImePrezimeTextBox" runat="server" Enabled="False" Width="260px"></asp:TextBox>
            </td>
            <td>&nbsp;</td>
        </tr>

        <tr>
            <td class="style1">Datum podnošenja:</td>
            <td class="style2">
                <asp:TextBox ID="DatumPodnosenjaTextBox" runat="server" Enabled="False"></asp:TextBox>
            </td>
            <td>&nbsp;</td>
        </tr>

        <tr>
            <td class="style1">Razlog izdavanja:</td>
            <td class="style2">
                <asp:TextBox ID="RazlogIzdavanjaTextBox" runat="server" Enabled="False"></asp:TextBox>
            </td>
            <td>&nbsp;</td>
        </tr>

        <tr>
            <td class="style1">Tip zahteva:</td>
            <td class="style2">
                <asp:TextBox ID="TipZahtevaTextBox" runat="server" Enabled="False"></asp:TextBox>
            </td>
            <td>&nbsp;</td>
        </tr>

        <tr>
            <td class="style1">Mesto podnošenja:</td>
            <td class="style2">
                <asp:TextBox ID="MestoPodnosenjaTextBox" runat="server" Enabled="False"></asp:TextBox>
            </td>
            <td>&nbsp;</td>
        </tr>

        <tr>
            <td class="style1">Status zahteva:</td>
            <td class="style2">
                <asp:DropDownList ID="StatusZahtevaDropDownList" runat="server" Enabled="False">
                    <asp:ListItem>U obradi</asp:ListItem>
                    <asp:ListItem>Na proveri</asp:ListItem>
                    <asp:ListItem>Odobren</asp:ListItem>
                    <asp:ListItem>Odbijen</asp:ListItem>
                    <asp:ListItem>Neispravan zahtev</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>&nbsp;</td>
        </tr>

        <tr>
            <td class="style1">Napomena:</td>
            <td class="style2">
                <asp:TextBox ID="NapomenaTextBox" runat="server" TextMode="MultiLine" Width="300px" Height="70px" Enabled="False"></asp:TextBox>

                <asp:RegularExpressionValidator ID="NapomenaValidator" runat="server"
                    ControlToValidate="NapomenaTextBox"
                    ValidationExpression="^.{0,300}$"
                    ErrorMessage="* Napomena može imati najviše 300 karaktera"
                    CssClass="validator"
                    Display="Dynamic">
                </asp:RegularExpressionValidator>
            </td>
            <td>&nbsp;</td>
        </tr>

        <tr>
            <td class="style1">&nbsp;</td>
            <td class="style2">
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" />
                <asp:Label ID="StatusLabel" runat="server" Text=""></asp:Label>
            </td>
            <td>&nbsp;</td>
        </tr>

        <tr>
            <td class="style1">&nbsp;</td>
            <td class="style2">
                <asp:Button ID="IzmeniButton" runat="server" Text="OMOGUĆI IZMENU" OnClick="IzmeniButton_Click" CausesValidation="False" />
                <asp:Button ID="SnimiIzmenuButton" runat="server" Text="SNIMI IZMENU" OnClick="SnimiIzmenuButton_Click" />
                <asp:Button ID="ObrisiButton" runat="server" Text="OBRIŠI" OnClick="ObrisiButton_Click" CausesValidation="False" />
            </td>
            <td>&nbsp;</td>
        </tr>

        <tr>
            <td colspan="3"><hr /><b>DOKUMENTACIJA UZ ZAHTEV</b></td>
        </tr>

        <tr>
            <td colspan="3">
                <asp:GridView ID="DokumentacijaGridView" runat="server"
                    AutoGenerateColumns="False"
                    DataKeyNames="IDDokumentacije"
                    Width="700px"
                    GridLines="Both"
                    CellPadding="5">

                    <Columns>
                        <asp:BoundField DataField="NazivDokumenta" HeaderText="Dokument" />

                        <asp:TemplateField HeaderText="Dostavljeno">
                            <ItemTemplate>
                                <asp:CheckBox ID="DostavljenoCheckBox" runat="server"
                                    Checked='<%# Convert.ToBoolean(Eval("Dostavljeno")) %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>

        <tr>
            <td colspan="3" style="text-align:center; padding-top:10px; padding-bottom:10px;">
                <asp:Button ID="SacuvajDokumentacijuButton" runat="server"
                    Text="SAČUVAJ DOKUMENTACIJU"
                    OnClick="SacuvajDokumentacijuButton_Click"
                    CausesValidation="False" />
            </td>
        </tr>

        <tr>
            <td colspan="3"><hr /><b>ISTORIJA STATUSA ZAHTEVA</b></td>
        </tr>

        <tr>
            <td colspan="3">
                <asp:GridView ID="IstorijaStatusaGridView" runat="server"
                    AutoGenerateColumns="False"
                    Width="850px"
                    GridLines="Both"
                    CellPadding="5">

                    <Columns>
                        <asp:BoundField DataField="StariStatus" HeaderText="Stari status" />
                        <asp:BoundField DataField="NoviStatus" HeaderText="Novi status" />
                        <asp:BoundField DataField="DatumPromene" HeaderText="Datum promene" DataFormatString="{0:dd.MM.yyyy HH:mm}" />
                        <asp:BoundField DataField="Korisnik" HeaderText="Korisnik" />
                        <asp:BoundField DataField="Napomena" HeaderText="Napomena" />
                    </Columns>
                </asp:GridView>
            </td>
        </tr>

    </table>
</asp:Content>