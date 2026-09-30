<%@ Page Title="Lesson" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Lesson.aspx.cs" Inherits="SkillCore.Member.Lesson" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <p><a href="<%= ResolveUrl("~/Pages/CourseDetails.aspx") %>?id=<%= courseIdForBackLink %>">&larr; Back to course</a></p>

    <h1><asp:Literal ID="litTitle" runat="server" /></h1>
    <div><asp:Literal ID="litContent" runat="server" /></div>

    <hr />
    <asp:Literal ID="litStatus" runat="server" />
    <asp:Button ID="btnComplete" runat="server" Text="Mark as Complete" OnClick="btnComplete_Click" />
</asp:Content>