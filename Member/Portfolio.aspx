<%@ Page Title="My Portfolio" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Portfolio.aspx.cs" Inherits="SkillCore.Member.Portfolio" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>My Portfolio</h1>
    <p>Upload finished coursework or project files to keep track of your progress.</p>

    <asp:Literal ID="litMessage" runat="server" />

    <p>
        <asp:Label runat="server" AssociatedControlID="txtTitle">Title</asp:Label><br />
        <asp:TextBox ID="txtTitle" runat="server" Width="300" />
        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtTitle"
            ErrorMessage="Title is required." Display="Dynamic" ForeColor="Red" />
    </p>
    <p>
        <asp:Label runat="server" AssociatedControlID="txtDescription">Description</asp:Label><br />
        <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" Rows="3" Width="300" />
    </p>
    <p>
        <asp:Label runat="server" AssociatedControlID="fuFile">File</asp:Label><br />
        <asp:FileUpload ID="fuFile" runat="server" />
    </p>
    <p>
        <asp:Button ID="btnUpload" runat="server" Text="Upload" OnClick="btnUpload_Click" />
    </p>

    <hr />
    <h2>My Uploads</h2>

    <asp:Repeater ID="rptPortfolio" runat="server">
        <ItemTemplate>
            <div class="card">
                <strong><%# Eval("Title") %></strong><br />
                <%# Eval("Description") %><br />
                <a href="<%# Eval("FilePath") %>" target="_blank">Download file</a>
                &nbsp;|&nbsp;
                <asp:LinkButton runat="server" CommandName="DeletePortfolio"
                    CommandArgument='<%# Eval("PortfolioID") %>'
                    OnCommand="lnkDelete_Command"
                    OnClientClick="return confirm('Delete this upload?');">Delete</asp:LinkButton>
            </div>
        </ItemTemplate>
    </asp:Repeater>
</asp:Content>