<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="KorisnickiInterfejs._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <p>
    Dobrodosli,
        <asp:Label ID="lblStatusKonekcije" runat="server" Text="STATUS KONEKCIJE"></asp:Label>
        !</p>
</asp:Content>
