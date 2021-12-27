<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMestra.Master" AutoEventWireup="true" CodeBehind="WebFrase.aspx.cs" Inherits="WebFrases.WebFrase" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="Panel1" runat="server">
    <asp:Label ID="Label1" runat="server" Text="Id"></asp:Label>
    <br />
    <asp:TextBox ID="txtId" runat="server" Width="145px" Enabled="False"></asp:TextBox>
    <br />
    <asp:Label ID="Label2" runat="server" Text="Frase"></asp:Label>
    <br />
    <asp:TextBox ID="txtFrase" runat="server" Width="277px"></asp:TextBox>
    <br />
    <asp:Label ID="Label3" runat="server" Text="Autor"></asp:Label>
    <br />
    <asp:DropDownList ID="dpAutor" runat="server" AutoPostBack="True" Width="169px">
    </asp:DropDownList>
    <br />
    <asp:Label ID="Label4" runat="server" Text="Categoria"></asp:Label>
    <br />
    <asp:DropDownList ID="dpCategoria" runat="server" Width="169px">
    </asp:DropDownList>
    <asp:Button ID="btnInserir" runat="server" Text="Inserir" OnClick="btnInserir_Click" />
    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" OnClick="btnCancelar_Click" />
    <br />
    <br />
    <asp:GridView ID="GridView1" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False" OnRowDeleting="GridView1_RowDeleting" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" Width="720px">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:CommandField ShowSelectButton="True" />
            <asp:CommandField ShowDeleteButton="True" />
            <asp:BoundField DataField="Id" HeaderText="Id" />
            <asp:BoundField DataField="frase" HeaderText="Frase" />
            <asp:BoundField DataField="nome" HeaderText="Autor" />
            <asp:BoundField DataField="descricao" HeaderText="Categoria" />
        </Columns>
        <EditRowStyle BackColor="#2461BF" />
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#EFF3FB" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        <SortedAscendingCellStyle BackColor="#F5F7FB" />
        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
        <SortedDescendingCellStyle BackColor="#E9EBEF" />
        <SortedDescendingHeaderStyle BackColor="#4870BE" />
    </asp:GridView>
</asp:Panel>
</asp:Content>
