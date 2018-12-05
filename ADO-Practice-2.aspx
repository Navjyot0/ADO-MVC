<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ADO-Practice-2.aspx.cs" Inherits="MvcApplication.ADO_Practice_2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:GridView ID="gvEmployee" runat="server" CellPadding="4" ForeColor="#333333" 
            GridLines="None" onrowcancelingedit="gvEmployee_RowCancelingEdit" 
            onrowdeleting="gvEmployee_RowDeleting" onrowediting="gvEmployee_RowEditing" 
            onrowupdating="gvEmployee_RowUpdating">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
            </Columns>
            <EmptyDataTemplate>
                No Data Available
            </EmptyDataTemplate>
            <EditRowStyle BackColor="#2461BF" />
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#EFF3FB" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#F5F7FB" />
            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
            <SortedDescendingCellStyle BackColor="#E9EBEF" />
            <SortedDescendingHeaderStyle BackColor="#4870BE" />
        </asp:GridView>
    </div>
    <br />
    <br />
    <div>
        Create Student 
        <br />
        <br />
        <asp:TextBox ID="txtName" runat="server" placeholder="Enter Name" required="true"></asp:TextBox>
        <br />
        <br />
        <asp:TextBox ID="txtAge" runat="server" TextMode="Number" MaxLength="100" placeholder="Age" required="true"></asp:TextBox>
        <br />
        <br />
        <asp:TextBox ID="txtCity" runat="server" placeholder="City" required="true"></asp:TextBox>
        <br />
        <br />
        <asp:TextBox ID="txtFees" runat="server" placeholder="Fees" required="true"></asp:TextBox>
        <br />
        <br />
        <asp:DropDownList ID="ddlDepartment" runat="server" required="true">
            <asp:ListItem Value="">Select </asp:ListItem>
        </asp:DropDownList>
        <br />
        <br />
        <asp:Button ID="btnSubmit" runat="server" Text="Submit" 
            onclick="btnSubmit_Click" />
    </div>
    </form>
</body>
</html>
