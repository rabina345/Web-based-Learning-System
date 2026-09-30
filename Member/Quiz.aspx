<%@ Page Title="Quiz" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Quiz.aspx.cs" Inherits="SkillCore.Member.Quiz" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1><asp:Literal ID="litQuizTitle" runat="server" /></h1>

    <asp:Panel ID="pnlResult" runat="server" Visible="false">
        <asp:Literal ID="litResult" runat="server" />
    </asp:Panel>

    <asp:Repeater ID="rptQuestions" runat="server" OnItemDataBound="rptQuestions_ItemDataBound">
        <ItemTemplate>
            <div class="card">
                <p><strong><%# Eval("QuestionText") %></strong></p>
                <asp:HiddenField ID="hdnQuestionId" runat="server" Value='<%# Eval("QuestionID") %>' />
                <asp:RadioButtonList ID="rblOptions" runat="server" DataTextField="OptionText" DataValueField="OptionID" />
            </div>
        </ItemTemplate>
    </asp:Repeater>

    <asp:Button ID="btnSubmit" runat="server" Text="Submit Quiz" OnClick="btnSubmit_Click" />
</asp:Content>