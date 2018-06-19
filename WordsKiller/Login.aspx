<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <h2><%: Title %>请填写以下</h2>

    <div class="row">
        <div class="col-md-8" style="left: 0px; top: 0px">
            <section id="loginForm">
                <div class="form-horizontal">
                    <h4>使用本地帐户登录。</h4>
                    <hr />
                    <asp:PlaceHolder runat="server" ID="ErrorMessage" Visible="false">
                        <p class="text-danger">
                            <asp:Literal runat="server" ID="FailureText" />
                        </p>
                    </asp:PlaceHolder>
                    <div class="form-group">
                        <asp:Label runat="server"  CssClass="col-md-2 control-label">邮箱/用户名</asp:Label>
                        <div class="col-md-10">
                            <asp:TextBox runat="server" ID="UserNameUserEmail" CssClass="form-control" >admin</asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="UserNameUseremail"
                                CssClass="text-danger" ErrorMessage="“邮箱/用户名”字段是必填字段。" />
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server"  CssClass="col-md-2 control-label">密码</asp:Label>
                        <div class="col-md-10">
                            <asp:TextBox runat="server" ID="Password" TextMode="Password" CssClass="form-control" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="Password" CssClass="text-danger" ErrorMessage="“密码”字段是必填字段。" />
                        </div>
                    </div>
                  
                    <div class="form-group">
                        <div class="col-md-offset-2 col-md-10">
                            <asp:Button runat="server" OnClick="LogIn" Text="登录" CssClass="btn btn-default" />
                        </div>
                    </div>
                </div>
                <p>
                    <a href="Register.aspx">注册</a>
                    如果你没有本地帐户。
                </p>
            </section>
        </div>

        <div class="col-md-4">
           <%--<section id="socialLoginForm">
            </section>--%>
        </div>
    </div>
</asp:Content>



