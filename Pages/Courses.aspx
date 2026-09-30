<%@ Page Title="Courses" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Courses.aspx.cs" Inherits="SkillCore.Pages.Courses" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>All Courses</h1>

    <asp:Repeater ID="rptCourses" runat="server">
        <ItemTemplate>
            <div class="card">

                <h3><a href="CourseDetails.aspx?id=<%# Eval("CourseID") %>"><%# Eval("Title") %></a></h3>
                <p><em><%# Eval("CategoryName") %> &middot; <%# Eval("DifficultyLevel") %></em></p>
                <p><%# Eval("ShortDescription") %></p>
            </div>
        </ItemTemplate>
    </asp:Repeater>
</asp:Content>