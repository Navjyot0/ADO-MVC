<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Disconnected-ADO-With-Cache.aspx.cs" Inherits="MvcApplication.Disconnected_ADO_With_Cache" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Label ID="lblMessage" runat="server"></asp:Label>
        <br />
        <br />
        <asp:Button ID="btnGetFromDB" runat="server" onclick="btnGetFromDB_Click" 
            Text="Get Data From DB" />
&nbsp;<asp:Button ID="btnGetFromCache" runat="server" onclick="btnGetFromCache_Click" 
            Text="Get From Cache" />
&nbsp;<asp:Button ID="btnClearCache" runat="server" onclick="btnClearCache_Click" 
            Text="Clear Cache" />
        &nbsp;<asp:Button ID="btnUndo" runat="server" onclick="btnUndo_Click" 
            Text="Undo All Changes" />
        <br />
        <br />
        <asp:GridView ID="gvEmployee" runat="server" CellPadding="4" 
            ForeColor="#333333" GridLines="None" 
            onrowcancelingedit="gvEmployee_RowCancelingEdit" 
            onrowdeleting="gvEmployee_RowDeleting" onrowediting="gvEmployee_RowEditing" 
            onrowupdating="gvEmployee_RowUpdating" DataKeyNames="Id">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:CommandField ShowDeleteButton="True" ShowEditButton="True"/>
            </Columns>
            <EditRowStyle BackColor="#2461BF" />
            <EmptyDataTemplate>
                No Employee
            </EmptyDataTemplate>
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
    
        <br />
        <asp:Button ID="btnUpdate" runat="server" Text="Update DB Table" 
            onclick="btnUpdate_Click" />
        <br />
        <br />
        <br />
        <div>
            <table>
                <tr>
                    <td>
                        First Name
                    </td>
                    <td>
                        <asp:TextBox ID="txtFirst" Width="100%" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        Last Name
                    </td>
                    <td>
                        <asp:TextBox ID="txtLastName" Width="100%" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        Age
                    </td>
                    <td>
                        <asp:TextBox ID="txtAge" TextMode="Number" Width="100%" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
    </form>
</body>
</html>
