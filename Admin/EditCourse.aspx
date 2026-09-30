<%@ Page Title="Edit Course" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditCourse.aspx.cs" Inherits="SkillCore.Admin.EditCourse" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1><asp:Literal ID="litHeading" runat="server" Text="Add Course" /></h1>

    <asp:Literal ID="litMessage" runat="server" />

    <p>
        <asp:Label runat="server" AssociatedControlID="txtTitle">Title</asp:Label><br />
        <asp:TextBox ID="txtTitle" runat="server" Width="300" />
        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtTitle"
            ErrorMessage="Title is required." Display="Dynamic" ForeColor="Red" />
    </p>

    <p>
        <asp:Label runat="server" AssociatedControlID="txtShortDescription">Short Description</asp:Label><br />
        <asp:TextBox ID="txtShortDescription" runat="server" Width="300" />
    </p>

    <p>
        <asp:Label runat="server" AssociatedControlID="txtFullDescription">Full Description</asp:Label><br />
        <asp:TextBox ID="txtFullDescription" runat="server" TextMode="MultiLine" Rows="5" Width="300" />
    </p>

    <p>
        <asp:Label runat="server" AssociatedControlID="ddlCategory">Category</asp:Label><br />
        <asp:DropDownList ID="ddlCategory" runat="server" DataTextField="CategoryName" DataValueField="CategoryID" />
    </p>

    <p>
        <asp:Label runat="server" AssociatedControlID="ddlDifficulty">Difficulty</asp:Label><br />
        <asp:DropDownList ID="ddlDifficulty" runat="server">
            <asp:ListItem Text="Beginner" Value="Beginner" />
            <asp:ListItem Text="Intermediate" Value="Intermediate" />
            <asp:ListItem Text="Advanced" Value="Advanced" />
        </asp:DropDownList>
    </p>

    <p>
        <asp:Label runat="server" AssociatedControlID="txtDuration">Duration (minutes)</asp:Label><br />
        <asp:TextBox ID="txtDuration" runat="server" />
    </p>

    <p>
        <asp:CheckBox ID="chkPublished" runat="server" Text="Published (visible to students)" />
    </p>

    <p>
        <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
        <a href="ManageCourses.aspx">Cancel</a>
    </p>
</asp:Content>