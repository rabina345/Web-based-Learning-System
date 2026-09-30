<%@ Page Title="Admin Dashboard" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="SkillCore.Admin.Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Admin Dashboard</h1>
    <p>Welcome, <%: Session["FullName"] %>. Choose what to manage:</p>

    <ul>
        <li><a href="ManageCourses.aspx">Manage Courses</a></li>
        <li><a href="ManageCategories.aspx">Manage Categories</a></li>
        <li><a href="ManageLessons.aspx">Manage Lessons</a></li>
        <li><a href="ManageUsers.aspx">Manage Users</a></li>
        <li><a href="ViewFeedback.aspx">View Feedback</a></li>
    </ul>
</asp:Content>