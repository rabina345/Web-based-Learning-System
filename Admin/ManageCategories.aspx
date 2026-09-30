<%@ Page Title="Manage Categories" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageCategories.aspx.cs" Inherits="SkillCore.Admin.ManageCategories" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Manage Categories</h1>

    <h2><asp:Literal ID="litFormHeading" runat="server" Text="Add Category" /></h2>
    <asp:Literal ID="litMessage" runat="server" />
    <asp:HiddenField ID="hdnCategoryId" runat="server" Value="0" />

    <p>
        <asp:Label runat="server" AssociatedControlID="txtName">Name</asp:Label><br />
        <asp:TextBox ID="txtName" runat="server" />
        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtName"
            ErrorMessage="Name is required." Display="Dynamic" ForeColor="Red" />
    </p>
    <p>
        <asp:Label runat="server" AssociatedControlID="txtDescription">Description</asp:Label><br />
        <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" Rows="3" />
    </p>
    <p>
        <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
    </p>

    <hr />

    <asp:GridView ID="gvCategories" runat="server" AutoGenerateColumns="false"
        DataKeyNames="CategoryID" OnRowCommand="gvCategories_RowCommand">
        <Columns>
            <asp:BoundField DataField="CategoryName" HeaderText="Name" />
            <asp:BoundField DataField="Description" HeaderText="Description" />
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:LinkButton runat="server" CommandName="EditCategory"
                        CommandArgument='<%# Eval("CategoryID") %>'>Edit</asp:LinkButton>
                    &nbsp;|&nbsp;
                    <asp:LinkButton runat="server" CommandName="DeleteCategory"
                        CommandArgument='<%# Eval("CategoryID") %>'
                        OnClientClick="return confirm('Delete this category? Courses using it may break.');">Delete</asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>