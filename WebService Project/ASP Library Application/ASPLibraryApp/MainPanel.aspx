<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="MainPanel.aspx.cs" Inherits="MainPanel" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Label ID="Label4" runat="server" Text="Member ID:"></asp:Label>
    <asp:TextBox ID="txtMemberID" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtMemberID"
        Display="Dynamic" ErrorMessage="Is required."></asp:RequiredFieldValidator>
    <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtMemberID"
        Display="Dynamic" ErrorMessage="Must be between 1 and 32767" MaximumValue="32767"
        MinimumValue="1" Type="Integer"></asp:RangeValidator>
    <asp:Button ID="btnGetInfo" runat="server" Text="Get Info" OnClick="btnGetInfo_Click" />
    <br />        
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Label ID="Label2" runat="server" Text="Expiration Date:"></asp:Label>
    <asp:TextBox ID="txtExpDte" runat="server" ReadOnly="true"></asp:TextBox>
    <asp:Button ID="btnRenewMember" runat="server" Text="Renew Membership" Visible="False" OnClick="btnRenewMember_Click" />&nbsp;<br />
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Label ID="lblAdultMember" runat="server"  Text="Adult Member ID:"></asp:Label>
    <asp:TextBox ID="txtAdultMemberID" runat="server" ReadOnly="True"></asp:TextBox>
    <asp:Label ID="lblBirthDte" runat="server"  Text="Birth Date:"></asp:Label>
    <asp:TextBox ID="txtBirthDte" runat="server" ReadOnly="True"></asp:TextBox>
    <br />
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Label ID="Label5" runat="server" Text="Name:"></asp:Label>
    <asp:TextBox ID="txtFirstName" runat="server" ReadOnly="True"></asp:TextBox>
    <asp:TextBox ID="txtMiddleInitial" runat="server" Width="21px" ReadOnly="True"></asp:TextBox>
    <asp:TextBox ID="txtLastName" runat="server" ReadOnly="True"></asp:TextBox>
    <br />
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Label ID="Label6" runat="server" Text="Street:"></asp:Label>
    <asp:TextBox ID="txtAddress" runat="server" ReadOnly="True"></asp:TextBox>&nbsp;<br />
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Label ID="Label7" runat="server" Text="City, State, Zip:"></asp:Label>
    <asp:TextBox ID="txtCity" runat="server" ReadOnly="True"></asp:TextBox>
    <asp:TextBox ID="txtState" runat="server" Width="23px" ReadOnly="True"></asp:TextBox>
    <asp:TextBox ID="txtZip" runat="server" ReadOnly="True"></asp:TextBox>
    <br />
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Label ID="Label8" runat="server" Text="Telephone:"></asp:Label>
    <asp:TextBox ID="txtTelephone" runat="server" ReadOnly="True"></asp:TextBox>&nbsp;<br /> <br />
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Label ID="Label10" runat="server" Text="Books On Loan:"></asp:Label>
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;<asp:Button ID="btnCheckout"
        runat="server" PostBackUrl="~/CheckOutPanel.aspx" Text="Check Out" Visible="false" />
    &nbsp; &nbsp; &nbsp;
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp;
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    <br />
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="ObjectDataSource1" CellPadding="4" ForeColor="#333333" GridLines="None" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" OnRowDataBound="GridView1_RowDataBound">
        <Columns>
            <asp:CommandField ButtonType="Button" SelectText="Check In" ShowSelectButton="True" />
            <asp:BoundField DataField="isbn" HeaderText="ISBN" SortExpression="isbn" />
            <asp:BoundField DataField="copy_no" HeaderText="Copy Number" SortExpression="copy_no" />
            <asp:BoundField DataField="title" HeaderText="Title" SortExpression="title" />
            <asp:BoundField DataField="author" HeaderText="Author" SortExpression="author" />
            <asp:TemplateField HeaderText="Check Out Date" SortExpression="out_date">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("out_date") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("out_date", "{0:d}") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Due Date" SortExpression="due_date">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("due_date") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# Eval("due_date", "{0:d}") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <RowStyle BackColor="#EFF3FB" />
        <EditRowStyle BackColor="#2461BF" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <AlternatingRowStyle BackColor="White" />
    </asp:GridView>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetItems" TypeName="LibraryWebService.ServiceWse" OnSelecting="ObjectDataSource1_Selecting" OnSelected="ObjectDataSource1_Selected" OnObjectCreated="ObjectDataSource1_ObjectCreated">
        <SelectParameters>
            <asp:ControlParameter ControlID="txtMemberID" Name="memberNumber" PropertyName="Text"
                Type="Int16" />
        </SelectParameters>
    </asp:ObjectDataSource>
    
</asp:Content>

