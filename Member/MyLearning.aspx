<%@ Page Title="My Learning" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MyLearning.aspx.cs" Inherits="SkillCore.Member.MyLearning" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>My Learning</h1>

    <asp:Literal ID="litEmpty" runat="server" Visible="false"
        Text="<p>You haven't enrolled in any courses yet. <a href='../Pages/Courses.aspx'>Browse courses</a></p>" />

    <asp:Repeater ID="rptEnrolled" runat="server">
        <ItemTemplate>
            <div class="card">
                <h3><a href="<%# ResolveUrl("~/Pages/CourseDetails.aspx") %>?id=<%# Eval("CourseID") %>"><%# Eval("Title") %></a></h3>
                <p><em><%# Eval("CategoryName") %> &middot; <%# Eval("DifficultyLevel") %></em></p>
            </div>
        </ItemTemplate>
    </asp:Repeater>
</asp:Content>