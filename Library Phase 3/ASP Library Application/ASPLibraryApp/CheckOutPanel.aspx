<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="CheckOutPanel.aspx.cs" Inherits="CheckOutPanel" Title="Untitled Page" %>
<%@ PreviousPageType VirtualPath="~/MainPanel.aspx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Label ID="Label1" runat="server" Text="ISBN:"></asp:Label>
    <asp:TextBox ID="txtISBN" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic"
        ErrorMessage="ISBN is Required" ControlToValidate="txtISBN"></asp:RequiredFieldValidator>
    <asp:RangeValidator ID="RangeValidator1" runat="server" Display="Dynamic" ErrorMessage="Must be between 1 and 2147483647" ControlToValidate="txtISBN" MaximumValue="2147483647" MinimumValue="1" Type="Integer"></asp:RangeValidator><br />
    <asp:Label ID="Label2" runat="server" Text="Copy Number:"></asp:Label>
    <asp:TextBox ID="txtCopyNum" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic"
        ErrorMessage="Copy Number is Required" ControlToValidate="txtCopyNum"></asp:RequiredFieldValidator>
    <asp:RangeValidator ID="RangeValidator2" runat="server" Display="Dynamic" ErrorMessage="Must be between 1 and 32767" ControlToValidate="txtCopyNum" MaximumValue="32767" MinimumValue="1" Type="Integer"></asp:RangeValidator><br />
    <br />
    <asp:Label ID="lblStatus" runat="server"></asp:Label>
    <br />
    <br />
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp
    <asp:Button ID="btnCheckOut" runat="server" Text="Begin Check Out" OnClick="btnCheckOut_Click" />
    &nbsp;&nbsp;&nbsp;<asp:Button ID="btnFinalCheckOut" runat="server" OnClick="btnFinalCheckOut_Click"
        Text="Check Out" Visible="False" />
    &nbsp;<asp:Button ID="btnCancel" runat="server" Text="Cancel" Visible="False" OnClick="btnCancel_Click" />&nbsp;
</asp:Content>


