<%@ Page Title="" Language="C#" MasterPageFile="~/mstInner.Master" AutoEventWireup="true" CodeBehind="CreateAttachmentHandle.aspx.cs" Inherits="Attachment.CreateAttachmentHandle" %>

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
    <div style="text-align: initial; font-size: 16px; font-weight: bold" runat="server" id="divHeader">Attachment Handle</div>
    <div style="margin-left: 0px; margin-top: 15px">
        <table>
            <tr>
                <td>Attachment Handle</td>
                <td>:</td>
                <td>
                    <asp:TextBox runat="server" ID="txtAttachmentHandle" MaxLength="50" Width="300" Height="23" /></td>
            </tr>

            <tr>
                <td>Database Id</td>
                <td>:</td>
                <td>
                    <asp:DropDownList runat="server" ID="ddlDBID" Width="300" Height="23" AutoPostBack="true"  OnSelectedIndexChanged="ddlDBID_SelectedIndexChanged" ></asp:DropDownList></td>
                <td><asp:TextBox runat="server" ID="txtDBDescription" Visible="false" Width="200" Height="23" /></td>
            </tr>

            <tr>
                <td>Table Name</td>
                <td>:</td>
                <td>
                    <asp:TextBox runat="server" ID="txtTableName"  Width="300" Height="23" /></td>
            </tr>

              <tr>
                <td>Table Description</td>
                <td>:</td>
                <td>
                    <asp:TextBox runat="server" ID="txtDescription" Width="300" Height="30"  TextMode="MultiLine" /></td>
             </tr>

             <tr>
                <td>Acess Index</td>
                <td>:</td>
                <td>
                    <asp:TextBox runat="server" ID="txtAcessIndex" MaxLength="50" Width="300" Height="23" /></td>
             </tr>

             <tr>
                <td>Remarks</td>
                <td>:</td>
                <td>
                    <asp:TextBox runat="server" ID="txtRemarks" MaxLength="50" Width="300" Height="23" /></td>
             </tr>

             <tr>
                <td></td>
                <td></td>
                <td>
                    <asp:Button Text="Save" runat="server" ID="btnSaveHandle" OnClick="btnSaveHandle_Click" />
                    <asp:Button runat="server" ID="lnkDelete" Text="Delete" OnClick="lnkDelete_Click" Visible="false" OnClientClick="return myFunction()"></asp:Button>
                </td>
             </tr>
        </table>
    </div>
</asp:Content>
