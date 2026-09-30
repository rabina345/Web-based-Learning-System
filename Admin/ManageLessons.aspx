<%@ Page Title="Manage Lessons" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageLessons.aspx.cs" Inherits="SkillCore.Admin.ManageLessons" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Manage Lessons</h1>

    <h2><asp:Literal ID="litFormHeading" runat="server" Text="Add Lesson" /></h2>
    <asp:Literal ID="litMessage" runat="server" />
    <asp:HiddenField ID="hdnLessonId" runat="server" Value="0" />

    <p>
        <asp:Label runat="server" AssociatedControlID="ddlCourse">Course</asp:Label><br />
        <asp:DropDownList ID="ddlCourse" runat="server" DataTextField="Title" DataValueField="CourseID" />
    </p>
    <p>
        <asp:Label runat="server" AssociatedControlID="txtTitle">Lesson Title</asp:Label><br />
        <asp:TextBox ID="txtTitle" runat="server" Width="300" />
        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtTitle"
            ErrorMessage="Title is required." Display="Dynamic" ForeColor="Red" />
    </p>
    <p>
        <asp:Label runat="server" AssociatedControlID="txtContent">Content</asp:Label><br />
        <asp:TextBox ID="txtContent" runat="server" TextMode="MultiLine" Rows="5" Width="300" />
    </p>
    <p>
        <asp:Label runat="server" AssociatedControlID="txtSortOrder">Sort Order</asp:Label><br />
        <asp:TextBox ID="txtSortOrder" runat="server" Text="1" Width="60" />
    </p>
    <p>
        <asp:Label runat="server" AssociatedControlID="txtDuration">Duration (minutes)</asp:Label><br />
        <asp:TextBox ID="txtDuration" runat="server" Width="60" />
    </p>
    <p>
        <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
    </p>

    <hr />

    <asp:GridView ID="gvLessons" runat="server" AutoGenerateColumns="false"
        DataKeyNames="LessonID" OnRowCommand="gvLessons_RowCommand">
        <Columns>
            <asp:BoundField DataField="CourseTitle" HeaderText="Course" />
            <asp:BoundField DataField="Title" HeaderText="Lesson" />
            <asp:BoundField DataField="SortOrder" HeaderText="Order" />
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:LinkButton runat="server" CommandName="EditLesson"
                        CommandArgument='<%# Eval("LessonID") %>'>Edit</asp:LinkButton>
                    &nbsp;|&nbsp;
                    <asp:LinkButton runat="server" CommandName="DeleteLesson"
                        CommandArgument='<%# Eval("LessonID") %>'
                        OnClientClick="return confirm('Delete this lesson?');">Delete</asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>