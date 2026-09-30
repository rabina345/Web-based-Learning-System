<%@ Page Title="Contact" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="SkillCore.Pages.Contact" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Contact SkillCore</h1>
    <p>Have a question or feedback? Send us a message below.</p>

    <asp:Literal ID="litMessage" runat="server" />

    <asp:Panel ID="pnlForm" runat="server">
        <p>
            <asp:Label runat="server" AssociatedControlID="txtName">Name</asp:Label><br />
            <asp:TextBox ID="txtName" runat="server" Width="300" />
            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtName"
                ErrorMessage="Name is required." Display="Dynamic" ForeColor="Red" />
        </p>

        <p>
            <asp:Label runat="server" AssociatedControlID="txtEmail">Email</asp:Label><br />
            <asp:TextBox ID="txtEmail" runat="server" TextMode="Email" Width="300" />
            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtEmail"
                ErrorMessage="Email is required." Display="Dynamic" ForeColor="Red" />
            <asp:RegularExpressionValidator runat="server" ControlToValidate="txtEmail"
                ValidationExpression="^[^@\s]+@[^@\s]+\.[^@\s]+$"
                ErrorMessage="Please enter a valid email." Display="Dynamic" ForeColor="Red" />
        </p>

        <p>
            <asp:Label runat="server" AssociatedControlID="txtSubject">Subject</asp:Label><br />
            <asp:TextBox ID="txtSubject" runat="server" Width="300" />
        </p>

        <p>
            <asp:Label runat="server" AssociatedControlID="txtMessage">Message</asp:Label><br />
            <asp:TextBox ID="txtMessage" runat="server" TextMode="MultiLine" Rows="5" Width="300" />
            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtMessage"
                ErrorMessage="Message is required." Display="Dynamic" ForeColor="Red" />
        </p>

        <p>
            <asp:Button ID="btnSend" runat="server" Text="Send Message" OnClick="btnSend_Click" />
        </p>
    </asp:Panel>
</asp:Content>