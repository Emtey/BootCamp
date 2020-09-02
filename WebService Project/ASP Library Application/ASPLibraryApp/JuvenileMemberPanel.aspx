<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="JuvenileMemberPanel.aspx.cs" Inherits="JuvenileMemberPanel" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    &nbsp;<br />
    <asp:Label ID="Label1" runat="server" Text="First Name:"></asp:Label>
    <asp:TextBox ID="txtFirstName" runat="server" MaxLength="15" ></asp:TextBox><asp:RequiredFieldValidator
        ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtFirstName"
        Display="None" ErrorMessage="First Name is required."></asp:RequiredFieldValidator><asp:RegularExpressionValidator
            ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtFirstName"
            Display="None" ErrorMessage="First Name must start with a Capital Letter and contain only characters up to a length of 15."
            ValidationExpression="^[A-Z][a-z]{0,14}$"></asp:RegularExpressionValidator>
    <asp:TextBox ID="txtMidInit" runat="server" Width="23px" MaxLength="1" ></asp:TextBox>
    <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="txtMidInit"
        Display="None" ErrorMessage="Middle Initial valid format is a single upper case character."
        ValidationExpression="^[A-Z]{0,1}$"></asp:RegularExpressionValidator>
    <asp:TextBox ID="txtLastName" runat="server" MaxLength="15" ></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtLastName"
        Display="None" ErrorMessage="Last Name is required."></asp:RequiredFieldValidator><asp:RegularExpressionValidator
            ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtLastName"
            Display="None" ErrorMessage="Last Name must start with a Capital Letter and contain only characters up to a length of 15"
            ValidationExpression="^[A-Z][a-z]{0,14}$"></asp:RegularExpressionValidator><br />
    <asp:Label ID="Label2" runat="server" Text="Adult Member ID:"></asp:Label>  
    <asp:TextBox ID="txtAdultMemberID" runat="server"></asp:TextBox> 
    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtAdultMemberID"
        Display="None" ErrorMessage="Adult Member ID is required."></asp:RequiredFieldValidator><asp:RangeValidator
            ID="RangeValidator1" runat="server" ControlToValidate="txtAdultMemberID" Display="None"
            ErrorMessage="Must be between 1 and 32767" MaximumValue="32767" MinimumValue="1"
            Type="Integer"></asp:RangeValidator><asp:Label ID="Label3" runat="server" Text="BirthDate:"></asp:Label>&nbsp;
    <asp:TextBox ID="txtBirthDate" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtBirthDate"
        Display="None" ErrorMessage="Birth Date is required."></asp:RequiredFieldValidator>
    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtBirthDate"
        Display="None" ErrorMessage="Date must be in mm/dd/ccyy format." ValidationExpression="\d{1,2}/\d{1,2}/\d{4}"></asp:RegularExpressionValidator><br />
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
    <br />
    <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Save" />
    
</asp:Content>

