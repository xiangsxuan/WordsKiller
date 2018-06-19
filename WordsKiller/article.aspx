<%@ Page Title="About" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="article.aspx.cs" Inherits="About" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %>.同难度文章阅读</h2>
    <h3>提供与您的词汇量相匹难度的文章</h3>
    <p> 文章阅读界面
        TODO:从数据库检查文章没有生词后,推送给读者(或者根据单词难度来推送)
    </p>
</asp:Content>
