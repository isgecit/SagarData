<%@ Page Title="" Language="C#" MasterPageFile="~/mstInner.Master" AutoEventWireup="true" CodeBehind="CreateDatabase.aspx.cs" Inherits="Attachment.CreateDatabase" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        table tr {
            margin-top: 20px;
        }
    </style>
     <script>
        function myFunction() {
            var txt;
            var r = confirm("Are you sure to Delete!");
            if (r == true) {
                return true;
            } else {
                return false;
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="text-align: initial; font-size: 16px; font-weight: bold" runat="server" id="divHeader">Create Database</div>
    <div style="margin-left: 0px; margin-top: 15px">
        <table>
            <tr>
                <td>Database Id</td>
                <td>:</td>
                <td>
                    <asp:TextBox runat="server" ID="txtDBID" MaxLength="50" Width="300" Height="23"/></td>
            </tr>

            <tr>
                <td>Description</td>
                <td>:</td>
                <td>
                    <asp:TextBox runat="server" ID="txtDBDesc" Width="300" Height="23"/></td>
            </tr>

            <tr>
                <td>Database Server Name/IP</td>
                <td>:</td>
                <td>
                    <asp:TextBox runat="server" ID="txtDBServerName" MaxLength="50" Width="300" Height="23"/></td>
            </tr>

            <tr>
                <td>Database Name</td>
                <td>:</td>
                <td>
                    <asp:TextBox runat="server" ID="txtDatabaseName" MaxLength="50" Width="300" Height="23"/></td>
            </tr>

            <tr>
                <td>Library Code</td>
                <td>:</td>
                <td>
                    <asp:DropDownList runat="server" ID="ddlLibrary" Width="300" Height="24"></asp:DropDownList></td>
            </tr>

            <tr>
                <td></td>
                <td></td>
                <td>
                    <asp:Button Text="Save" runat="server" ID="btnSaveDatabaseDetails" OnClick="btnSaveDatabaseDetails_Click" />
                    <asp:Button runat="server" ID="lnkDelete" Text="Delete" OnClick="lnkDelete_Click" Visible="false" OnClientClick="return myFunction()"></asp:Button>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
