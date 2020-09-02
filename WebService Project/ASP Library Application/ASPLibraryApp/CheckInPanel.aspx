<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="CheckInPanel.aspx.cs" Inherits="CheckInPanel" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Label ID="Label1" runat="server" Text="ISBN:"></asp:Label>
    <asp:TextBox ID="txtISBN" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtISBN"
        Display="Dynamic" ErrorMessage="is required."></asp:RequiredFieldValidator>
    <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtISBN"
        ErrorMessage="must be between 1 and 2147483647" MaximumValue="2147483647" MinimumValue="1"
        Type="Integer"></asp:RangeValidator><br />
    <asp:Label ID="Label2" runat="server" Text="Copy Number:"></asp:Label>
    <asp:TextBox ID="txtCopyNum" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtCopyNum"
        ErrorMessage="is required."></asp:RequiredFieldValidator>
    <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="txtCopyNum"
        ErrorMessage="must be between 1 and 32767" MaximumValue="32767" MinimumValue="1"
        Type="Integer"></asp:RangeValidator><br />
    <br />
    <asp:Label ID="lblStatus" runat="server" Text=""></asp:Label>
    <br />
    <br />
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Button ID="btnCheckIn" runat="server" Text="Begin Check In" OnClick="btnCheckIn_Click" />
    &nbsp;&nbsp;<asp:Button ID="btnFinalCheckIn" runat="server" OnClick="btnFinalCheckIn_Click"
        Text="Check In" Visible="False" />
    &nbsp;<asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" Text="Cancel"
        Visible="False" />
</asp:Content>

