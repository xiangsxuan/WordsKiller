<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>记单词</h1>
        <p class="lead">接着上次的学习进度记忆单词</p>
        <p><a href="toMemoryWords.aspx" class="btn btn-primary btn-lg">&nbsp;study &raquo;</a></p>
    </div>

    <div class="row">
        <div class="col-md-4">
            <h2>学习进度</h2>
            <p>
                您的付出不会没人知道,我们历历在目
            </p>
            <p>
                <a class="btn btn-default" href="learningRecord.aspx">&nbsp;前往&raquo;</a>
            </p>
        </div>
        <div class="col-md-4">
            <h2>调整词库</h2>
            <p>
                当您改变学习计划时,可以在这里进行更改
            </p>
            <p>
                <a class="btn btn-default" href="memListManage.aspx">&nbsp;前往&raquo;</a>
            </p>
        </div>
        <div class="col-md-4">
            <h2>同难度文章阅读</h2>
            <p>
                提供与您的词汇量相匹难度的文章
            </p>
            <p>
                <a class="btn btn-default" href="article.aspx">&nbsp;前往&raquo;</a>
            </p>
        </div>
    </div>
</asp:Content>
