<%@ Page Title="Browse Portfolios" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AllPortfolios.aspx.cs" Inherits="SkillCore.Member.AllPortfolios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Browse Portfolios</h1>
    <p>See what other SkillCore learners have built.</p>

    <asp:Literal ID="litMessage" runat="server" />

    <asp:Repeater ID="rptPortfolios" runat="server">
        <ItemTemplate>
            <div class="card">
                <strong><%# Eval("Title") %></strong>
                <em> — by <%# Eval("UploaderName") %></em><br />
                <%# Eval("Description") %><br />
                <a href="<%# Eval("FilePath") %>" target="_blank">Download file</a>

                <asp:PlaceHolder runat="server" Visible='<%# CanDelete((int)Eval("UserID")) %>'>
                    &nbsp;|&nbsp;
                    <asp:LinkButton runat="server" CommandName="DeletePortfolio"
                        CommandArgument='<%# Eval("PortfolioID") %>'
                        OnCommand="lnkDelete_Command"
                        OnClientClick="return confirm('Delete this upload?');">Delete</asp:LinkButton>
                </asp:PlaceHolder>
            </div>
        </ItemTemplate>
    </asp:Repeater>
</asp:Content>