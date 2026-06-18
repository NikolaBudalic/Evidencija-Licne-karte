<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="GradjaniDetaljiEdit.aspx.cs" Inherits="KorisnickiInterfejs.GradjaniDetaljiEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .style1 {
            width: 230px;
            text-align: right;
            padding-right: 10px;
        }

        .unos {
            width: 180px;
        }

        .unos-sirok {
            width: 300px;
        }

        .validator {
            color: red;
            margin-left: 8px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <asp:Button ID="NazadButton" runat="server" Text="NAZAD" PostBackUrl="~/GradjaniTabelarni.aspx" CausesValidation="False" />

    <table style="width:95%; margin:auto; margin-top:20px;">
        <tr>
            <td class="style1">&nbsp;</td>
            <td><b>DETALJNI PRIKAZ GRAĐANINA</b></td>
        </tr>

        <tr>
            <td class="style1">JMBG:</td>
            <td>
                <asp:TextBox ID="JMBGTextBox" runat="server" Enabled="False" CssClass="unos"></asp:TextBox>

                <asp:RequiredFieldValidator ID="JMBGRequiredValidator" runat="server"
                    ControlToValidate="JMBGTextBox"
                    ErrorMessage="* JMBG je obavezan"
                    CssClass="validator"
                    Display="Dynamic">
                </asp:RequiredFieldValidator>

                <asp:RegularExpressionValidator ID="JMBGRegexValidator" runat="server"
                    ControlToValidate="JMBGTextBox"
                    ValidationExpression="^\d{13}$"
                    ErrorMessage="* JMBG mora imati 13 cifara"
                    CssClass="validator"
                    Display="Dynamic">
                </asp:RegularExpressionValidator>
            </td>
        </tr>

        <tr>
            <td class="style1">Ime:</td>
            <td>
                <asp:TextBox ID="ImeTextBox" runat="server" Enabled="False" CssClass="unos"></asp:TextBox>

                <asp:RequiredFieldValidator ID="ImeValidator" runat="server"
                    ControlToValidate="ImeTextBox"
                    ErrorMessage="* Ime je obavezno"
                    CssClass="validator"
                    Display="Dynamic">
                </asp:RequiredFieldValidator>
            </td>
        </tr>

        <tr>
            <td class="style1">Prezime:</td>
            <td>
                <asp:TextBox ID="PrezimeTextBox" runat="server" Enabled="False" CssClass="unos"></asp:TextBox>

                <asp:RequiredFieldValidator ID="PrezimeValidator" runat="server"
                    ControlToValidate="PrezimeTextBox"
                    ErrorMessage="* Prezime je obavezno"
                    CssClass="validator"
                    Display="Dynamic">
                </asp:RequiredFieldValidator>
            </td>
        </tr>

        <tr>
            <td class="style1">Datum rođenja:</td>
            <td>
                <asp:TextBox ID="DatumRodjenjaTextBox" runat="server" TextMode="Date" Enabled="False" CssClass="unos"></asp:TextBox>

                <asp:RequiredFieldValidator ID="DatumRodjenjaValidator" runat="server"
                    ControlToValidate="DatumRodjenjaTextBox"
                    ErrorMessage="* Datum rođenja je obavezan"
                    CssClass="validator"
                    Display="Dynamic">
                </asp:RequiredFieldValidator>
            </td>
        </tr>

        <tr>
            <td class="style1">Pol:</td>
            <td>
                <asp:DropDownList ID="PolDropDownList" runat="server" Enabled="False">
                    <asp:ListItem>Muski</asp:ListItem>
                    <asp:ListItem>Zenski</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>

        <tr>
            <td class="style1">Državljanstvo:</td>
            <td>
                <asp:TextBox ID="DrzavljanstvoTextBox" runat="server" Enabled="False" CssClass="unos"></asp:TextBox>

                <asp:RequiredFieldValidator ID="DrzavljanstvoValidator" runat="server"
                    ControlToValidate="DrzavljanstvoTextBox"
                    ErrorMessage="* Državljanstvo je obavezno"
                    CssClass="validator"
                    Display="Dynamic">
                </asp:RequiredFieldValidator>
            </td>
        </tr>

        <tr>
            <td class="style1">Adresa prebivališta:</td>
            <td>
                <asp:TextBox ID="AdresaTextBox" runat="server" Enabled="False" CssClass="unos-sirok"></asp:TextBox>

                <asp:RequiredFieldValidator ID="AdresaValidator" runat="server"
                    ControlToValidate="AdresaTextBox"
                    ErrorMessage="* Adresa je obavezna"
                    CssClass="validator"
                    Display="Dynamic">
                </asp:RequiredFieldValidator>
            </td>
        </tr>

        <tr>
            <td class="style1">Kontakt telefon:</td>
            <td>
                <asp:TextBox ID="TelefonTextBox" runat="server" Enabled="False" CssClass="unos"></asp:TextBox>

                <asp:RegularExpressionValidator ID="TelefonValidator" runat="server"
                    ControlToValidate="TelefonTextBox"
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
                <asp:TextBox ID="EmailTextBox" runat="server" Enabled="False" CssClass="unos"></asp:TextBox>

                <asp:RegularExpressionValidator ID="EmailValidator" runat="server"
                    ControlToValidate="EmailTextBox"
                    ValidationExpression="\w+([-.+']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                    ErrorMessage="* Neispravan email"
                    CssClass="validator"
                    Display="Dynamic">
                </asp:RegularExpressionValidator>
            </td>
        </tr>

        <tr>
            <td class="style1">Broj stare LK:</td>
            <td>
                <asp:TextBox ID="BrojStareLKTextBox" runat="server" Enabled="False" CssClass="unos"></asp:TextBox>
            </td>
        </tr>

        <tr>
            <td class="style1">Datum isteka LK:</td>
            <td>
                <asp:TextBox ID="DatumIstekaLKTextBox" runat="server" TextMode="Date" Enabled="False" CssClass="unos"></asp:TextBox>
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
                <asp:Button ID="IzmeniButton" runat="server" Text="OMOGUĆI IZMENU" OnClick="IzmeniButton_Click" CausesValidation="False" />
                <asp:Button ID="SnimiIzmenuButton" runat="server" Text="SNIMI IZMENU" OnClick="SnimiIzmenuButton_Click" />
                <asp:Button ID="ObrisiButton" runat="server" Text="OBRIŠI" OnClick="ObrisiButton_Click" CausesValidation="False" />
            </td>
        </tr>
    </table>

</asp:Content>