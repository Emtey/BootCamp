<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ItemPanel.aspx.cs" Inherits="ItemPanel" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Label ID="Label1" runat="server" Text="ISBN:"></asp:Label>
    <asp:TextBox ID="txtISBN" runat="server" CausesValidation="True"></asp:TextBox>
    &nbsp;&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
        ControlToValidate="txtISBN" Display="Dynamic" ErrorMessage="is required"></asp:RequiredFieldValidator>
    <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtISBN"
        Display="Dynamic" ErrorMessage="must be between 1 and 2147483647" MaximumValue="2147483647"
        MinimumValue="1" Type="Integer"></asp:RangeValidator><br />
    &nbsp;<asp:Label ID="Label3" runat="server" Text="Title:"></asp:Label>
    <asp:TextBox ID="txtTitle" runat="server" CausesValidation="True" MaxLength="63"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtAuthor"
        Display="Dynamic" ErrorMessage="is required."></asp:RequiredFieldValidator><br />
    <asp:Label ID="Label4" runat="server" Text="Author:"></asp:Label>
    <asp:TextBox ID="txtAuthor" runat="server" CausesValidation="True" MaxLength="31"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtAuthor"
        Display="Dynamic" ErrorMessage="is required"></asp:RequiredFieldValidator><br />
    <asp:CheckBox ID="chkLoanable" runat="server" Checked="True" Text="Is Loanable:"
        TextAlign="Left" /><br />
    <br />
    <asp:Label ID="lblStatus" runat="server"></asp:Label><br />
    <br />
    &nbsp;<asp:Button ID="btnAddItem" runat="server" OnClick="btnAddItem_Click" Text="Add Item" />
</asp:Content>

