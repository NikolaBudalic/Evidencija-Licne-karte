<%@ Page Title="" Language="C#" MasterPageFile="~/StampaZaglavlje.Master" AutoEventWireup="true" CodeBehind="ZahteviStampa.aspx.cs" Inherits="KorisnickiInterfejs.ZahteviStampa" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

    <style type="text/css">

        .naslov {
            font-size: x-large;
            font-weight: bold;
            color: #003366;
        }

        .sekcijaNaslov {
            font-weight: bold;
            font-size: large;
            color: #003366;
        }

        .labela {
            width: 260px;
            font-weight: bold;
            text-align: right;
            padding-right: 10px;
        }

        .vrednost {
            width: 400px;
        }

        .linija {
            border-top: 1px solid #999999;
        }

    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

   <table style="width:90%; margin:auto; border-collapse:collapse;">

    <tr>
        <td colspan="4" style="text-align:center; font-size:32px; font-weight:bold; padding:25px;">
            ZAHTEV ZA IZRADU LIČNE KARTE
        </td>
    </tr>

    <!-- 1. LICNI PODACI -->

    <tr>
        <td colspan="4"
            style="font-weight:bold; font-size:22px; padding-top:20px; padding-bottom:10px;">
            1. LIČNI PODACI GRAĐANINA
        </td>
    </tr>

    <tr>
        <td style="border:1px solid black; padding:8px;"><b>Ime:</b></td>
        <td style="border:1px solid black; padding:8px;">
            <asp:Label ID="ImePrezimeLabel" runat="server"></asp:Label>
        </td>

        <td style="border:1px solid black; padding:8px;"><b>Prezime:</b></td>
        <td style="border:1px solid black; padding:8px;">
            <asp:Label ID="PrezimeLabel" runat="server"></asp:Label>
        </td>
    </tr>

    <tr>
        <td style="border:1px solid black; padding:8px;"><b>JMBG:</b></td>
        <td style="border:1px solid black; padding:8px;">
            <asp:Label ID="JMBGLabel" runat="server"></asp:Label>
        </td>

        <td style="border:1px solid black; padding:8px;"><b>Datum rođenja:</b></td>
        <td style="border:1px solid black; padding:8px;">
            <asp:Label ID="DatumRodjenjaLabel" runat="server"></asp:Label>
        </td>
    </tr>

    <tr>
        <td style="border:1px solid black; padding:8px;"><b>Pol:</b></td>
        <td style="border:1px solid black; padding:8px;">
            <asp:Label ID="PolLabel" runat="server"></asp:Label>
        </td>

        <td style="border:1px solid black; padding:8px;"><b>Državljanstvo:</b></td>
        <td style="border:1px solid black; padding:8px;">
            <asp:Label ID="DrzavljanstvoLabel" runat="server"></asp:Label>
        </td>
    </tr>

    <tr>
        <td style="border:1px solid black; padding:8px;"><b>Broj stare LK:</b></td>
        <td style="border:1px solid black; padding:8px;">
            <asp:Label ID="BrojStareLKLabel" runat="server"></asp:Label>
        </td>

        <td style="border:1px solid black; padding:8px;"><b>Datum isteka LK:</b></td>
        <td style="border:1px solid black; padding:8px;">
            <asp:Label ID="DatumIstekaLKLabel" runat="server"></asp:Label>
        </td>
    </tr>

    <tr>
        <td style="border:1px solid black; padding:8px;"><b>Broj nove LK:</b></td>
        <td style="border:1px solid black; padding:8px;">
            <asp:Label ID="BrojNoveLKLabel" runat="server"></asp:Label>
        </td>

        <td style="border:1px solid black; padding:8px;"><b>Datum isteka nove LK:</b></td>
        <td style="border:1px solid black; padding:8px;">
            <asp:Label ID="DatumIstekaNoveLKLabel" runat="server"></asp:Label>
        </td>
    </tr>

    <tr>
        <td style="border:1px solid black; padding:8px;"><b>Adresa prebivališta:</b></td>
        <td colspan="3" style="border:1px solid black; padding:8px;">
            <asp:Label ID="AdresaLabel" runat="server"></asp:Label>
        </td>
    </tr>

    <tr>
        <td style="border:1px solid black; padding:8px;"><b>Kontakt telefon:</b></td>
        <td style="border:1px solid black; padding:8px;">
            <asp:Label ID="TelefonLabel" runat="server"></asp:Label>
        </td>

        <td style="border:1px solid black; padding:8px;"><b>E-mail:</b></td>
        <td style="border:1px solid black; padding:8px;">
            <asp:Label ID="EmailLabel" runat="server"></asp:Label>
        </td>
    </tr>

    <!-- 2. PODACI O ZAHTEVU -->

    <tr>
        <td colspan="4"
            style="font-weight:bold; font-size:22px; padding-top:35px; padding-bottom:10px;">
            2. PODACI O LIČNOJ KARTI
        </td>
    </tr>

    <tr>
        <td colspan="4" style="border:1px solid black; padding:8px;">
            <b>Datum podnošenja zahteva:</b>
            <asp:Label ID="DatumPodnosenjaLabel" runat="server"></asp:Label>
        </td>
    </tr>

    <tr>
        <td colspan="4" style="border:1px solid black; padding:8px;">
            <b>Razlog izdavanja:</b>
            <asp:Label ID="RazlogLabel" runat="server"></asp:Label>
        </td>
    </tr>

    <tr>
        <td colspan="4" style="border:1px solid black; padding:8px;">
            <b>Tip zahteva:</b>
            <asp:Label ID="TipLabel" runat="server"></asp:Label>
        </td>
    </tr>

    <tr>
        <td colspan="4" style="border:1px solid black; padding:8px;">
            <b>Mesto podnošenja zahteva:</b>
            <asp:Label ID="MestoLabel" runat="server"></asp:Label>
        </td>
    </tr>

    <tr>
        <td colspan="4" style="border:1px solid black; padding:8px;">
            <b>Status zahteva:</b>
            <asp:Label ID="StatusZahtevaLabel" runat="server"></asp:Label>
        </td>
    </tr>

    <!-- 3. RODITELJ -->

    <tr>
        <td colspan="4"
            style="font-weight:bold; font-size:22px; padding-top:35px; padding-bottom:10px;">
            3. PODACI O RODITELJU ILI STARATELJU
        </td>
    </tr>

    <tr>
        <td colspan="4"
            style="border:1px solid black; padding:8px; text-align:center; background-color:#EEEEEE;">
            Popunjava se samo ako osoba koja podnosi zahtev ima manje od 18 godina.
        </td>
    </tr>

    <tr>
        <td colspan="4" style="border:1px solid black; padding:8px;">
            <b>Ime i prezime roditelja / staratelja:</b>
            <asp:Label ID="RoditeljImePrezimeLabel" runat="server"></asp:Label>
        </td>
    </tr>

    <tr>
        <td colspan="2" style="border:1px solid black; padding:8px;">
            <b>JMBG roditelja / staratelja:</b>
            <asp:Label ID="RoditeljJMBGLabel" runat="server"></asp:Label>
        </td>

        <td colspan="2" style="border:1px solid black; padding:8px;">
            <b>Srodstvo:</b>
            <asp:Label ID="SrodstvoLabel" runat="server"></asp:Label>
        </td>
    </tr>

    <tr>
        <td colspan="2" style="border:1px solid black; padding:8px;">
            <b>Kontakt telefon:</b>
            <asp:Label ID="RoditeljTelefonLabel" runat="server"></asp:Label>
        </td>

        <td colspan="2" style="border:1px solid black; padding:8px;">
            <b>E-mail:</b>
            <asp:Label ID="RoditeljEmailLabel" runat="server"></asp:Label>
        </td>
    </tr>

    <!-- 4. DOKUMENTACIJA -->

    <tr>
        <td colspan="4"
            style="font-weight:bold; font-size:22px; padding-top:35px; padding-bottom:10px;">
            4. DOKUMENTACIJA
        </td>
    </tr>

    <tr>
        <td colspan="4">

            <asp:GridView ID="DokumentacijaGridView"
                runat="server"
                Width="100%"
                AutoGenerateColumns="False"
                GridLines="Both"
                CellPadding="5">

                <Columns>

                    <asp:BoundField
                        DataField="NazivDokumenta"
                        HeaderText="Dokument" />

                    <asp:CheckBoxField
                        DataField="Dostavljeno"
                        HeaderText="Dostavljeno" />

                </Columns>

            </asp:GridView>

        </td>
    </tr>

    <!-- 5. NAPOMENE -->

    <tr>
        <td colspan="4"
            style="font-weight:bold; font-size:22px; padding-top:35px; padding-bottom:10px;">
            5. NAPOMENE
        </td>
    </tr>

    <tr>
        <td colspan="4"
            style="border:1px solid black; height:120px; vertical-align:top; padding:10px;">
            <asp:Label ID="NapomenaLabel" runat="server"></asp:Label>
        </td>
    </tr>

    <!-- 6. POTPISI -->

    <tr>
        <td colspan="4"
            style="font-weight:bold; font-size:22px; padding-top:35px; padding-bottom:10px;">
            6. POTPISI
        </td>
    </tr>

    <tr>
        <td style="text-align:center; padding-top:60px;">
            _____________________<br />
            Podnosilac zahteva
        </td>

        <td colspan="2" style="text-align:center; padding-top:60px;">
            _____________________<br />
            Roditelj / staratelj
        </td>

        <td colspan="2" style="text-align:center; padding-top:60px;">
            _____________________<br />
            Službeno lice
        </td>
    </tr>

    <tr>
        <td style="padding-top:50px; text-align:center;">
            Datum: ______________
        </td>

        <td colspan="2" style="padding-top:50px; text-align:center;">
            Datum: ______________
        </td>

        <td colspan="2" style="padding-top:50px; text-align:center;">
            Datum: ______________
        </td>
    </tr>

</table>

</asp:Content>