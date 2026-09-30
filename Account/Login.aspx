<%@ Page Title="Log In" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="SkillCore.Account.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Log in to SkillCore</h1>

    <asp:Literal ID="litMessage" runat="server" />

    <div class="card">
        <p>
            <asp:Label runat="server" AssociatedControlID="txtEmail">Email</asp:Label><br />
            <asp:TextBox ID="txtEmail" runat="server" TextMode="Email" />
            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtEmail"
                ErrorMessage="Email is required." Display="Dynamic" ForeColor="Red" />
        </p>

        <p>
            <asp:Label runat="server" AssociatedControlID="txtPassword">Password</asp:Label><br />
            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" />
            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtPassword"
                ErrorMessage="Password is required." Display="Dynamic" ForeColor="Red" />
        </p>

        <p>
            <asp:Button ID="btnLogin" runat="server" Text="Log In" OnClick="btnLogin_Click" />
        </p>
    </div>
</asp:Content>