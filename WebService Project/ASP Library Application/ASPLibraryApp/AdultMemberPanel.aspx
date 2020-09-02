<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AdultMemberPanel.aspx.cs" Inherits="AdultMemberPanel" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    &nbsp;<br />
    <asp:Label ID="Label1" runat="server" Text="First Name:"></asp:Label>
    <asp:TextBox ID="txtFirstName" runat="server" MaxLength="15" ></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtFirstName"
        Display="None" ErrorMessage="First Name is required."></asp:RequiredFieldValidator>
    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtFirstName"
        Display="None" ErrorMessage="First Name must start with a Capital Letter and contain only characters up to a length of 15." ValidationExpression="^[A-Z][a-z]{0,14}$"></asp:RegularExpressionValidator>&nbsp;
    <asp:TextBox ID="txtMidInit" runat="server" Width="11px" MaxLength="1" ></asp:TextBox>
    <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="txtMidInit"
        Display="None" ErrorMessage="Middle Initial valid format is a single upper case character."
        ValidationExpression="^[A-Z]{0,1}$"></asp:RegularExpressionValidator>
    <asp:TextBox ID="txtLastName" runat="server" MaxLength="15" ></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtLastName"
        Display="None" ErrorMessage="Last Name is required."></asp:RequiredFieldValidator><asp:RegularExpressionValidator
            ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtLastName"
            Display="None" ErrorMessage="Last Name must start with a Capital Letter and contain only characters up to a length of 15" ValidationExpression="^[A-Z][a-z]{0,14}$"></asp:RegularExpressionValidator><br />
    <asp:Label ID="Label2" runat="server" Text="Address"></asp:Label>
    <asp:TextBox ID="txtAddress" runat="server" MaxLength="15"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtAddress"
        Display="None" ErrorMessage="Address is required."></asp:RequiredFieldValidator>
    <br />
    <asp:Label ID="Label3" runat="server" Text="City, State, Zip"></asp:Label>
    <asp:TextBox ID="txtCity" runat="server" MaxLength="15"></asp:TextBox><asp:RequiredFieldValidator
        ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtCity" Display="None"
        ErrorMessage="City is required."></asp:RequiredFieldValidator>
    &nbsp;<asp:DropDownList
            ID="ddState" runat="server" Width="44px">
        </asp:DropDownList>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddState"
        Display="None" ErrorMessage="State is required."></asp:RequiredFieldValidator>
    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="ddState"
        Display="None" ErrorMessage="State must be 2 Capital Letters." ValidationExpression="^[A-Z]{0,2}$"></asp:RegularExpressionValidator>
    <asp:TextBox ID="txtZip" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtZip"
        Display="None" ErrorMessage="Zip is required."></asp:RequiredFieldValidator>
    <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtZip"
        Display="None" ErrorMessage="five digit zip code or the zip + 4 with a hyphen separator. (#####-####)"
        ValidationExpression="^\d{5}(-\d{4})?$"></asp:RegularExpressionValidator><br />
    <asp:Label ID="Label4" runat="server" Text="Telephone:"></asp:Label>
    <asp:TextBox ID="txtTelephone" runat="server"></asp:TextBox>
    <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtTelephone"
        Display="None" ErrorMessage="Telephone format is (###)###-####." ValidationExpression="^\(\d{3}\)\d{3}-\d{4}$"></asp:RegularExpressionValidator><br />
    <br />
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;    
    <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />&nbsp;
    <br />
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
    &nbsp;&nbsp;&nbsp;
</asp:Content>

