<%@ Page Title="Manage Users" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageUsers.aspx.cs" Inherits="SkillCore.Admin.ManageUsers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Manage Users</h1>
    <asp:Literal ID="litMessage" runat="server" />

    <asp:GridView ID="gvUsers" runat="server" AutoGenerateColumns="false"
        DataKeyNames="UserID" OnRowCommand="gvUsers_RowCommand" OnRowDataBound="gvUsers_RowDataBound">
        <Columns>
            <asp:BoundField DataField="FullName" HeaderText="Name" />
            <asp:BoundField DataField="Email" HeaderText="Email" />
            <asp:TemplateField HeaderText="Role">
                <ItemTemplate>
                    <asp:DropDownList ID="ddlRole" runat="server">
                        <asp:ListItem Text="Admin" Value="1" />
                        <asp:ListItem Text="Member" Value="2" />
                    </asp:DropDownList>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Active">
                <ItemTemplate>
                    <asp:CheckBox ID="chkActive" runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:LinkButton runat="server" CommandName="UpdateUser"
                        CommandArgument='<%# Eval("UserID") %>'>Save</asp:LinkButton>
                    &nbsp;|&nbsp;
                    <asp:LinkButton runat="server" CommandName="DeleteUser"
                        CommandArgument='<%# Eval("UserID") %>'
                        OnClientClick="return confirm('Delete this user permanently?');">Delete</asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>