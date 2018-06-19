<%@ Page Title="单词杀" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="toMemoryWords.aspx.cs" Inherits="About" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
 
    <div class="jumbotron">
        <h1><%=word %></h1>
        <p class="lead"><%=explanation %></p>
        <p>
            <asp:Button ID="Button1" runat="server" class="btn btn-primary btn-lg" Text="记住了,下一个" OnClick="Button2_Click" />
        </p>
    </div>
</asp:Content>
