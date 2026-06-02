<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="ZahteviUnos.aspx.cs" Inherits="KorisnickiInterfejs.ZahteviUnos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .style1 {
            width: 250px;
            text-align: right;
            padding-right: 10px;
        }

        .validator {
            color: red;
            margin-left: 8px;
        }

        .unos {
            width: 180px;
        }

        .unos-sirok {
            width: 300px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table style="width:95%; margin:auto;">
        <tr>
            <td class="style1">&nbsp;</td>
            <td><b>UNOS ZAHTEVA ZA IZDAVANJE LIČNE KARTE</b></td>
        </tr>

        <tr>
            <td class="style1">JMBG građanina:</td>
            <td>
                <asp:TextBox ID="JMBGGradjaninaTextBox" runat="server" CssClass="unos"></asp:TextBox>

                <asp:RequiredFieldValidator ID="JMBGGradjaninaRequiredValidator" runat="server"
                    ControlToValidate="JMBGGradjaninaTextBox"
                    ErrorMessage="* JMBG građanina je obavezan"
                    CssClass="validator"
                    Display="Dynamic">
                </asp:RequiredFieldValidator>

                <asp:RegularExpressionValidator ID="JMBGGradjaninaRegexValidator" runat="server"
                    ControlToValidate="JMBGGradjaninaTextBox"
                    ValidationExpression="^\d{13}$"
                    ErrorMessage="* JMBG mora imati 13 cifara"
                    CssClass="validator"
                    Display="Dynamic">
                </asp:RegularExpressionValidator>
            </td>
        </tr>

        <tr>
            <td class="style1">Datum podnošenja:</td>
            <td>
                <asp:TextBox ID="DatumPodnosenjaTextBox" runat="server" TextMode="Date" CssClass="unos"></asp:TextBox>

                <asp:RequiredFieldValidator ID="DatumPodnosenjaValidator" runat="server"
                    ControlToValidate="DatumPodnosenjaTextBox"
                    ErrorMessage="* Datum podnošenja je obavezan"
                    CssClass="validator"
                    Display="Dynamic">
                </asp:RequiredFieldValidator>
            </td>
        </tr>

        <tr>
            <td class="style1">Razlog izdavanja:</td>
            <td>
                <asp:DropDownList ID="RazlogIzdavanjaDropDownList" runat="server">
                    <asp:ListItem>Prvo izdavanje</asp:ListItem>
                    <asp:ListItem>Zamena</asp:ListItem>
                    <asp:ListItem>Gubitak</asp:ListItem>
                    <asp:ListItem>Oštećenje</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>

        <tr>
            <td class="style1">Tip zahteva:</td>
            <td>
                <asp:DropDownList ID="TipZahtevaDropDownList" runat="server">
                    <asp:ListItem>Redovan</asp:ListItem>
                    <asp:ListItem>Hitan</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>

        <tr>
            <td class="style1">Mesto podnošenja:</td>
            <td>
                <asp:TextBox ID="MestoPodnosenjaTextBox" runat="server" CssClass="unos"></asp:TextBox>

                <asp:RequiredFieldValidator ID="MestoPodnosenjaValidator" runat="server"
                    ControlToValidate="MestoPodnosenjaTextBox"
                    ErrorMessage="* Mesto podnošenja je obavezno"
                    CssClass="validator"
                    Display="Dynamic">
                </asp:RequiredFieldValidator>
            </td>
        </tr>

        <tr>
            <td class="style1">Napomena:</td>
            <td>
                <asp:TextBox ID="NapomenaTextBox" runat="server" TextMode="MultiLine" Width="300px" Height="70px"></asp:TextBox>
            </td>
        </tr>

        <tr>
            <td class="style1">&nbsp;</td>
            <td><b>PODACI O RODITELJU / STARATELJU</b></td>
        </tr>

        <tr>
            <td class="style1">Ime i prezime roditelja/staratelja:</td>
            <td>
                <asp:TextBox ID="RoditeljImePrezimeTextBox" runat="server" CssClass="unos-sirok"></asp:TextBox>
            </td>
        </tr>

        <tr>
            <td class="style1">JMBG roditelja/staratelja:</td>
            <td>
                <asp:TextBox ID="RoditeljJMBGTextBox" runat="server" CssClass="unos"></asp:TextBox>

                <asp:RegularExpressionValidator ID="RoditeljJMBGRegexValidator" runat="server"
                    ControlToValidate="RoditeljJMBGTextBox"
                    ValidationExpression="^\d{13}$"
                    ErrorMessage="* JMBG roditelja mora imati 13 cifara"
                    CssClass="validator"
                    Display="Dynamic">
                </asp:RegularExpressionValidator>
            </td>
        </tr>

        <tr>
            <td class="style1">Srodstvo:</td>
            <td>
                <asp:TextBox ID="SrodstvoTextBox" runat="server" CssClass="unos"></asp:TextBox>
            </td>
        </tr>

        <tr>
            <td class="style1">Kontakt telefon:</td>
            <td>
                <asp:TextBox ID="RoditeljTelefonTextBox" runat="server" CssClass="unos"></asp:TextBox>

                <asp:RegularExpressionValidator ID="RoditeljTelefonValidator" runat="server"
                    ControlToValidate="RoditeljTelefonTextBox"
                    ValidationExpression="^[0-9\/\-\+\s]{6,20}$"
                    ErrorMessage="* Neispravan telefon"
                    CssClass="validator"
                    Display="Dynamic">
                </asp:RegularExpressionValidator>
            </td>
        </tr>

        <tr>
            <td class="style1">E-mail:</td>
            <td>
                <asp:TextBox ID="RoditeljEmailTextBox" runat="server" CssClass="unos"></asp:TextBox>

                <asp:RegularExpressionValidator ID="RoditeljEmailValidator" runat="server"
                    ControlToValidate="RoditeljEmailTextBox"
                    ValidationExpression="\w+([-.+']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                    ErrorMessage="* Neispravan email"
                    CssClass="validator"
                    Display="Dynamic">
                </asp:RegularExpressionValidator>
            </td>
        </tr>

        <tr>
            <td class="style1">&nbsp;</td>
            <td>
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" />
                <asp:Label ID="StatusLabel" runat="server" Text=""></asp:Label>
            </td>
        </tr>

        <tr>
            <td class="style1">&nbsp;</td>
            <td>
                <asp:Button ID="SnimiButton" runat="server" OnClick="SnimiButton_Click" Text="SNIMI" Width="90px" />
                <asp:Button ID="OdustaniButton" runat="server" OnClick="OdustaniButton_Click" Text="ODUSTANI" CausesValidation="False" />
            </td>
        </tr>
    </table>
</asp:Content>