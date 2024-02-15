<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Concessionaria_Auto._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <main class="d-flex flex-column align-items-center">

        <h1 class="text-center">Benvenuto nella tua concessionaria</h1>

        <%-- qui scelgo la macchina --%>
        <asp:DropDownList ID="CarChoice"
            runat="server"
            CssClass="form-select form-select-lg mb-3 text-center"
            AutoPostBack="true"
            OnSelectedIndexChanged="CarChoice_SelectedIndexChanged">
            <asp:ListItem Selected="True" Text="Scegli la tua auto"></asp:ListItem>
        </asp:DropDownList>

        <%--qui vado ad inserire il div che conterrà la card selezionata--%>
        <div id="CardCarSelected" runat="server"></div>

    </main>

</asp:Content>
