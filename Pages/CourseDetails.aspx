<%@ Page Title="Course Details" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CourseDetails.aspx.cs" Inherits="SkillCore.Pages.CourseDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1><asp:Literal ID="litTitle" runat="server" /></h1>
    <p><asp:Literal ID="litMeta" runat="server" /></p>
    <p><asp:Literal ID="litDescription" runat="server" /></p>

    <h2>Lessons</h2>
    <asp:Literal ID="litEnrollMessage" runat="server" />
    <asp:Literal ID="litQuizLink" runat="server" />

<asp:Button ID="btnEnroll" runat="server" Text="Enroll in this Course" OnClick="btnEnroll_Click" />
    <asp:Repeater ID="rptLessons" runat="server">
        <ItemTemplate>
            <div class="card">
                <strong><%# Eval("SortOrder") %>. <a href="<%# ResolveUrl("~/Member/Lesson.aspx") %>?id=<%# Eval("LessonID") %>"><%# Eval("Title") %></a></strong>
            </div>
        </ItemTemplate>

    </asp:Repeater>
</asp:Content>

