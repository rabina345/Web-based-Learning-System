<%@ Page Title="Register" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="SkillCore.Account.Register" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Create your SkillCore account</h1>

    <asp:Literal ID="litMessage" runat="server" />

    <div class="card">
        <p>
            <asp:Label runat="server" AssociatedControlID="txtFullName">Full Name</asp:Label><br />
            <asp:TextBox ID="txtFullName" runat="server" />
            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtFullName"
                ErrorMessage="Full name is required." Display="Dynamic" ForeColor="Red" />
        </p>

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
            <asp:Button ID="btnRegister" runat="server" Text="Register" OnClick="btnRegister_Click" />
        </p>
    </div>
</asp:Content>