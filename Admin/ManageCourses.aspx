<%@ Page Title="Manage Courses" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageCourses.aspx.cs" Inherits="SkillCore.Admin.ManageCourses" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Manage Courses</h1>

    <a href="EditCourse.aspx">+ Add New Course</a>

    <asp:GridView ID="gvCourses" runat="server" AutoGenerateColumns="false"
        DataKeyNames="CourseID" OnRowCommand="gvCourses_RowCommand">
        <Columns>
            <asp:BoundField DataField="Title" HeaderText="Title" />
            <asp:BoundField DataField="CategoryName" HeaderText="Category" />
            <asp:BoundField DataField="DifficultyLevel" HeaderText="Level" />
            <asp:BoundField DataField="IsPublished" HeaderText="Published" />
            <asp:TemplateField>
                <ItemTemplate>
                    <a href="EditCourse.aspx?id=<%# Eval("CourseID") %>">Edit</a>
                    &nbsp;|&nbsp;
                    <asp:LinkButton runat="server" CommandName="DeleteCourse"
                        CommandArgument='<%# Eval("CourseID") %>'
                        OnClientClick="return confirm('Delete this course? This cannot be undone.');">Delete</asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>