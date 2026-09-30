<%@ Page Title="My Dashboard" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="SkillCore.Member.Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Welcome, <%: Session["FullName"] %></h1>

    <ul>
        <li><a href="MyLearning.aspx">My Learning</a></li>
        <li><a href="<%= ResolveUrl("~/Pages/Courses.aspx") %>">Browse Courses</a></li>
        <li><a href="Portfolio.aspx">My Portfolio</a></li>

    </ul>
</asp:Content>