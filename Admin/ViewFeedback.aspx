<%@ Page Title="View Feedback" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ViewFeedback.aspx.cs" Inherits="SkillCore.Admin.ViewFeedback" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Feedback Messages</h1>

    <asp:GridView ID="gvFeedback" runat="server" AutoGenerateColumns="false"
        DataKeyNames="FeedbackID" OnRowCommand="gvFeedback_RowCommand" OnRowDataBound="gvFeedback_RowDataBound">
        <Columns>
            <asp:BoundField DataField="SubmittedAt" HeaderText="Date" DataFormatString="{0:dd MMM yyyy HH:mm}" />
            <asp:BoundField DataField="Name" HeaderText="Name" />
            <asp:BoundField DataField="Email" HeaderText="Email" />
            <asp:BoundField DataField="Subject" HeaderText="Subject" />
            <asp:BoundField DataField="Message" HeaderText="Message" />
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:LinkButton ID="lnkMarkRead" runat="server" CommandName="MarkRead"
                        CommandArgument='<%# Eval("FeedbackID") %>'>Mark as Read</asp:LinkButton>
                    &nbsp;|&nbsp;
                    <asp:LinkButton runat="server" CommandName="DeleteFeedback"
                        CommandArgument='<%# Eval("FeedbackID") %>'
                        OnClientClick="return confirm('Delete this message?');">Delete</asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>